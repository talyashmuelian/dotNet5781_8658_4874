using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class StationInLineBO
    {
        public int NumStationInTheLine { get; set; }//מספר התחנה בקו
        public int CodeStation { get; set; }//קוד תחנה
        public string NameStation { get; set; }//שם תחנה
        public double Distance { get; set; }//מרחק מתחנה קודמת בקמ
        public TimeSpan TimeDriving { get; set; }//זמן נסיעה מתחנה קודמת
        public TimeSpan TimeDrivingFromFirstStation { get; set; }//זמן נסיעה מתחנה המוצא
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
