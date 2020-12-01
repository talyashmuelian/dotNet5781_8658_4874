using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_03b_8658_4874
{
    [Serializable]
    public class ObjectCannotDoException : Exception//חריגה שמציינת שהאובייקט להוספה כבר קיים במערכת
    {
        public ObjectCannotDoException() : base() { }
        public ObjectCannotDoException(string message) : base(message) { }
    }
}
