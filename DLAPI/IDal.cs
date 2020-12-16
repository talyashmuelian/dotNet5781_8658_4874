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
        //צריך להוסיף את הבקשות לכל ישות
        //אוטובוס
        bool addBus(BusDAO bus);
        bool updateBus(BusDAO bus);
        void deleteBus(BusDAO bus);
        IEnumerable<BusDAO> getAllBuses();
        IEnumerable<BusDAO> getPartOfBuses(Predicate<BusDAO> BusDAOCondition);
        BusDAO getOneObject()
        //קו אוטובוס
        bool addBusLine(BusLineDAO busLine);
        bool updateBusLine(BusLineDAO busLine);
        void deleteBusLine(BusLineDAO busLine);
        //BusStation תחנת אוטובוס
        bool addBusStation(BusStationDAO station);
        bool updateBusStation(BusStationDAO station);
        void deleteBusStation(BusStationDAO station);
        //ExitLine יציאת קו
        bool addExitLine(ExitLineDAO line);
        bool updateExitLine(ExitLineDAO line);
        void deleteExitLine(ExitLineDAO line);
        //LineStation תחנת קו
        bool addLineStation(LineStationDAO station);
        bool updateLineStation(LineStationDAO station);
        void deleteLineStation(LineStationDAO station);
        //PairConsecutiveStations//זוג תחנות עוקבות
        bool addPairConsecutiveStations(PairConsecutiveStationsDAO stations);
        bool updatePairConsecutiveStations(PairConsecutiveStationsDAO stations);
        void deletePairConsecutiveStations(PairConsecutiveStationsDAO stations);


    }
}
