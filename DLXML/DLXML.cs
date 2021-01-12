using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
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
        string usersPath = @"usersXml.xml"; //XMLSerializer
        string lineTripsPath = @"lineTripsXml.xml"; //XMLSerializer
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
            //List<BusLineDAO> ListBusLines11 = new List<BusLineDAO>();
            //ListBusLines11.Add(new BusLineDAO
            //{
            //    IdentifyNumber = 1,
            //    LineNumber = 1,
            //    Area = "מרכז",
            //    FirstStationNum = 123456,
            //    LastStationNum = 111111
            //});
            //XMLTools.SaveListToXMLSerializer(ListBusLines11, linesPath);
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
            List<PairConsecutiveStationsDAO> ListBusPairs = XMLTools.LoadListFromXMLSerializer<PairConsecutiveStationsDAO>(pairStationsPath);
            BusStationDAO sta = ListBusStations.Find(p => p.CodeStation == station.CodeStation);
            if (sta != null)
            {
                ListBusStations.Remove(sta);
                //מחיקת האובייקטים של תחנות עוקבות שקשורות לתחנה הזאת
                ListBusPairs.RemoveAll(mishehu => mishehu.StationNum1 == station.CodeStation || mishehu.StationNum2 == station.CodeStation);//מוחק את כל הזוגות שקשורים לתחנה הנמחקת
            }
            else
                throw new BusStationExceptionDO("Does not exist in the system");

            XMLTools.SaveListToXMLSerializer(ListBusStations, stationsPath);
            XMLTools.SaveListToXMLSerializer(ListBusPairs, pairStationsPath);
            return true;
        }
        public IEnumerable<BusStationDAO> getAllBusStations()
        {
            //List<BusStationDAO> ListBusStations11 = new List<BusStationDAO>();
            //ListBusStations11.Add(new BusStationDAO
            //{
            //    CodeStation = 111111,
            //    Latitude = 30.01,
            //    Longitude = 31.11,
            //    NameStation = "שומרון",
            //    IsAccessible = true
            //});
            //XMLTools.SaveListToXMLSerializer(ListBusStations11, stationsPath);
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
            List<LineStationDAO> ListLineStations = XMLTools.LoadListFromXMLSerializer<LineStationDAO>(lineStationsPath);
            return from station in ListLineStations
                   select station; //no need to Clone()
        }
        public IEnumerable<LineStationDAO> getPartOfLineStations(Predicate<LineStationDAO> LineStationDAOCondition)
        {
            //List<LineStationDAO> ListBusLines11 = new List<LineStationDAO>();
            //ListBusLines11.Add(new LineStationDAO
            //{
            //    CodeStation = 123456,
            //    IdentifyNumber = 1,
            //    NumStationInTheLine = 1
            //});
            //XMLTools.SaveListToXMLSerializer(ListBusLines11, lineStationsPath);
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
            List<PairConsecutiveStationsDAO> ListPairStations = XMLTools.LoadListFromXMLSerializer<PairConsecutiveStationsDAO>(pairStationsPath);
            if (ListPairStations.FirstOrDefault(mishehu => mishehu.StationNum1 == stations.StationNum1 && mishehu.StationNum2 == stations.StationNum2 || mishehu.StationNum1 == stations.StationNum2 && mishehu.StationNum2 == stations.StationNum1) != null)
                throw new PairConsecutiveStationsExceptionDO("The pair of stations already exists");
            ListPairStations.Add(stations);
            XMLTools.SaveListToXMLSerializer(ListPairStations, pairStationsPath);
            return true;
        }
        public bool updatePairConsecutiveStations(PairConsecutiveStationsDAO stations)
        {
            List<PairConsecutiveStationsDAO> ListPairStations = XMLTools.LoadListFromXMLSerializer<PairConsecutiveStationsDAO>(pairStationsPath);
            PairConsecutiveStationsDAO pair = ListPairStations.Find(mishehu => mishehu.StationNum1 == stations.StationNum1 && mishehu.StationNum2 == stations.StationNum2 || mishehu.StationNum1 == stations.StationNum2 && mishehu.StationNum2 == stations.StationNum1);
            if (pair != null)
            {
                ListPairStations.Remove(pair);
                ListPairStations.Add(stations); //no nee to Clone()
            }
            else
                throw new DO.PairConsecutiveStationsExceptionDO("The pair of stations does not exist in the system");
            XMLTools.SaveListToXMLSerializer(ListPairStations, pairStationsPath);
            return true;
        }
        public bool deletePairConsecutiveStations(PairConsecutiveStationsDAO stations)
        {
            List<PairConsecutiveStationsDAO> ListPairStations = XMLTools.LoadListFromXMLSerializer<PairConsecutiveStationsDAO>(pairStationsPath);
            PairConsecutiveStationsDAO pair = ListPairStations.Find(mishehu => mishehu.StationNum1 == stations.StationNum1 && mishehu.StationNum2 == stations.StationNum2 || mishehu.StationNum1 == stations.StationNum2 && mishehu.StationNum2 == stations.StationNum1);
            if (pair != null)
            {
                ListPairStations.Remove(pair);
            }
            else
                throw new PairConsecutiveStationsExceptionDO("Does not exist in the system");
            XMLTools.SaveListToXMLSerializer(ListPairStations, pairStationsPath);
            return true;
        }
        public IEnumerable<PairConsecutiveStationsDAO> getAllPairConsecutiveStations()
        {
            List<PairConsecutiveStationsDAO> ListPairStations = XMLTools.LoadListFromXMLSerializer<PairConsecutiveStationsDAO>(pairStationsPath);
            return from stations in ListPairStations
                   select stations; //no need to Clone()
        }
        public IEnumerable<PairConsecutiveStationsDAO> getPartOfPairConsecutiveStations(Predicate<PairConsecutiveStationsDAO> PairConsecutiveStationsDAOCondition)
        {
            List<PairConsecutiveStationsDAO> ListPairStations = XMLTools.LoadListFromXMLSerializer<PairConsecutiveStationsDAO>(pairStationsPath);
            IEnumerable<PairConsecutiveStationsDAO> TempPairConsecutiveStationsDAO = from PairConsecutiveStationsDAO item in ListPairStations
                                                                                     where PairConsecutiveStationsDAOCondition(item)
                                                                                     select item;//no need to Clone()
            if (TempPairConsecutiveStationsDAO.Count() == 0)
                throw new PairConsecutiveStationsExceptionDO("There is no pair of stations that meets the condition");
            return TempPairConsecutiveStationsDAO;
        }
        public PairConsecutiveStationsDAO getOneObjectPairConsecutiveStations(int stationNum1, int stationNum2)
        {
            //List<PairConsecutiveStationsDAO> ListBusLines11 = new List<PairConsecutiveStationsDAO>();
            //ListBusLines11.Add(new PairConsecutiveStationsDAO
            //{
            //    StationNum1 = 111111,
            //    StationNum2 = 123456,
            //    Distance = 23,
            //    TimeDriving = 23//כל קילומטר הוא דקת נסיעה
            //});
            //XMLTools.SaveListToXMLSerializer(ListBusLines11, pairStationsPath);
            List<PairConsecutiveStationsDAO> ListPairStations = XMLTools.LoadListFromXMLSerializer<PairConsecutiveStationsDAO>(pairStationsPath);

            DO.PairConsecutiveStationsDAO sta = ListPairStations.Find(p => p.StationNum1 == stationNum1 && p.StationNum2 == stationNum2 || p.StationNum2 == stationNum1 && p.StationNum1 == stationNum2);
            if (sta != null)
                return sta; //no need to Clone()
            else
                return null;//throw new DO.PairConsecutiveStationsExceptionDO("No object found for this pair of stations");
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
            //List<UserDAO> ListBusLines11 = new List<UserDAO>();
            //ListBusLines11.Add(new UserDAO
            //{
            //    UserName = "טליה",
            //    PassWord = "211378658",
            //    CheckAsk = "מעלה התורה",
            //});
            //XMLTools.SaveListToXMLSerializer(ListBusLines11, usersPath);
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
            List<LineTripDAO> ListLineTrips = XMLTools.LoadListFromXMLSerializer<LineTripDAO>(lineTripsPath);
            if (ListLineTrips.FirstOrDefault(mishehu => mishehu.IdentifyNumber == lineTrip.IdentifyNumber && mishehu.TripStart == lineTrip.TripStart) != null)
                throw new LineTripExceptionDO("The line exit already exists");//הוצאנו חריגה במצב שהיציאת קו הסאת כבר קיימת במערכת. מצד שני, ייתכן שזה דבר תקין, צריך לחשוב
            ListLineTrips.Add(lineTrip); //no need to Clone()
            XMLTools.SaveListToXMLSerializer(ListLineTrips, lineTripsPath);
            return true;
        }
        public bool updateLineTrip(LineTripDAO lineTrip)
        {
            List<LineTripDAO> ListLineTrips = XMLTools.LoadListFromXMLSerializer<LineTripDAO>(lineTripsPath);
            LineTripDAO trip = ListLineTrips.Find(mishehu => mishehu.IdentifyNumber == lineTrip.IdentifyNumber && mishehu.TripStart == lineTrip.TripStart);
            if (trip != null)
            {
                ListLineTrips.Remove(trip);
                ListLineTrips.Add(lineTrip); //no nee to Clone()
            }
            else
                throw new DO.LineTripExceptionDO("The line exit was not found");
            XMLTools.SaveListToXMLSerializer(ListLineTrips, lineTripsPath);
            return true;
        }
        public bool deleteLineTrip(LineTripDAO lineTrip)
        {
            List<LineTripDAO> ListLineTrips = XMLTools.LoadListFromXMLSerializer<LineTripDAO>(lineTripsPath);
            LineTripDAO trip = ListLineTrips.Find(mishehu => mishehu.IdentifyNumber == lineTrip.IdentifyNumber && mishehu.TripStart == lineTrip.TripStart);
            if (trip != null)
            {
                ListLineTrips.Remove(trip);
            }
            else
                throw new DO.LineTripExceptionDO("The line exit was not found");
            XMLTools.SaveListToXMLSerializer(ListLineTrips, lineTripsPath);
            return true;
        }
        public IEnumerable<LineTripDAO> getAllLineTrips()
        {
            List<LineTripDAO> ListLineTrips = XMLTools.LoadListFromXMLSerializer<LineTripDAO>(lineTripsPath);
            return from lineTrip in ListLineTrips
                   select lineTrip; //no need to Clone()
        }
        public IEnumerable<LineTripDAO> getPartOfLineTrip(Predicate<LineTripDAO> LineTripDAOCondition)
        {
            List<LineTripDAO> ListLineTrips = XMLTools.LoadListFromXMLSerializer<LineTripDAO>(lineTripsPath);
            IEnumerable<LineTripDAO> TempLineTripDAO = from LineTripDAO item in ListLineTrips
                                                       where LineTripDAOCondition(item)
                                                       select item;
            if (TempLineTripDAO.Count() == 0)
                throw new LineTripExceptionDO("There are no lineTrips in the system that meet the condition");
            return TempLineTripDAO;
        }
        public LineTripDAO getOneObjectLineTripDAO(int identifyNumber, TimeSpan timeOfExit)
        {
            List<LineTripDAO> ListLineTrips = XMLTools.LoadListFromXMLSerializer<LineTripDAO>(lineTripsPath);
            DO.LineTripDAO lineTrip = ListLineTrips.Find(mishehu => mishehu.IdentifyNumber == identifyNumber && mishehu.TripStart == timeOfExit);
            if (lineTrip != null)
                return lineTrip; //no need to Clone()
            else
                throw new DO.LineTripExceptionDO("The line exit was not found");
        }
        #endregion
    }
}
