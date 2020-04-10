using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDBCook.Models
{
    public class ProductComposition : ITable
    {
        private string _id;
        private int _quantity;
        private string _refProduct;
        private string _recipeName;

        public string Id
        {
            get
            {
                return this._id;
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
        public string RefProduct
        {
            get
            {
                return this._refProduct;
            }
        }
        public string RecipeName
        {
            get
            {
                return this._recipeName;
            }
            set
            {
                this._recipeName = value;
            }
        }
        public ProductComposition(string id, int quantity, string refProduct, string recipeName)
        {
            this._id = id;
            this._quantity = quantity;
            this._refProduct = refProduct;
            this._recipeName = recipeName;
        }


        public string GetTableName()
        {
            return $"cook.compose";
        }

        public string GetTableProperties()
        {
            return "id, quantite_produit, refProduit, nomRecette";
        }

        public string GetTableValues()
        {
            return $"'{this._id}', {this._quantity}, '{this._refProduct}', '{this._recipeName}'";
        }


        public override string ToString(){
            return $"{this.RecipeName} - {this.RefProduct}({this.Quantity}";
        }
    }
}
