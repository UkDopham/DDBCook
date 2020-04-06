using DDBCook.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDBCook.Models
{
    public class RecipeCreator : ITable
    {
        private string _id;

        public string Id
        {
            get
            {
                return this._id;
            }
        }
        public RecipeCreator(string id)
        {
            this._id = Id;
        }
        public string GetTableName()
        {
            return "cook.cdr";
        }

        public string GetTableProperties()
        {
            return "numero";
        }

        public string GetTableValues()
        {
            return $"'{this._id}'";
        }
    }
}
