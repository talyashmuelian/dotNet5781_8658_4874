using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLAPI;
using DO;
using DS;

namespace DL
{
    class DalObject : IDal
    {
        #region singelton
        static readonly DalObject instance = new DalObject();
        static DalObject() { }
        DalObject() { }
        public static DalObject Instance => instance;
        #endregion
        //אוטובוס
        #region bus
        public bool addBus(BusDAO bus)
        {
            if (DATA.Buses.Exists(mishehu => mishehu.License == bus.License))
            {
                throw new BusExceptionDO("license exists allready");
                //return false;
            }
            DATA.Buses.Add(bus.Clone());
            return true;
        }
        public bool updateBus(BusDAO bus)
        {
            if (!DATA.Buses.Exists(mishehu => mishehu.License == bus.License))
            {
                throw new DO.BusExceptionDO("The license number " + bus.License + " not found");
                //return false;
            }
            DATA.Buses.RemoveAll(b => b.License == bus.License);//מוחק את האוטובוס הקיים
            DATA.Buses.Add(bus.Clone());//מכניס את החדש במקומו
            return true;
        }
        public void deleteBus(BusDAO bus)
        {
            if (!DS.DATA.Buses.Exists(item => item.License == bus.License))
            {
                //return false;
                throw new BusExceptionDO("Does not exist in the system");
            }
            //BusDAO todelete = null;
            //foreach (var item in DS.DataSource.Buses)
            //{
            //    if(item.License == bus.License)
            //    {
            //        todelete = item;
            //        break;
            //    }
            //}
            //if(todelete != null)
            //{
            //    DS.DataSource.Buses.Remove(todelete);
            //}
            DS.DATA.Buses.RemoveAll(item => item.License == bus.License);
        }
        public IEnumerable<BusDAO> getAllBuses()
        {
            return from bus in DATA.Buses
                   select bus.Clone();
        }
        public IEnumerable<BusDAO> getPartOfBuses(Predicate<BusDAO> BusDAOCondition)
        {
            IEnumerable<BusDAO> TempBusDAO = from BusDAO item in DATA.Buses
                                             where BusDAOCondition(item)
                                             select item.Clone();
            if (TempBusDAO.Count() == 0)
                throw new BusExceptionDO("There are no buses in the system that meet the condition");
            return TempBusDAO;
        }
        public BusDAO getOneObjectBusDAO(int license)
        {
            BusDAO bus1 = DATA.Buses.Find(p => p.License == license);

            if (bus1 != null)
                return bus1.Clone();
            else
                throw new DO.BusExceptionDO("The license number " + license + " not found");
        }
        #endregion
        //קו אוטובוס
        #region busLine
        public bool addBusLine(BusLineDAO busLine)
        {
            if (DATA.BusLines.Exists(mishehu => mishehu.IdentifyNumber == busLine.IdentifyNumber))
            {
                throw new BusLineExceptionDO("Identify-Number-Line exists allready");
                //return false;
            }
            busLine.IdentifyNumber = configoration.RunNumber;
            DATA.BusLines.Add(busLine.Clone());
            DATA.LineStations.Add(new LineStationDAO { CodeStation = busLine.FirstStationNum, IdentifyNumber = busLine.IdentifyNumber, NumStationInTheLine = 1 });
            DATA.LineStations.Add(new LineStationDAO { CodeStation = busLine.LastStationNum, IdentifyNumber = busLine.IdentifyNumber, NumStationInTheLine = 2 });
            return true;
        }
        public bool updateBusLine(BusLineDAO busLine)
        {
            if (!DATA.BusLines.Exists(mishehu => mishehu.IdentifyNumber == busLine.IdentifyNumber))
            {
                throw new DO.BusLineExceptionDO("The Identify-Number-Line " + busLine.IdentifyNumber + " not found");
                //return false;
            }
            BusLineDAO currentLine = getOneObjectBusLineDAO(busLine.IdentifyNumber);//שמירה על הערכים הקודמים של הקו
            busLine.IdentifyNumber = currentLine.IdentifyNumber;
            busLine.FirstStationNum = currentLine.FirstStationNum;
            busLine.LastStationNum = currentLine.LastStationNum;
            DATA.BusLines.RemoveAll(b => b.IdentifyNumber == busLine.IdentifyNumber);//מוחק את הקו אוטובוס הקיים
            DATA.BusLines.Add(busLine.Clone());//מכניס את החדש במקומו
            return true;
        }
        public void deleteBusLine(BusLineDAO busLine)
        {
            if (!DS.DATA.BusLines.Exists(item => item.IdentifyNumber == busLine.IdentifyNumber))
            {
                //return false;
                throw new BusLineExceptionDO("Does not exist in the system");
            }
            //BusDAO todelete = null;
            //foreach (var item in DS.DataSource.Buses)
            //{
            //    if(item.License == bus.License)
            //    {
            //        todelete = item;
            //        break;
            //    }
            //}
            //if(todelete != null)
            //{
            //    DS.DataSource.Buses.Remove(todelete);
            //}
            DS.DATA.BusLines.RemoveAll(item => item.IdentifyNumber == busLine.IdentifyNumber);
        }
        public IEnumerable<BusLineDAO> getAllBusLines()
        {
            return from busLine in DATA.BusLines
                   select busLine.Clone();
        }
        public IEnumerable<BusLineDAO> getPartOfBusLines(Predicate<BusLineDAO> BusLineDAOCondition)
        {
            IEnumerable<BusLineDAO> TempBusLineDAO = from BusLineDAO item in DATA.BusLines
                                                     where BusLineDAOCondition(item)
                                                     select item.Clone();
            if (TempBusLineDAO.Count() == 0)
                throw new BusLineExceptionDO("There are no bus lines in the system that meet the condition");
            return TempBusLineDAO;
        }
        public BusLineDAO getOneObjectBusLineDAO(int identifyNumber)
        {
            BusLineDAO busLine1 = DATA.BusLines.Find(p => p.IdentifyNumber == identifyNumber);

            if (busLine1 != null)
                return busLine1.Clone();
            else
                throw new DO.BusLineExceptionDO("The Identify-Number-Line " + identifyNumber + " not found");
        }
        #endregion
        //BusStation תחנת אוטובוס
        #region BusStation
        public bool addBusStation(BusStationDAO station)
        {
            if (DATA.BusStations.Exists(mishehu => mishehu.CodeStation == station.CodeStation))
            {
                throw new BusStationExceptionDO("Code Station exists allready");
                //return false;
            }
            DATA.BusStations.Add(station.Clone());
            ///////להוסיף מידע על תחנות עוקבות
            /////ניצור לתחנה חדשה אובייקטים של זוג תחנות עוקבות בינה לבין כל תחנה קיימת
            foreach(var station1 in DATA.BusStations)
            {
                int dis = DATA.rand.Next(1, 500);
                DATA.PairConsecutiveStations.Add(new PairConsecutiveStationsDAO
                { 
                    StationNum1= station1.CodeStation,
                    StationNum2= station.CodeStation,
                    Distance= dis,
                    TimeDriving= dis
                }); 
            }
            return true;
        }
        public bool updateBusStation(BusStationDAO station)
        {
            if (!DATA.BusStations.Exists(mishehu => mishehu.CodeStation == station.CodeStation))
            {
                throw new DO.BusStationExceptionDO("The Code Station " + station.CodeStation + " not found");
                //return false;
            }
            DATA.BusStations.RemoveAll(b => b.CodeStation == station.CodeStation);//מוחק את האוטובוס הקיים
            DATA.BusStations.Add(station.Clone());//מכניס את החדש במקומו
            return true;
        }
        public void deleteBusStation(BusStationDAO station)
        {
            if (!DS.DATA.BusStations.Exists(item => item.CodeStation == station.CodeStation))
            {
                //return false;
                throw new BusStationExceptionDO("Does not exist in the system");
            }
            //BusDAO todelete = null;
            //foreach (var item in DS.DataSource.Buses)
            //{
            //    if(item.License == bus.License)
            //    {
            //        todelete = item;
            //        break;
            //    }
            //}
            //if(todelete != null)
            //{
            //    DS.DataSource.Buses.Remove(todelete);
            //}
            DS.DATA.BusStations.RemoveAll(item => item.CodeStation == station.CodeStation);
            //מחיקת האובייקטים של תחנות עוקבות שקשורות לתחנה הזאת
            DATA.PairConsecutiveStations.RemoveAll(mishehu => mishehu.StationNum1 == station.CodeStation || mishehu.StationNum2 == station.CodeStation);//מוחק את כל הזוגות שקשורים לתחנה הנמחקת
        }
        public IEnumerable<BusStationDAO> getAllBusStations()
        {
            return from station in DATA.BusStations
                   select station.Clone();
        }
        public IEnumerable<BusStationDAO> getPartOfBusStations(Predicate<BusStationDAO> BusStationDAOCondition)
        {
            IEnumerable<BusStationDAO> TempBusStationDAO = from BusStationDAO item in DATA.BusStations
                                                           where BusStationDAOCondition(item)
                                                           select item.Clone();
            if (TempBusStationDAO.Count() == 0)
                throw new BusStationExceptionDO("There are no stations in the system that meet the condition");
            return TempBusStationDAO;
        }
        public BusStationDAO getOneObjectBusStationDAO(int codeStation)
        {
            BusStationDAO station1 = DATA.BusStations.Find(p => p.CodeStation == codeStation);
            if (station1 != null)
                return station1.Clone();
            else
                throw new DO.BusStationExceptionDO("The Code Station " + codeStation + " not found");
        }
        #endregion
        //LineStation תחנת קו
        #region LineStation
        public bool addLineStation(LineStationDAO station)
        {
            //if (!DATA.BusLines.Exists(mishehu => mishehu.IdentifyNumber == station.IdentifyNumber))//אם הקו לא קיים בכלל
            //{
            //    throw new LineStationExceptionDO("The line does not exist and therefore no station can be added to it");
            //    //return false;
            //}
            if (DATA.LineStations.Exists(mishehu => mishehu.IdentifyNumber == station.IdentifyNumber && mishehu.CodeStation == station.CodeStation))
            {
                throw new LineStationExceptionDO("The station already exists on the line");
                //return false;
            }
            DATA.LineStations.Add(station.Clone());
            return true;
        }
        public bool updateLineStation(LineStationDAO station)
        {
            if (!DATA.LineStations.Exists(mishehu => mishehu.IdentifyNumber == station.IdentifyNumber && mishehu.CodeStation == station.CodeStation))
            {
                throw new DO.LineStationExceptionDO("The Station number " + station.CodeStation + " not found in the line " + station.IdentifyNumber);
                //return false;
            }
            DATA.LineStations.RemoveAll(b => b.IdentifyNumber == station.IdentifyNumber && b.CodeStation == station.CodeStation);//מוחק את האובייקט הקיים
            DATA.LineStations.Add(station.Clone());//מכניס את החדש במקומו
            return true;
        }
        public void deleteLineStation(LineStationDAO station)
        {
            if (!DS.DATA.LineStations.Exists(item => item.IdentifyNumber == station.IdentifyNumber && item.CodeStation == station.CodeStation))
            {
                //return false;
                throw new LineStationExceptionDO("Does not exist in the system");
            }
            //BusDAO todelete = null;
            //foreach (var item in DS.DataSource.Buses)
            //{
            //    if(item.License == bus.License)
            //    {
            //        todelete = item;
            //        break;
            //    }
            //}
            //if(todelete != null)
            //{
            //    DS.DataSource.Buses.Remove(todelete);
            //}
            DS.DATA.LineStations.RemoveAll(item => item.IdentifyNumber == station.IdentifyNumber && item.CodeStation == station.CodeStation);
        }
        public IEnumerable<LineStationDAO> getAllLineStations()
        {
            return from lineStation in DATA.LineStations
                   select lineStation.Clone();
        }
        public IEnumerable<LineStationDAO> getPartOfLineStations(Predicate<LineStationDAO> LineStationDAOCondition)
        {
            IEnumerable<LineStationDAO> TempLineStationDAO = from LineStationDAO item in DATA.LineStations
                                                             where LineStationDAOCondition(item)
                                                             select item.Clone();
            if (TempLineStationDAO.Count() == 0)
                throw new LineStationExceptionDO("There are no line stations that meet the condition");
            return TempLineStationDAO;
        }
        public LineStationDAO getOneObjectLineStationDAO(int identifyNumber, int codeStation)
        {
            LineStationDAO lineStation1 = DATA.LineStations.Find(p => p.IdentifyNumber == identifyNumber && p.CodeStation == codeStation);
            if (lineStation1 != null)
                return lineStation1.Clone();
            else
                throw new DO.LineStationExceptionDO("The Station number " + codeStation + " not found in the line " + identifyNumber);
        }
        #endregion
        //PairConsecutiveStations//זוג תחנות עוקבות
        #region PairConsecutiveStations
        public bool addPairConsecutiveStations(PairConsecutiveStationsDAO stations)
        {
            if (DATA.PairConsecutiveStations.Exists(mishehu => mishehu.StationNum1 == stations.StationNum1 && mishehu.StationNum2 == stations.StationNum2 || mishehu.StationNum1 == stations.StationNum2 && mishehu.StationNum2 == stations.StationNum1))
            {
                throw new PairConsecutiveStationsExceptionDO("The pair of stations already exists");
                //return false;
            }
            DATA.PairConsecutiveStations.Add(stations.Clone());
            return true;
        }
        public bool updatePairConsecutiveStations(PairConsecutiveStationsDAO stations)
        {
            if (!DATA.PairConsecutiveStations.Exists(mishehu => mishehu.StationNum1 == stations.StationNum1 && mishehu.StationNum2 == stations.StationNum2 || mishehu.StationNum1 == stations.StationNum2 && mishehu.StationNum2 == stations.StationNum1))
            {
                throw new DO.PairConsecutiveStationsExceptionDO("The pair of stations does not exist in the system");
                //return false;
            }
            DATA.PairConsecutiveStations.RemoveAll(mishehu => mishehu.StationNum1 == stations.StationNum1 && mishehu.StationNum2 == stations.StationNum2 || mishehu.StationNum1 == stations.StationNum2 && mishehu.StationNum2 == stations.StationNum1);//מוחק את האובייקט הקיים
            DATA.PairConsecutiveStations.Add(stations.Clone());//מכניס את החדש במקומו
            return true;
        }
        public void deletePairConsecutiveStations(PairConsecutiveStationsDAO stations)
        {
            if (!DS.DATA.PairConsecutiveStations.Exists(mishehu => mishehu.StationNum1 == stations.StationNum1 && mishehu.StationNum2 == stations.StationNum2 || mishehu.StationNum1 == stations.StationNum2 && mishehu.StationNum2 == stations.StationNum1))
            {
                //return false;
                throw new PairConsecutiveStationsExceptionDO("Does not exist in the system");
            }
            //BusDAO todelete = null;
            //foreach (var item in DS.DataSource.Buses)
            //{
            //    if(item.License == bus.License)
            //    {
            //        todelete = item;
            //        break;
            //    }
            //}
            //if(todelete != null)
            //{
            //    DS.DataSource.Buses.Remove(todelete);
            //}
            DS.DATA.PairConsecutiveStations.RemoveAll(mishehu => mishehu.StationNum1 == stations.StationNum1 && mishehu.StationNum2 == stations.StationNum2 || mishehu.StationNum1 == stations.StationNum2 && mishehu.StationNum2 == stations.StationNum1);
        }
        public IEnumerable<PairConsecutiveStationsDAO> getAllPairConsecutiveStations()
        {
            return from stations in DATA.PairConsecutiveStations
                   select stations.Clone();
        }
        public IEnumerable<PairConsecutiveStationsDAO> getPartOfPairConsecutiveStations(Predicate<PairConsecutiveStationsDAO> PairConsecutiveStationsDAOCondition)
        {
            IEnumerable<PairConsecutiveStationsDAO> TempPairConsecutiveStationsDAO = from PairConsecutiveStationsDAO item in DATA.PairConsecutiveStations
                                                                                     where PairConsecutiveStationsDAOCondition(item)
                                                                                     select item.Clone();
            if (TempPairConsecutiveStationsDAO.Count() == 0)
                throw new PairConsecutiveStationsExceptionDO("There is no pair of stations that meets the condition");
            return TempPairConsecutiveStationsDAO;
        }
        public PairConsecutiveStationsDAO getOneObjectPairConsecutiveStations(int stationNum1, int stationNum2)
        {
            PairConsecutiveStationsDAO stations1 = DATA.PairConsecutiveStations.Find(p => p.StationNum1 == stationNum1 && p.StationNum2 == stationNum2 || p.StationNum2 == stationNum1 && p.StationNum1 == stationNum2);

            if (stations1 != null)
                return stations1.Clone();
            else
                throw new DO.PairConsecutiveStationsExceptionDO("No object found for this pair of stations");
        }
        #endregion
    }
}

