using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class LineInStationBO//קו אוטובוס
    {
        public int IdentifyNumber { get; set; }//מזהה קו
        public int LineNumber { get; set; }//מספר קו
        public string LastStationName { get; set; }//שם תחנה אחרונה
        public int LastStationNum { get; set; }//מספר תחנה אחרונה
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
