using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_8658_4874
{
    class BusStation
    {
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
                Random rand = new Random(DateTime.Now.Millisecond);
                float num = rand.Next(31, 34);
                Latitude = num;

            }
        }
        public float Longitude_p
        { 
            get => Longitude; 
            set
            {
                Random rand = new Random(DateTime.Now.Millisecond);
                float num = rand.Next(34,36);
                Longitude = num;
            }
        }
        public BusStation()
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            int num = rand.Next(999999);
            BusStationKey = num;
            //double num2=Random.NextDouble() * (33.3- 31) + 31;
            rand = new Random(DateTime.Now.Millisecond);
            float num1 = rand.Next(31, 34);
            Latitude = num1;
            rand = new Random(DateTime.Now.Millisecond);
            num1 = rand.Next(34, 36);
            Longitude = num1;
        }
        public override string ToString()
        {
             { return "Bus Station Code: " + BusStationKey + ", Latitude: " + Latitude + ", Longitude" + Longitude; }
        }
    }
}
