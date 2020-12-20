using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLAPI;
using DLAPI;
//using DL;
using BO;
using DO;
namespace BL
{
    class BLIMP : IBL
    {
        readonly IDal dal = DLFactory.GetDal();
        private BusDAO convertDAO(BusBO bus)
        {
            BusDAO busDAO = new BusDAO
            {
                License = Int32.Parse(bus.License),
                StartOfWork = bus.StartOfWork,
                TotalKms = bus.TotalKms,
                Fuel = bus.Fuel,
                DateTreatLast= bus.DateTreatLast,
                KmFromTreament= bus.KmFromTreament,
                //Status = (bus.Status == true) ? Status.READY : Status.REFUELLING
                Status = (DO.Status)bus.Status
            };
            return busDAO;
        }

        private BusBO convertoBO(BusDAO bus)
        {
            return new BusBO
            {
                License = bus.License.ToString(),
                StartOfWork = bus.StartOfWork,
                TotalKms = bus.TotalKms,
                Fuel = bus.Fuel,
                DateTreatLast = bus.DateTreatLast,
                KmFromTreament = bus.KmFromTreament,
                Status = (BO.Status)bus.Status
            };
        }
        //הדפסת כל הקווים
        public IEnumerable<BusLineBO> GetAllBusLinesBO()
        {
            
        }
        //קבלת פרטים על קו בודד
        public BusLineBO GetBusLineBO(int identifyNumber)
        {

        }
        //הוספה, עדכון ומחיקת קו
        public bool addBusLine(BusLineBO busLine)
        {

        }
        public bool updateBusLine(BusLineBO busLine)
        {

        }
        public void deleteBusLine(BusLineBO busLine)
        {

        }
        //הדפסת כל האוטבוסים
        public IEnumerable<BusBO> GetAllBusesBO()
        {
            return from bus in dal.getAllBuses()
                   select convertoBO(bus);
        }
        //קבלת פרטי אוטובוס בודד
        public BusBO GetBusBO(int license)
        {
            BusBO result = new BusBO();
            BusDAO busDAO;
            try
            {
                busDAO = dal.getOneObjectBusDAO(license);
            }
            catch (DO.ObjectNotFoundException ex)
            {
                throw new BO.ObjectNotFoundException("License number not found", ex);
            }
            result = convertoBO(busDAO);
            return result;
        }
        //הוספה, עדכון ומחיקת אוטובוס
        public bool addBus(BusBO bus)
        {
            bool result;
            try
            {
                result = dal.addBus(convertDAO(bus));
            }
            catch (DO.BusException ex)
            {
                throw new BO.BusException("license exists allready", ex);
            }
            return result;
        }
        public bool updateBus(BusBO bus)
        {
            bool result;
            try
            {
                result = dal.updateBus(convertDAO(bus));
            }
            catch (DO.ObjectNotFoundException ex)
            {
                throw new BO.ObjectNotFoundException("The license number " + bus.License + " not found", ex);
            }
            return result;
        }
        public void deleteBus(BusBO bus)
        {

        }
        //שליחת אוטבוס לטיפול ותדלוק
        public void refuel(BusBO bus)
        {

        }
        public void treatment(BusBO bus)
        {

        }
        //הדפסת כל התחנות
        public IEnumerable<BusStationBO> GetAllBusStationsBO()
        {

        }
        //קבלת פרטים עבור תחנה מסוימת
        public BusBO GetBusStationBO(int codeStation)
        {

        }
        //הוספה עדכון ומחיקת תחנה
        //הוספת תחנה חדשה לגמרי שחייבת להיות לפחות בקו אחד
        public bool addBusStation(BusStationBO busStation)
        {

        }
        public bool updateBusStation(BusStationBO busStation)
        {

        }
        public void deleteBusStation(BusStationBO busStation)
        {


        }
        //הוספת תחנה קיימת לקו כלשהו, צריך לעדכן ברשימת התחנות של הקו, וברשימת הקווים של התחנה
        public void addExsistStationToLine(BusStationBO busStation)
        {

        }
        //עדכון מרחק וזמן נסיעה בין זוג תחנות עוקבות
        public void updatePairConsecutiveStations(int distance, int timeDriving)
        {

        }
    }
}

