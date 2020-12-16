using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class PairConsecutiveStationsDAO//זוג תחנות עוקבות
    {
        public int StationNum1 { get; set; }//מספר תחנה ראשונה
        public int StationNum2 { get; set; }//מספר תחנה שנייה
        public int Distance { get; set; }//מרחק בקילומטרים//ייתכן שצריך לעשות את זה ממשי
        public int TimeDriving { get; set; }//זמן נסיעה ממוצע בדקות
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
