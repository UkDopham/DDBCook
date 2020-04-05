using DDBCook.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDBCook.Models
{
    public class RecipeCreator : Client, ITable
    {
        private string _id;

        public string Id
        {
            get
            {
                return this._id;
            }
        }
        public RecipeCreator(string email, string password, UserType userType, string name, string phoneNumber, string adress, int money = 0):
            base(email, password, userType, name, phoneNumber, adress, money)
        {

        }
        public override string GetTableName()
        {
            return "cook.cdr";
        }

        public override string GetTableProperties()
        {
            return "numero";
        }

        public override string GetTableValues()
        {
            return $"'{this._id}'";
        }
    }
}
