using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class PairConsecutiveStationsBO
    {
        public int StationNum1 { get; set; }//מספר תחנה ראשונה
        public int StationNum2 { get; set; }//מספר תחנה שנייה
        public double Distance { get; set; }//מרחק בקילומטרים//ייתכן שצריך לעשות את זה ממשי
        public TimeSpan TimeDriving { get; set; }//זמן נסיעה ממוצע
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
