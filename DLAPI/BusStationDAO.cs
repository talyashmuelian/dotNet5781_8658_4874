using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class BusStationDAO
    {
        public int CodeStation { get; set; }//קוד תחנה
        public double Latitude { get; set; }//קו רוחב
        public double Longitude { get; set; }//קו אורך
        public string NameStation { get; set; }//שם תחנה
        public bool IsAccessible { get; set; }//האם יש גישה לנכים
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
