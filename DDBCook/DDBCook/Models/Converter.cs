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
                case TableType.user:
                    valueConverted = new User(values[0], values[1], (UserType)(Enum.Parse(typeof(UserType), values[2])));
                    break;
                    
            }

            return valueConverted;
        }

    }
}
