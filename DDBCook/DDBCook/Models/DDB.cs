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
        /// custom method to insert into sql command
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        private void Insert<T>(T table) where T : ITable
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
        public void InsertRecipe(string name, RecipeType recipeType, string description, List<ProductComposition> products, string numberCreator, int price = 2)
        {
            Recipe recipe = new Recipe(name, recipeType, description, numberCreator, price);
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



    }
}
