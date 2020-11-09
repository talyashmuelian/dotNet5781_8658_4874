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
        protected float Latitude;//קו רוחב
        protected float Longitude;//קו אורך
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
                float num = Program2.rand.Next(31, 34);
                Latitude = num;

            }
        }
        public float Longitude_p
        { 
            get => Longitude; 
            set
            {
                float num = Program2.rand.Next(34,36);
                Longitude = num;
            }
        }
        public BusStation(int numStation)//ctor with parameters
        {
            BusStationKey = numStation;
            float num1 = Program2.rand.Next(31, 34);
            Latitude = num1;
            num1 = Program2.rand.Next(34, 36);
            Longitude = num1;
        }
        public BusStation()//ctor empty
        {
            int num = Program2.rand.Next(11, 999999);
            BusStationKey = num;
            int num1 = Program2.rand.Next(3100000, 3330000);
            float temp = (float) num1 / 100000;
            Latitude = temp;
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
