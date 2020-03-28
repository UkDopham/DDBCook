using DDBCook.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDBCook.Models
{
    public class Recipe
    {
        private string _name;
        private RecipeType _recipeType;
        private string _description;
        private List<Product> _products;
        private int _price;

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
        private List<Product> Products
        {
            get
            {
                return this._products;
            }
        }
        public int Price
        {
            get
            {
                return this._price;
            }
        }
        public Recipe(string name, RecipeType recipeType, string description, List<Product> products, int price = 2)
        {
            this._name = name;
            this._recipeType = recipeType;
            this._description = description;
            this._products = products;
            this._price = price;
        }
    }
}
