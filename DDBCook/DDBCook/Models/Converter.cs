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
        /// <summary>
        /// Convert the result of the sql request in an object
        /// </summary>
        /// <param name="value"></param>
        /// <param name="tableType"></param>
        /// <returns></returns>
        public static ITable ConvertFromString(string value, TableType tableType)
        {
            string[] values = value.Split(';');
            ITable valueConverted = null;
            switch(tableType)
            {
                case TableType.client:
                    valueConverted = new Client(
                        name: values[0], 
                        money: Convert.ToInt32(values[1]),
                        adress: values[2],
                        phoneNumber: values[3],
                        email: values[4], 
                        password: values[5], 
                        userType: (UserType)(Enum.Parse(typeof(UserType), values[6])));
                    break;
                case TableType.order:
                    valueConverted = new Order(
                        id: values[0], 
                        orderDate: DateTime.Parse(values[1]),
                        clientNumber: values[2], 
                        recipeName: values[3]
                        );
                    break;     //  dateValue.ToString("yyyy-MM-dd HH:mm:ss"); dans l'autre sens
                case TableType.recipe:
                    valueConverted = new Recipe(
                        name:values[0],
                        recipeType:(RecipeType)(Enum.Parse(typeof(RecipeType),values[1])),
                        description:  values[2],
                        price: Convert.ToInt32(values[3]),
                        numberCreator : values[4],
                        isHealthy: values[5].Equals("True"),
                        isBio: values[6].Equals("True"),
                        isVegan: values[7].Equals("True"),
                        isChimical: values[8].Equals("True"),
                        isTrending: values[9].Equals("True"),
                        rating: Convert.ToInt32(values[10]));
                    break;
                case TableType.product:
                    valueConverted = new Product(
                        reference:values[0],
                        name:values[1],
                        productCategory:(ProductCategory)(Enum.Parse(typeof(ProductCategory),values[2])),
                        currentQuantity: Convert.ToInt32(values[3]),
                        minQuantity: Convert.ToInt32(values[4]),
                        maxQuantity: Convert.ToInt32(values[5]),
                        unit:values[6],
                        providerNumber:values[7]
                        );
                    break;
                case TableType.supplier:
                    valueConverted = new Supplier(
                        name:values[0],
                        number: values[1]
                        );
                    break;
                case TableType.recipeCreator:
                    valueConverted = new RecipeCreator(
                        id:values[0]
                        );
                    break;
                case TableType.productComposition:
                    valueConverted = new ProductComposition(
                        id:values[0],
                        quantity: Convert.ToInt32(values[1]),
                        refProduct:values[2],
                        recipeName:values[3]
                        );
                    break;


                default:
                    Console.WriteLine("ERROR : TableType unknown!   (Converter : ConvertFromString)");
                    break;
                    
            }

            return valueConverted;
        }

    }
}
