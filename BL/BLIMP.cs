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
        //קווים
        #region lines
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
            IEnumerable<LineStationDAO> listStationInLineOrder =//נשמרות כל התחנות קו של הקו הזה בצורה ממוינת
               from lineStation in listLineStations
               orderby lineStation.NumStationInTheLine
               //where lineStation.CodeStation == BusStation.CodeStation
               select lineStation;
            IEnumerable<PairConsecutiveStationsDAO> pairs = dal.getAllPairConsecutiveStations();
            List<StationInLineBO> listStationTypeCorrect = new List<StationInLineBO>();
            LineStationDAO prev = null;
            LineStationDAO current = null;
            bool first = true;
            //הלולאה הזאת עוברת על כל תחנות קו שמקושרות לקו הזה, ויוצרת רשימה של "תחנות בקו" שתוכנס לשדה המבוקש
            //ישנה בעיה- ההוספה של התחנה הראשונה בעייתית. לכן היא מחוץ ללולאה. צריך למצוא דרך לגרום שהלולאה תתחיל מהאיבר השני
            foreach (LineStationDAO lineStationNext in listStationInLineOrder)
            {
                /////////////////////////////////////
                ///לחשוב איפה לשים את שתי השורות האלה
                prev = current;
                current = lineStationNext;
                if (!first)
                {
                    listStationTypeCorrect.Add(new StationInLineBO
                    {
                        CodeStation = lineStationNext.CodeStation,
                        NumStationInTheLine= lineStationNext.NumStationInTheLine,
                        NameStation = dal.getOneObjectBusStationDAO(lineStationNext.CodeStation).NameStation,
                        Distance = dal.getOneObjectPairConsecutiveStations(current.CodeStation, prev.CodeStation).Distance,
                        TimeDriving = dal.getOneObjectPairConsecutiveStations(current.CodeStation, prev.CodeStation).TimeDriving
                    });
                }
                else//במקרה שזו התחנה הראשונה, נאתחל את המרחק והזמן לאפסים
                {
                    listStationTypeCorrect.Add(new StationInLineBO
                    {
                        CodeStation = lineStationNext.CodeStation,
                        NumStationInTheLine = lineStationNext.NumStationInTheLine,
                        NameStation = dal.getOneObjectBusStationDAO(lineStationNext.CodeStation).NameStation,
                        Distance = 0,
                        TimeDriving = 0
                    });
                }
                first = false;
            }
            busLineBO.ListOfStations = listStationTypeCorrect;
            return busLineBO;
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
        public bool addBusLine(BusLineBO busLine)//הוספת קו אינה מאפשרת הוספת רשימת תחנות אוטומטית אלא רק ראשונה ואחרונה. אם רוצים להוסיף תחנות זה דרך מתודת הוספת תחנה לקו
        {
            bool result;
            try
            {
                if (!dal.getAllBusStations().Any(mishehu => mishehu.CodeStation == busLine.FirstStationNum) || !dal.getAllBusStations().Any(mishehu => mishehu.CodeStation == busLine.LastStationNum))//בדיקה שהתחנות להוספה אכן קיימות במערכת
                {
                    throw new BusStationExceptionBO("אחת התחנות או שתיהן אינן קיימות במערכת ולכן אי אפשר להוסיף אותן לקו");
                }
                result = dal.addBusLine(convertDAO(busLine));
                
            }
            catch (DO.BusLineExceptionDO ex)
            {
                throw new BO.BusLineExceptionBO("Identify-Number-Line exists allready", ex);
            }
            return result;
        }
        public bool updateBusLine(BusLineBO busLine)//עדכון השדות הפשוטים עבור קו. אם רוצים לבצע שינוי בתחנות שלו, כלומר להוסיף או להוריד זה במתודות הוספה ומחיקה של תחנה מקו
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
        public bool deleteBusLine(BusLineBO busLine)
        {
            try
            {
                ////////צריך למחוק את כל התחנות קו שמקושרות לקו הזה
                List<LineStationDAO> stationsInLine = new List<LineStationDAO>();
                foreach (LineStationDAO lineStation in dal.getPartOfLineStations(item => item.IdentifyNumber == busLine.IdentifyNumber))
                {
                    stationsInLine.Add(lineStation);
                }
                foreach (LineStationDAO lineStation in stationsInLine)
                {
                    dal.deleteLineStation(lineStation);//מחיקת התחנות של הקו מהמאגר
                }
                
            }
            catch (DO.LineStationExceptionDO ex)
            {
                //לא אכפת לנו אם אין תחנות לקו. פשוט לא יהיה מה למחוק
            }
            try { dal.deleteBusLine(convertDAO(busLine)); }
            catch (DO.BusLineExceptionDO ex)
            {
                throw new BO.BusLineExceptionBO("Does not exist in the system", ex);
            }
            return true;
            
        }
        public void delStationToLine(int codeStation, int identifyNumber)//מחיקת תחנה קיימת מקו קיים
        {
            BusStationDAO busStationDAO;
            try
            {
                busStationDAO = dal.getOneObjectBusStationDAO(codeStation);
            }
            catch (DO.BusStationExceptionDO ex)
            {
                throw new BO.BusStationExceptionBO("Code Station number not found", ex);
            }
            BusLineDAO busLineDAO;
            try
            {
                busLineDAO = dal.getOneObjectBusLineDAO(identifyNumber);
            }
            catch (DO.BusLineExceptionDO ex)
            {
                throw new BO.BusLineExceptionBO("Identify Number not found", ex);
            }
            bool flag = true;//  דגל שיהיה אמת אם התחנה קיימת בקו ואפשר למחוק אותה, אחרת יהיה שקר
            int location = 0;
            foreach (LineStationDAO lineStation in dal.getPartOfLineStations(item => item.IdentifyNumber == identifyNumber))//בדיקה אם התחנה קיימת בקו
            {
                if (lineStation.CodeStation == codeStation && lineStation.IdentifyNumber == identifyNumber)
                {
                    location = lineStation.NumStationInTheLine;
                    flag = true;
                    break;
                }
                else flag = false;
            }
            if (flag== false) throw new BO.BusLineExceptionBO("The station does not exist on the line so it cannot be deleted");
            int countStations = 0;//כמה תחנות יש לקו לפני המחיקה
            foreach (LineStationDAO lineStation in dal.getPartOfLineStations(item => item.IdentifyNumber == identifyNumber))//בדיקה אם המיקום הגיוני ולא גדול יותר מידי
            {
                countStations++;
            }
            //צריך להזיז את כל התחנות שאחרי המיקום אחד אחורה אם ישנן
            List<LineStationDAO> stationsInLine = new List<LineStationDAO>();
            foreach (LineStationDAO lineStation in dal.getPartOfLineStations(item => item.IdentifyNumber == identifyNumber))
            {
                stationsInLine.Add(lineStation);
            }
            foreach (LineStationDAO lineStation in stationsInLine)
            {
                dal.deleteLineStation(lineStation);//מחיקת התחנות של הקו מהמאגר על מנת להכניס חדשות במיקום נכון
            }
            foreach (LineStationDAO lineStation in stationsInLine)
            {
                if (lineStation.NumStationInTheLine > location)
                {
                    dal.addLineStation(new LineStationDAO { CodeStation = lineStation.CodeStation, IdentifyNumber = lineStation.IdentifyNumber, NumStationInTheLine = lineStation.NumStationInTheLine-1 });
                }
                if (lineStation.NumStationInTheLine < location)
                {
                    dal.addLineStation(new LineStationDAO { CodeStation = lineStation.CodeStation, IdentifyNumber = lineStation.IdentifyNumber, NumStationInTheLine = lineStation.NumStationInTheLine });
                }
            }
            foreach (LineStationDAO lineStation in dal.getPartOfLineStations(item => item.IdentifyNumber == identifyNumber))
            {
                if (lineStation.NumStationInTheLine==1)
                {
                    bool a = updateBusLine(new BusLineBO
                    {
                        IdentifyNumber = GetBusLineBO(identifyNumber).IdentifyNumber,
                        LineNumber = GetBusLineBO(identifyNumber).LineNumber,
                        Area = GetBusLineBO(identifyNumber).Area,
                        FirstStationNum = lineStation.CodeStation,
                        LastStationNum = GetBusLineBO(identifyNumber).LastStationNum
                    });
                }
                if (lineStation.NumStationInTheLine == countStations -1)
                {
                    bool a = updateBusLine(new BusLineBO
                    {
                        IdentifyNumber = GetBusLineBO(identifyNumber).IdentifyNumber,
                        LineNumber = GetBusLineBO(identifyNumber).LineNumber,
                        Area = GetBusLineBO(identifyNumber).Area,
                        FirstStationNum = GetBusLineBO(identifyNumber).FirstStationNum,
                        LastStationNum = lineStation.CodeStation
                    });
                }
            }
        }
        public void addStationToLine(int codeStation, int identifyNumber, int location)//הוספת תחנה קיימת לקו קיים
        {
            if (location==0)
                throw new BO.BusLineExceptionBO("The location is incorrect");//אפשר להכניס מיקום מאחד והלאה
            BusStationDAO busStationDAO;
            try
            {
                busStationDAO = dal.getOneObjectBusStationDAO(codeStation);
            }
            catch (DO.BusStationExceptionDO ex)
            {
                throw new BO.BusStationExceptionBO("Code Station number not found", ex);
            }
            BusLineDAO busLineDAO;
            try
            {
                busLineDAO = dal.getOneObjectBusLineDAO(identifyNumber);
            }
            catch (DO.BusLineExceptionDO ex)
            {
                throw new BO.BusLineExceptionBO("Identify Number not found", ex);
            }
            foreach(LineStationDAO lineStation in dal.getPartOfLineStations(item => item.IdentifyNumber == identifyNumber))//בדיקה אם התחנה כבר קיימת בקו
            {
                if (lineStation.CodeStation == codeStation && lineStation.IdentifyNumber == identifyNumber)
                    throw new BO.BusLineExceptionBO("The station already exists on this line");
            }
            int countStations=0;//כמה תחנות יש לקו
            foreach (LineStationDAO lineStation in dal.getPartOfLineStations(item => item.IdentifyNumber == identifyNumber))//בדיקה אם המיקום הגיוני ולא גדול יותר מידי
            {
                    countStations++;
            }
            if(countStations+1< location)//אם המיקום גדול מידי
                throw new BO.BusLineExceptionBO("The location sent is invalid. You must enter a location as the number of stations on the line or one more.");
            if (location==1)//אם התחנה שמתווספת היא הראשונה צריך לשנות את זה בשדה של תחנה ראשונה
            {
                //GetBusLineBO(identifyNumber)
                bool a = updateBusLine(new BusLineBO {
                    IdentifyNumber= GetBusLineBO(identifyNumber).IdentifyNumber,
                    LineNumber= GetBusLineBO(identifyNumber).LineNumber,
                    Area= GetBusLineBO(identifyNumber).Area,
                    FirstStationNum= codeStation,
                    LastStationNum= GetBusLineBO(identifyNumber).LastStationNum
                });
            }
            if (location == countStations+1)//אם התחנה שמתווספת היא האחרונה צריך לשנות את זה בשדה של תחנה אחרונה
            {
                //GetBusLineBO(identifyNumber)
                bool a = updateBusLine(new BusLineBO
                {
                    IdentifyNumber = GetBusLineBO(identifyNumber).IdentifyNumber,
                    LineNumber = GetBusLineBO(identifyNumber).LineNumber,
                    Area = GetBusLineBO(identifyNumber).Area,
                    FirstStationNum = GetBusLineBO(identifyNumber).FirstStationNum,
                    LastStationNum = codeStation
                });
            }
            //צריך להזיז את כל התחנות שאחרי המיקום אחד קדימה אם ישנן
            List<LineStationDAO> stationsInLine = new List<LineStationDAO>();
            foreach (LineStationDAO lineStation in dal.getPartOfLineStations(item => item.IdentifyNumber == identifyNumber))
            {
                stationsInLine.Add(lineStation);
            }
            foreach (LineStationDAO lineStation in stationsInLine)
            {
                dal.deleteLineStation(lineStation);//מחיקת התחנות של הקו מהמאגר על מנת להכניס חדשות במיקום נכון
            }
            foreach (LineStationDAO lineStation in stationsInLine)
            {
                if (lineStation.NumStationInTheLine>=location)
                {
                    dal.addLineStation(new LineStationDAO {CodeStation= lineStation.CodeStation, IdentifyNumber= lineStation.IdentifyNumber, NumStationInTheLine= lineStation.NumStationInTheLine + 1 });
                }
                else
                {
                    dal.addLineStation(new LineStationDAO { CodeStation = lineStation.CodeStation, IdentifyNumber = lineStation.IdentifyNumber, NumStationInTheLine = lineStation.NumStationInTheLine });
                }
            }
            dal.addLineStation(new LineStationDAO { CodeStation = codeStation, IdentifyNumber = identifyNumber, NumStationInTheLine = location });
        }
        #endregion
        //אוטובוסים
        #region buses
        private BusDAO convertDAO(BusBO bus)
        {
            BusDAO busDAO = new BusDAO
            {
                License = bus.License,//Int32.Parse(bus.License)
                StartOfWork = bus.StartOfWork,
                TotalKms = bus.TotalKms,
                Fuel = bus.Fuel,
                DateTreatLast = bus.DateTreatLast,
                KmFromTreament = bus.KmFromTreament,
                //Status = (bus.Status == true) ? Status.READY : Status.REFUELLING
                Status = (DO.Status)bus.Status
            };
            return busDAO;
        }

        private BusBO convertoBO(BusDAO bus)
        {
            return new BusBO
            {
                License = bus.License,
                StartOfWork = bus.StartOfWork,
                TotalKms = bus.TotalKms,
                Fuel = bus.Fuel,
                DateTreatLast = bus.DateTreatLast,
                KmFromTreament = bus.KmFromTreament,
                Status = (BO.Status)bus.Status
            };
        }
        //הדפסת כל האוטבוסים
        public IEnumerable<BusBO> GetAllBusesBO()
        {
            return from bus in dal.getAllBuses()
                   select convertoBO(bus);
        }
        //קבלת פרטי אוטובוס בודד
        public BusBO GetBusBO(string license)
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
                if (bus.License.Length < 7 || bus.License.Length > 8)
                    throw new BusExceptionBO("מספר הרישוי ארוך או קצר מידי. הכנס מספר באורך תקין");
                if (bus.StartOfWork.Year < 2018 && bus.License.Length > 7)
                    throw new BusExceptionBO("מספר הרישוי ארוך מידי ואיננו תואם לתאריך הרישוי");
                if (bus.StartOfWork.Year >= 2018 && bus.License.Length <8)
                    throw new BusExceptionBO("מספר הרישוי קצר מידי ואיננו תואם לתאריך הרישוי");
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
        public bool deleteBus(BusBO bus)
        {
            bool result;
            try
            {
                result=dal.deleteBus(convertDAO(bus));
            }
            catch (DO.BusExceptionDO ex)
            {
                throw new BO.BusExceptionBO("Does not exist in the system", ex);
            }
            return result;
        }
        //שליחת אוטובוס לטיפול ותדלוק
        public void refuel(string license)
        {
            IEnumerable<BusDAO> buses = dal.getAllBuses();
            if (!buses.Any(item => item.License == license))
            {
                throw new ArgumentNullException("bus not found");
            }
            BusDAO busDAO=new BusDAO();
            foreach (BusDAO bus in buses)
            {
                if (bus.License == license)
                    busDAO = bus;
            }
            if (busDAO.Fuel == FULLTANK)
            {
                throw new BO.BusExceptionBO("tank full allready");
            }
            busDAO.Status = DO.Status.REFUELLING;
            //אחרי זה צריך לשנות את הסטטוס בחזרה למוכן
            busDAO.Fuel = FULLTANK;//התדלוק עצמו
            dal.updateBus(busDAO);
        }
        public void treatment(string license)
        {
            IEnumerable<BusDAO> buses = dal.getAllBuses();
            if (!buses.Any(item => item.License == license))
            {
                throw new ArgumentNullException("bus not found");
            }
            BusDAO busDAO = new BusDAO();
            foreach (BusDAO bus in buses)
            {
                if (bus.License == license)
                    busDAO = bus;
            }
            busDAO.Status = DO.Status.SERVICE;
            busDAO.DateTreatLast = DateTime.Now;
            busDAO.KmFromTreament = 0;
            dal.updateBus(busDAO);
        }
        #endregion
        //תחנות
        #region stations
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
            try
            {
                IEnumerable<LineStationDAO> listLineStations = dal.getPartOfLineStations(item => item.CodeStation == busStation.CodeStation);//רשימה של תחנות קו המתאימות לתחנה הזאת
                IEnumerable<LineInStationBO> listOfLineInStation =
                from lineStation in listLineStations
                from BusLine1 in dal.getAllBusLines()
                where lineStation.IdentifyNumber == BusLine1.IdentifyNumber
                let result = new LineInStationBO { LineNumber = BusLine1.LineNumber, LastStationName =dal.getOneObjectBusStationDAO(BusLine1.LastStationNum).NameStation, LastStationNum= BusLine1.LastStationNum }
                select result;
                busStationBO.ListOfLines = listOfLineInStation;
                return busStationBO;
            }
            catch//אין קווים שעוברים בתחנה
            {
                busStationBO.ListOfLines = null;
                return busStationBO;
            }
        }
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
                /////////בהוספת תחנה לא מוסיפים לה קווים מיד. אם רוצים להוסיף תחנה לקו עושים את זה דרך "הוספת תחנה לקו"
                result = dal.addBusStation(convertDAO(busStation));
            }
            catch (DO.BusStationExceptionDO ex)
            {
                throw new BO.BusStationExceptionBO("Code station exists allready", ex);
            }
            return result;
        }
        public bool updateBusStation(BusStationBO busStation)//עדכון השדות הפשוטים של תחנה. בכל מקרה של גריעת תחנה מקו או הוספה אליו יש מתודות ייעודיות
        {
            bool result;
            try
            {
                result = dal.updateBusStation(convertDAO(busStation));
            }
            catch (DO.BusStationExceptionDO ex)
            {
                throw new BO.BusStationExceptionBO("The Code Station number " + busStation.CodeStation + " not found", ex);
            }
            return result;
        }
        public bool deleteBusStation(BusStationBO busStation)//אפשר למחוק תחנה רק אם אף קו לא עובר בה
        {
            try
            {
                ////////ייתכן שצריך למחוק את כל התחנות קו שמקושרות לתחנה הזאת
                //foreach (BusLineBO busLine in busStation.ListOfLines)
                //{
                //    dal.deleteLineStation(new LineStationDAO
                //    {
                //        CodeStation = busStation.CodeStation,
                //        IdentifyNumber = busLine.IdentifyNumber
                //    });
                //}
                foreach(LineStationDAO lineStation in dal.getAllLineStations())
                {
                    if (lineStation.CodeStation== busStation.CodeStation)//יש קו שמקושר לתחנה הזאת אז אי אפשר למחוק אותה
                        throw new BO.BusStationExceptionBO("There are lines that pass through this station, so it is not possible to delete it.");
                }
                dal.deleteBusStation(convertDAO(busStation));
                return true;
            }
            catch (DO.BusStationExceptionDO ex)
            {
                throw new BO.BusStationExceptionBO("Does not exist in the system", ex);
            }

        }
        #endregion
        //עדכון מרחק וזמן נסיעה בין זוג תחנות עוקבות
        public void updatePairConsecutiveStations(int numStation1, int numStation2,int distance, int timeDriving)
        {
            PairConsecutiveStationsDAO forNow = new PairConsecutiveStationsDAO
            {
                StationNum1 = numStation1,
                StationNum2 = numStation2,
                Distance = distance,
                TimeDriving = timeDriving
            };
            dal.updatePairConsecutiveStations(forNow);
        }
    }
}

