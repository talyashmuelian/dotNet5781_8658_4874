using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_8658_4874
{
    class BusStation
    {
        //static public Program2.random Program2.rand = new Program2.random(DateTime.Now.Millisecond);
        protected int BusStationKey;//מספר תחנה
        protected float Latitude;
        protected float Longitude;
        //protected string Address;//להחליט אם לעשות מאפיין
        public int BusStationKey_p
        {
            get { return BusStationKey; }
            set {if (value > 999999)
                    Console.WriteLine("ERROR");
                BusStationKey = value;
                        } 
        }

        public float Latitude_p 
        { 
            get => Latitude; 
            set
            {
                //Program2.random Program2.rand = new Program2.random(DateTime.Now.Millisecond);
                float num = Program2.rand.Next(31, 34);
                Latitude = num;

            }
        }
        public float Longitude_p
        { 
            get => Longitude; 
            set
            {
                //Program2.random Program2.rand = new Program2.random(DateTime.Now.Millisecond);
                float num = Program2.rand.Next(34,36);
                Longitude = num;
            }
        }
        public BusStation(int numStation)
        {
            BusStationKey = numStation;
            //double num2=Program2.random.NextDouble() * (33.3- 31) + 31;
            //Program2.random Program2.rand = new Program2.random(DateTime.Now.Millisecond);
            float num1 = Program2.rand.Next(31, 34);
            Latitude = num1;
            //Program2.rand = new Program2.random(DateTime.Now.Millisecond);
            num1 = Program2.rand.Next(34, 36);
            Longitude = num1;
        }
        public BusStation()
        {
            //Program2.random Program2.rand = new Program2.random(DateTime.Now.Millisecond);
            int num = Program2.rand.Next(11, 999999);
            BusStationKey = num;
            //Program2.rand = new Program2.random(DateTime.Now.Millisecond);
            int num1 = Program2.rand.Next(3100000, 3330000);
            float temp = (float) num1 / 100000;
            Latitude = temp;
            //Program2.rand = new Program2.random(DateTime.Now.Millisecond);
            num1 = Program2.rand.Next(3430000, 3550000);
            temp = (float)num1 / 100000;
            Longitude = temp;
        }
        public override string ToString()
        {
             { return "Bus Station Code: " + BusStationKey + ", Latitude: " + Latitude + ", Longitude: " + Longitude; }
        }
    }
}
