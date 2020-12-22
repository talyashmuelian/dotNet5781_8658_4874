using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class StationInLineBO
    {
        public int CodeStation { get; set; }//קוד תחנה
        public string NameStation { get; set; }//שם תחנה
        public int Distance { get; set; }//מרחק בקילומטרים//ייתכן שצריך לעשות את זה ממשי
        public int TimeDriving { get; set; }//זמן נסיעה ממוצע בדקות
    }
}
