using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_8658_4874
{
    class BusLineStation: BusStation
    {
        //static public Program2.random Program2.rand = new Program2.random(DateTime.Now.Millisecond);
        //לקלוט את המידע מהמשתמש
        protected float Distance;//מרחק מתחנת קו אוטובוס הקודמת
        protected float TimeOfDriving;//זמן הנסיעה בדקות מתחנת קו אוטובוס הקודמת

        public float Distance_p { get => Distance; set => Distance = value; }
        public float TimeOfDriving_p { get => TimeOfDriving; set => TimeOfDriving = value; }
        public BusLineStation()
        {
            //Program2.random.NextDouble() * (maximum - minimum) + minimum;
            //Program2.random Program2.rand = new Program2.random(DateTime.Now.Millisecond);
            float num = Program2.rand.Next(10000);
            Distance = num;
            TimeOfDriving = Distance / 500;//כל חצי קילומטר לוקח דקה נסיעה
        }
        public BusLineStation(int numStation) : base(numStation)//קריאה לבנאי עם פרמטרים ממחלקת תחנת אוטובוס
        {
            //Program2.random.NextDouble() * (maximum - minimum) + minimum;
            //Program2.random Program2.rand = new Program2.random(DateTime.Now.Millisecond);
            float num = Program2.rand.Next(10000);
            Distance = num;
            TimeOfDriving = Distance / 500;//כל חצי קילומטר לוקח דקה נסיעה
        }
    }
}
