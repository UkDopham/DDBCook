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
        private bool _isHealthy;
        private bool _isBio;
        private bool _isVegan;
        private bool _isChimical;
        private bool _isTrending;
        private int _rating;
        private string _numberCreator;

        public bool IsHealthy
        {
            get
            {
                return this._isHealthy;
            }
        }
        public bool IsBio
        {
            get
            {
                return this._isBio;
            }
        }
        public bool IsVegan
        {
            get
            {
                return this._isVegan;
            }
        }
        public bool IsChimical
        {
            get
            {
                return this._isChimical;
            }
        }
        public bool IsTrending
        {
            get
            {
                return this._isTrending;
            }
        }
        public int Rating
        {
            get
            {
                return this._rating;
            }
            set
            {
                this._rating = value;
            }
        }
        public string NumberCreator
        {
            get
            {
                return this._numberCreator;
            }
        }
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


        public Recipe(
            string name,
            RecipeType recipeType,
            string description,
            // List<ProductComposition> productsComposition,
            // RecipeCreator recipeCreator,
            string numberCreator,
            int price = 2,
            bool isHealthy = false,
            bool isBio = false,
            bool isVegan = false,
            bool isChimical = false,
            bool isTrending = false,
            int rating = 0
            )
        {
            this._name = name;
            this._recipeType = recipeType;
            this._description = description;
            // this._productsComposition = productsComposition;
            // this._recipeCreator = recipeCreator;
            this._price = price;
            this._isHealthy = isHealthy;
            this._isBio = isBio;
            this._isVegan = isVegan;
            this._isChimical = isChimical;
            this._isTrending = isTrending;
            this._rating = rating;
            this._numberCreator = numberCreator;
        }

        public string GetTableName()
        {
            return "cook.recette";
        }

        public string GetTableProperties()
        {
            return "nom, categorie, description, prix, numeroCreateur,estHealthy,estBio,estVegan,estChimique,estTendance";
        }

        public string GetTableValues()
        {
            return $"'{this._name}', '{this._recipeCreator}', '{this._description}', {this._price}, '{this._numberCreator}'";
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
