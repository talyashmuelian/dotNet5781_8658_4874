using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class TripBO
    {
        public int StationNumFrom { get; set; }//מספר תחנת מקור
        public int StationNumTo { get; set; }//מספר תחנה יעד
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
