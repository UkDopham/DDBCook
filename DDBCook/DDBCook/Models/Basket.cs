using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDBCook.Models
{
    public static class Basket
    {
        private static List<Recipe> recipes = new List<Recipe>();

        public static List<Recipe> Recipes
        {
            get
            {
                return recipes;
            }
            set
            {
                recipes = value;
            }
        }
    }
}
