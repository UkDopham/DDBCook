using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDBCook.Models
{
    public static class User
    {
        public const string Path = "settings.txt";
        public static string Username;
        public static string Password;
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
