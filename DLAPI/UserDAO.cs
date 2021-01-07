using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class UserDAO
    {
        public string UserName { get; set; }//שם משתמש
        public string PassWord { get; set; }//סיסמה
        public string CheckAsk { get; set; }//שאלת אימות-שם בית הספר היסודי בו למדת

        public override string ToString()
        {
            return this.ToStringProperty();
        }
        //....
    }
}
