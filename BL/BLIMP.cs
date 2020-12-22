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
        const int FULLTANK = 1200;
        readonly IDal dal = DLFactory.GetDal();
        //המרות
        #region coverting
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
        private BusLineDAO convertDAO(BusLineBO busLine)
        {
            BusLineDAO busLineDAO = new BusLineDAO
            {
                IdentifyNumber = busLine.IdentifyNumber,
                LineNumber = busLine.LineNumber,
                Area = busLine.Area,
                FirstStationNum = busLine.FirstStationNum,
                LastStationNum = busLine.LastStationNum,
            };
            //יכול להיות שצריך לעדכן את רשימת תחנות הקו
            return busLineDAO;
        }

        private BusLineBO convertoBO(BusLineDAO busLine)
        {
            BusLineBO busLineBO = new BusLineBO
            {
                IdentifyNumber = busLine.IdentifyNumber,
                LineNumber = busLine.LineNumber,
                Area = busLine.Area,
                FirstStationNum = busLine.FirstStationNum,
                LastStationNum = busLine.LastStationNum,
            };
            IEnumerable<LineStationDAO> listLineStations = dal.getPartOfLineStations(item => item.IdentifyNumber == busLine.IdentifyNumber);//רשימה של תחנות קו המתאימות לקו הזה
            IEnumerable<BusStationDAO> listBusStationInLine =
                from lineStation in listLineStations
                from BusStation in dal.getAllBusStations()
                orderby lineStation.NumStationInTheLine
                where lineStation.CodeStation == BusStation.CodeStation
                select BusStation;
            busLineBO.ListOfStations = listBusStationInLine;
            return busLineBO;
        }
        private BusStationDAO convertDAO(BusStationBO busStation)
        {
            BusStationDAO busStationDAO = new BusStationDAO
            {
                CodeStation = busStation.CodeStation,
                Latitude = busStation.Latitude,
                Longitude = busStation.Longitude,
                NameStation = busStation.NameStation,
                IsAccessible = busStation.IsAccessible,
            };
            //IEnumerable<LineStationDAO> listLineStations = dal.getPartOfLineStations(item => item.CodeStation == busStation.CodeStation);
            //IEnumerable<BusLineBO> listOfLineInStation =
            //    from lineStation in listLineStations
            //    from BusLine in busStation.ListOfLines
            //    where lineStation.IdentifyNumber != BusLine.IdentifyNumber
            //    select BusLine;
            //busStationBO.ListOfLines = listOfLineInStation;
            return busStationDAO;
        }

        private BusStationBO convertoBO(BusStationDAO busStation)
        {
            BusStationBO busStationBO = new BusStationBO
            {
                CodeStation = busStation.CodeStation,
                Latitude = busStation.Latitude,
                Longitude = busStation.Longitude,
                NameStation = busStation.NameStation,
                IsAccessible = busStation.IsAccessible,
            };
            IEnumerable<LineStationDAO> listLineStations = dal.getPartOfLineStations(item => item.CodeStation == busStation.CodeStation);//רשימה של תחנות קו המתאימות לתחנה הזאת
            IEnumerable<BusLineBO> listOfLineInStation =
                from lineStation in listLineStations
                from BusLine in GetAllBusLinesBO()
                where lineStation.IdentifyNumber == BusLine.IdentifyNumber
                select BusLine;
            busStationBO.ListOfLines = listOfLineInStation;
            return busStationBO;
        }
        //הדפסת כל הקווים
        public IEnumerable<BusLineBO> GetAllBusLinesBO()
        {
            return from busLine in dal.getAllBusLines()
                   select convertoBO(busLine);
        }
        //קבלת פרטים על קו בודד
        public BusLineBO GetBusLineBO(int identifyNumber)
        {
            BusLineBO result = new BusLineBO();
            BusLineDAO busLineDAO;
            try
            {
                busLineDAO = dal.getOneObjectBusLineDAO(identifyNumber);
            }
            catch (DO.BusLineExceptionDO ex)
            {
                throw new BO.BusLineExceptionBO("The Identify-Number-Line " + identifyNumber + " not found", ex);
            }
            result = convertoBO(busLineDAO);
            return result;
        }
        //הוספה, עדכון ומחיקת קו
        public bool addBusLine(BusLineBO busLine)
        {
            bool result;
            try
            {
                result = dal.addBusLine(convertDAO(busLine));
            }
            catch (DO.BusLineExceptionDO ex)
            {
                throw new BO.BusLineExceptionBO("Identify-Number-Line exists allready", ex);
            }
            return result;
        }
        public bool updateBusLine(BusLineBO busLine)
        {
            bool result;
            try
            {
                result = dal.updateBusLine(convertDAO(busLine));
            }
            catch (DO.BusLineExceptionDO ex)
            {
                throw new BO.BusLineExceptionBO("The Identify-Number-Line " + busLine.IdentifyNumber + " not found", ex);
            }
            return result;
        }
        public void deleteBusLine(BusLineBO busLine)
        {

        }
        #endregion
        //אוטובוסים
        #region buses

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
            catch (DO.BusExceptionDO ex)
            {
                throw new BO.BusExceptionBO("License number not found", ex);
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
            catch (DO.BusExceptionDO ex)
            {
                throw new BO.BusExceptionBO("license exists allready", ex);
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
            catch (DO.BusExceptionDO ex)
            {
                throw new BO.BusExceptionBO("The license number " + bus.License + " not found", ex);
            }
            return result;
        }
        public void deleteBus(BusBO bus)
        {
            try
            {
                dal.deleteBus(convertDAO(bus));
            }
            catch (DO.BusExceptionDO ex)
            {
                throw new BO.BusExceptionBO("Does not exist in the system", ex);
            }
        }
        //שליחת אוטובוס לטיפול ותדלוק
        public void refuel(BusBO bus)
        {
            IEnumerable<BusDAO> buses = dal.getAllBuses();
            BusDAO busDAO = convertDAO(bus);
            if (!buses.Any(item => item.License == busDAO.License))
            {
                throw new ArgumentNullException("bus not found");
            }
            if (busDAO.Fuel == FULLTANK)
            {
                throw new BO.BusExceptionBO("tank full allready");
            }
            busDAO.Status = DO.Status.REFUELLING;
            busDAO.Fuel = FULLTANK;//התדלוק עצמו
            dal.updateBus(busDAO);
        }
        public void treatment(BusBO bus)
        {
            IEnumerable<BusDAO> buses = dal.getAllBuses();
            BusDAO busDAO = convertDAO(bus);
            if (!buses.Any(item => item.License == busDAO.License))
            {
                throw new ArgumentNullException("bus not found");
            }
            busDAO.Status = DO.Status.SERVICE;
            busDAO.DateTreatLast = DateTime.Now;
            busDAO.KmFromTreament = 0;
            dal.updateBus(busDAO);
        }
        #endregion
        //תחנות
        #region stations
        //הדפסת כל התחנות
        public IEnumerable<BusStationBO> GetAllBusStationsBO()
        {
            return from bus in dal.getAllBusStations()
                   select convertoBO(bus);
        }
        //קבלת פרטים עבור תחנה מסוימת
        public BusStationBO GetBusStationBO(int codeStation)
        {
            BusStationBO result = new BusStationBO();
            BusStationDAO busStationDAO;
            try
            {
                busStationDAO = dal.getOneObjectBusStationDAO(codeStation);
            }
            catch (DO.BusStationExceptionDO ex)
            {
                throw new BO.BusStationExceptionBO("Code Station number not found", ex);
            }
            result = convertoBO(busStationDAO);
            return result;
        }
        //הוספה עדכון ומחיקת תחנה
        //הוספת תחנה חדשה לגמרי שחייבת להיות לפחות בקו אחד
        public bool addBusStation(BusStationBO busStation)
        {
            bool result;
            try
            {
                foreach (BusLineBO busLine in busStation.ListOfLines)
                {
                    dal.addLineStation(new LineStationDAO
                    {
                        CodeStation= busStation.CodeStation, 
                        IdentifyNumber= busLine.IdentifyNumber
                        //לבדוק איך לדעת את המיקום של התחנה בכל קו
                    });
                }
                result = dal.addBusStation(convertDAO(busStation));
            }
            catch (DO.BusStationExceptionDO ex)
            {
                throw new BO.BusStationExceptionBO("Code station exists allready", ex);
            }
            return result;
        }
        public bool updateBusStation(BusStationBO busStation)
        {
            bool result;
            try
            {
                foreach (BusLineBO busLine in busStation.ListOfLines)
                {
                    if (dal.getAllLineStations().Any(mishehu => mishehu.CodeStation == busStation.CodeStation && mishehu.CodeStation == busStation.CodeStation))
                    {
                        //במצב שכבר קיימת תחנת קו מקשרת בין התחנה לקו, אז לא צריך ליצור אובייקט חדש, אבל אולי צריך לעדכן את מיקום התחנה בקו
                    }
                    else//במצב שנוסף קו חדש לתחנה, ניצור להם אובייקט מחבר
                    {
                        dal.addLineStation(new LineStationDAO
                        {
                            CodeStation = busStation.CodeStation,
                            IdentifyNumber = busLine.IdentifyNumber
                            //לבדוק איך לדעת את המיקום של התחנה בכל קו
                        });
                    }
                }
                result = dal.updateBusStation(convertDAO(busStation));
            }
            catch (DO.BusStationExceptionDO ex)
            {
                throw new BO.BusStationExceptionBO("The Code Station number " + busStation.CodeStation + " not found", ex);
            }
            return result;
        }
        public void deleteBusStation(BusStationBO busStation)
        {
            try
            {
                foreach (BusLineBO busLine in busStation.ListOfLines)
                {
                    dal.deleteLineStation(new LineStationDAO
                    {
                        CodeStation = busStation.CodeStation,
                        IdentifyNumber = busLine.IdentifyNumber
                    });
                }
                dal.deleteBusStation(convertDAO(busStation));
            }
            catch (DO.BusStationExceptionDO ex)
            {
                throw new BO.BusStationExceptionBO("Does not exist in the system", ex);
            }

        }
        #endregion
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

