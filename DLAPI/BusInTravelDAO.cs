using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class BusInTravelDAO//לא סיימנו איתה נורית אמרה לא להתעסק כרגע
    {
        //private static int serial = 1;
        ////compound key
        public int License { get; set; }
        public int Line { get; set; }
        public DateTime Start { get; set; }
    }
}
