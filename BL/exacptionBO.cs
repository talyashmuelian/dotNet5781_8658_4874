using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using DO;
namespace BO
{
    [Serializable]
    public class BusExceptionBO : Exception
    {
        public BusExceptionBO()
        {
        }

        public BusExceptionBO(string message) : base(message)
        {
        }

        public BusExceptionBO(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BusExceptionBO(SerializationInfo info, StreamingContext context) : base(info, context)
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
    public class BusLineExceptionBO : Exception
    {
        public BusLineExceptionBO()
        {
        }

        public BusLineExceptionBO(string message) : base(message)
        {
        }

        public BusLineExceptionBO(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BusLineExceptionBO(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
    public class BusStationExceptionBO : Exception
    {
        public BusStationExceptionBO()
        {
        }

        public BusStationExceptionBO(string message) : base(message)
        {
        }

        public BusStationExceptionBO(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BusStationExceptionBO(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
    public class LineStationExceptionBO : Exception
    {
        public LineStationExceptionBO()
        {
        }

        public LineStationExceptionBO(string message) : base(message)
        {
        }

        public LineStationExceptionBO(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LineStationExceptionBO(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
    public class PairConsecutiveStationsExceptionBO : Exception
    {
        public PairConsecutiveStationsExceptionBO()
        {
        }

        public PairConsecutiveStationsExceptionBO(string message) : base(message)
        {
        }

        public PairConsecutiveStationsExceptionBO(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PairConsecutiveStationsExceptionBO(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
