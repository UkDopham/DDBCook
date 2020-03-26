using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDBCook.Models
{
    public class DDB
    {
        private MySqlConnection _mySqlConnection;

        public DDB(string database, string username, string password)
        {
            Open(database, username, password);
        }

        private void Open(string database, string username, string password)
        {
            try
            {
                string connectionString = $"SERVER=localhost;PORT=3306;DATABASE={database};UID={username};PASSWORD={password};";
                this._mySqlConnection = new MySqlConnection(connectionString);
                this._mySqlConnection.Open();
            }
            catch(Exception ex)
            {

            }
        }
    }
}
