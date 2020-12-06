using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_8658_4874
{
    [Serializable]
    public class ObjectNotAllowedException : Exception//חריגה שמציינת שהאובייקט לא מורשה או לא תואם
    {
        public ObjectNotAllowedException() : base() { }
        public ObjectNotAllowedException(string message) : base(message) { }
    }
}
