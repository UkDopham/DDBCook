using DDBCook.Models.Enums;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDBCook.Models
{
    public class DDB
    {
        private MySqlConnection _mySqlConnection;

        public DDB(string database, string username, string password)
        {
            Open(database, username, password);
        }

        private void Open(string database, string username, string password)
        {
            try
            {
                string connectionString = $"SERVER=localhost;PORT=3306;DATABASE={database};UID={username};PASSWORD={password};";
                this._mySqlConnection = new MySqlConnection(connectionString);
                this._mySqlConnection.Open();
            }
            catch (Exception ex)
            {

            }
        }
        public void Close()
        {
            this._mySqlConnection.Close();
        }



        /// <summary>
        /// custom method to execute a sql delete method. It can use the where statement if whereColumn and whereValue are not null or empty.
        /// 
        /// </summary>
        /// <param name="className"></param>
        /// <param name="whereColumns">contains all the 'where columns' </param>
        /// <param name="whereValues">contains all the 'where values' </param>
        /// <returns></returns>
        private void Delete(string className, string[] whereColumns = null, string[] whereValues = null)
        {
            try
            {
                MySqlCommand command = this._mySqlConnection.CreateCommand();
                command.CommandText = "DELETE FROM " + className;

                // add a where statement if necessary
                if (whereColumns != null && whereValues != null
                && whereColumns.Length != 0
                && whereColumns.Length == whereValues.Length)
                {
                    command.CommandText += " WHERE ";
                    for (int i = 0; i < whereColumns.Length; i++)
                    {
                        command.CommandText += whereColumns[i] + " = " + whereValues[i] + " ";
                        if (i < whereColumns.Length - 1) command.CommandText += "and ";
                    }
                }
                command.CommandText += ";";
                MySqlDataReader reader;
                reader = command.ExecuteReader();
                reader.Close();
            }
            catch (Exception ex)
            {

            }
        }

        // Custom methods for each tab of the data tables. They will execute the method above and return a list of objects (corresponding to the table). 
        public void DeleteProudctComposition(string[] whereColumns = null, string[] whereValues = null)
        {
            Delete("compose", whereColumns, whereValues);
        }
        public void DeleteClient(string[] whereColumns = null, string[] whereValues = null)
        {
            Delete("client", whereColumns, whereValues);
        }
        public void DeleteSupplier(string[] whereColumns = null, string[] whereValues = null)
        {
            Delete("fournisseur", whereColumns, whereValues);
        }
        public void DeleteOrder(string[] whereColumns = null, string[] whereValues = null)
        {
            Delete("commande", whereColumns, whereValues);
        }
        public void DeleteProduct(string[] whereColumns = null, string[] whereValues = null)
        {
            Delete("produit", whereColumns, whereValues);
        }
        public void DeleteRecipe(string[] whereColumns = null, string[] whereValues = null)
        {
            Delete("recette", whereColumns, whereValues);
        }
        public void DeleteRecipeCreator(string[] whereColumns = null, string[] whereValues = null)
        {
            Delete("cdr", whereColumns, whereValues);
        }



        /// <summary>
        /// custom method to insert into sql command
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        public void Insert<T>(T table) where T : ITable
        {
            try
            {
                MySqlCommand command = this._mySqlConnection.CreateCommand();
                command.CommandText = $"insert into {table.GetTableName()} ({table.GetTableProperties()}) values ({table.GetTableValues()});";
                MySqlDataReader reader;
                reader = command.ExecuteReader();
                reader.Close();
            }
            catch (Exception ex)
            {

            }
        }

        // Custom methods for each tab of the data tables. They will execute the method above and return a list of objects (corresponding to the table). 
        public void InsertSupplier(string name, string number)
        {
            Supplier supplier = new Supplier(name, number);
            Insert<Supplier>(supplier);
        }
        public void InsertClient(string email, string password, UserType userType, string name, string phoneNumber, string adress, int money)
        {
            Client client = new Client(email, password, userType, name, phoneNumber, adress, money);
            Insert<Client>(client);
        }
        public void InsertOrder(string id, DateTime orderDate, string clientNumber, string recipeName)
        {
            Order order = new Order(id, orderDate, clientNumber, recipeName);
            Insert<Order>(order);
        }
        public void InsertProduct(string name, ProductCategory productCategory, int quantity, int currentQuantity, int minQuantity, int maxQuantity, string provider, string reference, string unity)
        {
            Product product = new Product(name, productCategory, currentQuantity, minQuantity, maxQuantity, provider, reference, unity);
            Insert<Product>(product);
        }
        public void InsertProudctComposition(string id, int quantity, string refProduct, string recipeName)
        {
            ProductComposition productComposition = new ProductComposition(id, quantity, refProduct, recipeName);
            Insert<ProductComposition>(productComposition);
        }
        public void InsertRecipe(string name, RecipeType recipeType, string description, string numberCreator, int price = 2, bool isHealthy = false, bool isBio = false, bool isVegan = false, bool isChimical = false)
        {
            Recipe recipe = new Recipe(name, recipeType, description, numberCreator, price, isHealthy, isBio, isBio, isVegan, isChimical);
            Insert<Recipe>(recipe);
        }
        public void InsertRecipeCreator(string email, string password, UserType userType, string name, string phoneNumber, string adress, int money = 0)
        {
            RecipeCreator recipeCreator = new RecipeCreator(phoneNumber);
            Insert<RecipeCreator>(recipeCreator);
        }


        /// <summary>
        /// custom method to execute a sql select method. It can use the where statement if whereColumn and whereValue are not null or empty.
        /// 
        /// </summary>
        /// <param name="className"></param>
        /// <param name="whereColumns">contains all the 'where columns' </param>
        /// <param name="whereValues">contains all the 'where values' </param>
        /// <returns></returns>
        private List<string> Select(string className, string[] whereColumns = null, string[] whereValues = null)
        {
            string[] requestResult = new string[0];
            try
            {
                MySqlCommand command = this._mySqlConnection.CreateCommand();
                command.CommandText = "select * from " + className;

                // add a where statement if necessary
                if (whereColumns != null && whereValues != null 
                && whereColumns.Length!=0
                && whereColumns.Length == whereValues.Length)
                {
                    command.CommandText += " WHERE ";
                    for (int i = 0; i < whereColumns.Length; i++)
                    {
                        command.CommandText += whereColumns[i] + " = " + whereValues[i] + " ";
                        if (i < whereColumns.Length - 1) command.CommandText += "and ";
                    }
                }
                command.CommandText += ";";
                MySqlDataReader reader;
                reader = command.ExecuteReader();

                List<string> valueString = new List<string>();
                int cpt = 0;
                while (reader.Read())
                {
                    valueString.Add("");
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        valueString[cpt] += reader.GetValue(i).ToString();
                        valueString[cpt] += ";";
                    }
                    cpt++;
                }


                reader.Close();
                requestResult = valueString.ToArray();
            }
            catch (Exception ex)
            {

            }
            return requestResult.ToList();
        }

        // Custom methods for each tab of the data tables. They will execute the method above and return a list of objects (corresponding to the table). 
        public List<ProductComposition> SelectProudctComposition(string[] whereColumns = null, string[] whereValues = null)
        {
            List<ProductComposition> listPC = new List<ProductComposition>();
            Select("compose", whereColumns, whereValues).ForEach(x => listPC.Add((ProductComposition)Converter.ConvertFromString(x, TableType.productComposition)));

            return listPC;
        }
        public List<Client> SelectClient(string[] whereColumns = null, string[] whereValues = null)
        {
            List<Client> listPC = new List<Client>();
            Select("client", whereColumns, whereValues).ForEach(x => listPC.Add((Client)Converter.ConvertFromString(x, TableType.client)));

            return listPC;
        }
        public List<Supplier> SelectSupplier(string[] whereColumns = null, string[] whereValues = null)
        {
            List<Supplier> listPC = new List<Supplier>();
            Select("fournisseur", whereColumns, whereValues).ForEach(x => listPC.Add((Supplier)Converter.ConvertFromString(x, TableType.supplier)));

            return listPC;
        }
        public List<Order> SelectOrder(string[] whereColumns = null, string[] whereValues = null)
        {
            List<Order> listPC = new List<Order>();
            Select("commande", whereColumns, whereValues).ForEach(x => listPC.Add((Order)Converter.ConvertFromString(x, TableType.order)));

            return listPC;
        }
        public List<Product> SelectProduct(string[] whereColumns = null, string[] whereValues = null)
        {
            List<Product> listPC = new List<Product>();
            Select("produit", whereColumns, whereValues).ForEach(x => listPC.Add((Product)Converter.ConvertFromString(x, TableType.product)));

            return listPC;
        }
        public List<Recipe> SelectRecipe(string[] whereColumns = null, string[] whereValues = null)
        {
            List<Recipe> listPC = new List<Recipe>();
            Select("recette", whereColumns, whereValues).ForEach(x => listPC.Add((Recipe)Converter.ConvertFromString(x, TableType.recipe)));

            return listPC;
        }
        public List<RecipeCreator> SelectRecipeCreator(string[] whereColumns = null, string[] whereValues = null)
        {
            List<RecipeCreator> listPC = new List<RecipeCreator>();
            Select("cdr", whereColumns, whereValues).ForEach(x => listPC.Add((RecipeCreator)Converter.ConvertFromString(x, TableType.recipeCreator)));

            return listPC;
        }




        /// <summary>
        /// custom method to execute a sql update method. It can use the where statement if whereColumn and whereValue are not null or empty.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <param name="setColumns"></param>
        /// <param name="setValues"></param>
        /// <param name="whereColumns"></param>
        /// <param name="whereValues"></param>
        private void Update<T>(T table, string[] setColumns = null, string[] setValues = null,string[] whereColumns = null, string[] whereValues = null) where T : ITable
        {
            bool conditionsValides(){
                return (setColumns != null && setValues != null 
                && setColumns.Length!=0
                && setColumns.Length == setValues.Length) 
                &&
                (whereColumns != null && whereValues != null 
                && whereColumns.Length!=0
                && whereColumns.Length == whereValues.Length);
            }


            try
            {
                MySqlCommand command = this._mySqlConnection.CreateCommand();
                command.CommandText = "UPDATE " + table.GetTableName();


                // add a where statement if necessary
                if (conditionsValides() )
                {
                    command.CommandText += " SET ";
                    for (int i = 0; i < setColumns.Length; i++)
                    {
                        command.CommandText += setColumns[i] + " = " + setValues[i] + " ";
                        if (i < setColumns.Length - 1) command.CommandText += ", ";
                    }

                    command.CommandText += " WHERE ";
                    for (int i = 0; i < whereColumns.Length; i++)
                    {
                        command.CommandText += whereColumns[i] + " = " + whereValues[i] + " ";
                        if (i < whereColumns.Length - 1) command.CommandText += "and ";
                    }
                }
                command.CommandText += ";";
                MySqlDataReader reader;
                reader = command.ExecuteReader();
                reader.Close();
            }
            catch (Exception ex)
            {

            }
        }

        // Custom methods for each tab of the data tables. They will execute the method above and return a list of objects (corresponding to the table). 
        public void UpdateProudctComposition(ProductComposition productComposition, string[] whereColumns = null, string[] whereValues = null)
        {
            string[] setColumns = { "id","quantite_produit","refProduit","nomRecette"};
            string[] setValues = { "'" + productComposition.Id + "'", Convert.ToString(productComposition.Quantity), "'" + productComposition.RefProduct + "'", "'" + productComposition.RecipeName + "'"};

            Update<ProductComposition>(productComposition, setColumns, setValues, whereColumns, whereValues);
        }
        public void UpdateClient(Client client,string[] whereColumns = null, string[] whereValues = null)
        {
            string[] setColumns = { "nom", "balance", "adresse", "numero", "email", "password", "type" };
            string[] setValues = { "'"+client.Name+ "'", Convert.ToString(client.Money), "'"+client.Adress+ "'", "'"+client.PhoneNumber+ "'", "'"+client.Email+ "'", "'"+client.Password+ "'", "'"+client.UserType.ToString() + "'" };

            Update<Client>(client, setColumns, setValues, whereColumns, whereValues);
        }
        public void UpdateSupplier(Supplier supplier, string[] whereColumns = null, string[] whereValues = null)
        {
            string[] setColumns = { "numero","nom"};
            string[] setValues = { "'" + supplier.Name + "'",  "'" + supplier.Number + "'"};

            Update<Supplier>(supplier, setColumns, setValues, whereColumns, whereValues);
        }
        public void UpdateOrder(Order order, string[] whereColumns = null, string[] whereValues = null)
        {
            string[] setColumns = { "id","date","numeroClient","nomRecette"};// dateValue.ToString("yyyy-MM-dd HH:mm:ss")
            string[] setValues = { "'" + order.Id + "'",  "'" + order.OrderDate.ToString("yyyy-MM-dd HH:mm:ss") + "'","'" + order.ClientNumber + "'","'" + order.RecipeName + "'"};

            Update<Order>(order, setColumns, setValues, whereColumns, whereValues);
        }
        public void UpdateProduct(Product product, string[] whereColumns = null, string[] whereValues = null)
        {
            string[] setColumns = { "ref","nom","categorie","quantite_actuelle","quantite_min","quantite_max","unite","numeroFournisseur"};
            string[] setValues = { "'" + product.Reference + "'",  "'" + product.Name + "'","'" + product.ProductCategory + "'","'" + Convert.ToString(product.Quantity) + "'","'" + Convert.ToString(product.MinQuantity) + "'","'" + Convert.ToString(product.MaxQuantity) + "'","'" + product.Unit + "'","'" + product.ProviderNumber + "'"};

            Update<Product>(product, setColumns, setValues, whereColumns, whereValues);
        }
        public void UpdateRecipe(Recipe recipe, string[] whereColumns = null, string[] whereValues = null)
        {
            string[] setColumns = { "nom","categorie","description","nomRecette","prix","numeroCreateur","estHealthy","estBio","estVegan","estChimique","estTendance"};
            string[] setValues = { "'" + recipe.Name + "'",  "'" + recipe.RecipeType + "'","'" + recipe.Description + "'","'" + Convert.ToString(recipe.Price) + "'","'" + recipe.NumberCreator + "'",
            "'"+ "estBio"+ "'",  "'" +"estVegan"+ "'",  "'" +"estChimique"+ "'",  "'" +"estTendance" };

            Update<Recipe>(recipe, setColumns, setValues, whereColumns, whereValues);
        }
        public void UpdateRecipeCreator(RecipeCreator recipeCreator, string[] whereColumns = null, string[] whereValues = null)
        {
            string[] setColumns = { "numero"};
            string[] setValues = { "'" + recipeCreator.Id + "'"};

            Update<RecipeCreator>(recipeCreator, setColumns, setValues, whereColumns, whereValues);
        }

    }
}
