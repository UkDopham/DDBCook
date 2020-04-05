using DDBCook.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDBCook.Models
{
    public class Product : ITable
    {
        private string _name;
        private ProductCategory _productCategory;
        private int _quantity;
        private int _currentQuantity;
        private int _minQuantity;
        private int _maxQuantity;
        private string _provider;
        private string _reference;
        private string _unit;

        public string Name
        {
            get
            {
                return this._name;
            }
        }
        public ProductCategory ProductCategory
        {
            get
            {
                return this._productCategory;
            }
        }
        public int Quantity
        {
            get
            {
                return this._quantity;
            }
            set
            {
                this._quantity = value;
            }
        }
        public int CurrentQuantity
        {
            get
            {
                return this._currentQuantity;
            }
            set
            {
                this._currentQuantity = value;
            }
        }
        public int MinQuantity
        {
            get
            {
                return this._minQuantity;
            }
            set
            {
                this._minQuantity = value;
            }
        }
        public int MaxQuantity
        {
            get
            {
                return this._maxQuantity;
            }
            set
            {
                this._maxQuantity = value;
            }
        }
        public string Provider
        {
            get
            {
                return this._provider;
            }
        }
        public string Reference
        {
            get
            {
                return this._reference;
            }
        }
        public string Unit
        {
            get
            {
                return this._unit;
            }
        }

        public Product(
            string name,
            ProductCategory productCategory,
            int quantity,
            int currentQuantity,
            int minQuantity,
            int maxQuantity,
            string provider,
            string reference,
            string unit)
        {
            this._name = name;
            this._productCategory = ProductCategory;
            this._quantity = quantity;
            this._currentQuantity = currentQuantity;
            this._minQuantity = minQuantity;
            this._maxQuantity = maxQuantity;
            this._provider = provider;
            this._reference = reference;
            this._unit = unit;
        }

        public string GetTableName()
        {
            return "cook.produit";
        }

        public string GetTableProperties()
        {
            return "ref, nom, categorie, quantite_actuelle, quantite_min, quantite_max, unite, numeroFournisseur";
        }

        public string GetTableValues()
        {
            return $"'{this._reference}', '{this._name}', '{this._productCategory}', {this._currentQuantity}, {this._minQuantity}, {this._maxQuantity}, '{this._reference}', '{this._unit}'";
        }
    }
}
