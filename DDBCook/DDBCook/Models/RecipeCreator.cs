using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDBCook.Models
{
    public class RecipeCreator : Client
    {
        public RecipeCreator(string name, string phoneNumber, int money = 0):
            base(name, phoneNumber, money)
        {

        }
    }
}
