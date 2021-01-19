using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class WayForPassBO
    {
        public int LineNumber { get; set; }//מספר קו
        public TimeSpan TimeOfTrip { get; set; }//כמה זמן לוקחת הדרך הזאת
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
