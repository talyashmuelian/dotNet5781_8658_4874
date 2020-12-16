using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class LineStationDAO//תחנת קו
    {
        public int IdentifyNumber { get; set; } //מספר מזהה קו
        public int CodeStation { get; set; }//קוד תחנה
        public int NumStationInTheLine { get; set; }//מספר התחנה בקו
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
