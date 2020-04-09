using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDBCook.Models
{
    public class Order : ITable
    {
        private string _id;
        private DateTime _orderDate;
        private string _clientNumber;
        private string _recipeName;

        public string Id
        {
            get
            {
                return this._id;
            }
        }
        public DateTime OrderDate
        {
            get
            {
                return this._orderDate;
            }
        }
        public string ClientNumber
        {
            get
            {
                return this._clientNumber;
            }
        }
        public string RecipeName
        {
            get
            {
                return this._recipeName;
            }
        }
        public Order(string id, DateTime orderDate, string clientNumber, string recipeName)
        {
            this._id = id;
            this._orderDate = orderDate;
            this._clientNumber = clientNumber;
            this._recipeName = recipeName;              
        }

        public string GetTableName()
        {
            return "cook.commande";
        }

        public string GetTableProperties()
        {
            return "id, date, numero, nomRecette";
        }

        public string GetTableValues()
        {
            return $"'{this._id}', '{this._orderDate.Year}-{this._orderDate.Month}-{this._orderDate.Day}', '{this._clientNumber}', '{this._recipeName}'";
        }
    }
}
