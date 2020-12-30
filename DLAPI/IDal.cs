using System;
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
        void deleteBus(BusDAO bus);
        IEnumerable<BusDAO> getAllBuses();
        IEnumerable<BusDAO> getPartOfBuses(Predicate<BusDAO> BusDAOCondition);
        BusDAO getOneObjectBusDAO(int license);
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
        void deletePairConsecutiveStations(PairConsecutiveStationsDAO stations);
        IEnumerable<PairConsecutiveStationsDAO> getAllPairConsecutiveStations();
        IEnumerable<PairConsecutiveStationsDAO> getPartOfPairConsecutiveStations(Predicate<PairConsecutiveStationsDAO> PairConsecutiveStationsDAOCondition);
        PairConsecutiveStationsDAO getOneObjectPairConsecutiveStations(int stationNum1, int stationNum2);
        #endregion


    }
}
