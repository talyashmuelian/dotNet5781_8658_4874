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
        //public int capacity { get; private set; }
        public ListEmptyException() : base() { }
        public ListEmptyException(string message) : base(message) { }
        public ListEmptyException(string message, Exception inner) : base(message, inner) { }
        protected ListEmptyException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        // special constructor for our custom exception
        //override public string ToString()
        //{
        //    return "OverloadCapacityException: DAL capacity of " + capacity + " overloaded\n" + Message;
        //}
    }
}
