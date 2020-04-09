using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDBCook.Models
{
    public static class User
    {
        public const string Username = "root";
        public const string Password = "alexandre1";
        public const string DataBase = "cook";

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
