using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDBCook.Models
{
    public class DoubleContainer<T, U>
    {
        private T _value;
        private U _otherValue;

        public T Value
        {
            get
            {
                return this._value;
            }
        }

        public U OtherValue
        {
            get
            {
                return this._otherValue;
            }
        }

        public DoubleContainer(T value, U otherValue)
        {
            this._value = value;
            this._otherValue = otherValue;
        }


        public override string ToString()
        {
            return this._value.ToString();
        }

    }
}
