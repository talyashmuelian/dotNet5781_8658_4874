using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;
using BLAPI;
using BO;
//using BL;
using BLAPI;
using DLAPI;
//using DL;
using BO;
using DO;
namespace UI
{
    class Program
    {
        static IBL bl;
        static IDal dal;
        static void Main(string[] args)
        {
            try
            {
                bl = BLFactory.GetBl();
                dal = DLFactory.GetDal();
                var locA = new GeoCoordinate(32.183921, 34.917806);
                var locB = new GeoCoordinate(31.870034, 34.819541);
                double dis = locA.GetDistanceTo(locB)/1000;
                double result = Math.Round(dis, 2);
                Console.WriteLine(result);
                TimeSpan TimeDriving = TimeSpan.FromMinutes(((dis) / 30) * 60);
                Console.WriteLine(TimeDriving.ToString());
                //Console.WriteLine(new DateTime(2017,5,24).Year);
                //printAllbusses();
                //bl.addBus(new BusBO { License = "93939393", });
                //printAllusers();
                //bl.deleteBus(new BusBO { License = "93939393", });
                //printAllbusses();
                //bl.updateBus(new BusBO { License = "93939393", Status = Status.READY });
                //bl.refuel(3213333);
                //printAllbusses();
                //bl.treatment(33333333);
                //printAllbusses();
                //Console.WriteLine(bl.GetBusBO(33333333));
                //printAllBusStations();
                //bl.addBusStation(new BusStationBO { CodeStation = 93939393, NameStation = "rachel hameshoreret" });
                //printAllBusStations();
                //bl.deleteBusStation(new BusStationBO { CodeStation = 93939393, NameStation = "rachel hameshoreret" });
                //printAllBusStations();
                ////bl.updateBus(new BusBO { License = "93939393", Status = Status.READY });;
                //Console.WriteLine(bl.GetBusStationBO(123456));
                //Console.WriteLine(dal.getOneObjectPairConsecutiveStations(123456, 111111));
                foreach (var item in dal.getAllLineStations())
                {
                    Console.WriteLine(item);
                }
                //printAllBusLines();
                //bl.addStationToLine(123456, 5, 11);
                //bool a=bl.addBusLine(new BusLineBO { LineNumber = 12, Area = "aaa", FirstStationNum = 123456, LastStationNum = 111111 });
                //if (a)
                //{ printAllBusLines(); }
                //bl.delStationToLine(123456, 5);
                //printAllBusLines();
                //bl.deleteBusLine(new BusLineBO { IdentifyNumber = 2 });
                //bl.deleteBusStation(new BusStationBO { CodeStation = 123456 });

                //printAllBusStations();
                //bl.orderLinesByArea();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("hiiiiiiiiiiiii");

        }
        private static void printAllbusses()
        {
            foreach (var item in bl.GetAllBusesBO())
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("-------------------------------");
        }
        private static void printAllusers()
        {
            foreach (var item in bl.GetAllUsersBO())
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("-------------------------------");
        }
        private static void printAllBusStations()
        {
            foreach (var item in bl.GetAllBusStationsBO())
            {
                Console.WriteLine(item);
                try
                {
                    foreach (var vvv in item.ListOfLines)
                    {
                        Console.WriteLine(vvv);
                        //Console.WriteLine(vvv.LastStationName);
                        //Console.WriteLine(vvv.LastStationNum);
                    }
                }
                catch { Console.WriteLine("no lines"); }
            }
            Console.WriteLine("-------------------------------");
        }
        private static void printAllBusLines()
        {
            foreach (var item in bl.GetAllBusLinesBO())
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("-------------------------------");
        }
    }
}
