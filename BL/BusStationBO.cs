using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class BusStationBO
    {
        public int CodeStation { get; set; }//קוד תחנה
        public double Latitude { get; set; }//קו רוחב
        public double Longitude { get; set; }//קו אורך
        public string NameStation { get; set; }//שם תחנה
        public bool IsAccessible { get; set; }//האם יש גישה לנכים
        public IEnumerable<LineInStationBO> ListOfLines { get; set;}
        public override string ToString()
        {
            return this.ToStringProperty();
            ///לטפל בטוסטרינג הזה. הוא לא מדפיס את רשימת הקווים בתחנה
        }
    }
}
