using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
namespace BO
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
    public class ObjectNotFoundException : Exception//חריגה שמציינת שהאובייקט לא נמצא
    {
        public ObjectNotFoundException()
        {
        }
        public ObjectNotFoundException(string message) : base(message)
        {
        }

        public ObjectNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ObjectNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
    public class BusLineException : Exception
    {
        public BusLineException()
        {
        }

        public BusLineException(string message) : base(message)
        {
        }

        public BusLineException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BusLineException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
    public class BusStationException : Exception
    {
        public BusStationException()
        {
        }

        public BusStationException(string message) : base(message)
        {
        }

        public BusStationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BusStationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
    public class LineStationException : Exception
    {
        public LineStationException()
        {
        }

        public LineStationException(string message) : base(message)
        {
        }

        public LineStationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LineStationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
    public class PairConsecutiveStationsException : Exception
    {
        public PairConsecutiveStationsException()
        {
        }

        public PairConsecutiveStationsException(string message) : base(message)
        {
        }

        public PairConsecutiveStationsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PairConsecutiveStationsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
