using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS
{
    static public class configoration//לא בטוח שההרשאות נכונות
    {
        static int runNumber = 11;
        public static int RunNumber => ++runNumber; //מספר רץ בשביל מספר מזהה
    }
}
