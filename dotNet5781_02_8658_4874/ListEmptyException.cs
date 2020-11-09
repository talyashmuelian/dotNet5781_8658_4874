using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace dotNet5781_02_8658_4874
{
    [Serializable]
    public class ListEmptyException : Exception
    {
        public ListEmptyException() : base() {  }
        public ListEmptyException(string message) : base(message) { }
        //public ListEmptyException(string message, Exception inner) : base(message, inner) { }
        //protected ListEmptyException(SerializationInfo info, StreamingContext context) : base(info, context) { }
     
    }
    public class ObjectNotFoundException : Exception//חריגה שמציינת שהאובייקט להוספה או למחיקה לא קיים במערכת
    {
        public ObjectNotFoundException() : base() { }
        public ObjectNotFoundException(string message) : base(message) { }
    }
    public class AnObjectAlreadyExistsException : Exception//חריגה שמציינת שהאובייקט להוספה כבר קיים במערכת
    {
        public AnObjectAlreadyExistsException() : base() { }
        public AnObjectAlreadyExistsException(string message) : base(message) { }
    }
}
