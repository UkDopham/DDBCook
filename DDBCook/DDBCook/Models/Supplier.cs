using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDBCook.Models
{
    public class Supplier : ITable
    {
        private string _name;
        private string _number;
        public Supplier(string name, string number)
        {
            this._name = name;
            this._number = number;
        }

        public string GetTableName()
        {
            return $"cook.fournisseur";
        }

        public string GetTableProperties()
        {
            return $"numero, nom";
        }

        public string GetTableValues()
        {
            return $"'{this._number}', '{this._name}'";
        }
    }
}
