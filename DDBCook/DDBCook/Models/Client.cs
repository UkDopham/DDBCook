using DDBCook.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDBCook.Models
{
    public class Client : ITable
    {
        private string _name;
        private string _phoneNumber;
        private string _adress;
        protected int _money;
        protected string _email;
        protected string _password;
        protected UserType _userType;

        public string Adress
        {
            get
            {
                return this._adress;
            }
        }
        public int Money
        {
            get
            {
                return this._money;
            }
        }

        public string Name
        {
            get
            {
                return this._name;
            }
        }

        public string PhoneNumber
        {
            get
            {
                return this._phoneNumber;
            }
        }
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

        public Client(string email, string password, UserType userType, string name, string phoneNumber, string adress, int money = 0)
        {
            this._name = name;
            this._phoneNumber = phoneNumber;
            this._adress = adress;
            this._money = money;
            this._email = email;
            this._password = password;
            this._userType = userType;
        }

        public virtual string GetTableName()
        {
            return $"cook.client";
        }

        public virtual string GetTableProperties()
        {
            return $"nom, balance, adresse, numero, emailUtilisateur, password, type";
        }

        public virtual string GetTableValues()
        {
            return $"'{this._name}', {this._money}, '{this._adress}', '{this._phoneNumber}', '{this._email}', '{this._password}', '{this._userType}'";
        }


        public override string ToString()
        {
            return this.Name;
        } 
    }
}
