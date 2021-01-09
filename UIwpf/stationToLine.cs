using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIwpf
{
    public class stationToLine
    {
        public int IdentifyNumber { get; set; } //מספר מזהה קו
        public int LineNumber { get; set; }//מספר קו
        public int CodeStation { get; set; }//קוד תחנה
        public int Location { get; set; }//מספר התחנה בקו
    }
}
