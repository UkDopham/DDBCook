using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDBCook.Models
{
    public static class User
    {
        public static string Username = "root";
        public static string Password = "alexandre1";
        public static string DataBase = "cook";

        private static Client connectedClient;

        public static Client ConnectedClient
        {
            get
            {
                return connectedClient;
            }
            set
            {
                connectedClient = value;
            }
        }

    }
}
