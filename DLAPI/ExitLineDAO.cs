using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class ExitLineDAO//יציאת קו
    {
        //אולי צריך להוסיף שדה עבור מזהה קו אוטובוס
        public TimeSpan TimeOfStart { get; set; }//זמן התחלה
        public TimeSpan TimeOfEnd { get; set; }//זמן סיום
        public int Frequency { get; set; }//תדירות

        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
