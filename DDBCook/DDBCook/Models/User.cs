using DDBCook.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDBCook.Models
{
    public class User : ITable
    {
        protected string _email;
        private string _password;
        private UserType _userType;
        
        public string Email
        {
            get
            {
                return this._email;
            }
        }
        public string Password
        {
            get
            {
                return this._password;
            }
        }
        public UserType UserType
        {
            get
            {
                return this._userType;
            }
        }
        public User(string email, string password, UserType userType)
        {
            this._email = email;
            this._password = password;
            this._userType = userType;
        }

        public virtual string GetTableName()
        {
            return "cook.utilisateur";
        }

        public virtual string GetTableProperties()
        {
            return "email, password";
        }

        public virtual string GetTableValues()
        {
            return $"'{this._email}', '{this._password}'";
        }
    }
}
