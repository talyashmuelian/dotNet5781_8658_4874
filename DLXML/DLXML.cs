using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Device.Location;
using DLAPI;
using DO;

namespace DL
{
    class DLXML : IDal    //internal
    {
        #region singelton
        static readonly DLXML instance = new DLXML();
        static DLXML() { }// static ctor to ensure instance init is done just before first usage
        DLXML() { } // default => private
        public static DLXML Instance { get => instance; }// The public Instance property to use
        #endregion

        #region DS XML File
        string busesPath = @"busesXml.xml"; //XElement
        string linesPath = @"linesXml.xml"; //XMLSerializer
        string stationsPath = @"stationsXml.xml"; //XMLSerializer
        string lineStationsPath = @"lineStationsXml.xml"; //XMLSerializer
        string pairStationsPath = @"pairStationsXml.xml"; //XMLSerializer
        string usersPath = @"usersXml.xml"; //XElement
        string lineTripsPath = @"lineTripsXml.xml"; //XElement
        #endregion
        //אוטובוס
        #region Bus
        public bool addBus(BusDAO bus)
        {
            XElement busesRootElem = XMLTools.LoadListFromXMLElement(busesPath);

            XElement bus1 = (from p in busesRootElem.Elements()
                             where p.Element("License").Value == bus.License
                             select p).FirstOrDefault();

            if (bus1 != null)
                throw new BusExceptionDO("license exists allready");

            XElement busElem = new XElement("BusDAO",
                                   new XElement("License", bus.License),
                                   new XElement("StartOfWork", bus.StartOfWork),
                                   new XElement("TotalKms", bus.TotalKms),
                                   new XElement("Fuel", bus.Fuel),
                                   new XElement("DateTreatLast", bus.DateTreatLast),
                                   new XElement("KmFromTreament", bus.KmFromTreament),
                                   new XElement("Status", bus.Status));

            busesRootElem.Add(busElem);

            XMLTools.SaveListToXMLElement(busesRootElem, busesPath);
            return true;
        }
        public bool updateBus(BusDAO bus)
        {
            XElement busesRootElem = XMLTools.LoadListFromXMLElement(busesPath);

            XElement bus1 = (from p in busesRootElem.Elements()
                            where p.Element("License").Value == bus.License
                            select p).FirstOrDefault();
            if (bus1 != null)
            {
                bus1.Element("License").Value = bus.License;
                bus1.Element("StartOfWork").Value = bus.StartOfWork.ToString();
                bus1.Element("TotalKms").Value = bus.TotalKms.ToString();
                bus1.Element("Fuel").Value = bus.Fuel.ToString();
                bus1.Element("DateTreatLast").Value = bus.DateTreatLast.ToString();
                bus1.Element("KmFromTreament").Value = bus.KmFromTreament.ToString();
                bus1.Element("Status").Value = bus.Status.ToString();

                XMLTools.SaveListToXMLElement(busesRootElem, busesPath);
                return true;
            }
            else
                throw new DO.BusExceptionDO("The license number " + bus.License + " not found");
        }
        public bool deleteBus(BusDAO bus)
        {
            XElement busesRootElem = XMLTools.LoadListFromXMLElement(busesPath);

            XElement bus1 = (from p in busesRootElem.Elements()
                            where p.Element("License").Value == bus.License
                             select p).FirstOrDefault();

            if (bus1 != null)
            {
                bus1.Remove();
                XMLTools.SaveListToXMLElement(busesRootElem, busesPath);
                return true;
            }
            else
                throw new BusExceptionDO("Does not exist in the system");
    }
        public IEnumerable<BusDAO> getAllBuses()
        {
            
            XElement busesRootElem = XMLTools.LoadListFromXMLElement(busesPath);

            return (from bus in busesRootElem.Elements()
                    select new BusDAO()
                    {
                        License = bus.Element("License").Value,
                        StartOfWork = DateTime.Parse(bus.Element("StartOfWork").Value),
                        TotalKms = Int32.Parse(bus.Element("TotalKms").Value),
                        Fuel = Int32.Parse(bus.Element("Fuel").Value),
                        DateTreatLast = DateTime.Parse(bus.Element("DateTreatLast").Value),
                        KmFromTreament = Int32.Parse(bus.Element("KmFromTreament").Value),
                        Status = (Status)Enum.Parse(typeof(Status), bus.Element("Status").Value),
                    }
                   );
        }
        public IEnumerable<BusDAO> getPartOfBuses(Predicate<BusDAO> BusDAOCondition)
        {
            XElement busesRootElem = XMLTools.LoadListFromXMLElement(busesPath);

            return from bus in busesRootElem.Elements()
                   let p1 = new BusDAO()
                   {
                       License = bus.Element("License").Value,
                       StartOfWork = DateTime.Parse(bus.Element("StartOfWork").Value),
                       TotalKms = Int32.Parse(bus.Element("TotalKms").Value),
                       Fuel = Int32.Parse(bus.Element("Fuel").Value),
                       DateTreatLast = DateTime.Parse(bus.Element("DateTreatLast").Value),
                       KmFromTreament = Int32.Parse(bus.Element("KmFromTreament").Value),
                       Status = (Status)Enum.Parse(typeof(Status), bus.Element("Status").Value),
                   }
                   where BusDAOCondition(p1)
                   select p1;
        }
        public BusDAO getOneObjectBusDAO(string license)
        {
            XElement busesRootElem = XMLTools.LoadListFromXMLElement(busesPath);

            BusDAO p = (from bus in busesRootElem.Elements()
                        where bus.Element("License").Value== license
                        select new BusDAO()
                        {
                            License = bus.Element("License").Value,
                            StartOfWork = DateTime.Parse(bus.Element("StartOfWork").Value),
                            TotalKms = Int32.Parse(bus.Element("TotalKms").Value),
                            Fuel = Int32.Parse(bus.Element("Fuel").Value),
                            DateTreatLast = DateTime.Parse(bus.Element("DateTreatLast").Value),
                            KmFromTreament = Int32.Parse(bus.Element("KmFromTreament").Value),
                            Status = (Status)Enum.Parse(typeof(Status), bus.Element("Status").Value),
                        }
                        ).FirstOrDefault();
            if (p == null)
                throw new DO.BusExceptionDO("license not found");
            return p;
        }
        #endregion
        //קו אוטובוס
        #region BusLine
        public bool addBusLine(BusLineDAO busLine)
        {
            List<BusLineDAO> ListBusLines = XMLTools.LoadListFromXMLSerializer<BusLineDAO>(linesPath);
            List<LineStationDAO> ListLineStations = XMLTools.LoadListFromXMLSerializer<LineStationDAO>(lineStationsPath);
            if (ListBusLines.FirstOrDefault(s => s.IdentifyNumber == busLine.IdentifyNumber) != null)
                throw new BusLineExceptionDO("Identify-Number-Line exists allready");
            XElement dlConfig = XElement.Load(@"config.xml");
            int identifyNumber = int.Parse(dlConfig.Element("LineID").Value);
            busLine.IdentifyNumber = identifyNumber++;
            dlConfig.Element("LineID").Value = identifyNumber.ToString();
            dlConfig.Save(@"config.xml");
            //busLine.IdentifyNumber = configoration.RunNumber;
            ListBusLines.Add(busLine); //no need to Clone()
            ListLineStations.Add(new LineStationDAO { CodeStation = busLine.FirstStationNum, IdentifyNumber = busLine.IdentifyNumber, NumStationInTheLine = 1 });
            ListLineStations.Add(new LineStationDAO { CodeStation = busLine.LastStationNum, IdentifyNumber = busLine.IdentifyNumber, NumStationInTheLine = 2 });
            XMLTools.SaveListToXMLSerializer(ListBusLines, linesPath);
            XMLTools.SaveListToXMLSerializer(ListLineStations, lineStationsPath);
            return true;
        }
        public bool updateBusLine(BusLineDAO busLine)
        {
            List<BusLineDAO> ListBusLines = XMLTools.LoadListFromXMLSerializer<BusLineDAO>(linesPath);
            BusLineDAO line = ListBusLines.Find(p => p.IdentifyNumber == busLine.IdentifyNumber);
            if (line != null)
            {
                ListBusLines.Remove(line);
                ListBusLines.Add(busLine); //no nee to Clone()
            }
            else
                throw new DO.BusLineExceptionDO("The Identify-Number-Line " + busLine.IdentifyNumber + " not found");
            XMLTools.SaveListToXMLSerializer(ListBusLines, linesPath);
            return true;
        }
        public bool deleteBusLine(BusLineDAO busLine)
        {
            List<BusLineDAO> ListBusLines = XMLTools.LoadListFromXMLSerializer<BusLineDAO>(linesPath);
            BusLineDAO line = ListBusLines.Find(p => p.IdentifyNumber == busLine.IdentifyNumber);
            if (line != null)
            {
                ListBusLines.Remove(line);
            }
            else
                throw new BusLineExceptionDO("Does not exist in the system");

            XMLTools.SaveListToXMLSerializer(ListBusLines, linesPath);
            return true;
        }
        public IEnumerable<BusLineDAO> getAllBusLines()
        {
            #region try
            //List<BusLineDAO> BusLines = new List<BusLineDAO>();
            //BusLines.Add(new BusLineDAO
            //{
            //    IdentifyNumber = 1,
            //    LineNumber = 1,
            //    Area = "מרכז",
            //    FirstStationNum = 123456,
            //    LastStationNum = 111111,
            //});
            //BusLines.Add(new BusLineDAO
            //{
            //    IdentifyNumber = 2,
            //    LineNumber = 2,
            //    Area = "ירושלים",
            //    FirstStationNum = 111222,
            //    LastStationNum = 123123,
            //});
            //BusLines.Add(new BusLineDAO
            //{
            //    IdentifyNumber = 3,
            //    LineNumber = 3,
            //    Area = "דרום",
            //    FirstStationNum = 234234,
            //    LastStationNum = 144144,
            //});
            //BusLines.Add(new BusLineDAO
            //{
            //    IdentifyNumber = 4,
            //    LineNumber = 4,
            //    Area = "צפון",
            //    FirstStationNum = 122122,
            //    LastStationNum = 344344,
            //});
            //BusLines.Add(new BusLineDAO
            //{
            //    IdentifyNumber = 5,
            //    LineNumber = 5,
            //    Area = "מרכז",
            //    FirstStationNum = 544544,
            //    LastStationNum = 654321,
            //});
            //BusLines.Add(new BusLineDAO
            //{
            //    IdentifyNumber = 6,
            //    LineNumber = 6,
            //    Area = "ירושלים",
            //    FirstStationNum = 654654,
            //    LastStationNum = 345999,
            //});
            //BusLines.Add(new BusLineDAO
            //{
            //    IdentifyNumber = 7,
            //    LineNumber = 7,
            //    Area = "צפון",
            //    FirstStationNum = 877877,
            //    LastStationNum = 435435,
            //});
            //BusLines.Add(new BusLineDAO
            //{
            //    IdentifyNumber = 8,
            //    LineNumber = 8,
            //    Area = "מרכז",
            //    FirstStationNum = 987658,
            //    LastStationNum = 374657,
            //});
            //BusLines.Add(new BusLineDAO
            //{
            //    IdentifyNumber = 9,
            //    LineNumber = 9,
            //    Area = "ירושלים",
            //    FirstStationNum = 756284,
            //    LastStationNum = 376592,
            //});
            //BusLines.Add(new BusLineDAO
            //{
            //    IdentifyNumber = 10,
            //    LineNumber = 10,
            //    Area = "דרום",
            //    FirstStationNum = 568377,
            //    LastStationNum = 473829,
            //});
            //XMLTools.SaveListToXMLSerializer(BusLines, linesPath);
            #endregion
            List<BusLineDAO> ListBusLines = XMLTools.LoadListFromXMLSerializer<BusLineDAO>(linesPath);

            return from line in ListBusLines
                   select line; //no need to Clone()
        }
        public IEnumerable<BusLineDAO> getPartOfBusLines(Predicate<BusLineDAO> BusLineDAOCondition)
        {
            List<BusLineDAO> ListBusLines = XMLTools.LoadListFromXMLSerializer<BusLineDAO>(linesPath);
            IEnumerable<BusLineDAO> TempBusLineDAO = from BusLineDAO item in ListBusLines
                                                     where BusLineDAOCondition(item)
                                                     select item; //no need to Clone()
            if (TempBusLineDAO.Count() == 0)
                throw new BusLineExceptionDO("There are no bus lines in the system that meet the condition");
            return TempBusLineDAO;
        }
        public BusLineDAO getOneObjectBusLineDAO(int identifyNumber)
        {
            List<BusLineDAO> ListBusLines = XMLTools.LoadListFromXMLSerializer<BusLineDAO>(linesPath);

            DO.BusLineDAO line = ListBusLines.Find(p => p.IdentifyNumber == identifyNumber);
            if (line != null)
                return line; //no need to Clone()
            else
                throw new DO.BusLineExceptionDO("The Identify-Number-Line " + identifyNumber + " not found");
        }
        #endregion
        //BusStation תחנת אוטובוס
        #region BusStation
        public bool addBusStation(BusStationDAO station)
        {
            List<BusStationDAO> ListBusStations = XMLTools.LoadListFromXMLSerializer<BusStationDAO>(stationsPath);
            
            if (ListBusStations.FirstOrDefault(s => s.CodeStation == station.CodeStation) != null)
                throw new BusStationExceptionDO("Code Station exists allready");
            ListBusStations.Add(station); //no need to Clone()
            XMLTools.SaveListToXMLSerializer(ListBusStations, stationsPath);
            return true;
        }
        public bool updateBusStation(BusStationDAO station)
        {
            List<BusStationDAO> ListBusStations = XMLTools.LoadListFromXMLSerializer<BusStationDAO>(stationsPath);
            BusStationDAO sta = ListBusStations.Find(p => p.CodeStation == station.CodeStation);
            if (sta != null)
            {
                ListBusStations.Remove(sta);
                ListBusStations.Add(station); //no nee to Clone()
            }
            else
                throw new DO.BusStationExceptionDO("The Code Station " + station.CodeStation + " not found");
            XMLTools.SaveListToXMLSerializer(ListBusStations, stationsPath);
            return true;
        }
        public bool deleteBusStation(BusStationDAO station)
        {
            List<BusStationDAO> ListBusStations = XMLTools.LoadListFromXMLSerializer<BusStationDAO>(stationsPath);
            //XElement pairsRootElem = XMLTools.LoadListFromXMLElement(pairStationsPath);
            BusStationDAO sta = ListBusStations.Find(p => p.CodeStation == station.CodeStation);
            if (sta != null)
            {
                
                ListBusStations.Remove(sta);
                //מחיקת האובייקטים של תחנות עוקבות שקשורות לתחנה הזאת
                

                //pairsRootElem.RemoveAll(mishehu => mishehu.StationNum1 == station.CodeStation || mishehu.StationNum2 == station.CodeStation);//מוחק את כל הזוגות שקשורים לתחנה הנמחקת
            }
            else
                throw new BusStationExceptionDO("Does not exist in the system");

            XMLTools.SaveListToXMLSerializer(ListBusStations, stationsPath);
            //XMLTools.SaveListToXMLElement(pairsRootElem, pairStationsPath);
            return true;
        }
        public IEnumerable<BusStationDAO> getAllBusStations()
        {
            #region try
            //List<BusStationDAO> BusStations = new List<BusStationDAO>();
            //BusStations.Add(new BusStationDAO
            //{
            //    CodeStation = 123456,
            //    Latitude = 32.183921,
            //    Longitude = 34.917806,
            //    NameStation = "בי''ס בר לב/בן יהודה",
            //    IsAccessible = true
            //});
            //BusStations.Add(new BusStationDAO
            //{
            //    CodeStation = 111111,
            //    Latitude = 31.870034,
            //    Longitude = 34.819541,
            //    NameStation = "הרצל/צומת בילו",
            //    IsAccessible = true
            //});
            //BusStations.Add(new BusStationDAO
            //{
            //    CodeStation = 111222,
            //    Latitude = 31.984553,
            //    Longitude = 34.782828,
            //    NameStation = "הנחשול/הדייגים",
            //    IsAccessible = true
            //});
            //BusStations.Add(new BusStationDAO
            //{
            //    CodeStation = 123123,
            //    Latitude = 31.88855,
            //    Longitude = 34.790904,
            //    NameStation = "פריד/ששת הימים",
            //    IsAccessible = true
            //});
            //BusStations.Add(new BusStationDAO
            //{
            //    CodeStation = 234234,
            //    Latitude = 31.857565,
            //    Longitude = 34.824106,
            //    NameStation = "הרצל/משה שרת",
            //    IsAccessible = true
            //});
            //BusStations.Add(new BusStationDAO
            //{
            //    CodeStation = 144144,
            //    Latitude = 31.862305,
            //    Longitude = 34.821857,
            //    NameStation = "הבנים/אלי כהן",
            //    IsAccessible = true
            //});
            //BusStations.Add(new BusStationDAO
            //{
            //    CodeStation = 122122,
            //    Latitude = 31.865085,
            //    Longitude = 34.822237,
            //    NameStation = "ויצמן/הבנים",
            //    IsAccessible = true
            //});
            //BusStations.Add(new BusStationDAO
            //{
            //    CodeStation = 344344,
            //    Latitude = 31.865222,
            //    Longitude = 34.818957,
            //    NameStation = "האירוס/הכלנית",
            //    IsAccessible = true
            //});
            //BusStations.Add(new BusStationDAO
            //{
            //    CodeStation = 544544,
            //    Latitude = 31.867597,
            //    Longitude = 34.818392,
            //    NameStation = "הכלנית/הנרקיס",
            //    IsAccessible = true
            //});
            //BusStations.Add(new BusStationDAO
            //{
            //    CodeStation = 654321,
            //    Latitude = 31.86244,
            //    Longitude = 34.827023,
            //    NameStation = "אלי כהן/לוחמי הגטאות",
            //    IsAccessible = true
            //});
            //BusStations.Add(new BusStationDAO
            //{
            //    CodeStation = 654654,
            //    Latitude = 31.863501,
            //    Longitude = 34.828702,
            //    NameStation = "שבזי/שבת אחים",
            //    IsAccessible = true
            //});
            //BusStations.Add(new BusStationDAO
            //{
            //    CodeStation = 345999,
            //    Latitude = 31.865348,
            //    Longitude = 34.827102,
            //    NameStation = "שבזי/ויצמן",
            //    IsAccessible = true
            //});
            //BusStations.Add(new BusStationDAO
            //{
            //    CodeStation = 877877,
            //    Latitude = 31.977409,
            //    Longitude = 34.763896,
            //    NameStation = "חיים בר לב/שדרות יצחק רבין",
            //    IsAccessible = true
            //});
            //BusStations.Add(new BusStationDAO
            //{
            //    CodeStation = 435435,
            //    Latitude = 31.963668,
            //    Longitude = 34.836363,
            //    NameStation = "הולצמן/המדע",
            //    IsAccessible = true
            //});
            //BusStations.Add(new BusStationDAO
            //{
            //    CodeStation = 987658,
            //    Latitude = 31.963668,
            //    Longitude = 34.836363,
            //    NameStation = "מחנה צריפין/מועדון",
            //    IsAccessible = true
            //});
            //BusStations.Add(new BusStationDAO
            //{
            //    CodeStation = 374657,
            //    Latitude = 31.856115,
            //    Longitude = 34.825249,
            //    NameStation = "הרצל/גולני",
            //    IsAccessible = true
            //});
            //BusStations.Add(new BusStationDAO
            //{
            //    CodeStation = 756284,
            //    Latitude = 31.874963,
            //    Longitude = 34.81249,
            //    NameStation = "הרותם/הדגניות",
            //    IsAccessible = true
            //});
            //BusStations.Add(new BusStationDAO
            //{
            //    CodeStation = 376592,
            //    Latitude = 32.305234,
            //    Longitude = 34.948647,
            //    NameStation = "מבוא הגפן/מורד התאנה",
            //    IsAccessible = true
            //});
            //BusStations.Add(new BusStationDAO
            //{
            //    CodeStation = 568377,
            //    Latitude = 31.883019,
            //    Longitude = 34.818708,
            //    NameStation = "החבורה/דב הוז",
            //    IsAccessible = true
            //});
            //BusStations.Add(new BusStationDAO
            //{
            //    CodeStation = 473829,
            //    Latitude = 31.897286,
            //    Longitude = 34.775083,
            //    NameStation = "הגאון בן איש חי/צאלון",
            //    IsAccessible = true
            //});
            //BusStations.Add(new BusStationDAO
            //{
            //    CodeStation = 376483,
            //    Latitude = 31.883941,
            //    Longitude = 34.807039,
            //    NameStation = "עוקשי/לוי אשכול",
            //    IsAccessible = true
            //});
            //BusStations.Add(new BusStationDAO
            //{
            //    CodeStation = 625783,
            //    Latitude = 32.805581,
            //    Longitude = 34.997928,
            //    NameStation = "ארלוזורוב/הפועל",
            //    IsAccessible = true
            //});
            //BusStations.Add(new BusStationDAO
            //{
            //    CodeStation = 874593,
            //    Latitude = 32.299994,
            //    Longitude = 34.878765,
            //    NameStation = "דרך הפארק/הרב נריה",
            //    IsAccessible = true
            //});
            //BusStations.Add(new BusStationDAO
            //{
            //    CodeStation = 654733,
            //    Latitude = 31.865457,
            //    Longitude = 34.859437,
            //    NameStation = "התאנה/הגפן",
            //    IsAccessible = true
            //});
            //BusStations.Add(new BusStationDAO
            //{
            //    CodeStation = 929292,
            //    Latitude = 31.809325,
            //    Longitude = 34.784347,
            //    NameStation = "דרך הפרחים/יסמין",
            //    IsAccessible = true
            //});
            //BusStations.Add(new BusStationDAO
            //{
            //    CodeStation = 454545,
            //    Latitude = 31.80037,
            //    Longitude = 34.778239,
            //    NameStation = "יצחק רבין/פנחס ספיר",
            //    IsAccessible = false
            //});
            //BusStations.Add(new BusStationDAO
            //{
            //    CodeStation = 878787,
            //    Latitude = 31.800334,
            //    Longitude = 34.785069,
            //    NameStation = "חיים הרצוג/דולב",
            //    IsAccessible = false
            //});
            //BusStations.Add(new BusStationDAO
            //{
            //    CodeStation = 363636,
            //    Latitude = 31.802319,
            //    Longitude = 34.786735,
            //    NameStation = "בית ספר גוונים/ארז",
            //    IsAccessible = false
            //});
            //BusStations.Add(new BusStationDAO
            //{
            //    CodeStation = 747474,
            //    Latitude = 31.805041,
            //    Longitude = 34.785098,
            //    NameStation = "דרך האילנות/מנחם בגין",
            //    IsAccessible = false
            //});
            //BusStations.Add(new BusStationDAO
            //{
            //    CodeStation = 757575,
            //    Latitude = 31.801182,
            //    Longitude = 34.787199,
            //    NameStation = "צאלה/אלמוג",
            //    IsAccessible = false
            //});
            //BusStations.Add(new BusStationDAO
            //{
            //    CodeStation = 292929,
            //    Latitude = 31.806959,
            //    Longitude = 34.773504,
            //    NameStation = "בן גוריון/פוקס",
            //    IsAccessible = false
            //});
            //BusStations.Add(new BusStationDAO
            //{
            //    CodeStation = 919191,
            //    Latitude = 31.884187,
            //    Longitude = 34.805494,
            //    NameStation = "לוי אשכול/הרב דוד ישראל",
            //    IsAccessible = false
            //});
            //BusStations.Add(new BusStationDAO
            //{
            //    CodeStation = 982457,
            //    Latitude = 31.982177,
            //    Longitude = 34.789445,
            //    NameStation = "אנילביץ'/שלום אש",
            //    IsAccessible = false
            //});
            //BusStations.Add(new BusStationDAO
            //{
            //    CodeStation = 637291,
            //    Latitude = 31.967732,
            //    Longitude = 34.816339,
            //    NameStation = "יהודה הלוי/יוחנן הסנדלר",
            //    IsAccessible = false
            //});
            //BusStations.Add(new BusStationDAO
            //{
            //    CodeStation = 928384,
            //    Latitude = 31.893823,
            //    Longitude = 34.824617,
            //    NameStation = "ההגנה/חי''ש",
            //    IsAccessible = false
            //});
            //BusStations.Add(new BusStationDAO
            //{
            //    CodeStation = 234890,
            //    Latitude = 32.026119,
            //    Longitude = 34.743063,
            //    NameStation = "הרצל/רוטשילד",
            //    IsAccessible = false
            //});
            //BusStations.Add(new BusStationDAO
            //{
            //    CodeStation = 627489,
            //    Latitude = 32.104003,
            //    Longitude = 34.827875,
            //    NameStation = "לח''י/מבצע קדש",
            //    IsAccessible = false
            //});
            //BusStations.Add(new BusStationDAO
            //{
            //    CodeStation = 128373,
            //    Latitude = 32.760078,
            //    Longitude = 35.047209,
            //    NameStation = "הרקפות/קיבוץ גלויות",
            //    IsAccessible = false
            //});
            //BusStations.Add(new BusStationDAO
            //{
            //    CodeStation = 736940,
            //    Latitude = 32.85079,
            //    Longitude = 35.090609,
            //    NameStation = "צה''ל/ עופרה חזה",
            //    IsAccessible = false
            //});
            //BusStations.Add(new BusStationDAO
            //{
            //    CodeStation = 982822,
            //    Latitude = 32.84852,
            //    Longitude = 35.088374,
            //    NameStation = "עופרה חזה/עוזי חיטמן",
            //    IsAccessible = false
            //});
            //BusStations.Add(new BusStationDAO
            //{
            //    CodeStation = 267788,
            //    Latitude = 33.209293,
            //    Longitude = 35.567504,
            //    NameStation = "הרצל/טשרניחובסקי",
            //    IsAccessible = false
            //});
            //BusStations.Add(new BusStationDAO
            //{
            //    CodeStation = 456666,
            //    Latitude = 33.215482,
            //    Longitude = 35.566929,
            //    NameStation = "האצ''ל/נוף החרמון",
            //    IsAccessible = false
            //});
            //BusStations.Add(new BusStationDAO
            //{
            //    CodeStation = 398475,
            //    Latitude = 31.770427,
            //    Longitude = 34.639009,
            //    NameStation = "אפרסק/מנגו",
            //    IsAccessible = false
            //});
            //BusStations.Add(new BusStationDAO
            //{
            //    CodeStation = 384444,
            //    Latitude = 32.521606,
            //    Longitude = 34.918544,
            //    NameStation = "הכרמל/הר המוריה",
            //    IsAccessible = false
            //});
            //BusStations.Add(new BusStationDAO
            //{
            //    CodeStation = 387566,
            //    Latitude = 32.490773,
            //    Longitude = 35.48975,
            //    NameStation = "משה שרת/גולדה מאיר",
            //    IsAccessible = false
            //});
            //BusStations.Add(new BusStationDAO
            //{
            //    CodeStation = 383744,
            //    Latitude = 32.647827,
            //    Longitude = 35.209895,
            //    NameStation = "התעשייה/מכונה",
            //    IsAccessible = false
            //});
            //BusStations.Add(new BusStationDAO
            //{
            //    CodeStation = 383838,
            //    Latitude = 32.164439,
            //    Longitude = 34.819187,
            //    NameStation = "משה דיין/דקל",
            //    IsAccessible = false
            //});
            //BusStations.Add(new BusStationDAO
            //{
            //    CodeStation = 628493,
            //    Latitude = 31.542458,
            //    Longitude = 35.119347,
            //    NameStation = "הרצי''ה קוק/יוני נתניהו",
            //    IsAccessible = false
            //});
            //BusStations.Add(new BusStationDAO
            //{
            //    CodeStation = 942938,
            //    Latitude = 32.092849,
            //    Longitude = 34.844702,
            //    NameStation = "מחלף גהה",
            //    IsAccessible = false
            //});
            //BusStations.Add(new BusStationDAO
            //{
            //    CodeStation = 938477,
            //    Latitude = 31.933927,
            //    Longitude = 34.878348,
            //    NameStation = "לוחמי בית''ר/אלמוג",
            //    IsAccessible = false
            //});
            //BusStations.Add(new BusStationDAO
            //{
            //    CodeStation = 902476,
            //    Latitude = 33.211723,
            //    Longitude = 35.567089,
            //    NameStation = "הרצל/ששת הימים",
            //    IsAccessible = true
            //});
            //BusStations.Add(new BusStationDAO
            //{
            //    CodeStation = 837593,
            //    Latitude = 31.811081,
            //    Longitude = 34.802492,
            //    NameStation = "הזית/החרצית",
            //    IsAccessible = true
            //});
            //BusStations.Add(new BusStationDAO
            //{
            //    CodeStation = 649586,
            //    Latitude = 31.424528,
            //    Longitude = 34.892056,
            //    NameStation = "מסעף שומריה",
            //    IsAccessible = true
            //});
            //BusStations.Add(new BusStationDAO
            //{
            //    CodeStation = 394859,
            //    Latitude = 31.865655,
            //    Longitude = 35.152186,
            //    NameStation = "אגן האיילות/היעל",
            //    IsAccessible = true
            //});
            //BusStations.Add(new BusStationDAO
            //{
            //    CodeStation = 938475,
            //    Latitude = 32.801374,
            //    Longitude = 35.008053,
            //    NameStation = "רזיאל/יעקב פת",
            //    IsAccessible = true
            //});
            //BusStations.Add(new BusStationDAO
            //{
            //    CodeStation = 935837,
            //    Latitude = 31.533771,
            //    Longitude = 34.58592,
            //    NameStation = "יחזקאל/משה רבנו",
            //    IsAccessible = true
            //});
            //BusStations.Add(new BusStationDAO
            //{
            //    CodeStation = 239474,
            //    Latitude = 31.69663,
            //    Longitude = 34.694836,
            //    NameStation = "משואות יצחק/כניסה",
            //    IsAccessible = true
            //});
            //BusStations.Add(new BusStationDAO
            //{
            //    CodeStation = 324908,
            //    Latitude = 31.301844,
            //    Longitude = 34.618134,
            //    NameStation = "דובדבן/צאלון",
            //    IsAccessible = true
            //});
            //BusStations.Add(new BusStationDAO
            //{
            //    CodeStation = 235235,
            //    Latitude = 31.417584,
            //    Longitude = 34.566963,
            //    NameStation = "נווה דקלים",
            //    IsAccessible = true
            //});
            //BusStations.Add(new BusStationDAO
            //{
            //    CodeStation = 345098,
            //    Latitude = 31.627305,
            //    Longitude = 34.603696,
            //    NameStation = "דרך הכפר/דרך הגיא",
            //    IsAccessible = true
            //});
            //XMLTools.SaveListToXMLSerializer(BusStations, stationsPath);
            #endregion
            List<BusStationDAO> ListBusStations = XMLTools.LoadListFromXMLSerializer<BusStationDAO>(stationsPath);
            return from station in ListBusStations
                   select station; //no need to Clone()
        }
        public IEnumerable<BusStationDAO> getPartOfBusStations(Predicate<BusStationDAO> BusStationDAOCondition)
        {
            List<BusStationDAO> ListBusStations = XMLTools.LoadListFromXMLSerializer<BusStationDAO>(stationsPath);
            IEnumerable<BusStationDAO> TempBusStationDAO = from BusStationDAO item in ListBusStations
                                                           where BusStationDAOCondition(item)
                                                           select item;//no need to Clone()
            if (TempBusStationDAO.Count() == 0)
                throw new BusStationExceptionDO("There are no stations in the system that meet the condition");
            return TempBusStationDAO;
        }
        public BusStationDAO getOneObjectBusStationDAO(int codeStation)
        {
            List<BusStationDAO> ListBusStations = XMLTools.LoadListFromXMLSerializer<BusStationDAO>(stationsPath);

            DO.BusStationDAO sta = ListBusStations.Find(p => p.CodeStation == codeStation);
            if (sta != null)
                return sta; //no need to Clone()
            else
                throw new DO.BusStationExceptionDO("The Code Station " + codeStation + " not found");
        }
        #endregion
        //lineStations
        #region LineStation
        public bool addLineStation(LineStationDAO station)
        {
            List<LineStationDAO> ListLineStations = XMLTools.LoadListFromXMLSerializer<LineStationDAO>(lineStationsPath);
            if (ListLineStations.FirstOrDefault(mishehu => mishehu.IdentifyNumber == station.IdentifyNumber && mishehu.CodeStation == station.CodeStation) != null)
                throw new LineStationExceptionDO("The station already exists on the line");
            ListLineStations.Add(station); //no need to Clone()
            XMLTools.SaveListToXMLSerializer(ListLineStations, lineStationsPath);
            return true;
        }
        public bool updateLineStation(LineStationDAO station)
        {
            List<LineStationDAO> ListLineStations = XMLTools.LoadListFromXMLSerializer<LineStationDAO>(lineStationsPath);
            LineStationDAO sta = ListLineStations.Find(mishehu => mishehu.IdentifyNumber == station.IdentifyNumber && mishehu.CodeStation == station.CodeStation);
            if (sta != null)
            {
                ListLineStations.Remove(sta);
                ListLineStations.Add(station); //no nee to Clone()
            }
            else
                throw new DO.LineStationExceptionDO("The Station number " + station.CodeStation + " not found in the line " + station.IdentifyNumber);
            XMLTools.SaveListToXMLSerializer(ListLineStations, lineStationsPath);
            return true;
        }
        public void deleteLineStation(LineStationDAO station)
        {
            List<LineStationDAO> ListLineStations = XMLTools.LoadListFromXMLSerializer<LineStationDAO>(lineStationsPath);
            LineStationDAO sta = ListLineStations.Find(item => item.IdentifyNumber == station.IdentifyNumber && item.CodeStation == station.CodeStation);
            if (sta != null)
            {
                ListLineStations.Remove(sta);
            }
            else
                throw new LineStationExceptionDO("Does not exist in the system");
            XMLTools.SaveListToXMLSerializer(ListLineStations, lineStationsPath);
        }
        public IEnumerable<LineStationDAO> getAllLineStations()
        {
            #region try
            //List<LineStationDAO> LineStations = new List<LineStationDAO>();
            ////קו 1
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 123456,
            //    IdentifyNumber = 1,
            //    NumStationInTheLine = 1
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 376483,
            //    IdentifyNumber = 1,
            //    NumStationInTheLine = 2
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 625783,
            //    IdentifyNumber = 1,
            //    NumStationInTheLine = 3
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 874593,
            //    IdentifyNumber = 1,
            //    NumStationInTheLine = 4
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 654733,
            //    IdentifyNumber = 1,
            //    NumStationInTheLine = 5
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 929292,
            //    IdentifyNumber = 1,
            //    NumStationInTheLine = 6
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 454545,
            //    IdentifyNumber = 1,
            //    NumStationInTheLine = 7
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 878787,
            //    IdentifyNumber = 1,
            //    NumStationInTheLine = 8
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 363636,
            //    IdentifyNumber = 1,
            //    NumStationInTheLine = 9
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 111111,
            //    IdentifyNumber = 1,
            //    NumStationInTheLine = 10
            //});
            ////קו 2
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 111222,
            //    IdentifyNumber = 2,
            //    NumStationInTheLine = 1
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 747474,
            //    IdentifyNumber = 2,
            //    NumStationInTheLine = 2
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 757575,
            //    IdentifyNumber = 2,
            //    NumStationInTheLine = 3
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 292929,
            //    IdentifyNumber = 2,
            //    NumStationInTheLine = 4
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 919191,
            //    IdentifyNumber = 2,
            //    NumStationInTheLine = 5
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 982457,
            //    IdentifyNumber = 2,
            //    NumStationInTheLine = 6
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 637291,
            //    IdentifyNumber = 2,
            //    NumStationInTheLine = 7
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 928384,
            //    IdentifyNumber = 2,
            //    NumStationInTheLine = 8
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 234890,
            //    IdentifyNumber = 2,
            //    NumStationInTheLine = 9
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 123123,
            //    IdentifyNumber = 2,
            //    NumStationInTheLine = 10
            //});
            ////קו 3
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 234234,
            //    IdentifyNumber = 3,
            //    NumStationInTheLine = 1
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 627489,
            //    IdentifyNumber = 3,
            //    NumStationInTheLine = 2
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 128373,
            //    IdentifyNumber = 3,
            //    NumStationInTheLine = 3
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 736940,
            //    IdentifyNumber = 3,
            //    NumStationInTheLine = 4
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 982822,
            //    IdentifyNumber = 3,
            //    NumStationInTheLine = 5
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 267788,
            //    IdentifyNumber = 3,
            //    NumStationInTheLine = 6
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 456666,
            //    IdentifyNumber = 3,
            //    NumStationInTheLine = 7
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 398475,
            //    IdentifyNumber = 3,
            //    NumStationInTheLine = 8
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 384444,
            //    IdentifyNumber = 3,
            //    NumStationInTheLine = 9
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 144144,
            //    IdentifyNumber = 3,
            //    NumStationInTheLine = 10
            //});
            ////קו 4
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 122122,
            //    IdentifyNumber = 4,
            //    NumStationInTheLine = 1
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 387566,
            //    IdentifyNumber = 4,
            //    NumStationInTheLine = 2
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 383744,
            //    IdentifyNumber = 4,
            //    NumStationInTheLine = 3
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 383838,
            //    IdentifyNumber = 4,
            //    NumStationInTheLine = 4
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 628493,
            //    IdentifyNumber = 4,
            //    NumStationInTheLine = 5
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 942938,
            //    IdentifyNumber = 4,
            //    NumStationInTheLine = 6
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 938477,
            //    IdentifyNumber = 4,
            //    NumStationInTheLine = 7
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 902476,
            //    IdentifyNumber = 4,
            //    NumStationInTheLine = 8
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 837593,
            //    IdentifyNumber = 4,
            //    NumStationInTheLine = 9
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 344344,
            //    IdentifyNumber = 4,
            //    NumStationInTheLine = 10
            //});
            ////קו 5
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 544544,
            //    IdentifyNumber = 5,
            //    NumStationInTheLine = 1
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 649586,
            //    IdentifyNumber = 5,
            //    NumStationInTheLine = 2
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 394859,
            //    IdentifyNumber = 5,
            //    NumStationInTheLine = 3
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 363636,
            //    IdentifyNumber = 5,
            //    NumStationInTheLine = 4
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 454545,
            //    IdentifyNumber = 5,
            //    NumStationInTheLine = 5
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 878787,
            //    IdentifyNumber = 5,
            //    NumStationInTheLine = 6
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 929292,
            //    IdentifyNumber = 5,
            //    NumStationInTheLine = 7
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 874593,
            //    IdentifyNumber = 5,
            //    NumStationInTheLine = 8
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 654733,
            //    IdentifyNumber = 5,
            //    NumStationInTheLine = 9
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 654321,
            //    IdentifyNumber = 5,
            //    NumStationInTheLine = 10
            //});
            ////קו 6

            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 654654,
            //    IdentifyNumber = 6,
            //    NumStationInTheLine = 1
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 938475,
            //    IdentifyNumber = 6,
            //    NumStationInTheLine = 2
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 935837,
            //    IdentifyNumber = 6,
            //    NumStationInTheLine = 3
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 234890,
            //    IdentifyNumber = 6,
            //    NumStationInTheLine = 4
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 637291,
            //    IdentifyNumber = 6,
            //    NumStationInTheLine = 5
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 928384,
            //    IdentifyNumber = 6,
            //    NumStationInTheLine = 6
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 982457,
            //    IdentifyNumber = 6,
            //    NumStationInTheLine = 7
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 292929,
            //    IdentifyNumber = 6,
            //    NumStationInTheLine = 8
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 919191,
            //    IdentifyNumber = 6,
            //    NumStationInTheLine = 9
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 345999,
            //    IdentifyNumber = 6,
            //    NumStationInTheLine = 10
            //});
            ////קו 7
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 877877,
            //    IdentifyNumber = 7,
            //    NumStationInTheLine = 1
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 239474,
            //    IdentifyNumber = 7,
            //    NumStationInTheLine = 2
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 324908,
            //    IdentifyNumber = 7,
            //    NumStationInTheLine = 3
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 387566,
            //    IdentifyNumber = 7,
            //    NumStationInTheLine = 4
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 383838,
            //    IdentifyNumber = 7,
            //    NumStationInTheLine = 5
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 383744,
            //    IdentifyNumber = 7,
            //    NumStationInTheLine = 6
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 942938,
            //    IdentifyNumber = 7,
            //    NumStationInTheLine = 7
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 628493,
            //    IdentifyNumber = 7,
            //    NumStationInTheLine = 8
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 902476,
            //    IdentifyNumber = 7,
            //    NumStationInTheLine = 9
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 435435,
            //    IdentifyNumber = 7,
            //    NumStationInTheLine = 10
            //});
            ////קו 8
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 987658,
            //    IdentifyNumber = 8,
            //    NumStationInTheLine = 1
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 111111,
            //    IdentifyNumber = 8,
            //    NumStationInTheLine = 2
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 654321,
            //    IdentifyNumber = 8,
            //    NumStationInTheLine = 3
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 345999,
            //    IdentifyNumber = 8,
            //    NumStationInTheLine = 4
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 654654,
            //    IdentifyNumber = 8,
            //    NumStationInTheLine = 5
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 544544,
            //    IdentifyNumber = 8,
            //    NumStationInTheLine = 6
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 877877,
            //    IdentifyNumber = 8,
            //    NumStationInTheLine = 7
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 363636,
            //    IdentifyNumber = 8,
            //    NumStationInTheLine = 8
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 625783,
            //    IdentifyNumber = 8,
            //    NumStationInTheLine = 9
            //});

            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 374657,
            //    IdentifyNumber = 8,
            //    NumStationInTheLine = 10
            //});
            ////קו 9
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 756284,
            //    IdentifyNumber = 9,
            //    NumStationInTheLine = 1
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 757575,
            //    IdentifyNumber = 9,
            //    NumStationInTheLine = 2
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 928384,
            //    IdentifyNumber = 9,
            //    NumStationInTheLine = 3
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 747474,
            //    IdentifyNumber = 9,
            //    NumStationInTheLine = 4
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 637291,
            //    IdentifyNumber = 9,
            //    NumStationInTheLine = 5
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 292929,
            //    IdentifyNumber = 9,
            //    NumStationInTheLine = 6
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 982457,
            //    IdentifyNumber = 9,
            //    NumStationInTheLine = 7
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 919191,
            //    IdentifyNumber = 9,
            //    NumStationInTheLine = 8
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 938475,
            //    IdentifyNumber = 9,
            //    NumStationInTheLine = 9
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 376592,
            //    IdentifyNumber = 9,
            //    NumStationInTheLine = 10
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 568377,
            //    IdentifyNumber = 10,
            //    NumStationInTheLine = 1
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 235235,
            //    IdentifyNumber = 10,
            //    NumStationInTheLine = 2
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 345098,
            //    IdentifyNumber = 10,
            //    NumStationInTheLine = 3
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 384444,
            //    IdentifyNumber = 10,
            //    NumStationInTheLine = 4
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 456666,
            //    IdentifyNumber = 10,
            //    NumStationInTheLine = 5
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 398475,
            //    IdentifyNumber = 10,
            //    NumStationInTheLine = 6
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 267788,
            //    IdentifyNumber = 10,
            //    NumStationInTheLine = 7
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 982822,
            //    IdentifyNumber = 10,
            //    NumStationInTheLine = 8
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 128373,
            //    IdentifyNumber = 10,
            //    NumStationInTheLine = 9
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 473829,
            //    IdentifyNumber = 10,
            //    NumStationInTheLine = 10
            //});
            //XMLTools.SaveListToXMLSerializer(LineStations, lineStationsPath);
            #endregion
            List<LineStationDAO> ListLineStations = XMLTools.LoadListFromXMLSerializer<LineStationDAO>(lineStationsPath);
            return from station in ListLineStations
                   select station; //no need to Clone()
        }
        public IEnumerable<LineStationDAO> getPartOfLineStations(Predicate<LineStationDAO> LineStationDAOCondition)
        {
            
            List<LineStationDAO> ListLineStations = XMLTools.LoadListFromXMLSerializer<LineStationDAO>(lineStationsPath);
            IEnumerable<LineStationDAO> TempLineStationDAO = from LineStationDAO item in ListLineStations
                                                             where LineStationDAOCondition(item)
                                                             select item;//no need to Clone()
            if (TempLineStationDAO.Count() == 0)
                throw new LineStationExceptionDO("There are no line stations that meet the condition");
            return TempLineStationDAO;
        }
        public LineStationDAO getOneObjectLineStationDAO(int identifyNumber, int codeStation)
        {
            List<LineStationDAO> ListLineStations = XMLTools.LoadListFromXMLSerializer<LineStationDAO>(lineStationsPath);

            DO.LineStationDAO sta = ListLineStations.Find(p => p.IdentifyNumber == identifyNumber && p.CodeStation == codeStation);
            if (sta != null)
                return sta; //no need to Clone()
            else
                throw new DO.LineStationExceptionDO("The Station number " + codeStation + " not found in the line " + identifyNumber);
        }
        #endregion
        //PairConsecutiveStations//זוג תחנות עוקבות
        #region PairConsecutiveStations
        public bool addPairConsecutiveStations(PairConsecutiveStationsDAO stations)
        {
            //List<PairConsecutiveStationsDAO> ListPairStations = XMLTools.LoadListFromXMLSerializer<PairConsecutiveStationsDAO>(pairStationsPath);
            //if (ListPairStations.FirstOrDefault(mishehu => mishehu.StationNum1 == stations.StationNum1 && mishehu.StationNum2 == stations.StationNum2 || mishehu.StationNum1 == stations.StationNum2 && mishehu.StationNum2 == stations.StationNum1) != null)
            //    throw new PairConsecutiveStationsExceptionDO("The pair of stations already exists");
            //ListPairStations.Add(stations);
            //XMLTools.SaveListToXMLSerializer(ListPairStations, pairStationsPath);
            //return true;
            XElement pairsRootElem = XMLTools.LoadListFromXMLElement(pairStationsPath);

            XElement pair1 = (from p in pairsRootElem.Elements()
                             where p.Element("StationNum1").Value == stations.StationNum1.ToString() && p.Element("StationNum2").Value == stations.StationNum2.ToString() || p.Element("StationNum2").Value == stations.StationNum1.ToString() && p.Element("StationNum1").Value == stations.StationNum2.ToString()
                              select p).FirstOrDefault();

            if (pair1 != null)
                throw new PairConsecutiveStationsExceptionDO("The pair of stations already exists");

            XElement pairElem = new XElement("PairConsecutiveStationsDAO",
                                   new XElement("StationNum1", stations.StationNum1),
                                   new XElement("StationNum2", stations.StationNum2),
                                   new XElement("Distance", stations.Distance),
                                   new XElement("TimeDriving", stations.TimeDriving.ToString()));

            pairsRootElem.Add(pairElem);

            XMLTools.SaveListToXMLElement(pairsRootElem, pairStationsPath);
            return true;

        }
        public bool updatePairConsecutiveStations(PairConsecutiveStationsDAO stations)
        {
            //List<PairConsecutiveStationsDAO> ListPairStations = XMLTools.LoadListFromXMLSerializer<PairConsecutiveStationsDAO>(pairStationsPath);
            //PairConsecutiveStationsDAO pair = ListPairStations.Find(mishehu => mishehu.StationNum1 == stations.StationNum1 && mishehu.StationNum2 == stations.StationNum2 || mishehu.StationNum1 == stations.StationNum2 && mishehu.StationNum2 == stations.StationNum1);
            //if (pair != null)
            //{
            //    ListPairStations.Remove(pair);
            //    ListPairStations.Add(stations); //no nee to Clone()
            //}
            //else
            //    throw new DO.PairConsecutiveStationsExceptionDO("The pair of stations does not exist in the system");
            //XMLTools.SaveListToXMLSerializer(ListPairStations, pairStationsPath);
            //return true;
            XElement pairsRootElem = XMLTools.LoadListFromXMLElement(pairStationsPath);

            XElement pair1 = (from p in pairsRootElem.Elements()
                              where p.Element("StationNum1").Value == stations.StationNum1.ToString() && p.Element("StationNum2").Value == stations.StationNum2.ToString() || p.Element("StationNum2").Value == stations.StationNum1.ToString() && p.Element("StationNum1").Value == stations.StationNum2.ToString()
                              select p).FirstOrDefault();
            if (pair1 != null)
            {
                pair1.Element("StationNum1").Value = stations.StationNum1.ToString();
                pair1.Element("StationNum2").Value = stations.StationNum2.ToString();
                pair1.Element("Distance").Value = stations.Distance.ToString();
                pair1.Element("TimeDriving").Value = stations.TimeDriving.ToString();

                XMLTools.SaveListToXMLElement(pairsRootElem, pairStationsPath);
                return true;
            }
            else
                throw new DO.PairConsecutiveStationsExceptionDO("The pair of stations does not exist in the system");
        }
        public bool deletePairConsecutiveStations(PairConsecutiveStationsDAO stations)
        {
            //List<PairConsecutiveStationsDAO> ListPairStations = XMLTools.LoadListFromXMLSerializer<PairConsecutiveStationsDAO>(pairStationsPath);
            //PairConsecutiveStationsDAO pair = ListPairStations.Find(mishehu => mishehu.StationNum1 == stations.StationNum1 && mishehu.StationNum2 == stations.StationNum2 || mishehu.StationNum1 == stations.StationNum2 && mishehu.StationNum2 == stations.StationNum1);
            //if (pair != null)
            //{
            //    ListPairStations.Remove(pair);
            //}
            //else
            //    throw new PairConsecutiveStationsExceptionDO("Does not exist in the system");
            //XMLTools.SaveListToXMLSerializer(ListPairStations, pairStationsPath);
            //return true;
            XElement pairsRootElem = XMLTools.LoadListFromXMLElement(pairStationsPath);

            XElement pair1 = (from p in pairsRootElem.Elements()
                              where p.Element("StationNum1").Value == stations.StationNum1.ToString() && p.Element("StationNum2").Value == stations.StationNum2.ToString() || p.Element("StationNum2").Value == stations.StationNum1.ToString() && p.Element("StationNum1").Value == stations.StationNum2.ToString()
                              select p).FirstOrDefault();

            if (pair1 != null)
            {
                pair1.Remove();
                XMLTools.SaveListToXMLElement(pairsRootElem, pairStationsPath);
                return true;
            }
            else
                throw new PairConsecutiveStationsExceptionDO("Does not exist in the system");
        }
        public IEnumerable<PairConsecutiveStationsDAO> getAllPairConsecutiveStations()
        {
            #region try
            //XElement a = new XElement(pairStationsPath);
            //List<LineStationDAO> LineStations = new List<LineStationDAO>();
            ////קו 1
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 123456,
            //    IdentifyNumber = 1,
            //    NumStationInTheLine = 1
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 376483,
            //    IdentifyNumber = 1,
            //    NumStationInTheLine = 2
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 625783,
            //    IdentifyNumber = 1,
            //    NumStationInTheLine = 3
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 874593,
            //    IdentifyNumber = 1,
            //    NumStationInTheLine = 4
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 654733,
            //    IdentifyNumber = 1,
            //    NumStationInTheLine = 5
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 929292,
            //    IdentifyNumber = 1,
            //    NumStationInTheLine = 6
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 454545,
            //    IdentifyNumber = 1,
            //    NumStationInTheLine = 7
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 878787,
            //    IdentifyNumber = 1,
            //    NumStationInTheLine = 8
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 363636,
            //    IdentifyNumber = 1,
            //    NumStationInTheLine = 9
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 111111,
            //    IdentifyNumber = 1,
            //    NumStationInTheLine = 10
            //});
            ////קו 2
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 111222,
            //    IdentifyNumber = 2,
            //    NumStationInTheLine = 1
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 747474,
            //    IdentifyNumber = 2,
            //    NumStationInTheLine = 2
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 757575,
            //    IdentifyNumber = 2,
            //    NumStationInTheLine = 3
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 292929,
            //    IdentifyNumber = 2,
            //    NumStationInTheLine = 4
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 919191,
            //    IdentifyNumber = 2,
            //    NumStationInTheLine = 5
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 982457,
            //    IdentifyNumber = 2,
            //    NumStationInTheLine = 6
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 637291,
            //    IdentifyNumber = 2,
            //    NumStationInTheLine = 7
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 928384,
            //    IdentifyNumber = 2,
            //    NumStationInTheLine = 8
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 234890,
            //    IdentifyNumber = 2,
            //    NumStationInTheLine = 9
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 123123,
            //    IdentifyNumber = 2,
            //    NumStationInTheLine = 10
            //});
            ////קו 3
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 234234,
            //    IdentifyNumber = 3,
            //    NumStationInTheLine = 1
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 627489,
            //    IdentifyNumber = 3,
            //    NumStationInTheLine = 2
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 128373,
            //    IdentifyNumber = 3,
            //    NumStationInTheLine = 3
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 736940,
            //    IdentifyNumber = 3,
            //    NumStationInTheLine = 4
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 982822,
            //    IdentifyNumber = 3,
            //    NumStationInTheLine = 5
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 267788,
            //    IdentifyNumber = 3,
            //    NumStationInTheLine = 6
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 456666,
            //    IdentifyNumber = 3,
            //    NumStationInTheLine = 7
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 398475,
            //    IdentifyNumber = 3,
            //    NumStationInTheLine = 8
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 384444,
            //    IdentifyNumber = 3,
            //    NumStationInTheLine = 9
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 144144,
            //    IdentifyNumber = 3,
            //    NumStationInTheLine = 10
            //});
            ////קו 4
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 122122,
            //    IdentifyNumber = 4,
            //    NumStationInTheLine = 1
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 387566,
            //    IdentifyNumber = 4,
            //    NumStationInTheLine = 2
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 383744,
            //    IdentifyNumber = 4,
            //    NumStationInTheLine = 3
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 383838,
            //    IdentifyNumber = 4,
            //    NumStationInTheLine = 4
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 628493,
            //    IdentifyNumber = 4,
            //    NumStationInTheLine = 5
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 942938,
            //    IdentifyNumber = 4,
            //    NumStationInTheLine = 6
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 938477,
            //    IdentifyNumber = 4,
            //    NumStationInTheLine = 7
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 902476,
            //    IdentifyNumber = 4,
            //    NumStationInTheLine = 8
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 837593,
            //    IdentifyNumber = 4,
            //    NumStationInTheLine = 9
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 344344,
            //    IdentifyNumber = 4,
            //    NumStationInTheLine = 10
            //});
            ////קו 5
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 544544,
            //    IdentifyNumber = 5,
            //    NumStationInTheLine = 1
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 649586,
            //    IdentifyNumber = 5,
            //    NumStationInTheLine = 2
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 394859,
            //    IdentifyNumber = 5,
            //    NumStationInTheLine = 3
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 363636,
            //    IdentifyNumber = 5,
            //    NumStationInTheLine = 4
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 454545,
            //    IdentifyNumber = 5,
            //    NumStationInTheLine = 5
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 878787,
            //    IdentifyNumber = 5,
            //    NumStationInTheLine = 6
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 929292,
            //    IdentifyNumber = 5,
            //    NumStationInTheLine = 7
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 874593,
            //    IdentifyNumber = 5,
            //    NumStationInTheLine = 8
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 654733,
            //    IdentifyNumber = 5,
            //    NumStationInTheLine = 9
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 654321,
            //    IdentifyNumber = 5,
            //    NumStationInTheLine = 10
            //});
            ////קו 6

            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 654654,
            //    IdentifyNumber = 6,
            //    NumStationInTheLine = 1
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 938475,
            //    IdentifyNumber = 6,
            //    NumStationInTheLine = 2
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 935837,
            //    IdentifyNumber = 6,
            //    NumStationInTheLine = 3
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 234890,
            //    IdentifyNumber = 6,
            //    NumStationInTheLine = 4
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 637291,
            //    IdentifyNumber = 6,
            //    NumStationInTheLine = 5
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 928384,
            //    IdentifyNumber = 6,
            //    NumStationInTheLine = 6
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 982457,
            //    IdentifyNumber = 6,
            //    NumStationInTheLine = 7
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 292929,
            //    IdentifyNumber = 6,
            //    NumStationInTheLine = 8
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 919191,
            //    IdentifyNumber = 6,
            //    NumStationInTheLine = 9
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 345999,
            //    IdentifyNumber = 6,
            //    NumStationInTheLine = 10
            //});
            ////קו 7
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 877877,
            //    IdentifyNumber = 7,
            //    NumStationInTheLine = 1
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 239474,
            //    IdentifyNumber = 7,
            //    NumStationInTheLine = 2
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 324908,
            //    IdentifyNumber = 7,
            //    NumStationInTheLine = 3
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 387566,
            //    IdentifyNumber = 7,
            //    NumStationInTheLine = 4
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 383838,
            //    IdentifyNumber = 7,
            //    NumStationInTheLine = 5
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 383744,
            //    IdentifyNumber = 7,
            //    NumStationInTheLine = 6
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 942938,
            //    IdentifyNumber = 7,
            //    NumStationInTheLine = 7
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 628493,
            //    IdentifyNumber = 7,
            //    NumStationInTheLine = 8
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 902476,
            //    IdentifyNumber = 7,
            //    NumStationInTheLine = 9
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 435435,
            //    IdentifyNumber = 7,
            //    NumStationInTheLine = 10
            //});
            ////קו 8
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 987658,
            //    IdentifyNumber = 8,
            //    NumStationInTheLine = 1
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 111111,
            //    IdentifyNumber = 8,
            //    NumStationInTheLine = 2
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 654321,
            //    IdentifyNumber = 8,
            //    NumStationInTheLine = 3
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 345999,
            //    IdentifyNumber = 8,
            //    NumStationInTheLine = 4
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 654654,
            //    IdentifyNumber = 8,
            //    NumStationInTheLine = 5
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 544544,
            //    IdentifyNumber = 8,
            //    NumStationInTheLine = 6
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 877877,
            //    IdentifyNumber = 8,
            //    NumStationInTheLine = 7
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 363636,
            //    IdentifyNumber = 8,
            //    NumStationInTheLine = 8
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 625783,
            //    IdentifyNumber = 8,
            //    NumStationInTheLine = 9
            //});

            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 374657,
            //    IdentifyNumber = 8,
            //    NumStationInTheLine = 10
            //});
            ////קו 9
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 756284,
            //    IdentifyNumber = 9,
            //    NumStationInTheLine = 1
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 757575,
            //    IdentifyNumber = 9,
            //    NumStationInTheLine = 2
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 928384,
            //    IdentifyNumber = 9,
            //    NumStationInTheLine = 3
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 747474,
            //    IdentifyNumber = 9,
            //    NumStationInTheLine = 4
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 637291,
            //    IdentifyNumber = 9,
            //    NumStationInTheLine = 5
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 292929,
            //    IdentifyNumber = 9,
            //    NumStationInTheLine = 6
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 982457,
            //    IdentifyNumber = 9,
            //    NumStationInTheLine = 7
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 919191,
            //    IdentifyNumber = 9,
            //    NumStationInTheLine = 8
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 938475,
            //    IdentifyNumber = 9,
            //    NumStationInTheLine = 9
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 376592,
            //    IdentifyNumber = 9,
            //    NumStationInTheLine = 10
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 568377,
            //    IdentifyNumber = 10,
            //    NumStationInTheLine = 1
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 235235,
            //    IdentifyNumber = 10,
            //    NumStationInTheLine = 2
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 345098,
            //    IdentifyNumber = 10,
            //    NumStationInTheLine = 3
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 384444,
            //    IdentifyNumber = 10,
            //    NumStationInTheLine = 4
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 456666,
            //    IdentifyNumber = 10,
            //    NumStationInTheLine = 5
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 398475,
            //    IdentifyNumber = 10,
            //    NumStationInTheLine = 6
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 267788,
            //    IdentifyNumber = 10,
            //    NumStationInTheLine = 7
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 982822,
            //    IdentifyNumber = 10,
            //    NumStationInTheLine = 8
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 128373,
            //    IdentifyNumber = 10,
            //    NumStationInTheLine = 9
            //});
            //LineStations.Add(new LineStationDAO
            //{
            //    CodeStation = 473829,
            //    IdentifyNumber = 10,
            //    NumStationInTheLine = 10
            //});
            //List<PairConsecutiveStationsDAO> PairConsecutiveStations = new List<PairConsecutiveStationsDAO>();
            //for (int i = 0; i < LineStations.Count() - 1; i++)
            //{
            //    var locA = new GeoCoordinate(getOneObjectBusStationDAO(LineStations[i].CodeStation).Latitude, getOneObjectBusStationDAO(LineStations[i].CodeStation).Longitude);//חישוב מיקום תחנה 1 ע"י המחלקה הייעודית
            //    var locB = new GeoCoordinate(getOneObjectBusStationDAO(LineStations[i + 1].CodeStation).Latitude, getOneObjectBusStationDAO(LineStations[i + 1].CodeStation).Longitude);//חישוב מיקום תחנה 2 ע"י המחלקה הייעודית
            //    double dis = locA.GetDistanceTo(locB) / 1000;//חישוב המרחק בין המיקומים
            //    double result = Math.Round(dis, 2);//כדי להשאיר רק שתי ספרות אחרי הנקודה העשרונית
            //                                       //int dis = rand.Next(1, 500);//הגרלת מרחק וזמן בקילומטרים ודקות
            //    PairConsecutiveStations.Add(new PairConsecutiveStationsDAO
            //    {
            //        StationNum1 = LineStations[i].CodeStation,
            //        StationNum2 = LineStations[i + 1].CodeStation,
            //        Distance = result,
            //        TimeDriving = TimeSpan.FromMinutes(((dis) / 30) * 60)
            //    });
            //}

            //foreach (var item in PairConsecutiveStations)
            //{
            //    string timeDrivingString = item.TimeDriving.Hours.ToString() + ":" + item.TimeDriving.Minutes.ToString() + ":" + item.TimeDriving.Seconds.ToString();
            //    XElement result = new XElement("PairConsecutiveStationsDAO",
            //new XElement("StationNum1", item.StationNum1),
            //new XElement("StationNum2", item.StationNum2),
            //new XElement("Distance", item.Distance),
            //new XElement("TimeDriving", timeDrivingString)
            //);
            //    a.Add(result);
            //}
            //XMLTools.SaveListToXMLElement(a, pairStationsPath);
            #endregion
            //List<PairConsecutiveStationsDAO> ListPairStations = XMLTools.LoadListFromXMLSerializer<PairConsecutiveStationsDAO>(pairStationsPath);
            //return from stations in ListPairStations
            //       select stations; //no need to Clone()
            XElement pairsRootElem = XMLTools.LoadListFromXMLElement(pairStationsPath);

            return (from pair in pairsRootElem.Elements()
                    select new PairConsecutiveStationsDAO()
                    {
                        StationNum1 = Int32.Parse(pair.Element("StationNum1").Value),
                        StationNum2 = Int32.Parse(pair.Element("StationNum2").Value),
                        Distance = Double.Parse(pair.Element("Distance").Value),
                        TimeDriving = TimeSpan.Parse(pair.Element("TimeDriving").Value),
                    }
                   );
        }
        public IEnumerable<PairConsecutiveStationsDAO> getPartOfPairConsecutiveStations(Predicate<PairConsecutiveStationsDAO> PairConsecutiveStationsDAOCondition)
        {
            //List<PairConsecutiveStationsDAO> ListPairStations = XMLTools.LoadListFromXMLSerializer<PairConsecutiveStationsDAO>(pairStationsPath);
            //IEnumerable<PairConsecutiveStationsDAO> TempPairConsecutiveStationsDAO = from PairConsecutiveStationsDAO item in ListPairStations
            //                                                                         where PairConsecutiveStationsDAOCondition(item)
            //                                                                         select item;//no need to Clone()
            //if (TempPairConsecutiveStationsDAO.Count() == 0)
            //    throw new PairConsecutiveStationsExceptionDO("There is no pair of stations that meets the condition");
            //return TempPairConsecutiveStationsDAO;
            XElement pairsRootElem = XMLTools.LoadListFromXMLElement(pairStationsPath);

            return from pair in pairsRootElem.Elements()
                   let p1 = new PairConsecutiveStationsDAO()
                   {
                       StationNum1 = Int32.Parse(pair.Element("StationNum1").Value),
                       StationNum2 = Int32.Parse(pair.Element("StationNum2").Value),
                       Distance = Double.Parse(pair.Element("Distance").Value),
                       TimeDriving = TimeSpan.Parse(pair.Element("TimeDriving").Value),
                   }
                   where PairConsecutiveStationsDAOCondition(p1)
                   select p1;
        }
        public PairConsecutiveStationsDAO getOneObjectPairConsecutiveStations(int stationNum1, int stationNum2)
        {
            //List<PairConsecutiveStationsDAO> ListPairStations = XMLTools.LoadListFromXMLSerializer<PairConsecutiveStationsDAO>(pairStationsPath);

            //DO.PairConsecutiveStationsDAO sta = ListPairStations.Find(p1 => p1.StationNum1 == stationNum1 && p1.StationNum2 == stationNum2 || p1.StationNum2 == stationNum1 && p1.StationNum1 == stationNum2);
            //if (sta != null)
            //    return sta; //no need to Clone()
            //else
            //    return null;//throw new DO.PairConsecutiveStationsExceptionDO("No object found for this pair of stations");

            XElement pairsRootElem = XMLTools.LoadListFromXMLElement(pairStationsPath);

            PairConsecutiveStationsDAO p = (from pair in pairsRootElem.Elements()
                        where pair.Element("StationNum1").Value == stationNum1.ToString() && pair.Element("StationNum2").Value == stationNum2.ToString() || pair.Element("StationNum1").Value == stationNum2.ToString() && pair.Element("StationNum2").Value == stationNum1.ToString()
                                            select new PairConsecutiveStationsDAO()
                                            {
                                                StationNum1 = Int32.Parse(pair.Element("StationNum1").Value),
                                                StationNum2 = Int32.Parse(pair.Element("StationNum2").Value),
                                                Distance = Double.Parse(pair.Element("Distance").Value),
                                                TimeDriving = TimeSpan.Parse(pair.Element("TimeDriving").Value),
                                            }
                        ).FirstOrDefault();
            if (p == null)
                return null;
            return p;
        }
        #endregion
        //users
        #region user
        public bool addUser(UserDAO user)
        {
            List<UserDAO> ListUsers = XMLTools.LoadListFromXMLSerializer<UserDAO>(usersPath);
            if (ListUsers.FirstOrDefault(mishehu => mishehu.UserName == user.UserName) != null)
                throw new UserExceptionDO("שם המשתמש כבר קיים");
            ListUsers.Add(user); //no need to Clone()
            XMLTools.SaveListToXMLSerializer(ListUsers, usersPath);
            return true;
        }
        public bool updateUser(UserDAO user)
        {
            List<UserDAO> ListUsers = XMLTools.LoadListFromXMLSerializer<UserDAO>(usersPath);
            UserDAO sta = ListUsers.Find(mishehu => mishehu.UserName == user.UserName);
            if (sta != null)
            {
                ListUsers.Remove(sta);
                ListUsers.Add(user); //no nee to Clone()
            }
            else
                throw new DO.UserExceptionDO("The UserName " + user.UserName + " not found");
            XMLTools.SaveListToXMLSerializer(ListUsers, usersPath);
            return true;
        }
        public bool deleteUser(UserDAO user)
        {
            List<UserDAO> ListUsers = XMLTools.LoadListFromXMLSerializer<UserDAO>(usersPath);
            UserDAO sta = ListUsers.Find(item => item.UserName == user.UserName);
            if (sta != null)
            {
                ListUsers.Remove(sta);
            }
            else
                throw new UserExceptionDO("Does not exist in the system");
            XMLTools.SaveListToXMLSerializer(ListUsers, usersPath);
            return true;
        }
        public IEnumerable<UserDAO> getAllUsers()
        {
            #region try
            //List<UserDAO> Users = new List<UserDAO>();
            //Users.Add(new UserDAO
            //{
            //    UserName = "טליה",
            //    PassWord = "211378658",
            //    CheckAsk = "מעלה התורה",
            //});
            //Users.Add(new UserDAO
            //{
            //    UserName = "נריה",
            //    PassWord = "211344874",
            //    CheckAsk = "רננות",
            //});
            //Users.Add(new UserDAO
            //{
            //    UserName = "שירה",
            //    PassWord = "1234567",
            //    CheckAsk = "אורות",
            //});
            //Users.Add(new UserDAO
            //{
            //    UserName = "דוד",
            //    PassWord = "7654321",
            //    CheckAsk = "מוריה",
            //});
            //XMLTools.SaveListToXMLSerializer(Users, usersPath);
            #endregion
            List<UserDAO> ListUsers = XMLTools.LoadListFromXMLSerializer<UserDAO>(usersPath);
            return from user in ListUsers
                   select user; //no need to Clone()
        }
        public IEnumerable<UserDAO> getPartOfUsers(Predicate<UserDAO> UserDAOCondition)
        {
            List<UserDAO> ListUsers = XMLTools.LoadListFromXMLSerializer<UserDAO>(usersPath);
            IEnumerable<UserDAO> TempUserDAO = from UserDAO item in ListUsers
                                               where UserDAOCondition(item)
                                               select item;
            if (TempUserDAO.Count() == 0)
                throw new UserExceptionDO("There are no users in the system that meet the condition");
            return TempUserDAO;
        }
        public UserDAO getOneObjectUserDAO(string userName)
        {
            List<UserDAO> ListUsers = XMLTools.LoadListFromXMLSerializer<UserDAO>(usersPath);
            DO.UserDAO user = ListUsers.Find(p => p.UserName == userName);
            if (user != null)
                return user; //no need to Clone()
            else
                throw new DO.UserExceptionDO("The UserName number " + userName + " not found");
        }
        #endregion
        #region lineTrip
        public bool addLineTrip(LineTripDAO lineTrip)
        {
            //List<LineTripDAO> ListLineTrips = XMLTools.LoadListFromXMLSerializer<LineTripDAO>(lineTripsPath);
            //if (ListLineTrips.FirstOrDefault(mishehu => mishehu.IdentifyNumber == lineTrip.IdentifyNumber && mishehu.TripStart == lineTrip.TripStart) != null)
            //    throw new LineTripExceptionDO("The line exit already exists");//הוצאנו חריגה במצב שהיציאת קו הסאת כבר קיימת במערכת. מצד שני, ייתכן שזה דבר תקין, צריך לחשוב
            //ListLineTrips.Add(lineTrip); //no need to Clone()
            //XMLTools.SaveListToXMLSerializer(ListLineTrips, lineTripsPath);
            //return true;
            XElement lineTripsRootElem = XMLTools.LoadListFromXMLElement(lineTripsPath);

            XElement lineTrips1 = (from p in lineTripsRootElem.Elements()
                              where p.Element("IdentifyNumber").Value == lineTrip.IdentifyNumber.ToString() && p.Element("TripStart").Value == lineTrip.TripStart.ToString()
                              select p).FirstOrDefault();

            if (lineTrips1 != null)
                throw new LineTripExceptionDO("The line exit already exists");//הוצאנו חריגה במצב שהיציאת קו הסאת כבר קיימת במערכת. מצד שני, ייתכן שזה דבר תקין, צריך לחשוב

            XElement lineTripElem = new XElement("LineTripDAO",
                                   new XElement("IdentifyNumber", lineTrip.IdentifyNumber),
                                   new XElement("TripStart", lineTrip.TripStart.ToString()));

            lineTripsRootElem.Add(lineTripElem);

            XMLTools.SaveListToXMLElement(lineTripsRootElem, lineTripsPath);
            return true;
        }
        public bool updateLineTrip(LineTripDAO lineTrip)
        {
            //List<LineTripDAO> ListLineTrips = XMLTools.LoadListFromXMLSerializer<LineTripDAO>(lineTripsPath);
            //LineTripDAO trip = ListLineTrips.Find(mishehu => mishehu.IdentifyNumber == lineTrip.IdentifyNumber && mishehu.TripStart == lineTrip.TripStart);
            //if (trip != null)
            //{
            //    ListLineTrips.Remove(trip);
            //    ListLineTrips.Add(lineTrip); //no nee to Clone()
            //}
            //else
            //    throw new DO.LineTripExceptionDO("The line exit was not found");
            //XMLTools.SaveListToXMLSerializer(ListLineTrips, lineTripsPath);
            //return true;
            XElement lineTripsRootElem = XMLTools.LoadListFromXMLElement(lineTripsPath);

            XElement lineTrips1 = (from p in lineTripsRootElem.Elements()
                                   where p.Element("IdentifyNumber").Value == lineTrip.IdentifyNumber.ToString() && p.Element("TripStart").Value == lineTrip.TripStart.ToString()
                                   select p).FirstOrDefault();
            if (lineTrips1 != null)
            {
                lineTrips1.Element("IdentifyNumber").Value = lineTrip.IdentifyNumber.ToString();
                lineTrips1.Element("TripStart").Value = lineTrip.TripStart.ToString();

                XMLTools.SaveListToXMLElement(lineTripsRootElem, lineTripsPath);
                return true;
            }
            else
                throw new DO.LineTripExceptionDO("The line exit was not found");
        }
        public bool deleteLineTrip(LineTripDAO lineTrip)
        {
            //List<LineTripDAO> ListLineTrips = XMLTools.LoadListFromXMLSerializer<LineTripDAO>(lineTripsPath);
            //LineTripDAO trip = ListLineTrips.Find(mishehu => mishehu.IdentifyNumber == lineTrip.IdentifyNumber && mishehu.TripStart == lineTrip.TripStart);
            //if (trip != null)
            //{
            //    ListLineTrips.Remove(trip);
            //}
            //else
            //    throw new DO.LineTripExceptionDO("The line exit was not found");
            //XMLTools.SaveListToXMLSerializer(ListLineTrips, lineTripsPath);
            //return true;
            XElement lineTripsRootElem = XMLTools.LoadListFromXMLElement(lineTripsPath);

            XElement lineTrips1 = (from p in lineTripsRootElem.Elements()
                                   where p.Element("IdentifyNumber").Value == lineTrip.IdentifyNumber.ToString() && p.Element("TripStart").Value == lineTrip.TripStart.ToString()
                                   select p).FirstOrDefault();

            if (lineTrips1 != null)
            {
                lineTrips1.Remove();
                XMLTools.SaveListToXMLElement(lineTripsRootElem, lineTripsPath);
                return true;
            }
            else
                throw new DO.LineTripExceptionDO("The line exit was not found");
        }
        public IEnumerable<LineTripDAO> getAllLineTrips()
        {
            #region init
            //XElement a = new XElement(lineTripsPath);
            //List<LineTripDAO> LineTrips = new List<LineTripDAO>();
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 1,
            //    TripStart = new TimeSpan(6, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 1,
            //    TripStart = new TimeSpan(7, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 1,
            //    TripStart = new TimeSpan(8, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 1,
            //    TripStart = new TimeSpan(9, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 1,
            //    TripStart = new TimeSpan(10, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 1,
            //    TripStart = new TimeSpan(11, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 1,
            //    TripStart = new TimeSpan(12, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 1,
            //    TripStart = new TimeSpan(13, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 1,
            //    TripStart = new TimeSpan(14, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 1,
            //    TripStart = new TimeSpan(15, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 1,
            //    TripStart = new TimeSpan(16, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 1,
            //    TripStart = new TimeSpan(17, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 1,
            //    TripStart = new TimeSpan(18, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 1,
            //    TripStart = new TimeSpan(19, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 1,
            //    TripStart = new TimeSpan(20, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 1,
            //    TripStart = new TimeSpan(21, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 1,
            //    TripStart = new TimeSpan(22, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 1,
            //    TripStart = new TimeSpan(23, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 1,
            //    TripStart = new TimeSpan(6, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 1,
            //    TripStart = new TimeSpan(7, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 1,
            //    TripStart = new TimeSpan(8, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 1,
            //    TripStart = new TimeSpan(9, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 1,
            //    TripStart = new TimeSpan(10, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 1,
            //    TripStart = new TimeSpan(11, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 1,
            //    TripStart = new TimeSpan(12, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 1,
            //    TripStart = new TimeSpan(13, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 1,
            //    TripStart = new TimeSpan(14, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 1,
            //    TripStart = new TimeSpan(15, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 1,
            //    TripStart = new TimeSpan(16, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 1,
            //    TripStart = new TimeSpan(17, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 1,
            //    TripStart = new TimeSpan(18, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 1,
            //    TripStart = new TimeSpan(19, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 1,
            //    TripStart = new TimeSpan(20, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 1,
            //    TripStart = new TimeSpan(21, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 1,
            //    TripStart = new TimeSpan(22, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 1,
            //    TripStart = new TimeSpan(23, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 2,
            //    TripStart = new TimeSpan(6, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 2,
            //    TripStart = new TimeSpan(7, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 2,
            //    TripStart = new TimeSpan(8, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 2,
            //    TripStart = new TimeSpan(9, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 2,
            //    TripStart = new TimeSpan(10, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 2,
            //    TripStart = new TimeSpan(11, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 2,
            //    TripStart = new TimeSpan(12, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 2,
            //    TripStart = new TimeSpan(13, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 2,
            //    TripStart = new TimeSpan(14, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 2,
            //    TripStart = new TimeSpan(15, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 2,
            //    TripStart = new TimeSpan(16, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 2,
            //    TripStart = new TimeSpan(17, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 2,
            //    TripStart = new TimeSpan(18, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 2,
            //    TripStart = new TimeSpan(19, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 2,
            //    TripStart = new TimeSpan(20, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 2,
            //    TripStart = new TimeSpan(21, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 2,
            //    TripStart = new TimeSpan(22, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 2,
            //    TripStart = new TimeSpan(23, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 2,
            //    TripStart = new TimeSpan(6, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 2,
            //    TripStart = new TimeSpan(7, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 2,
            //    TripStart = new TimeSpan(8, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 2,
            //    TripStart = new TimeSpan(9, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 2,
            //    TripStart = new TimeSpan(10, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 2,
            //    TripStart = new TimeSpan(11, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 2,
            //    TripStart = new TimeSpan(12, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 2,
            //    TripStart = new TimeSpan(13, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 2,
            //    TripStart = new TimeSpan(14, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 2,
            //    TripStart = new TimeSpan(15, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 2,
            //    TripStart = new TimeSpan(16, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 2,
            //    TripStart = new TimeSpan(17, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 2,
            //    TripStart = new TimeSpan(18, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 2,
            //    TripStart = new TimeSpan(19, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 2,
            //    TripStart = new TimeSpan(20, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 2,
            //    TripStart = new TimeSpan(21, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 2,
            //    TripStart = new TimeSpan(22, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 2,
            //    TripStart = new TimeSpan(23, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 3,
            //    TripStart = new TimeSpan(6, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 3,
            //    TripStart = new TimeSpan(7, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 3,
            //    TripStart = new TimeSpan(8, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 3,
            //    TripStart = new TimeSpan(9, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 3,
            //    TripStart = new TimeSpan(10, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 3,
            //    TripStart = new TimeSpan(11, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 3,
            //    TripStart = new TimeSpan(12, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 3,
            //    TripStart = new TimeSpan(13, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 3,
            //    TripStart = new TimeSpan(14, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 3,
            //    TripStart = new TimeSpan(15, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 3,
            //    TripStart = new TimeSpan(16, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 3,
            //    TripStart = new TimeSpan(17, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 3,
            //    TripStart = new TimeSpan(18, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 3,
            //    TripStart = new TimeSpan(19, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 3,
            //    TripStart = new TimeSpan(20, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 3,
            //    TripStart = new TimeSpan(21, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 3,
            //    TripStart = new TimeSpan(22, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 3,
            //    TripStart = new TimeSpan(23, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 3,
            //    TripStart = new TimeSpan(6, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 3,
            //    TripStart = new TimeSpan(7, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 3,
            //    TripStart = new TimeSpan(8, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 3,
            //    TripStart = new TimeSpan(9, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 3,
            //    TripStart = new TimeSpan(10, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 3,
            //    TripStart = new TimeSpan(11, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 3,
            //    TripStart = new TimeSpan(12, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 3,
            //    TripStart = new TimeSpan(13, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 3,
            //    TripStart = new TimeSpan(14, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 3,
            //    TripStart = new TimeSpan(15, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 3,
            //    TripStart = new TimeSpan(16, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 3,
            //    TripStart = new TimeSpan(17, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 3,
            //    TripStart = new TimeSpan(18, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 3,
            //    TripStart = new TimeSpan(19, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 3,
            //    TripStart = new TimeSpan(20, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 3,
            //    TripStart = new TimeSpan(21, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 3,
            //    TripStart = new TimeSpan(22, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 3,
            //    TripStart = new TimeSpan(23, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 4,
            //    TripStart = new TimeSpan(6, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 4,
            //    TripStart = new TimeSpan(7, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 4,
            //    TripStart = new TimeSpan(8, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 4,
            //    TripStart = new TimeSpan(9, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 4,
            //    TripStart = new TimeSpan(10, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 4,
            //    TripStart = new TimeSpan(11, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 4,
            //    TripStart = new TimeSpan(12, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 4,
            //    TripStart = new TimeSpan(13, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 4,
            //    TripStart = new TimeSpan(14, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 4,
            //    TripStart = new TimeSpan(15, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 4,
            //    TripStart = new TimeSpan(16, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 4,
            //    TripStart = new TimeSpan(17, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 4,
            //    TripStart = new TimeSpan(18, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 4,
            //    TripStart = new TimeSpan(19, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 4,
            //    TripStart = new TimeSpan(20, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 4,
            //    TripStart = new TimeSpan(21, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 4,
            //    TripStart = new TimeSpan(22, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 4,
            //    TripStart = new TimeSpan(23, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 4,
            //    TripStart = new TimeSpan(6, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 4,
            //    TripStart = new TimeSpan(7, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 4,
            //    TripStart = new TimeSpan(8, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 4,
            //    TripStart = new TimeSpan(9, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 4,
            //    TripStart = new TimeSpan(10, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 4,
            //    TripStart = new TimeSpan(11, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 4,
            //    TripStart = new TimeSpan(12, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 4,
            //    TripStart = new TimeSpan(13, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 4,
            //    TripStart = new TimeSpan(14, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 4,
            //    TripStart = new TimeSpan(15, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 4,
            //    TripStart = new TimeSpan(16, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 4,
            //    TripStart = new TimeSpan(17, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 4,
            //    TripStart = new TimeSpan(18, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 4,
            //    TripStart = new TimeSpan(19, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 4,
            //    TripStart = new TimeSpan(20, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 4,
            //    TripStart = new TimeSpan(21, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 4,
            //    TripStart = new TimeSpan(22, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 4,
            //    TripStart = new TimeSpan(23, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 5,
            //    TripStart = new TimeSpan(6, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 5,
            //    TripStart = new TimeSpan(7, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 5,
            //    TripStart = new TimeSpan(8, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 5,
            //    TripStart = new TimeSpan(9, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 5,
            //    TripStart = new TimeSpan(10, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 5,
            //    TripStart = new TimeSpan(11, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 5,
            //    TripStart = new TimeSpan(12, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 5,
            //    TripStart = new TimeSpan(13, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 5,
            //    TripStart = new TimeSpan(14, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 5,
            //    TripStart = new TimeSpan(15, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 5,
            //    TripStart = new TimeSpan(16, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 5,
            //    TripStart = new TimeSpan(17, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 5,
            //    TripStart = new TimeSpan(18, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 5,
            //    TripStart = new TimeSpan(19, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 5,
            //    TripStart = new TimeSpan(20, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 5,
            //    TripStart = new TimeSpan(21, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 5,
            //    TripStart = new TimeSpan(22, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 5,
            //    TripStart = new TimeSpan(23, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 5,
            //    TripStart = new TimeSpan(6, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 5,
            //    TripStart = new TimeSpan(7, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 5,
            //    TripStart = new TimeSpan(8, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 5,
            //    TripStart = new TimeSpan(9, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 5,
            //    TripStart = new TimeSpan(10, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 5,
            //    TripStart = new TimeSpan(11, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 5,
            //    TripStart = new TimeSpan(12, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 5,
            //    TripStart = new TimeSpan(13, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 5,
            //    TripStart = new TimeSpan(14, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 5,
            //    TripStart = new TimeSpan(15, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 5,
            //    TripStart = new TimeSpan(16, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 5,
            //    TripStart = new TimeSpan(17, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 5,
            //    TripStart = new TimeSpan(18, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 5,
            //    TripStart = new TimeSpan(19, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 5,
            //    TripStart = new TimeSpan(20, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 5,
            //    TripStart = new TimeSpan(21, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 5,
            //    TripStart = new TimeSpan(22, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 5,
            //    TripStart = new TimeSpan(23, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 6,
            //    TripStart = new TimeSpan(6, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 6,
            //    TripStart = new TimeSpan(7, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 6,
            //    TripStart = new TimeSpan(8, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 6,
            //    TripStart = new TimeSpan(9, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 6,
            //    TripStart = new TimeSpan(10, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 6,
            //    TripStart = new TimeSpan(11, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 6,
            //    TripStart = new TimeSpan(12, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 6,
            //    TripStart = new TimeSpan(13, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 6,
            //    TripStart = new TimeSpan(14, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 6,
            //    TripStart = new TimeSpan(15, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 6,
            //    TripStart = new TimeSpan(16, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 6,
            //    TripStart = new TimeSpan(17, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 6,
            //    TripStart = new TimeSpan(18, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 6,
            //    TripStart = new TimeSpan(19, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 6,
            //    TripStart = new TimeSpan(20, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 6,
            //    TripStart = new TimeSpan(21, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 6,
            //    TripStart = new TimeSpan(22, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 6,
            //    TripStart = new TimeSpan(23, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 6,
            //    TripStart = new TimeSpan(6, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 6,
            //    TripStart = new TimeSpan(7, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 6,
            //    TripStart = new TimeSpan(8, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 6,
            //    TripStart = new TimeSpan(9, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 6,
            //    TripStart = new TimeSpan(10, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 6,
            //    TripStart = new TimeSpan(11, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 6,
            //    TripStart = new TimeSpan(12, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 6,
            //    TripStart = new TimeSpan(13, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 6,
            //    TripStart = new TimeSpan(14, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 6,
            //    TripStart = new TimeSpan(15, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 6,
            //    TripStart = new TimeSpan(16, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 6,
            //    TripStart = new TimeSpan(17, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 6,
            //    TripStart = new TimeSpan(18, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 6,
            //    TripStart = new TimeSpan(19, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 6,
            //    TripStart = new TimeSpan(20, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 6,
            //    TripStart = new TimeSpan(21, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 6,
            //    TripStart = new TimeSpan(22, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 6,
            //    TripStart = new TimeSpan(23, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 7,
            //    TripStart = new TimeSpan(6, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 7,
            //    TripStart = new TimeSpan(7, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 7,
            //    TripStart = new TimeSpan(8, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 7,
            //    TripStart = new TimeSpan(9, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 7,
            //    TripStart = new TimeSpan(10, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 7,
            //    TripStart = new TimeSpan(11, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 7,
            //    TripStart = new TimeSpan(12, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 7,
            //    TripStart = new TimeSpan(13, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 7,
            //    TripStart = new TimeSpan(14, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 7,
            //    TripStart = new TimeSpan(15, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 7,
            //    TripStart = new TimeSpan(16, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 7,
            //    TripStart = new TimeSpan(17, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 7,
            //    TripStart = new TimeSpan(18, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 7,
            //    TripStart = new TimeSpan(19, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 7,
            //    TripStart = new TimeSpan(20, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 7,
            //    TripStart = new TimeSpan(21, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 7,
            //    TripStart = new TimeSpan(22, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 7,
            //    TripStart = new TimeSpan(23, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 7,
            //    TripStart = new TimeSpan(6, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 7,
            //    TripStart = new TimeSpan(7, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 7,
            //    TripStart = new TimeSpan(8, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 7,
            //    TripStart = new TimeSpan(9, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 7,
            //    TripStart = new TimeSpan(10, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 7,
            //    TripStart = new TimeSpan(11, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 7,
            //    TripStart = new TimeSpan(12, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 7,
            //    TripStart = new TimeSpan(13, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 7,
            //    TripStart = new TimeSpan(14, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 7,
            //    TripStart = new TimeSpan(15, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 7,
            //    TripStart = new TimeSpan(16, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 7,
            //    TripStart = new TimeSpan(17, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 7,
            //    TripStart = new TimeSpan(18, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 7,
            //    TripStart = new TimeSpan(19, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 7,
            //    TripStart = new TimeSpan(20, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 7,
            //    TripStart = new TimeSpan(21, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 7,
            //    TripStart = new TimeSpan(22, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 7,
            //    TripStart = new TimeSpan(23, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 8,
            //    TripStart = new TimeSpan(6, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 8,
            //    TripStart = new TimeSpan(7, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 8,
            //    TripStart = new TimeSpan(8, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 8,
            //    TripStart = new TimeSpan(9, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 8,
            //    TripStart = new TimeSpan(10, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 8,
            //    TripStart = new TimeSpan(11, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 8,
            //    TripStart = new TimeSpan(12, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 8,
            //    TripStart = new TimeSpan(13, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 8,
            //    TripStart = new TimeSpan(14, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 8,
            //    TripStart = new TimeSpan(15, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 8,
            //    TripStart = new TimeSpan(16, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 8,
            //    TripStart = new TimeSpan(17, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 8,
            //    TripStart = new TimeSpan(18, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 8,
            //    TripStart = new TimeSpan(19, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 8,
            //    TripStart = new TimeSpan(20, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 8,
            //    TripStart = new TimeSpan(21, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 8,
            //    TripStart = new TimeSpan(22, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 8,
            //    TripStart = new TimeSpan(23, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 8,
            //    TripStart = new TimeSpan(6, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 8,
            //    TripStart = new TimeSpan(7, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 8,
            //    TripStart = new TimeSpan(8, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 8,
            //    TripStart = new TimeSpan(9, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 8,
            //    TripStart = new TimeSpan(10, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 8,
            //    TripStart = new TimeSpan(11, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 8,
            //    TripStart = new TimeSpan(12, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 8,
            //    TripStart = new TimeSpan(13, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 8,
            //    TripStart = new TimeSpan(14, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 8,
            //    TripStart = new TimeSpan(15, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 8,
            //    TripStart = new TimeSpan(16, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 8,
            //    TripStart = new TimeSpan(17, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 8,
            //    TripStart = new TimeSpan(18, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 8,
            //    TripStart = new TimeSpan(19, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 8,
            //    TripStart = new TimeSpan(20, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 8,
            //    TripStart = new TimeSpan(21, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 8,
            //    TripStart = new TimeSpan(22, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 8,
            //    TripStart = new TimeSpan(23, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 9,
            //    TripStart = new TimeSpan(6, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 9,
            //    TripStart = new TimeSpan(7, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 9,
            //    TripStart = new TimeSpan(8, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 9,
            //    TripStart = new TimeSpan(9, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 9,
            //    TripStart = new TimeSpan(10, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 9,
            //    TripStart = new TimeSpan(11, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 9,
            //    TripStart = new TimeSpan(12, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 9,
            //    TripStart = new TimeSpan(13, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 9,
            //    TripStart = new TimeSpan(14, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 9,
            //    TripStart = new TimeSpan(15, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 9,
            //    TripStart = new TimeSpan(16, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 9,
            //    TripStart = new TimeSpan(17, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 9,
            //    TripStart = new TimeSpan(18, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 9,
            //    TripStart = new TimeSpan(19, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 9,
            //    TripStart = new TimeSpan(20, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 9,
            //    TripStart = new TimeSpan(21, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 9,
            //    TripStart = new TimeSpan(22, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 9,
            //    TripStart = new TimeSpan(23, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 9,
            //    TripStart = new TimeSpan(6, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 9,
            //    TripStart = new TimeSpan(7, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 9,
            //    TripStart = new TimeSpan(8, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 9,
            //    TripStart = new TimeSpan(9, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 9,
            //    TripStart = new TimeSpan(10, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 9,
            //    TripStart = new TimeSpan(11, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 9,
            //    TripStart = new TimeSpan(12, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 9,
            //    TripStart = new TimeSpan(13, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 9,
            //    TripStart = new TimeSpan(14, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 9,
            //    TripStart = new TimeSpan(15, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 9,
            //    TripStart = new TimeSpan(16, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 9,
            //    TripStart = new TimeSpan(17, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 9,
            //    TripStart = new TimeSpan(18, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 9,
            //    TripStart = new TimeSpan(19, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 9,
            //    TripStart = new TimeSpan(20, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 9,
            //    TripStart = new TimeSpan(21, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 9,
            //    TripStart = new TimeSpan(22, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 9,
            //    TripStart = new TimeSpan(23, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 10,
            //    TripStart = new TimeSpan(6, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 10,
            //    TripStart = new TimeSpan(7, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 10,
            //    TripStart = new TimeSpan(8, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 10,
            //    TripStart = new TimeSpan(9, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 10,
            //    TripStart = new TimeSpan(10, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 10,
            //    TripStart = new TimeSpan(11, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 10,
            //    TripStart = new TimeSpan(12, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 10,
            //    TripStart = new TimeSpan(13, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 10,
            //    TripStart = new TimeSpan(14, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 10,
            //    TripStart = new TimeSpan(15, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 10,
            //    TripStart = new TimeSpan(16, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 10,
            //    TripStart = new TimeSpan(17, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 10,
            //    TripStart = new TimeSpan(18, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 10,
            //    TripStart = new TimeSpan(19, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 10,
            //    TripStart = new TimeSpan(20, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 10,
            //    TripStart = new TimeSpan(21, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 10,
            //    TripStart = new TimeSpan(22, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 10,
            //    TripStart = new TimeSpan(23, 30, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 10,
            //    TripStart = new TimeSpan(6, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 10,
            //    TripStart = new TimeSpan(7, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 10,
            //    TripStart = new TimeSpan(8, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 10,
            //    TripStart = new TimeSpan(9, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 10,
            //    TripStart = new TimeSpan(10, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 10,
            //    TripStart = new TimeSpan(11, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 10,
            //    TripStart = new TimeSpan(12, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 10,
            //    TripStart = new TimeSpan(13, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 10,
            //    TripStart = new TimeSpan(14, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 10,
            //    TripStart = new TimeSpan(15, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 10,
            //    TripStart = new TimeSpan(16, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 10,
            //    TripStart = new TimeSpan(17, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 10,
            //    TripStart = new TimeSpan(18, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 10,
            //    TripStart = new TimeSpan(19, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 10,
            //    TripStart = new TimeSpan(20, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 10,
            //    TripStart = new TimeSpan(21, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 10,
            //    TripStart = new TimeSpan(22, 00, 0)
            //});
            //LineTrips.Add(new LineTripDAO
            //{
            //    IdentifyNumber = 10,
            //    TripStart = new TimeSpan(23, 00, 0)
            //});


            //foreach (var item in LineTrips)
            //{
            //    //string timeDrivingString = item.TimeDriving.Hours.ToString() + ":" + item.TimeDriving.Minutes.ToString() + ":" + item.TimeDriving.Seconds.ToString();
            //    XElement result = new XElement("PairConsecutiveStationsDAO",
            //new XElement("IdentifyNumber", item.IdentifyNumber),
            //new XElement("TripStart", item.TripStart.ToString())
            //);
            //    a.Add(result);
            //}
            //XMLTools.SaveListToXMLElement(a, lineTripsPath);
            #endregion
            //List<LineTripDAO> ListLineTrips = XMLTools.LoadListFromXMLSerializer<LineTripDAO>(lineTripsPath);
            //return from lineTrip in ListLineTrips
            //       select lineTrip; //no need to Clone()
            XElement lineTripsRootElem = XMLTools.LoadListFromXMLElement(lineTripsPath);

            return (from lineTrip in lineTripsRootElem.Elements()
                    select new LineTripDAO()
                    {
                        IdentifyNumber = Int32.Parse(lineTrip.Element("IdentifyNumber").Value),
                        TripStart = TimeSpan.Parse(lineTrip.Element("TripStart").Value),
                    }
                   );
        }
        public IEnumerable<LineTripDAO> getPartOfLineTrip(Predicate<LineTripDAO> LineTripDAOCondition)
        {
            //List<LineTripDAO> ListLineTrips = XMLTools.LoadListFromXMLSerializer<LineTripDAO>(lineTripsPath);
            //IEnumerable<LineTripDAO> TempLineTripDAO = from LineTripDAO item in ListLineTrips
            //                                           where LineTripDAOCondition(item)
            //                                           select item;
            //if (TempLineTripDAO.Count() == 0)
            //    throw new LineTripExceptionDO("There are no lineTrips in the system that meet the condition");
            //return TempLineTripDAO;
            XElement lineTripsRootElem = XMLTools.LoadListFromXMLElement(lineTripsPath);

            return from lineTrip in lineTripsRootElem.Elements()
                   let p1 = new LineTripDAO()
                   {
                       IdentifyNumber = Int32.Parse(lineTrip.Element("IdentifyNumber").Value),
                       TripStart = TimeSpan.Parse(lineTrip.Element("TripStart").Value),
                   }
                   where LineTripDAOCondition(p1)
                   select p1;
        }
        public LineTripDAO getOneObjectLineTripDAO(int identifyNumber, TimeSpan tripStart)
        {
            //List<LineTripDAO> ListLineTrips = XMLTools.LoadListFromXMLSerializer<LineTripDAO>(lineTripsPath);
            //DO.LineTripDAO lineTrip = ListLineTrips.Find(mishehu => mishehu.IdentifyNumber == identifyNumber && mishehu.TripStart == tripStart);
            //if (lineTrip != null)
            //    return lineTrip; //no need to Clone()
            //else
            //    throw new DO.LineTripExceptionDO("The line exit was not found");
            XElement lineTripsRootElem = XMLTools.LoadListFromXMLElement(lineTripsPath);

            LineTripDAO p = (from lineTrip in lineTripsRootElem.Elements()
                                            where lineTrip.Element("IdentifyNumber").Value == identifyNumber.ToString() && lineTrip.Element("TripStart").Value == tripStart.ToString()
                                            select new LineTripDAO()
                                            {
                                                IdentifyNumber = Int32.Parse(lineTrip.Element("IdentifyNumber").Value),
                                                TripStart = TimeSpan.Parse(lineTrip.Element("TripStart").Value),
                                            }
                        ).FirstOrDefault();
            if (p == null)
                return null;
            return p;
        }
        #endregion
    }
}
