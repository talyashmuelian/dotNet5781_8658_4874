using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
namespace DO
{
    public class PairConsecutiveStationsDAO//זוג תחנות עוקבות
    {
        public int StationNum1 { get; set; }//מספר תחנה ראשונה
        public int StationNum2 { get; set; }//מספר תחנה שנייה
        public double Distance { get; set; }//מרחק בקילומטרים//ייתכן שצריך לעשות את זה ממשי
        public TimeSpan TimeDriving { get; set; }//זמן נסיעה ממוצע 
        //private TimeSpan timeDriving;
        //[XmlIgnore]

        //public TimeSpan TimeDriving
        //{
        //    get { return timeDriving; }
        //    set { timeDriving = value; } 
        //}//זמן נסיעה ממוצע 
        //[XmlElement("TimeDriving", DataType ="duration")]
        //[DefaultValue("PI10M")]
        //public string XmlTime
        //{
        //    get { return XmlConvert.ToString(timeDriving); }
        //    set { timeDriving = XmlConvert.ToTimeSpan(value); }
        //}
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
