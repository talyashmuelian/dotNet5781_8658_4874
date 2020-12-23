using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
namespace DS
{
    public static class DATA
    {
        static public Random rand = new Random();
        private static List<BusDAO> busses = new List<BusDAO>();//רשימת אוטבוסים
        //private static List<BusInTravelDAO> busestravel = new List<BusInTravelDAO>();//רשימת אוטובוסים בנסיעה
        private static List<BusLineDAO> busLines = new List<BusLineDAO>();//רשימה של קווי אוטובוס
        private static List<BusStationDAO> busStations = new List<BusStationDAO>();//רשימת תחנות
        private static List<LineStationDAO> lineStations = new List<LineStationDAO>();//רשימת תחנות קו (נקודות במסלול)
        private static List<PairConsecutiveStationsDAO> pairConsecutiveStations = new List<PairConsecutiveStationsDAO>();//אוסף כל זוגות התחנות העוקבות
        public static List<BusDAO> Buses { get => busses; }
        //public static List<BusInTravelDAO> BusesTravel { get => busestravel; }
        public static List<BusLineDAO> BusLines { get => busLines; }
        public static List<BusStationDAO> BusStations { get => busStations; }
        public static List<LineStationDAO> LineStations { get => lineStations; }
        public static List<PairConsecutiveStationsDAO> PairConsecutiveStations { get => pairConsecutiveStations; }
        static DATA()
        {
            initBuses();
            initBusLines();
            initBusStations();
            initLineStations();
            initPairConsecutiveStations();
        }
        public static void initBuses()
        {
            Buses.Add(new BusDAO
            {
                License = 1234567,
                StartOfWork = DateTime.Today.AddYears(-3),
                TotalKms = 5000,
                Fuel = 1200,
                DateTreatLast= DateTime.Now,
                KmFromTreament=0,
                Status = Status.READY
            });
            Buses.Add(new BusDAO
            {
                License = 1234578,
                StartOfWork = DateTime.Today.AddYears(-3),
                TotalKms = 6000,
                Fuel = 1200,
                DateTreatLast = DateTime.Now,
                KmFromTreament = 0,
                Status = Status.READY
            });
            Buses.Add(new BusDAO
            {
                License = 1222567,
                StartOfWork = DateTime.Today.AddYears(-3),
                TotalKms = 12000,
                Fuel = 1000,
                DateTreatLast = DateTime.Now,
                KmFromTreament = 0,
                Status = Status.READY
            });
            Buses.Add(new BusDAO
            {
                License = 4444567,
                StartOfWork = DateTime.Today.AddYears(-10),
                TotalKms = 1000,
                Fuel = 500,
                DateTreatLast = DateTime.Now,
                KmFromTreament = 0,
                Status = Status.READY
            });
            Buses.Add(new BusDAO
            {
                License = 12341234,
                StartOfWork = DateTime.Today.AddYears(-1),
                TotalKms = 14000,
                Fuel = 1200,
                DateTreatLast = DateTime.Now,
                KmFromTreament = 0,
                Status = Status.READY
            });
            Buses.Add(new BusDAO
            {
                License = 88888888,
                StartOfWork = DateTime.Today.AddYears(-2),
                TotalKms = 13000,
                Fuel = 700,
                DateTreatLast = DateTime.Now,
                KmFromTreament = 0,
                Status = Status.READY
            });
            Buses.Add(new BusDAO
            {
                License = 33333333,
                StartOfWork = DateTime.Today.AddYears(-0),
                TotalKms = 15000,
                Fuel = 1200,
                DateTreatLast = DateTime.Now,
                KmFromTreament = 0,
                Status = Status.READY
            });
            Buses.Add(new BusDAO
            {
                License = 4444444,
                StartOfWork = DateTime.Today.AddYears(-4),
                TotalKms = 3000,
                Fuel = 1200,
                DateTreatLast = DateTime.Now,
                KmFromTreament = 0,
                Status = Status.READY
            });
            Buses.Add(new BusDAO
            {
                License = 1231235,
                StartOfWork = DateTime.Today.AddYears(-11),
                TotalKms = 18000,
                Fuel = 1200,
                DateTreatLast = DateTime.Now,
                KmFromTreament = 0,
                Status = Status.READY
            });
            Buses.Add(new BusDAO
            {
                License = 56756788,
                StartOfWork = DateTime.Today.AddYears(-1),
                TotalKms = 8000,
                Fuel = 900,
                DateTreatLast = DateTime.Now,
                KmFromTreament = 0,
                Status = Status.READY
            });
            Buses.Add(new BusDAO
            {
                License = 23423455,
                StartOfWork = DateTime.Today.AddYears(-2),
                TotalKms = 9000,
                Fuel = 1200,
                DateTreatLast = DateTime.Now,
                KmFromTreament = 0,
                Status = Status.READY
            });
            Buses.Add(new BusDAO
            {
                License = 32132166,
                StartOfWork = DateTime.Today.AddYears(-0),
                TotalKms = 8000,
                Fuel = 900,
                DateTreatLast = DateTime.Now,
                KmFromTreament = 0,
                Status = Status.READY
            });
            Buses.Add(new BusDAO
            {
                License = 65466777,
                StartOfWork = DateTime.Today.AddYears(-0),
                TotalKms = 7000,
                Fuel = 1200,
                DateTreatLast = DateTime.Now,
                KmFromTreament = 0,
                Status = Status.READY
            });
            Buses.Add(new BusDAO
            {
                License = 44444555,
                StartOfWork = DateTime.Today.AddYears(-1),
                TotalKms = 10000,
                Fuel = 800,
                DateTreatLast = DateTime.Now,
                KmFromTreament = 0,
                Status = Status.READY
            });
            Buses.Add(new BusDAO
            {
                License = 3213214,
                StartOfWork = DateTime.Today.AddYears(-13),
                TotalKms = 30000,
                Fuel = 1200,
                DateTreatLast = DateTime.Now,
                KmFromTreament = 0,
                Status = Status.READY
            });
            Buses.Add(new BusDAO
            {
                License = 12332112,
                StartOfWork = DateTime.Today.AddYears(-2),
                TotalKms = 45000,
                Fuel = 600,
                DateTreatLast = DateTime.Now,
                KmFromTreament = 0,
                Status = Status.READY
            });
            Buses.Add(new BusDAO
            {
                License = 9877896,
                StartOfWork = DateTime.Today.AddYears(-9),
                TotalKms = 8000,
                Fuel = 1200,
                DateTreatLast = DateTime.Now,
                KmFromTreament = 0,
                Status = Status.READY
            });

            Buses.Add(new BusDAO
            {
                License = 3213333,
                StartOfWork = DateTime.Today.AddYears(-20),
                TotalKms = 9999999,
                Fuel = 500,
                DateTreatLast = DateTime.Now,
                KmFromTreament = 0,
                Status = Status.READY
            });
            Buses.Add(new BusDAO
            {
                License = 77745617,
                StartOfWork = DateTime.Today.AddYears(-2),
                TotalKms = 5000,
                Fuel = 1200,
                DateTreatLast = DateTime.Now,
                KmFromTreament = 0,
                Status = Status.READY
            });
            Buses.Add(new BusDAO
            {
                License = 6666666,
                StartOfWork = DateTime.Today.AddYears(-100),
                TotalKms = 5000,
                Fuel = 1200,
                DateTreatLast = DateTime.Now,
                KmFromTreament = 0,
                Status = Status.READY
            });

        }
        public static void initBusLines()
        {
            BusLines.Add(new BusLineDAO
            {
                IdentifyNumber = 1,
                LineNumber = 1,
                Area="center",
                FirstStationNum =123456,
                LastStationNum=111111,
            });
            BusLines.Add(new BusLineDAO
            {
                IdentifyNumber = 2,
                LineNumber = 2,
                Area ="jerusalem",
                FirstStationNum =111222,
                LastStationNum =123123,
            });
            BusLines.Add(new BusLineDAO
            {
                IdentifyNumber = 3,
                LineNumber = 3,
                Area ="south",
                FirstStationNum =234234,
                LastStationNum =144144,
            });
            BusLines.Add(new BusLineDAO
            {
                IdentifyNumber =4 ,
                LineNumber = 4,
                Area = "north",
                FirstStationNum =122122,
                LastStationNum =344344,
            });
            BusLines.Add(new BusLineDAO
            {
                IdentifyNumber =5 ,
                LineNumber = 5,
                Area = "center",
                FirstStationNum =544544,
                LastStationNum =654321,
            });
            BusLines.Add(new BusLineDAO
            {
                IdentifyNumber = 6,
                LineNumber = 6,
                Area = "jerusalem",
                FirstStationNum =654654,
                LastStationNum =345999,
            });
            BusLines.Add(new BusLineDAO
            {
                IdentifyNumber = 7,
                LineNumber = 7,
                Area = "north",
                FirstStationNum =877877,
                LastStationNum =435435,
            });
            BusLines.Add(new BusLineDAO
            {
                IdentifyNumber =8 ,
                LineNumber =8 ,
                Area = "center",
                FirstStationNum =987658,
                LastStationNum =374657,
            });
            BusLines.Add(new BusLineDAO
            {
                IdentifyNumber =9 ,
                LineNumber = 9,
                Area = "jerusalem",
                FirstStationNum =756284,
                LastStationNum =376592,
            });
            BusLines.Add(new BusLineDAO
            {
                IdentifyNumber = 10,
                LineNumber = 10,
                Area = "south",
                FirstStationNum =568377,
                LastStationNum =473829,
            });
        }
        public static void initBusStations()
        {
            BusStations.Add(new BusStationDAO
            {
                CodeStation =123456,
                Latitude=34.45,
                Longitude=33.6,
                NameStation="mizpe nevo",
                IsAccessible=true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =111111,
                Latitude =30.01,
                Longitude =31.11,
                NameStation ="shomron",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =111222,
                Latitude =40.6,
                Longitude =20.45,
                NameStation ="avraham avinu",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =123123,
                Latitude =50.9,
                Longitude =49.7,
                NameStation ="yizhak avinu",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =234234,
                Latitude =56.23,
                Longitude =57.45,
                NameStation ="yaakov avinu",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =144144,
                Latitude =40.1,
                Longitude =41.2,
                NameStation ="sara imenu",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =122122,
                Latitude =54.3,
                Longitude =34.45,
                NameStation ="rivka imenu",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =344344,
                Latitude =23.78,
                Longitude =24.55,
                NameStation ="lea imenu",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =544544,
                Latitude =24.55,
                Longitude =77.8,
                NameStation ="rachel imenu",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =654321,
                Latitude =43,
                Longitude =23.5,
                NameStation ="reuven",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =654654,
                Latitude =76.3,
                Longitude =87.54,
                NameStation ="shimon",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =345999,
                Latitude =12.33,
                Longitude =33.21,
                NameStation ="levi",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =877877,
                Latitude =76.8,
                Longitude =87.9,
                NameStation ="yehooda",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =435435,
                Latitude =65.2,
                Longitude =23.67,
                NameStation ="yissachar",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =987658,
                Latitude =16.52,
                Longitude =95.36,
                NameStation ="zvulun",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =374657,
                Latitude =73.54,
                Longitude =34.73,
                NameStation ="gad",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =756284,
                Latitude =74.32,
                Longitude =36.63,
                NameStation ="asher",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =376592,
                Latitude =73.58,
                Longitude =47.83,
                NameStation ="dan",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =568377,
                Latitude =93.64,
                Longitude =47.54,
                NameStation ="naftali",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =473829,
                Latitude =75.3,
                Longitude =43.5,
                NameStation ="yosef",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =376483,
                Latitude =98.65,
                Longitude =65.34,
                NameStation ="benyamin",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =625783,
                Latitude =34.56,
                Longitude =87.54,
                NameStation ="david hamelech",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =874593,
                Latitude =32.56,
                Longitude =19.43,
                NameStation ="shlomo hamelech",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =654733,
                Latitude =92.43,
                Longitude =74.53,
                NameStation ="avigail",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =929292,
                Latitude =83.54,
                Longitude =29.64,
                NameStation ="bat-sheva",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =454545,
                Latitude =65.43,
                Longitude =73.43,
                NameStation ="ester",
                IsAccessible = false
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =878787,
                Latitude =46.87,
                Longitude =38.24,
                NameStation ="mordechai",
                IsAccessible = false
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =363636,
                Latitude =45.78,
                Longitude =98.1,
                NameStation ="moshe rabenu",
                IsAccessible = false
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =747474,
                Latitude =25.63,
                Longitude =28.4,
                NameStation ="aharon hacohen",
                IsAccessible = false
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =757575,
                Latitude =18.62,
                Longitude =93.54,
                NameStation ="eliyahu hanavy",
                IsAccessible = false
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =292929,
                Latitude =91.4,
                Longitude =51.3,
                NameStation ="elisha hanavy",
                IsAccessible = false
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =919191,
                Latitude =64.7,
                Longitude =82.54,
                NameStation ="michal",
                IsAccessible = false
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =982457,
                Latitude =84.6,
                Longitude =93.5,
                NameStation ="yehonatan",
                IsAccessible = false
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =637291,
                Latitude =92.64,
                Longitude =92.43,
                NameStation ="shaul hamelech",
                IsAccessible = false
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =928384,
                Latitude =73.87,
                Longitude =53.5,
                NameStation ="shmuel hanavy",
                IsAccessible = false
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =234890,
                Latitude =84.69,
                Longitude =92.84,
                NameStation ="yishay",
                IsAccessible = false
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =627489,
                Latitude =65.87,
                Longitude =94.65,
                NameStation ="yehoshua bin nun",
                IsAccessible = false
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =128373,
                Latitude =73.65,
                Longitude =93.23,
                NameStation ="ehood ben gera",
                IsAccessible = false
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =736940,
                Latitude =82.59,
                Longitude =93.65,
                NameStation ="otniel ben kenaz",
                IsAccessible = false
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =982822,
                Latitude =48.65,
                Longitude =18,
                NameStation ="shamgar ben anat",
                IsAccessible = false
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =267788,
                Latitude =73.44,
                Longitude =83.45,
                NameStation ="devora hanevia",
                IsAccessible = false
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =456666,
                Latitude =34,
                Longitude =45,
                NameStation ="gidon ben yoash",
                IsAccessible = false
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =398475,
                Latitude =93.34,
                Longitude =39.30,
                NameStation ="yair hagiladi",
                IsAccessible = false
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =384444,
                Latitude =83.85,
                Longitude =94.55,
                NameStation ="yiftach hagiladu",
                IsAccessible = false
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =387566,
                Latitude =85.55,
                Longitude =49.65,
                NameStation ="ivzan mibeit lechem",
                IsAccessible = false
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =383744,
                Latitude =93.55,
                Longitude =94.65,
                NameStation ="eilon hazvulony",
                IsAccessible = false
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =383838,
                Latitude =83.33,
                Longitude =54.66,
                NameStation ="avdon ben hilel",
                IsAccessible = false
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =628493,
                Latitude =93.33,
                Longitude =33.33,
                NameStation ="shimshon hagibor",
                IsAccessible = false
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =942938,
                Latitude =44.44,
                Longitude =55.55,
                NameStation ="eli hacohen",
                IsAccessible = false
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =938477,
                Latitude =99.33,
                Longitude =77.66,
                NameStation ="noach",
                IsAccessible = false
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation = 902476,
                Latitude = 85.4,
                Longitude = 83.5,
                NameStation = "nadav veavihu",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation = 837593,
                Latitude = 39.45,
                Longitude = 98.44,
                NameStation = "elazar veitamar",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation = 649586,
                Latitude = 38.4,
                Longitude = 85.4,
                NameStation = "hosha",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation = 394859,
                Latitude = 28.33,
                Longitude = 49.44,
                NameStation = "yeshaayahu",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation = 938475,
                Latitude = 76.55,
                Longitude = 49.44,
                NameStation = "yirmiahu",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation = 935837,
                Latitude = 87.44,
                Longitude = 34.4,
                NameStation = "yechezkel",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation = 239474,
                Latitude = 73.55,
                Longitude = 96.55,
                NameStation = "yoel",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation = 324908,
                Latitude = 24.96,
                Longitude = 36.77,
                NameStation = "amos",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =235235 ,
                Latitude = 85.44,
                Longitude = 98.36,
                NameStation = "ovadya",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation = 345098,
                Latitude = 82.55,
                Longitude = 43.09,
                NameStation = "yona",
                IsAccessible = true
            });
        }
        public static void initLineStations()
        {
            //קו 1
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 123456,
                IdentifyNumber=1,
                NumStationInTheLine=1
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation =376483,
                IdentifyNumber = 1,
                NumStationInTheLine =2
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation =625783,
                IdentifyNumber = 1,
                NumStationInTheLine =3
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation =874593,
                IdentifyNumber = 1,
                NumStationInTheLine =4
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation =654733,
                IdentifyNumber = 1,
                NumStationInTheLine =5
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation =929292,
                IdentifyNumber = 1,
                NumStationInTheLine =6
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation =454545,
                IdentifyNumber = 1,
                NumStationInTheLine =7
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation =878787,
                IdentifyNumber = 1,
                NumStationInTheLine =8
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation =363636,
                IdentifyNumber = 1,
                NumStationInTheLine =9
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 111111,
                IdentifyNumber = 1,
                NumStationInTheLine =10
            });
            //קו 2
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 111222,
                IdentifyNumber = 2,
                NumStationInTheLine = 1
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 747474,
                IdentifyNumber = 2,
                NumStationInTheLine =2
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 757575,
                IdentifyNumber = 2,
                NumStationInTheLine =3
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 292929,
                IdentifyNumber = 2,
                NumStationInTheLine =4
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 919191,
                IdentifyNumber = 2,
                NumStationInTheLine =5
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation =982457 ,
                IdentifyNumber = 2,
                NumStationInTheLine =6
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 637291,
                IdentifyNumber = 2,
                NumStationInTheLine =7
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation =928384 ,
                IdentifyNumber = 2,
                NumStationInTheLine =8
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 234890,
                IdentifyNumber = 2,
                NumStationInTheLine =9
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 123123,
                IdentifyNumber = 2,
                NumStationInTheLine =10
            });
            //קו 3
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 234234,
                IdentifyNumber = 3,
                NumStationInTheLine = 1
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 627489,
                IdentifyNumber = 3,
                NumStationInTheLine =2
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 128373,
                IdentifyNumber = 3,
                NumStationInTheLine =3
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 736940,
                IdentifyNumber = 3,
                NumStationInTheLine =4
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 982822,
                IdentifyNumber = 3,
                NumStationInTheLine =5
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 267788,
                IdentifyNumber = 3,
                NumStationInTheLine =6
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 456666,
                IdentifyNumber = 3,
                NumStationInTheLine =7
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 398475,
                IdentifyNumber = 3,
                NumStationInTheLine =8
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 384444,
                IdentifyNumber = 3,
                NumStationInTheLine =9
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 144144,
                IdentifyNumber = 3,
                NumStationInTheLine =10
            });
            //קו 4
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 122122,
                IdentifyNumber = 4,
                NumStationInTheLine = 1
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation =387566 ,
                IdentifyNumber = 4,
                NumStationInTheLine =2
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 383744,
                IdentifyNumber = 4,
                NumStationInTheLine =3
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 383838,
                IdentifyNumber = 4,
                NumStationInTheLine =4
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 628493,
                IdentifyNumber = 4,
                NumStationInTheLine =5
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 942938,
                IdentifyNumber = 4,
                NumStationInTheLine =6
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 938477,
                IdentifyNumber = 4,
                NumStationInTheLine =7
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 902476,
                IdentifyNumber = 4,
                NumStationInTheLine =8
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 837593,
                IdentifyNumber = 4,
                NumStationInTheLine =9
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 344344,
                IdentifyNumber = 4,
                NumStationInTheLine =10
            });
            //קו 5
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 544544,
                IdentifyNumber = 5,
                NumStationInTheLine = 1
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 649586,
                IdentifyNumber = 5,
                NumStationInTheLine =2
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 394859,
                IdentifyNumber = 5,
                NumStationInTheLine =3
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 363636,
                IdentifyNumber = 5,
                NumStationInTheLine =4
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 454545,
                IdentifyNumber = 5,
                NumStationInTheLine =5
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 878787,
                IdentifyNumber = 5,
                NumStationInTheLine =6
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 929292,
                IdentifyNumber = 5,
                NumStationInTheLine =7
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 874593,
                IdentifyNumber = 5,
                NumStationInTheLine =8
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 654733,
                IdentifyNumber = 5,
                NumStationInTheLine =9
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 654321,
                IdentifyNumber = 5,
                NumStationInTheLine =10
            });
            //קו 6
            
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 654654,
                IdentifyNumber = 6,
                NumStationInTheLine =1
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 938475,
                IdentifyNumber = 6,
                NumStationInTheLine =2
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 935837,
                IdentifyNumber = 6,
                NumStationInTheLine =3
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 234890,
                IdentifyNumber = 6,
                NumStationInTheLine =4
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 637291,
                IdentifyNumber = 6,
                NumStationInTheLine =5
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 928384,
                IdentifyNumber = 6,
                NumStationInTheLine =6
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 982457,
                IdentifyNumber = 6,
                NumStationInTheLine =7
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 292929,
                IdentifyNumber = 6,
                NumStationInTheLine =8
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 919191,
                IdentifyNumber = 6,
                NumStationInTheLine =9
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 345999,
                IdentifyNumber = 6,
                NumStationInTheLine =10
            });
            //קו 7
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 877877,
                IdentifyNumber = 7,
                NumStationInTheLine = 1
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation =239474 ,
                IdentifyNumber = 7,
                NumStationInTheLine =2
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation =324908 ,
                IdentifyNumber = 7,
                NumStationInTheLine =3
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 387566,
                IdentifyNumber = 7,
                NumStationInTheLine =4
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 383838,
                IdentifyNumber = 7,
                NumStationInTheLine =5
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 3836744,
                IdentifyNumber = 7,
                NumStationInTheLine =6
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 942938,
                IdentifyNumber = 7,
                NumStationInTheLine =7
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 628493,
                IdentifyNumber = 7,
                NumStationInTheLine =8
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 902476,
                IdentifyNumber = 7,
                NumStationInTheLine =9
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 435435,
                IdentifyNumber = 7,
                NumStationInTheLine =10
            });
            //קו 8
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 987658,
                IdentifyNumber = 8,
                NumStationInTheLine = 1
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 111111,
                IdentifyNumber = 8,
                NumStationInTheLine =2
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 654321,
                IdentifyNumber = 8,
                NumStationInTheLine =3
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 345999,
                IdentifyNumber = 8,
                NumStationInTheLine =4
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation =654654 ,
                IdentifyNumber = 8,
                NumStationInTheLine =5
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 544544,
                IdentifyNumber = 8,
                NumStationInTheLine =6
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 877877,
                IdentifyNumber = 8,
                NumStationInTheLine =7
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 987658,
                IdentifyNumber = 8,
                NumStationInTheLine =8
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 625783,
                IdentifyNumber = 8,
                NumStationInTheLine =9
            });

            LineStations.Add(new LineStationDAO
            {
                CodeStation = 374657,
                IdentifyNumber = 8,
                NumStationInTheLine =10
            });
            //קו 9
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 756284,
                IdentifyNumber = 9,
                NumStationInTheLine =1 
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 757575,
                IdentifyNumber = 9,
                NumStationInTheLine =2
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 928384,
                IdentifyNumber = 9,
                NumStationInTheLine =3
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 747474,
                IdentifyNumber = 9,
                NumStationInTheLine =4
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 637291,
                IdentifyNumber = 9,
                NumStationInTheLine =5
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 292929,
                IdentifyNumber = 9,
                NumStationInTheLine =6
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 982457,
                IdentifyNumber = 9,
                NumStationInTheLine =7
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 919191,
                IdentifyNumber = 9,
                NumStationInTheLine =8
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 938475,
                IdentifyNumber = 9,
                NumStationInTheLine =9
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 376592,
                IdentifyNumber = 9,
                NumStationInTheLine =10
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 568377,
                IdentifyNumber = 10,
                NumStationInTheLine = 1
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 235235,
                IdentifyNumber = 10,
                NumStationInTheLine =2
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 345098,
                IdentifyNumber = 10,
                NumStationInTheLine =3
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 384444,
                IdentifyNumber = 10,
                NumStationInTheLine =4
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation =456666 ,
                IdentifyNumber = 10,
                NumStationInTheLine =5
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation =39475 ,
                IdentifyNumber = 10,
                NumStationInTheLine =6
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 267788,
                IdentifyNumber = 10,
                NumStationInTheLine =7
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 982822,
                IdentifyNumber = 10,
                NumStationInTheLine =8
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation =128373 ,
                IdentifyNumber = 10,
                NumStationInTheLine =9
            });
            LineStations.Add(new LineStationDAO
            {
                CodeStation = 473829,
                IdentifyNumber = 10,
                NumStationInTheLine =10
            });
        }
        public static void initPairConsecutiveStations()
        {
            foreach(BusStationDAO station1 in BusStations)
            {
                foreach (BusStationDAO station2 in BusStations)
                {
                    bool flag = false;//דגל שיהיה אמת אם התחנות כבר קיימות ושקר אם לא
                    foreach (PairConsecutiveStationsDAO zug in PairConsecutiveStations)//בדיקה האם זוג התתחנות הזה כבר קיים באוסף
                    {
                        if (station1.CodeStation == zug.StationNum1 && station2.CodeStation == zug.StationNum2 || station1.CodeStation == zug.StationNum2 && station2.CodeStation == zug.StationNum1)
                            flag=true; return;
                    }
                    if (flag == false)
                    {
                        int dis = rand.Next(1, 500);//הגרלת מרחק וזמן בקילומטרים ודקות
                        PairConsecutiveStations.Add(new PairConsecutiveStationsDAO
                        {
                            StationNum1 = station1.CodeStation,
                            StationNum2 = station2.CodeStation,
                            Distance = dis,
                            TimeDriving = dis//כל קילומטר הוא דקת נסיעה
                        });
                    }
                }
            }
        }


    }
}
