using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_8658_4874
{
    class BusLineStation: BusStation
    {
        protected double Distance;//מרחק בקילומטרים מתחנת קו אוטובוס הקודמת
        protected double TimeOfDriving;//זמן הנסיעה בדקות מתחנת קו אוטובוס הקודמת

        public double Distance_p { get => Distance; set => Distance = value; }
        public double TimeOfDriving_p { get => TimeOfDriving; set => TimeOfDriving = value; }
        public BusLineStation()
        {
            double num = Program2.rand.NextDouble() * (10000 - 0) + 0;
            Distance = num;
            TimeOfDriving = Distance / 500;
        }
        public BusLineStation(int numStation) : base(numStation)//קריאה לבנאי עם פרמטרים ממחלקת תחנת אוטובוס
        {
            double num = Program2.rand.NextDouble() * (10000 - 0) + 0;
            Distance = num;
            TimeOfDriving = Distance / 500;
        }
       
    }
}
