using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class BusLineDAO//קו אוטובוס
    {
        //אולי צריך להוסיף פה שדה עבור המספר הרץ- המזהה
        public int IdentifyNumber { get; set; }//מספר מזהה קו
        public int LineNumber { get; set; }//מספר קו
        public string Area { get; set; }//איזור
        public int FirstStationNum { get; set; }//מספר תחנה ראשונה
        public int LastStationNum { get; set; }//מספר תחנה אחרונה

        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
