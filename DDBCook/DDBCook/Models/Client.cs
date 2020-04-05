using DDBCook.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDBCook.Models
{
    public class Client : User, ITable
    {
        private string _name;
        private string _phoneNumber;
        private string _adress;
        protected int _money;

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

        public Client(string email, string password, UserType userType, string name, string phoneNumber, string adress, int money = 0)
            : base(email, password, userType)
        {
            this._name = name;
            this._phoneNumber = phoneNumber;
            this._adress = adress;
            this._money = money;
        }

        public override string GetTableName()
        {
            return $"cook.client";
        }

        public override string GetTableProperties()
        {
            return $"nom, balance, adresse, numero, emailUtilisateur";
        }

        public override string GetTableValues()
        {
            return $"{this._name}, {this._money}, {this._adress}, {this._phoneNumber}, {this._email}";
        }
    }
}
