﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace DO
{
    [Serializable]
    public class BusException : Exception
    {
        public BusException()
        {
        }

        public BusException(string message) : base(message)
        {
        }

        public BusException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BusException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
