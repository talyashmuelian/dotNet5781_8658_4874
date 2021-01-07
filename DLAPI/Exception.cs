using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace DO
{
    [Serializable]
    public class BusExceptionDO : Exception
    {
        public BusExceptionDO()
        {
        }

        public BusExceptionDO(string message) : base(message)
        {
        }

        public BusExceptionDO(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BusExceptionDO(SerializationInfo info, StreamingContext context) : base(info, context)
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
    public class BusLineExceptionDO : Exception
    {
        public BusLineExceptionDO()
        {
        }

        public BusLineExceptionDO(string message) : base(message)
        {
        }

        public BusLineExceptionDO(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BusLineExceptionDO(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
    public class BusStationExceptionDO : Exception
    {
        public BusStationExceptionDO()
        {
        }

        public BusStationExceptionDO(string message) : base(message)
        {
        }

        public BusStationExceptionDO(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BusStationExceptionDO(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
    public class LineStationExceptionDO : Exception
    {
        public LineStationExceptionDO()
        {
        }

        public LineStationExceptionDO(string message) : base(message)
        {
        }

        public LineStationExceptionDO(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LineStationExceptionDO(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
    public class PairConsecutiveStationsExceptionDO : Exception
    {
        public PairConsecutiveStationsExceptionDO()
        {
        }

        public PairConsecutiveStationsExceptionDO(string message) : base(message)
        {
        }

        public PairConsecutiveStationsExceptionDO(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PairConsecutiveStationsExceptionDO(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
    public class UserExceptionDO : Exception
    {
        public UserExceptionDO()
        {
        }

        public UserExceptionDO(string message) : base(message)
        {
        }

        public UserExceptionDO(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UserExceptionDO(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

}
