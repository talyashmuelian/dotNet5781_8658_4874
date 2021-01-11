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
    //public class BadPersonIdException : Exception
    //{
    //    public int ID;
    //    public BadPersonIdException(int id) : base() => ID = id;
    //    public BadPersonIdException(int id, string message) :
    //        base(message) => ID = id;
    //    public BadPersonIdException(int id, string message, Exception innerException) :
    //        base(message, innerException) => ID = id;

    //    public override string ToString() => base.ToString() + $", bad person id: {ID}";
    //}

    //public class BadPersonIdCourseIDException : Exception
    //{
    //    public int personID;
    //    public int courseID;
    //    public BadPersonIdCourseIDException(int perID, int crsID) : base() { personID = perID; courseID = crsID; }
    //    public BadPersonIdCourseIDException(int perID, int crsID, string message) :
    //        base(message)
    //    { personID = perID; courseID = crsID; }
    //    public BadPersonIdCourseIDException(int perID, int crsID, string message, Exception innerException) :
    //        base(message, innerException)
    //    { personID = perID; courseID = crsID; }

    //    public override string ToString() => base.ToString() + $", bad person id: {personID} and course id: {courseID}";
    //}

    public class XMLFileLoadCreateException : Exception
    {
        public string xmlFilePath;
        public XMLFileLoadCreateException(string xmlPath) : base() { xmlFilePath = xmlPath; }
        public XMLFileLoadCreateException(string xmlPath, string message) :
            base(message)
        { xmlFilePath = xmlPath; }
        public XMLFileLoadCreateException(string xmlPath, string message, Exception innerException) :
            base(message, innerException)
        { xmlFilePath = xmlPath; }

        public override string ToString() => base.ToString() + $", fail to load or create xml file: {xmlFilePath}";
    }

}
