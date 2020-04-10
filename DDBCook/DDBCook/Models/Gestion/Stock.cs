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
        private const string fichierCommande = "Commandes.xml";

        /// <summary>
        /// Method appele a chaque commande. Elle permet de simuler la gestion des stockes
        /// </summary>
        /// <param name="recipe"></param>
        public static void ManageOrder(Recipe recipe)
        {
            DDB ddB = new DDB(User.DataBase, User.Username, User.Password);

            List<ProductComposition> productCompositions = ddB.SelectProductComposition(new string[] { "nomRecette" }, new string[] { "'" + recipe.Name + "'" });

            List<Product> products = new List<Product>();
            productCompositions.ForEach(p => products.AddRange(ddB.SelectProduct(new string[] { "ref" }, new string[] { "'" + p.RefProduct + "'" })));


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

                Product product = ddB.SelectProduct(new string[] { "ref" }, new string[] { "'" + prodComp.RefProduct + "'" }).First();

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
        public static List<Product> RottenProducts(int jourMax = 30)
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
            List<Product> rottenProducts = allProducts.FindAll(x => !Contain(products, x));


            foreach (Product rotten in rottenProducts)
            {


                if (VerificationRapportCommande(rotten))
                {
                    rotten.CurrentQuantity /= 2;

                    if (rotten.CurrentQuantity < rotten.MinQuantity)
                    {
                        int orderQuantity = rotten.MinQuantity - rotten.CurrentQuantity;
                        OrderSuplies(rotten, orderQuantity, "reserves pourries");
                        ddb.UpdateProduct(rotten, new string[] { "ref" }, new string[] { "'" + rotten.Reference + "'" });
                    }
                }
            }
            ddb.Close();
            return rottenProducts;


            bool Contain(List<Product> list, Product prod)
            {
                foreach (Product p in list)
                {
                    if (p.Reference.Equals(prod.Reference))
                        return true;
                }
                return false;
            }

            /// Verifie grace au rapport de commande si le produit na pas deja pourris il y a moins de 1 mois
            bool VerificationRapportCommande(Product rottenProduct)
            {
                XDocument xDocument = XDocument.Load(fichierCommande);
                XElement root = xDocument.Element("Commandes");
                IEnumerable<XElement> rows = root.Descendants("Commande");

                return !rows.Any(product =>
                (product.Descendants().Any
                (prod => (((((string)prod.Attribute("name")).Equals(rottenProduct.Name)) && (((string)prod.Attribute("cause")).Equals("reserves pourries")))
                && (DateTime.Now.Subtract(DateTime.Parse((string)product.Attribute("Date"))).Days < jourMax)))
                ));
            }
        }


        /// <summary>
        /// Cree une nouvelle commande de matieres premieres. (qui arrive instantanement)
        /// </summary>
        /// <param name="product"></param>
        private static void OrderSuplies(Product product, int quantityNeeded, string cause = "plus de stockes")
        {

            CreationRapport();
            product.CurrentQuantity += quantityNeeded;


            void CreationRapport(string nomDoc = fichierCommande)
            {
                if (!File.Exists(nomDoc))
                {
                    XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
                    xmlWriterSettings.Indent = true;
                    xmlWriterSettings.NewLineOnAttributes = true;
                    using (XmlWriter xmlWriter = XmlWriter.Create(nomDoc, xmlWriterSettings))
                    {
                        xmlWriter.WriteStartDocument(); //commence le document
                        xmlWriter.WriteComment("Ce fichier comporte l'ensemble des commandes effectues par les clients");  //cree un commentaire

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


                    bool exists = rows.Any(x => ((string)x.Attribute("Date")).Equals(DateTime.Now.ToShortDateString()));


                    if (!exists) // cree une nouvelle date pour mettre la commande
                    {
                        XElement firstRow = rows.First();
                        firstRow.AddBeforeSelf(
                      new XElement("Commande",
                      new XAttribute("Date", DateTime.Now.ToShortDateString()),
                      new XElement("Product",
                      new XAttribute("name", product.Name), new XAttribute("amount", Convert.ToString(quantityNeeded)), new XAttribute("cause", cause)
                      )));

                    }
                    else // rajoute la commande a la date deja cree
                    {
                        XElement el = rows.First(c => ((string)c.Attribute("Date")).Equals(DateTime.Now.ToShortDateString()));
                        IEnumerable<XElement> productsRows = el.Descendants();
                        XElement lastRow = productsRows.Last();
                        lastRow.AddAfterSelf(new XElement("Product",
                      new XAttribute("name", product.Name), new XAttribute("amount", Convert.ToString(quantityNeeded)), new XAttribute("cause", cause)
                      ));
                    }
                    xDocument.Save(nomDoc);
                }
                //}
            }
        }

    }
}
