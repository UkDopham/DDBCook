using DDBCook.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDBCook.Models
{
    public static class Converter
    {
        public static ITable ConvertFromString(string value, TableType tableType)
        {
            string[] values = value.Split(';');
            ITable valueConverted = null;
            switch(tableType)
            {
                case TableType.client:
                    valueConverted = new Client(name: values[0], money: Convert.ToInt32(values[1]), adress: values[2],phoneNumber: values[3],email: values[4], password: values[5], userType: (UserType)(Enum.Parse(typeof(UserType), values[6])));
                    break;
                // case TableType.order:
                //     valueConverted = new Order(values[0], DateTime.Parse(values[1]),values[2], values[3]);
                //     break;     //  dateValue.ToString("yyyy-MM-dd HH:mm:ss"); dans l'autre sens
                // case TableType.recipe:
                //     valueConverted = new Recipe(values[0],(RecipeType)(Enum.Parse(typeof(RecipeType), values[1])), values[2],Convert.ToInt32(values[3]),  values[5]);
                //     break;
                default:
                    Console.WriteLine("ERROR : TableType unknown!   (Converter : ConvertFromString)");
                    break;
                    
            }

            return valueConverted;
        }

    }
}
