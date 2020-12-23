using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLAPI;
using BO;
//using BL;
namespace UI
{
    class Program
    {
        static IBL bl;
        static void Main(string[] args)
        {
            try
            {
                bl = BLFactory.GetBl();
                //printAllbusses();
                //bl.addBus(new BusBO { License = "93939393", });
                //printAllbusses();
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
                printAllBusLines();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
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
        private static void printAllBusStations()
        {
            foreach (var item in bl.GetAllBusStationsBO())
            {
                Console.WriteLine(item);
                foreach (var vvv in item.ListOfLines)
                    Console.WriteLine(vvv.LineNumber);
            }
            Console.WriteLine("-------------------------------");
        }
        private static void printAllBusLines()
        {
            foreach (var item in bl.GetAllBusLinesBO())
            {
                Console.WriteLine(item);
                //foreach (var vvv in item.ListOfStations)
                //    Console.WriteLine(vvv.CodeStation);
            }
            Console.WriteLine("-------------------------------");
        }
    }
}
