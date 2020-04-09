using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace DDBCook.Models.Gestion
{
    public class Stock
    {

        /// <summary>
        /// Method appele a chaque commande. Elle permet de simuler la gestion des stockes
        /// </summary>
        /// <param name="recipe"></param>
        public static void ManageOrder(Recipe recipe)
        {
            DDB ddB = new DDB(User.DataBase, User.Username, User.Password);

            List<ProductComposition> productCompositions = ddB.SelectProductComposition(new string[] { "nomRecette" }, new string[] { "'" + recipe.Name + "'" });

            List<Product> products = new List<Product>();
            productCompositions.ForEach(p => products.AddRange(ddB.SelectProduct(new string[] { "refProduit" }, new string[] { "'" + p.RefProduct + "'" })));


            products.ForEach(p => ConsumeProduct(p, productCompositions));


            ddB.Close();
        }


        /// <summary>
        /// Le produit commande est consomme
        /// </summary>
        /// <param name="product"></param>
        /// <param name="productsComposition"></param>
        private static void ConsumeProduct(Product product, List<ProductComposition> productsComposition)
        {
            ProductComposition productComposition = productsComposition.Find(x => x.RefProduct.Equals(product.Reference));
            product.CurrentQuantity -= productComposition.Quantity;

            if (product.CurrentQuantity < product.MinQuantity)
            {
                int quantityNeeded = product.MaxQuantity - product.CurrentQuantity;
                OrderSuplies(product, quantityNeeded, "plus de stockes");
            }
            DDB ddb = new DDB();
            ddb.UpdateProduct(product, new string[] { "ref" }, new string[] { "'" + product.Reference + "'" });
            ddb.Close();
        }

        /// <summary>
        /// Change the value of min quantity and max quantity if needed.
        /// </summary>
        /// <param name="recipe"></param>
        public static void UpdateMinMaxQuantities(Recipe recipe)
        {
            DDB ddB = new DDB(User.DataBase, User.Username, User.Password);

            List<ProductComposition> productCompositions = ddB.SelectProductComposition(new string[] { "nomRecette" }, new string[] { "'" + recipe.Name + "'" });

            foreach (ProductComposition prodComp in productCompositions)
            {
                int quantityConsumed = prodComp.Quantity;

                Product product= ddB.SelectProduct(new string[] { "ref" }, new string[] { "'" + prodComp.RefProduct + "'" }).First();
                
                if (product.MinQuantity < quantityConsumed)
                {
                    product.MinQuantity = quantityConsumed;

                    product.MaxQuantity = 20 * quantityConsumed;


                    ddB.UpdateProduct(product, new string[] { "ref" }, new string[] { "'" + product.Reference + "'" });
                    
                }

            }
            ddB.Close();
            
        }


        /// <summary>
        /// Verifie si il y a des produits qui n'ont pas ete utilises depuis plus de 30 jours
        /// </summary>
        /// <returns></returns>
        public static List<Product> RottenProducts()
        {
            DDB ddb = new DDB();

            //On recupere toutes les commandes effectuees depuis 30 jours
            List<Order> orders = ddb.SelectOrder(new string[] { "date" }, new string[] { "NOW()" }, "BETWEEN DATE_SUB(NOW(), INTERVAL 30 DAY) AND");

            // .  .  .  recettes effectuees depuis 30 jours
            List<Recipe> recipes = new List<Recipe>();
            orders.ForEach(x => recipes.AddRange(ddb.SelectRecipe(new string[] { "nom" }, new string[] { "'" + x.RecipeName + "'" })));

            // .  .  .  produit utilises depuis 30 jours
            List<ProductComposition> productCompositions = new List<ProductComposition>();
            recipes.ForEach(x => productCompositions.AddRange(ddb.SelectProductComposition(new string[] { "nomRecette" }, new string[] { "'" + x.Name + "'" })));
            List<Product> products = new List<Product>();
            productCompositions.ForEach(x => products.AddRange(ddb.SelectProduct(new string[] { "ref" }, new string[] { "'" + x.RefProduct + "'" })));

            // .  .  .  tout les produits
            List<Product> allProducts = ddb.SelectProduct();

            //On soustrait les deux ensembles
            List<Product> rottenProducts = allProducts.FindAll(x => !products.Contains(x));


            foreach (Product rotten in rottenProducts)
            {
                rotten.CurrentQuantity /= 2;

                if (rotten.CurrentQuantity < rotten.MinQuantity)
                {
                    int orderQuantity = rotten.MinQuantity - rotten.CurrentQuantity;
                    OrderSuplies(rotten, orderQuantity, "reserves pourries");
                    ddb.UpdateProduct(rotten, new string[] { "ref" }, new string[] { "'" + rotten.Reference + "'" });
                }
            }
            ddb.Close();
            return rottenProducts;
        }


        /// <summary>
        /// Cree une nouvelle commande de matieres premieres. (qui arrive instantanement)
        /// </summary>
        /// <param name="product"></param>
        private static void OrderSuplies(Product product, int quantityNeeded, string cause = "plus de stockes")
        {

            CreationRapport();
            product.CurrentQuantity += quantityNeeded;


            void CreationRapport(string nomDoc = "Commandes.xml")  //TODO: rajouter produit a date correspondante  ET enlever hhmmss au format
            {
                if (!File.Exists(nomDoc))
                {
                    XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
                    xmlWriterSettings.Indent = true;
                    xmlWriterSettings.NewLineOnAttributes = true;
                    using (XmlWriter xmlWriter = XmlWriter.Create(nomDoc, xmlWriterSettings))
                    {
                        xmlWriter.WriteStartDocument(); //commence le document
                        xmlWriter.WriteComment("Ce fichier comporte l'ensemble des commandes effectues par");  //cree un commentaire

                        xmlWriter.WriteStartElement("Commandes");    //cree un element

                        xmlWriter.WriteStartElement("Commande");
                        xmlWriter.WriteAttributeString("Date", DateTime.Now.ToShortDateString());  //ajoute une propriete

                        xmlWriter.WriteStartElement("Product");
                        xmlWriter.WriteAttributeString("name", product.Name);
                        xmlWriter.WriteAttributeString("amount", Convert.ToString(quantityNeeded));
                        xmlWriter.WriteAttributeString("cause", cause);
                        xmlWriter.WriteEndElement(); //ferme element

                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteEndDocument(); //ferme document


                        xmlWriter.Flush(); // sauve fichier
                    }
                }
                else
                {
                    XDocument xDocument = XDocument.Load(nomDoc);
                    XElement root = xDocument.Element("Commandes");
                    IEnumerable<XElement> rows = root.Descendants("Commande");

                    XElement el = rows.First(c => ((string)c.Attribute("Date")).Equals(DateTime.Now.ToShortDateString()));
                    if (el == null)
                    {
                        XElement firstRow = rows.First();
                        firstRow.AddBeforeSelf(
                      new XElement("Commande",
                      new XAttribute("Date", DateTime.Now.ToString()),
                      new XElement("Product",
                      new XAttribute("name", product.Name), new XAttribute("amount", Convert.ToString(quantityNeeded)), new XAttribute("cause", cause)
                      )));

                    }
                    else
                    {
                        IEnumerable<XElement> productsRows = el.Descendants();
                        XElement lastRow = productsRows.Last();
                        lastRow.AddAfterSelf(new XElement("Product",
                      new XAttribute("name", product.Name), new XAttribute("amount", Convert.ToString(quantityNeeded)), new XAttribute("cause", cause)
                      ));
                    }
                    xDocument.Save(nomDoc);
                }
            }
        }

    }
}
