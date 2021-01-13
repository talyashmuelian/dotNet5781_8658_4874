using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;
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
        private static List<UserDAO> users = new List<UserDAO>();
        private static List<PairConsecutiveStationsDAO> pairConsecutiveStations = new List<PairConsecutiveStationsDAO>();//אוסף כל זוגות התחנות העוקבות
        public static List<BusDAO> Buses { get => busses; }
        //public static List<BusInTravelDAO> BusesTravel { get => busestravel; }
        public static List<BusLineDAO> BusLines { get => busLines; }
        public static List<BusStationDAO> BusStations { get => busStations; }
        public static List<LineStationDAO> LineStations { get => lineStations; }
        public static List<PairConsecutiveStationsDAO> PairConsecutiveStations { get => pairConsecutiveStations; }
        public static List<UserDAO> Users { get => users; }
        static DATA()
        {
            initBuses();
            initBusLines();
            initBusStations();
            initLineStations();
            initPairConsecutiveStations();
            initUsers();
        }
        public static void initBuses()
        {
            Buses.Add(new BusDAO
            {
                License = "1234567",
                StartOfWork = new DateTime(2017, 5, 24),
                TotalKms = 5000,
                Fuel = 1200,
                DateTreatLast= DateTime.Now,
                KmFromTreament=0,
                Status = Status.READY
            });
            Buses.Add(new BusDAO
            {
                License = "1234578",
                StartOfWork = new DateTime(2017, 8, 24),
                TotalKms = 6000,
                Fuel = 1200,
                DateTreatLast = DateTime.Now,
                KmFromTreament = 0,
                Status = Status.READY
            });
            Buses.Add(new BusDAO
            {
                License = "1222567",
                StartOfWork = new DateTime(2017, 5, 30),
                TotalKms = 12000,
                Fuel = 1000,
                DateTreatLast = DateTime.Now,
                KmFromTreament = 0,
                Status = Status.READY
            });
            Buses.Add(new BusDAO
            {
                License = "4444567",
                StartOfWork = new DateTime(2010, 5, 24),
                TotalKms = 1000,
                Fuel = 500,
                DateTreatLast = DateTime.Now,
                KmFromTreament = 0,
                Status = Status.READY
            });
            Buses.Add(new BusDAO
            {
                License = "12341234",
                StartOfWork = new DateTime(2019, 5, 24),
                TotalKms = 14000,
                Fuel = 1200,
                DateTreatLast = DateTime.Now,
                KmFromTreament = 0,
                Status = Status.READY
            });
            Buses.Add(new BusDAO
            {
                License = "88888888",
                StartOfWork = new DateTime(2018, 8, 29),
                TotalKms = 13000,
                Fuel = 700,
                DateTreatLast = DateTime.Now,
                KmFromTreament = 0,
                Status = Status.READY
            });
            Buses.Add(new BusDAO
            {
                License = "33333333",
                StartOfWork = new DateTime(2021, 1, 6),
                TotalKms = 15000,
                Fuel = 1200,
                DateTreatLast = DateTime.Now,
                KmFromTreament = 0,
                Status = Status.READY
            });
            Buses.Add(new BusDAO
            {
                License = "4444444",
                StartOfWork = new DateTime(2015, 5, 24),
                TotalKms = 3000,
                Fuel = 1200,
                DateTreatLast = DateTime.Now,
                KmFromTreament = 0,
                Status = Status.READY
            });
            Buses.Add(new BusDAO
            {
                License = "1231235",
                StartOfWork = new DateTime(2006, 5, 24),
                TotalKms = 18000,
                Fuel = 1200,
                DateTreatLast = DateTime.Now,
                KmFromTreament = 0,
                Status = Status.READY
            });
            Buses.Add(new BusDAO
            {
                License = "56756788",
                StartOfWork = new DateTime(2020, 2, 24),
                TotalKms = 8000,
                Fuel = 900,
                DateTreatLast = DateTime.Now,
                KmFromTreament = 0,
                Status = Status.READY
            });
            Buses.Add(new BusDAO
            {
                License = "23423455",
                StartOfWork = new DateTime(2018, 3, 24),
                TotalKms = 9000,
                Fuel = 1200,
                DateTreatLast = DateTime.Now,
                KmFromTreament = 0,
                Status = Status.READY
            });
            Buses.Add(new BusDAO
            {
                License = "32132166",
                StartOfWork = new DateTime(2020, 10, 24),
                TotalKms = 8000,
                Fuel = 900,
                DateTreatLast = DateTime.Now,
                KmFromTreament = 0,
                Status = Status.READY
            });
            Buses.Add(new BusDAO
            {
                License = "65466777",
                StartOfWork = new DateTime(2021, 1, 1),
                TotalKms = 7000,
                Fuel = 1200,
                DateTreatLast = DateTime.Now,
                KmFromTreament = 0,
                Status = Status.READY
            });
            Buses.Add(new BusDAO
            {
                License = "44444555",
                StartOfWork = new DateTime(2018, 3, 24),
                TotalKms = 10000,
                Fuel = 800,
                DateTreatLast = DateTime.Now,
                KmFromTreament = 0,
                Status = Status.READY
            });
            Buses.Add(new BusDAO
            {
                License = "3213214",
                StartOfWork = new DateTime(2009, 5, 17),
                TotalKms = 30000,
                Fuel = 1200,
                DateTreatLast = DateTime.Now,
                KmFromTreament = 0,
                Status = Status.READY
            });
            Buses.Add(new BusDAO
            {
                License = "12332112",
                StartOfWork = new DateTime(2019, 5, 24),
                TotalKms = 45000,
                Fuel = 600,
                DateTreatLast = DateTime.Now,
                KmFromTreament = 0,
                Status = Status.READY
            });
            Buses.Add(new BusDAO
            {
                License = "9877896",
                StartOfWork = new DateTime(2016, 9, 24),
                TotalKms = 8000,
                Fuel = 1200,
                DateTreatLast = DateTime.Now,
                KmFromTreament = 0,
                Status = Status.READY
            });

            Buses.Add(new BusDAO
            {
                License = "3213333",
                StartOfWork = new DateTime(2002, 10, 24),
                TotalKms = 9999999,
                Fuel = 500,
                DateTreatLast = DateTime.Now,
                KmFromTreament = 0,
                Status = Status.READY
            });
            Buses.Add(new BusDAO
            {
                License = "77745617",
                StartOfWork = new DateTime(2018, 8, 20),
                TotalKms = 5000,
                Fuel = 1200,
                DateTreatLast = DateTime.Now,
                KmFromTreament = 0,
                Status = Status.READY
            });
            Buses.Add(new BusDAO
            {
                License = "6666666",
                StartOfWork = new DateTime(2005, 4, 24),
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
                Area="מרכז",
                FirstStationNum =123456,
                LastStationNum=111111,
            });
            BusLines.Add(new BusLineDAO
            {
                IdentifyNumber = 2,
                LineNumber = 2,
                Area ="ירושלים",
                FirstStationNum =111222,
                LastStationNum =123123,
            });
            BusLines.Add(new BusLineDAO
            {
                IdentifyNumber = 3,
                LineNumber = 3,
                Area = "דרום",
                FirstStationNum =234234,
                LastStationNum =144144,
            });
            BusLines.Add(new BusLineDAO
            {
                IdentifyNumber =4 ,
                LineNumber = 4,
                Area = "צפון",
                FirstStationNum =122122,
                LastStationNum =344344,
            });
            BusLines.Add(new BusLineDAO
            {
                IdentifyNumber =5 ,
                LineNumber = 5,
                Area = "מרכז",
                FirstStationNum =544544,
                LastStationNum =654321,
            });
            BusLines.Add(new BusLineDAO
            {
                IdentifyNumber = 6,
                LineNumber = 6,
                Area = "ירושלים",
                FirstStationNum =654654,
                LastStationNum =345999,
            });
            BusLines.Add(new BusLineDAO
            {
                IdentifyNumber = 7,
                LineNumber = 7,
                Area = "צפון",
                FirstStationNum =877877,
                LastStationNum =435435,
            });
            BusLines.Add(new BusLineDAO
            {
                IdentifyNumber =8 ,
                LineNumber =8 ,
                Area = "מרכז",
                FirstStationNum =987658,
                LastStationNum =374657,
            });
            BusLines.Add(new BusLineDAO
            {
                IdentifyNumber =9 ,
                LineNumber = 9,
                Area = "ירושלים",
                FirstStationNum =756284,
                LastStationNum =376592,
            });
            BusLines.Add(new BusLineDAO
            {
                IdentifyNumber = 10,
                LineNumber = 10,
                Area = "דרום",
                FirstStationNum =568377,
                LastStationNum =473829,
            });
        }
        public static void initBusStations()
        {
            BusStations.Add(new BusStationDAO
            {
                CodeStation =123456,
                Latitude= 32.183921,
                Longitude= 34.917806,
                NameStation= "בי''ס בר לב/בן יהודה",
                IsAccessible=true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =111111,
                Latitude = 31.870034,
                Longitude = 34.819541,
                NameStation = "הרצל/צומת בילו",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =111222,
                Latitude = 31.984553,
                Longitude = 34.782828,
                NameStation = "הנחשול/הדייגים",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =123123,
                Latitude = 31.88855,
                Longitude = 34.790904,
                NameStation = "פריד/ששת הימים",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =234234,
                Latitude = 31.857565,
                Longitude = 34.824106,
                NameStation = "הרצל/משה שרת",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =144144,
                Latitude = 31.862305,
                Longitude = 34.821857,
                NameStation = "הבנים/אלי כהן",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =122122,
                Latitude = 31.865085,
                Longitude = 34.822237,
                NameStation = "ויצמן/הבנים",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =344344,
                Latitude = 31.865222,
                Longitude = 34.818957,
                NameStation = "האירוס/הכלנית",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =544544,
                Latitude = 31.867597,
                Longitude = 34.818392,
                NameStation = "הכלנית/הנרקיס",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =654321,
                Latitude = 31.86244,
                Longitude = 34.827023,
                NameStation = "אלי כהן/לוחמי הגטאות",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =654654,
                Latitude = 31.863501,
                Longitude = 34.828702,
                NameStation = "שבזי/שבת אחים",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =345999,
                Latitude = 31.865348,
                Longitude = 34.827102,
                NameStation = "שבזי/ויצמן",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =877877,
                Latitude = 31.977409,
                Longitude = 34.763896,
                NameStation = "חיים בר לב/שדרות יצחק רבין",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =435435,
                Latitude = 31.963668,
                Longitude = 34.836363,
                NameStation = "הולצמן/המדע",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =987658,
                Latitude = 31.963668,
                Longitude = 34.836363,
                NameStation = "מחנה צריפין/מועדון",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =374657,
                Latitude = 31.856115,
                Longitude = 34.825249,
                NameStation = "הרצל/גולני",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =756284,
                Latitude = 31.874963,
                Longitude = 34.81249,
                NameStation = "הרותם/הדגניות",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =376592,
                Latitude = 32.305234,
                Longitude = 34.948647,
                NameStation = "מבוא הגפן/מורד התאנה",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =568377,
                Latitude = 31.883019,
                Longitude = 34.818708,
                NameStation = "החבורה/דב הוז",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =473829,
                Latitude = 31.897286,
                Longitude = 34.775083,
                NameStation = "הגאון בן איש חי/צאלון",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =376483,
                Latitude = 31.883941,
                Longitude = 34.807039,
                NameStation = "עוקשי/לוי אשכול",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =625783,
                Latitude = 32.805581,
                Longitude = 34.997928,
                NameStation = "ארלוזורוב/הפועל",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =874593,
                Latitude = 32.299994,
                Longitude = 34.878765,
                NameStation = "דרך הפארק/הרב נריה",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =654733,
                Latitude = 31.865457,
                Longitude = 34.859437,
                NameStation = "התאנה/הגפן",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =929292,
                Latitude = 31.809325,
                Longitude = 34.784347,
                NameStation = "דרך הפרחים/יסמין",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =454545,
                Latitude = 31.80037,
                Longitude = 34.778239,
                NameStation = "יצחק רבין/פנחס ספיר",
                IsAccessible = false
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =878787,
                Latitude = 31.800334,
                Longitude = 34.785069,
                NameStation = "חיים הרצוג/דולב",
                IsAccessible = false
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =363636,
                Latitude = 31.802319,
                Longitude = 34.786735,
                NameStation = "בית ספר גוונים/ארז",
                IsAccessible = false
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =747474,
                Latitude = 31.805041,
                Longitude = 34.785098,
                NameStation = "דרך האילנות/מנחם בגין",
                IsAccessible = false
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =757575,
                Latitude = 31.801182,
                Longitude = 34.787199,
                NameStation = "צאלה/אלמוג",
                IsAccessible = false
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =292929,
                Latitude = 31.806959,
                Longitude = 34.773504,
                NameStation = "בן גוריון/פוקס",
                IsAccessible = false
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =919191,
                Latitude = 31.884187,
                Longitude = 34.805494,
                NameStation = "לוי אשכול/הרב דוד ישראל",
                IsAccessible = false
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =982457,
                Latitude = 31.982177,
                Longitude = 34.789445,
                NameStation = "אנילביץ'/שלום אש",
                IsAccessible = false
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =637291,
                Latitude = 31.967732,
                Longitude = 34.816339,
                NameStation = "יהודה הלוי/יוחנן הסנדלר",
                IsAccessible = false
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =928384,
                Latitude = 31.893823,
                Longitude = 34.824617,
                NameStation = "ההגנה/חי''ש",
                IsAccessible = false
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =234890,
                Latitude = 32.026119,
                Longitude = 34.743063,
                NameStation = "הרצל/רוטשילד",
                IsAccessible = false
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =627489,
                Latitude = 32.104003,
                Longitude = 34.827875,
                NameStation = "לח''י/מבצע קדש",
                IsAccessible = false
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =128373,
                Latitude = 32.760078,
                Longitude = 35.047209,
                NameStation = "הרקפות/קיבוץ גלויות",
                IsAccessible = false
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =736940,
                Latitude = 32.85079,
                Longitude = 35.090609,
                NameStation = "צה''ל/ עופרה חזה",
                IsAccessible = false
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =982822,
                Latitude = 32.84852,
                Longitude = 35.088374,
                NameStation = "עופרה חזה/עוזי חיטמן",
                IsAccessible = false
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =267788,
                Latitude = 33.209293,
                Longitude = 35.567504,
                NameStation = "הרצל/טשרניחובסקי",
                IsAccessible = false
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =456666,
                Latitude = 33.215482,
                Longitude = 35.566929,
                NameStation = "האצ''ל/נוף החרמון",
                IsAccessible = false
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =398475,
                Latitude = 31.770427,
                Longitude = 34.639009,
                NameStation = "אפרסק/מנגו",
                IsAccessible = false
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =384444,
                Latitude = 32.521606,
                Longitude = 34.918544,
                NameStation = "הכרמל/הר המוריה",
                IsAccessible = false
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =387566,
                Latitude = 32.490773,
                Longitude = 35.48975,
                NameStation = "משה שרת/גולדה מאיר",
                IsAccessible = false
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =383744,
                Latitude = 32.647827,
                Longitude = 35.209895,
                NameStation = "התעשייה/מכונה",
                IsAccessible = false
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =383838,
                Latitude = 32.164439,
                Longitude = 34.819187,
                NameStation = "משה דיין/דקל",
                IsAccessible = false
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =628493,
                Latitude = 31.542458,
                Longitude = 35.119347,
                NameStation = "הרצי''ה קוק/יוני נתניהו",
                IsAccessible = false
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =942938,
                Latitude = 32.092849,
                Longitude = 34.844702,
                NameStation = "מחלף גהה",
                IsAccessible = false
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =938477,
                Latitude = 31.933927,
                Longitude = 34.878348,
                NameStation = "לוחמי בית''ר/אלמוג",
                IsAccessible = false
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation = 902476,
                Latitude = 33.211723,
                Longitude = 35.567089,
                NameStation = "הרצל/ששת הימים",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation = 837593,
                Latitude = 31.811081,
                Longitude = 34.802492,
                NameStation = "הזית/החרצית",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation = 649586,
                Latitude = 31.424528,
                Longitude = 34.892056,
                NameStation = "מסעף שומריה",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation = 394859,
                Latitude = 31.865655,
                Longitude = 35.152186,
                NameStation = "אגן האיילות/היעל",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation = 938475,
                Latitude = 32.801374,
                Longitude = 35.008053,
                NameStation = "רזיאל/יעקב פת",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation = 935837,
                Latitude = 31.533771,
                Longitude = 34.58592,
                NameStation = "יחזקאל/משה רבנו",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation = 239474,
                Latitude = 31.69663,
                Longitude = 34.694836,
                NameStation = "משואות יצחק/כניסה",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation = 324908,
                Latitude = 31.301844,
                Longitude = 34.618134,
                NameStation = "דובדבן/צאלון",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation =235235 ,
                Latitude = 31.417584,
                Longitude = 34.566963,
                NameStation = "נווה דקלים",
                IsAccessible = true
            });
            BusStations.Add(new BusStationDAO
            {
                CodeStation = 345098,
                Latitude = 31.627305,
                Longitude = 34.603696,
                NameStation = "דרך הכפר/דרך הגיא",
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
                CodeStation = 383744,
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
                CodeStation = 398475,
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
                    foreach (PairConsecutiveStationsDAO zug in PairConsecutiveStations)//בדיקה האם זוג התחנות הזה כבר קיים באוסף
                    {
                        if (station1.CodeStation == zug.StationNum1 && station2.CodeStation == zug.StationNum2 || station1.CodeStation == zug.StationNum2 && station2.CodeStation == zug.StationNum1)
                        {
                            flag = true; break;
                        }
                    }
                    if (flag == false)
                    {
                        var locA = new GeoCoordinate(station1.Latitude, station1.Longitude);
                        var locB = new GeoCoordinate(station2.Latitude, station2.Longitude);
                        double dis = locA.GetDistanceTo(locB);
                        //int dis = rand.Next(1, 500);//הגרלת מרחק וזמן בקילומטרים ודקות
                        PairConsecutiveStations.Add(new PairConsecutiveStationsDAO
                        {
                            StationNum1 = station1.CodeStation,
                            StationNum2 = station2.CodeStation,
                            Distance = dis,
                            TimeDriving = TimeSpan.FromMinutes(((dis/1000)/30)*60)
                        }) ;
                    }
                }
            }
        }
        public static void initUsers()
        {
            Users.Add(new UserDAO
            {
                UserName = "טליה",
                PassWord = "211378658",
                CheckAsk = "מעלה התורה",
            });
            Users.Add(new UserDAO
            {
                UserName = "נריה",
                PassWord = "211344874",
                CheckAsk = "רננות",
            });
            Users.Add(new UserDAO
            {
                UserName = "שירה",
                PassWord = "1234567",
                CheckAsk = "אורות",
            });
            Users.Add(new UserDAO
            {
                UserName = "דוד",
                PassWord = "7654321",
                CheckAsk = "מוריה",
            });
        }


    }
}
