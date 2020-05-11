using Microsoft.VisualStudio.TestTools.UnitTesting;
using DDBCook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDBCook.Models.Tests
{
    [TestClass()]
    public class ClientTests
    {
        [TestMethod()]
        public void ClientSelect()
        {
            DDB ddb = new DDB(User.DataBase, User.Username, User.Password);

            List<Client> clients = ddb.SelectClient();
            ddb.Close();
            Assert.AreEqual(0, clients.Count);
        }
        [TestMethod()]
        public void RecipeSelect()
        {
            DDB ddb = new DDB(User.DataBase, User.Username, User.Password);

            List<Recipe> recipes = ddb.SelectRecipe();
            ddb.Close();
            Assert.AreEqual(0, recipes.Count);
        }
        [TestMethod()]
        public void ProductSelect()
        {
            DDB ddb = new DDB(User.DataBase, User.Username, User.Password);

            List<Product> products = ddb.SelectProduct();
            ddb.Close();
            Assert.AreEqual(0, products.Count);
        }
        [TestMethod()]
        public void OrderSelect()
        {
            DDB ddb = new DDB(User.DataBase, User.Username, User.Password);

            List<Order> orders = ddb.SelectOrder();
            ddb.Close();
            Assert.AreEqual(0, orders.Count);
        }
        [TestMethod()]
        public void CDRSelect()
        {
            DDB ddb = new DDB(User.DataBase, User.Username, User.Password);

            List<RecipeCreator> cdrs = ddb.SelectRecipeCreator();
            ddb.Close();
            Assert.AreEqual(0, cdrs.Count);
        }
        [TestMethod()]
        public void SupplierSelect()
        {
            DDB ddb = new DDB(User.DataBase, User.Username, User.Password);

            List<Supplier> suppliers = ddb.SelectSupplier();
            ddb.Close();
            Assert.AreEqual(0, suppliers.Count);
        }
        [TestMethod()]
        public void TestDatabase()
        {           
            Assert.AreEqual("cook", User.DataBase);
        }
        [TestMethod()]
        public void TestSettings()
        {
            Assert.AreEqual("settings.txt", User.Path);
        }
    }
}