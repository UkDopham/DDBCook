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
            catch(Exception ex)
            {

            }
        }

        /// <summary>
        /// insert a new supplier in the table
        /// </summary>
        /// <param name="name"></param>
        /// <param name="number"></param>
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
        /// custom method to insert into sql command
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        private void Insert<T>(T table) where T:ITable
        {
            try
            {
                MySqlCommand command = this._mySqlConnection.CreateCommand();
                command.CommandText = $"insert into {table.GetTableName()} ({table.GetTableProperties()}) values ({table.GetTableValues()});";
                MySqlDataReader reader;
                reader = command.ExecuteReader();
                reader.Close();
            }
            catch(Exception ex)
            {

            }
        }

    }
}
