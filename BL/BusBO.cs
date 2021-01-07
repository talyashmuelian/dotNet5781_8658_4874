using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BO
{
    public class BusBO
    {
        public string License { get; set; }//מספר רישוי
        public DateTime StartOfWork { get; set; }//תאריך רישוי
        public int TotalKms { get; set; }//קילומטרז'
        public int Fuel { get; set; }//מיכל דלק
        public DateTime DateTreatLast { get; set; }//תאריך טיפול אחרון
        public int KmFromTreament { get; set; }//מספר הקילומטרים מאז הטיפול האחרון
        public Status Status { get; set; }
        public string LicenseFormat
        {
            get; set;
            //get
            //{
            //    if (StartOfWork.Year > 2017)//8 ספרות numbers
            //    {
            //        return License.Substring(0, 3) + "-" + License[3] + License[4] + "-" + License[5] + License[6] + License[7];

            //    }
            //    else//7 ספרות numbers
            //    {
            //        return License.Substring(0, 2) + "-" + License[2] + License[3] + License[4] + "-" + License[5] + License[6];
            //    }
            //}
            //set => License = value;
        }

        public override string ToString()
        {
            return this.ToStringProperty();
        }
        //....
    }
}
