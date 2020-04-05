using DDBCook.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDBCook.Models
{
    public class Recipe : ITable
    {
        private string _name;
        private RecipeType _recipeType;
        private string _description;
        private List<ProductComposition> _productsComposition;
        private RecipeCreator _recipeCreator;
        private int _price;

        public RecipeCreator RecipeCreator
        {
            get
            {
                return this._recipeCreator;
            }
        }
        public string Name
        {
            get
            {
                return this._name;
            }
        }
        public RecipeType RecipeType
        {
            get
            {
                return this._recipeType;
            }
        }
        public string Description
        {
            get
            {
                return this._description;
            }
        }
        private List<ProductComposition> ProductsComposition
        {
            get
            {
                return this._productsComposition;
            }
        }
        public int Price
        {
            get
            {
                return this._price;
            }
        }
        public Recipe(string name, RecipeType recipeType, string description, List<ProductComposition> productsComposition, RecipeCreator recipeCreator, int price = 2)
        {
            this._name = name;
            this._recipeType = recipeType;
            this._description = description;
            this._productsComposition = productsComposition;
            this._recipeCreator = recipeCreator;
            this._price = price;
        }

        public string GetTableName()
        {
            return "cook.recette";
        }

        public string GetTableProperties()
        {
            return "nom, categorie, description, prix, numeroCreateur";
        }

        public string GetTableValues()
        {
            return $"{this._name}, {this._recipeCreator}, {this._description}, {this._price}, {this._recipeCreator.PhoneNumber}";
        }
    }
}
