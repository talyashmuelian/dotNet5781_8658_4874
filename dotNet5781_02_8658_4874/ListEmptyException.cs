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
        //public ListEmptyException() : base() { }
        public ListEmptyException(string message) : base(message) { }
        //public ListEmptyException(string message, Exception inner) : base(message, inner) { }
        //protected ListEmptyException(SerializationInfo info, StreamingContext context) : base(info, context) { }
     
    }
}
