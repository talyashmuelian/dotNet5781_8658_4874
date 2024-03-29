﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DLAPI
{
    public interface IDal
    {
        //אוטובוס
        #region Bus
        bool addBus(BusDAO bus);
        bool updateBus(BusDAO bus);
        bool deleteBus(BusDAO bus);
        IEnumerable<BusDAO> getAllBuses();
        IEnumerable<BusDAO> getPartOfBuses(Predicate<BusDAO> BusDAOCondition);
        BusDAO getOneObjectBusDAO(string license);
        #endregion
        //קו אוטובוס
        #region BusLine
        bool addBusLine(BusLineDAO busLine);
        bool updateBusLine(BusLineDAO busLine);
        bool deleteBusLine(BusLineDAO busLine);
        IEnumerable<BusLineDAO> getAllBusLines();
        IEnumerable<BusLineDAO> getPartOfBusLines(Predicate<BusLineDAO> BusLineDAOCondition);
        BusLineDAO getOneObjectBusLineDAO(int identifyNumber);
        #endregion
        //BusStation תחנת אוטובוס
        #region BusStation
        bool addBusStation(BusStationDAO station);
        bool updateBusStation(BusStationDAO station);
        bool deleteBusStation(BusStationDAO station);
        IEnumerable<BusStationDAO> getAllBusStations();
        IEnumerable<BusStationDAO> getPartOfBusStations(Predicate<BusStationDAO> BusStationDAOCondition);
        BusStationDAO getOneObjectBusStationDAO(int codeStation);
        #endregion
        //ExitLine יציאת קו
        //bool addExitLine(ExitLineDAO line);
        //bool updateExitLine(ExitLineDAO line);
        //void deleteExitLine(ExitLineDAO line);
        //IEnumerable<ExitLineDAO> getAllExitLines();
        //IEnumerable<ExitLineDAO> getPartOfExitLines(Predicate<ExitLineDAO> BusDAOCondition);
        //ExitLineDAO getOneObjectExitLineDAO(int license);

        //LineStation תחנת קו
        #region LineStation
        bool addLineStation(LineStationDAO station);
        bool updateLineStation(LineStationDAO station);
        void deleteLineStation(LineStationDAO station);
        IEnumerable<LineStationDAO> getAllLineStations();
        IEnumerable<LineStationDAO> getPartOfLineStations(Predicate<LineStationDAO> LineStationDAOCondition);
        LineStationDAO getOneObjectLineStationDAO(int identifyNumber, int codeStation);
        #endregion
        //PairConsecutiveStations//זוג תחנות עוקבות
        #region PairConsecutiveStations
        bool addPairConsecutiveStations(PairConsecutiveStationsDAO stations);
        bool updatePairConsecutiveStations(PairConsecutiveStationsDAO stations);
        bool deletePairConsecutiveStations(PairConsecutiveStationsDAO stations);
        IEnumerable<PairConsecutiveStationsDAO> getAllPairConsecutiveStations();
        IEnumerable<PairConsecutiveStationsDAO> getPartOfPairConsecutiveStations(Predicate<PairConsecutiveStationsDAO> PairConsecutiveStationsDAOCondition);
        PairConsecutiveStationsDAO getOneObjectPairConsecutiveStations(int stationNum1, int stationNum2);
        #endregion
        bool addUser(UserDAO user);
        bool updateUser(UserDAO user);
        bool deleteUser(UserDAO user);
        IEnumerable<UserDAO> getAllUsers();
        IEnumerable<UserDAO> getPartOfUsers(Predicate<UserDAO> UserDAOCondition);
        UserDAO getOneObjectUserDAO(string userName);
        bool addLineTrip(LineTripDAO lineTrip);
        bool updateLineTrip(LineTripDAO lineTrip);
        bool deleteLineTrip(LineTripDAO lineTrip);
        IEnumerable<LineTripDAO> getAllLineTrips();
        IEnumerable<LineTripDAO> getPartOfLineTrip(Predicate<LineTripDAO> LineTripDAOCondition);
        LineTripDAO getOneObjectLineTripDAO(int identifyNumber, TimeSpan timeOfExit);
    }
}
