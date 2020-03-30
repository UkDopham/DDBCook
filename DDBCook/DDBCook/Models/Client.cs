using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDBCook.Models
{
    public class Client
    {
        private string _name;
        private string _phoneNumber;
        protected int _money;

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

        public Client(string name, string phoneNumber, int money = 0)
        {
            this._name = name;
            this._phoneNumber = phoneNumber;
            this._money = money;
        }

    }
}
