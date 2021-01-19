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
        #region singleton implementaion
        private readonly static BLIMP instance = new BLIMP();

        public BLIMP() { } // בנאי ברירת מחדל 
        static BLIMP() { }  // בנאי שמוודא אתחול של אינטנס לפני השימוש הראשון  

        internal static BLIMP Instance { get => instance; }

        #endregion singleton
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
            foreach (LineStationDAO lineStationNext in listStationInLineOrder)
            {
                prev = current;//מקוות שזה המקום הנכון
                current = lineStationNext;
                if (!first)
                {
                    StationInLineBO forNow = new StationInLineBO();
                    forNow.CodeStation = lineStationNext.CodeStation;
                    forNow.NumStationInTheLine = lineStationNext.NumStationInTheLine;
                    forNow.NameStation = dal.getOneObjectBusStationDAO(lineStationNext.CodeStation).NameStation;
                    TimeSpan count = new TimeSpan(0, 0, 0);
                    for (int i = forNow.NumStationInTheLine; i > 1; i--)//חישוב זמן הנסיעה של התחנה הנוכחית מתחנת המוצא
                    {
                        count += dal.getOneObjectPairConsecutiveStations(listStationInLineOrder.ToArray()[i-1].CodeStation, listStationInLineOrder.ToArray()[i - 2].CodeStation).TimeDriving;
                    }
                    forNow.TimeDrivingFromFirstStation = count;
                    if (dal.getOneObjectPairConsecutiveStations(current.CodeStation, prev.CodeStation) != null)
                    {
                        forNow.Distance = dal.getOneObjectPairConsecutiveStations(current.CodeStation, prev.CodeStation).Distance;
                        forNow.TimeDriving = dal.getOneObjectPairConsecutiveStations(current.CodeStation, prev.CodeStation).TimeDriving;
                    }
                    else
                    {
                        forNow.Distance = 0;
                        forNow.TimeDriving = new TimeSpan(0,0,0);
                    }
                    //חישוב זמן הנסיעה של התחנה הזאת מתחנת המוצא
                    listStationTypeCorrect.Add(forNow);
                }
                else//במקרה שזו התחנה הראשונה, נאתחל את המרחק והזמן לאפסים
                {
                    listStationTypeCorrect.Add(new StationInLineBO
                    {
                        CodeStation = lineStationNext.CodeStation,
                        NumStationInTheLine = lineStationNext.NumStationInTheLine,
                        NameStation = dal.getOneObjectBusStationDAO(lineStationNext.CodeStation).NameStation,
                        Distance = 0,
                        TimeDriving = new TimeSpan(0, 0, 0),
                        TimeDrivingFromFirstStation= new TimeSpan(0, 0, 0)
                    });
                }
                first = false;
            }
            busLineBO.ListOfStations = listStationTypeCorrect;
            IEnumerable<LineTripBO> trips=
                from trip in dal.getPartOfLineTrip(item => item.IdentifyNumber == busLine.IdentifyNumber)
                orderby trip.TripStart
                select convertoBO(trip);
            busLineBO.ListOfTrips = trips;
            return busLineBO;
        }
        //הדפסת כל הקווים
        public IEnumerable<BusLineBO> GetAllBusLinesBO()
        {
            return from busLine in dal.getAllBusLines()
                   orderby busLine.LineNumber
                   select convertoBO(busLine);
        }
        public IEnumerable<LineInAreaBO> orderLinesByArea()//ממיינת את הקווים לפי איזורים
        {
            IEnumerable<LineInAreaBO> result =
                from line in GetAllBusLinesBO()
                group line by line.Area into lineInArea
                select new LineInAreaBO
                {
                    Key = lineInArea.Key,
                    ListOfLinesInArea = lineInArea
                };
            return result;
            //foreach (var group in result)
            //{
            //    Console.WriteLine(group.key);
            //    foreach (var line in group.Line)
            //    {
            //        Console.WriteLine("-"+ line);
            //    }
            //}
        }
        public IEnumerable<MiniStationBO> GetListMiniStationsByLine(BusLineBO line)//מחזירה את רשימת המיני תחנות של קו ספציפי
        {
            IEnumerable<MiniStationBO> result =
                from station in line.ListOfStations
                select new MiniStationBO { CodeStation = station.CodeStation, NameStation = station.NameStation };
            return result;
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
                //var numStationsInLine =
                //from lineStation in dal.getPartOfLineStations(item => item.IdentifyNumber == busLine.IdentifyNumber)
                //select new { numStation = lineStation.CodeStation };
                //foreach (var obj in numStationsInLine)
                //{
                //    dal.deleteLineStation(new LineStationDAO {IdentifyNumber= busLine.IdentifyNumber, CodeStation= obj.numStation });//מחיקת התחנות של הקו מהמאגר
                //}
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
        public void chekIfCanToDelStationFromLine(int codeStation, int identifyNumber)
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
            int sumStationsInLine = 0;
            foreach (LineStationDAO lineStation in dal.getPartOfLineStations(item => item.IdentifyNumber == identifyNumber))//סופר כמה תחנות יש בקו 
            {
                sumStationsInLine++;
            }
            if (sumStationsInLine==2)
                throw new BO.BusLineExceptionBO("לא ניתן למחוק תחנה מקו כאשר מסלולו עובר בשתי תחנות בלבד. אם ברצונך למחוק את הקו, בצע זאת דרך הכפתור הייעודי");
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
            if (flag == false) throw new BO.BusLineExceptionBO("The station does not exist on the line so it cannot be deleted");
        }
        public PairConsecutiveStationsBO ifNeedToGetDataBetweenTwoStation(int identifyNumber, int codeStation)
        {
            int location = 0;
            foreach (LineStationDAO lineStation in dal.getPartOfLineStations(item => item.IdentifyNumber == identifyNumber))
            {
                if (lineStation.CodeStation == codeStation && lineStation.IdentifyNumber == identifyNumber)
                {
                    location = lineStation.NumStationInTheLine;
                }
            }
            int countStations = 0;//כמה תחנות יש לקו לפני המחיקה
            foreach (LineStationDAO lineStation in dal.getPartOfLineStations(item => item.IdentifyNumber == identifyNumber))//כמה תחנות יש לקו
            {
                countStations++;
            }
            if (location == 1 || location == countStations)
                return null;//לא צריך לקבל מידע חדש על זוג חדש כי אין כזה
            int numStationBefore = 0;
            int numStationAfter = 0;
            foreach (var station in GetBusLineBO(identifyNumber).ListOfStations)
            {
                if (station.NumStationInTheLine == location - 1)
                    numStationBefore = station.CodeStation;
                if (station.NumStationInTheLine == location + 1)
                    numStationAfter = station.CodeStation;
            }
            if (GetPairConsecutiveStationsBO(numStationBefore, numStationAfter) == null)
                return new PairConsecutiveStationsBO {StationNum1= numStationBefore , StationNum2= numStationAfter };
            return null;//במידה וכבר קיים מידע על זוג התחנות הללו
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
        public void chekIfCanToddStationToLine(int codeStation, int identifyNumber,int location)
        {
            if (location == 0)
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
            foreach (LineStationDAO lineStation in dal.getPartOfLineStations(item => item.IdentifyNumber == identifyNumber))//בדיקה אם התחנה כבר קיימת בקו
            {
                if (lineStation.CodeStation == codeStation && lineStation.IdentifyNumber == identifyNumber)
                    throw new BO.BusLineExceptionBO("The station already exists on this line");
            }
            int countStations = 0;//כמה תחנות יש לקו
            foreach (LineStationDAO lineStation in dal.getPartOfLineStations(item => item.IdentifyNumber == identifyNumber))//בדיקה אם המיקום הגיוני ולא גדול יותר מידי
            {
                countStations++;
            }
            if (countStations + 1 < location)//אם המיקום גדול מידי
                throw new BO.BusLineExceptionBO("The location sent is invalid. You must enter a location as the number of stations on the line or one more.");
        }
        public int ifNeedToGetDataToBeforeStation(int identifyNumber, int codeStation, int location)
        {
            int numStationBefore = 0;
            foreach (var station in GetBusLineBO(identifyNumber).ListOfStations)
            {
                if (station.NumStationInTheLine == location - 1)
                    numStationBefore = station.CodeStation;
            }
            if (GetPairConsecutiveStationsBO(numStationBefore, codeStation) == null)
                return numStationBefore;
            return 0;
        }
        public int ifNeedToGetDataToAfterStation(int identifyNumber, int codeStation, int location)
        {
            int numStationAfter = 0;
            foreach (var station in GetBusLineBO(identifyNumber).ListOfStations)
            {
                if (station.NumStationInTheLine == location)
                    numStationAfter = station.CodeStation;
            }
            if (GetPairConsecutiveStationsBO(numStationAfter, codeStation) == null)
                return numStationAfter;
            return 0;
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
            BusBO result = new BusBO
            {
                License = bus.License,
                StartOfWork = bus.StartOfWork,
                TotalKms = bus.TotalKms,
                Fuel = bus.Fuel,
                DateTreatLast = bus.DateTreatLast,
                KmFromTreament = bus.KmFromTreament,
                Status = (BO.Status)bus.Status
            };
            if (bus.StartOfWork.Year > 2017)//8 ספרות numbers
            {
                result.LicenseFormat = bus.License.Substring(0, 3) + "-" + bus.License[3] + bus.License[4] + "-" + bus.License[5] + bus.License[6] + bus.License[7];

            }
            else//7 ספרות numbers
            {
                result.LicenseFormat = bus.License.Substring(0, 2) + "-" + bus.License[2] + bus.License[3] + bus.License[4] + "-" + bus.License[5] + bus.License[6];
            }
            return result;
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
                let result = new LineInStationBO 
                { 
                    IdentifyNumber= BusLine1.IdentifyNumber,
                    LineNumber = BusLine1.LineNumber, 
                    LastStationName =dal.getOneObjectBusStationDAO(BusLine1.LastStationNum).NameStation, 
                    LastStationNum= BusLine1.LastStationNum 
                }
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
            return from busStation in dal.getAllBusStations()
                   orderby busStation.CodeStation
                   select convertoBO(busStation);
        }
        public IEnumerable<MiniStationBO> GetAllMiniStationsBO()
        {
            IEnumerable<MiniStationBO> result =
                from station in GetAllBusStationsBO()
                select new MiniStationBO { CodeStation = station.CodeStation, NameStation = station.NameStation };
            return result;
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
                //foreach(LineStationDAO lineStation in dal.getAllLineStations())
                //{
                //    if (lineStation.CodeStation== busStation.CodeStation)//יש קו שמקושר לתחנה הזאת אז אי אפשר למחוק אותה
                //        throw new BO.BusStationExceptionBO("There are lines that pass through this station, so it is not possible to delete it.");
                //}
                dal.deleteBusStation(convertDAO(busStation));
                return true;
            }
            catch (DO.BusStationExceptionDO ex)
            {
                throw new BO.BusStationExceptionBO("Does not exist in the system", ex);
            }

        }
        #endregion
        //זוג תחנות עוקבות
        #region pairs
        private PairConsecutiveStationsDAO convertDAO(PairConsecutiveStationsBO pair)
        {
            PairConsecutiveStationsDAO pairConsecutiveStationsDAO = new PairConsecutiveStationsDAO
            {
                StationNum1 = pair.StationNum1,
                StationNum2 = pair.StationNum2,
                Distance = pair.Distance,
                TimeDriving = pair.TimeDriving,
            };
            return pairConsecutiveStationsDAO;
        }
        private PairConsecutiveStationsBO convertoBO(PairConsecutiveStationsDAO pair)
        {
            PairConsecutiveStationsBO pairConsecutiveStationsBO = new PairConsecutiveStationsBO
            {
                StationNum1 = pair.StationNum1,
                StationNum2 = pair.StationNum2,
                Distance = pair.Distance,
                TimeDriving = pair.TimeDriving,
            };
            return pairConsecutiveStationsBO;
        }
        //קבלת פרטי זוג תחנות בודד
        public PairConsecutiveStationsBO GetPairConsecutiveStationsBO(int stationNum1, int stationNum2)
        {
            PairConsecutiveStationsBO result = new PairConsecutiveStationsBO();
            PairConsecutiveStationsDAO PairConsecutiveStationsDAO;
            if (dal.getOneObjectPairConsecutiveStations(stationNum1, stationNum2) != null)
            {
                PairConsecutiveStationsDAO = dal.getOneObjectPairConsecutiveStations(stationNum1, stationNum2);
            }
            else
                return null;//אין מידע על הזוג הזה
            result = convertoBO(PairConsecutiveStationsDAO);
            return result;
        }
        //הוספה, עדכון ומחיקת זוג תחנות
        public bool addPairConsecutiveStations(PairConsecutiveStationsBO pair)
        {
            bool result;
            try
            {
                result = dal.addPairConsecutiveStations(convertDAO(pair));
            }
            catch (DO.PairConsecutiveStationsExceptionDO ex)
            {
                throw new BO.PairConsecutiveStationsExceptionBO("זוג התחנות כבר קיים", ex);
            }
            return result;
        }
        public bool updatePairConsecutiveStations(PairConsecutiveStationsBO pair)
        {
            bool result;
            try
            {
                result = dal.updatePairConsecutiveStations(convertDAO(pair));
            }
            catch (DO.PairConsecutiveStationsExceptionDO ex)
            {
                throw new BO.PairConsecutiveStationsExceptionBO("זוג התחנות לא נמצא", ex);
            }
            return result;
        }
        public bool deletePairConsecutiveStations(PairConsecutiveStationsBO pair)
        {
            bool result;
            try
            {
                result = dal.deletePairConsecutiveStations(convertDAO(pair));
            }
            catch (DO.PairConsecutiveStationsExceptionDO ex)
            {
                throw new BO.PairConsecutiveStationsExceptionBO("Does not exist in the system", ex);
            }
            return result;
        }
        public IEnumerable<PairConsecutiveStationsBO> GetPairThatConnect(int codeStation)
        {
            try
            {
                IEnumerable<PairConsecutiveStationsDAO> listPairs = dal.getPartOfPairConsecutiveStations(item => item.StationNum1 == codeStation || item.StationNum2 == codeStation);//רשימה של תחנות עוקבות המתאימות לתחנה הזאת
                IEnumerable<PairConsecutiveStationsBO> result =
                    from pair in listPairs
                    select convertoBO(pair);
                return result;
            }
            catch (DO.PairConsecutiveStationsExceptionDO ex)
            {
                throw new BO.PairConsecutiveStationsExceptionBO("There is no pair of stations that meets the condition", ex);
            }
            
        }
        //עדכון מרחק וזמן נסיעה בין זוג תחנות עוקבות
        public void updatePairConsecutiveStations(int numStation1, int numStation2, double distance, TimeSpan timeDriving)
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
        #endregion
        //משתמשים
        #region users
        private UserDAO convertDAO(UserBO user)
        {
            UserDAO userDAO = new UserDAO
            {
                UserName = user.UserName,
                PassWord = user.PassWord,
                CheckAsk = user.CheckAsk
            };
            return userDAO;
        }

        private UserBO convertoBO(UserDAO user)
        {
            return new UserBO
            {
                UserName = user.UserName,
                PassWord = user.PassWord,
                CheckAsk = user.CheckAsk
            };
        }
        public IEnumerable<UserBO> GetAllUsersBO()//הדפסת כל המשתמשים
        {
            return from user in dal.getAllUsers()
                   select convertoBO(user);
        }
        public UserBO GetUserBO(string userName)//קבלת פרטי משתמש בודד
        {
            UserBO result = new UserBO();
            UserDAO userDAO;
            try
            {
                userDAO = dal.getOneObjectUserDAO(userName);
            }
            catch (DO.UserExceptionDO ex)
            {
                throw new BO.UserExceptionBO("userName not found", ex);
            }
            result = convertoBO(userDAO);
            return result;
        }
        //הוספה, עדכון ומחיקת משתמש
        public bool addUser(UserBO user)
        {
            bool result;
            try
            {
                result = dal.addUser(convertDAO(user));
            }
            catch (DO.UserExceptionDO ex)
            {
                throw new BO.UserExceptionBO("userName exists allready", ex);
            }
            return result;
        }
        public bool updateUser(UserBO user)
        {
            bool result;
            try
            {
                result = dal.updateUser(convertDAO(user));
            }
            catch (DO.UserExceptionDO ex)
            {
                throw new BO.UserExceptionBO("The userName " + user.UserName + " not found", ex);
            }
            return result;
        }
        public bool deleteUser(UserBO user)
        {
            bool result;
            try
            {
                result = dal.deleteUser(convertDAO(user));
            }
            catch (DO.UserExceptionDO ex)
            {
                throw new BO.UserExceptionBO("Does not exist in the system", ex);
            }
            return result;
        }
        public string forgetPassWord(string userName, string checkAsk)//שחזור סיסמה לפי שם משתמש ושאלת אימות
        {
            UserDAO user1 = dal.getAllUsers().ToList().Find(p => p.UserName == userName && p.CheckAsk == checkAsk);

            if (user1 != null)
                return user1.PassWord;
            else
                throw new BO.UserExceptionBO("שם המשתמש אינו קיים במערכת ו/או שאלת האימות שהזנת אינם תואמים");
        }
        public bool ifUserAndPassCorrect(string userName, string passWord)
        {
            UserDAO user1 = dal.getAllUsers().ToList().Find(p => p.UserName == userName && p.PassWord == passWord);

            if (user1 != null)
                return true;
            else
                throw new BO.UserExceptionBO("אחד או יותר מהשדות שהזנת שגויים");
        }
        #endregion
        //פונקציות עבור לוח אלקטרוני
        public IEnumerable<LineTimingBO> GetLineTimingsPerStation(BusStationBO cuurentStation, TimeSpan now)
        {
            List<LineTimingBO> result = new List<LineTimingBO>();
            foreach (LineInStationBO line in cuurentStation.ListOfLines)//נעבור על כל הקווים שעוברים בתחנה
            {
                LineTimingBO lineTiming = new LineTimingBO();
                lineTiming.IdentifyNumber = line.IdentifyNumber;
                lineTiming.LineNumber = line.LineNumber;
                lineTiming.LastStationName = line.LastStationName;
                BusLineBO curLine = GetBusLineBO(line.IdentifyNumber);
                TimeSpan TimeTripFromStart = new TimeSpan(0, 0, 0);
                foreach (StationInLineBO stationInLine in curLine.ListOfStations)//חישוב כמה זמן לוקח לקו הספציפי להגיע לתחנה שלנו
                {
                    if (stationInLine.CodeStation == cuurentStation.CodeStation)
                        TimeTripFromStart = stationInLine.TimeDrivingFromFirstStation;
                }
                try
                {
                    //נוצרת רשימה של יציאות הקו הרלוונטיות, כלומר שייכות לקו הנוכחי ועוברות בתחנה בחצי השעה הקרובה
                    List<LineTripDAO> relevantTripToLine = dal.getPartOfLineTrip(item => item.IdentifyNumber == line.IdentifyNumber && item.TripStart + TimeTripFromStart > now && item.TripStart + TimeTripFromStart < now + new TimeSpan(0, 30, 0)).ToList();
                    foreach (LineTripDAO lineTrip in relevantTripToLine)
                    {
                        lineTiming.TripStart = lineTrip.TripStart;
                        lineTiming.ExpectedTimeTillArrive = lineTrip.TripStart + TimeTripFromStart;
                        lineTiming.MoreHowMinutesCome = (lineTiming.ExpectedTimeTillArrive-now).Minutes;
                        result.Add(lineTiming);
                    }
                }
                catch { }//במקרה של תחנה שלא עוברים בה קווים בחצי השעה הקרובה
            }
            return result;
        }
        private LineTripDAO convertDAO(LineTripBO lineTrip)
        {
            LineTripDAO lineTripDAO = new LineTripDAO
            {
                IdentifyNumber = lineTrip.IdentifyNumber,
                TripStart = lineTrip.TripStart,
            };
            return lineTripDAO;
        }

        private LineTripBO convertoBO(LineTripDAO lineTrip)
        {
            LineTripBO result = new LineTripBO
            {
                IdentifyNumber = lineTrip.IdentifyNumber,
                TripStart = lineTrip.TripStart,
            };
            return result;
        }
        public bool addLineTrip(LineTripBO lineTrip)
        {
            bool result;
            try
            {
                result = dal.addLineTrip(convertDAO(lineTrip));
            }
            catch (DO.LineTripExceptionDO ex)
            {
                throw new LineTripExceptionBO("The line exit already exists", ex);
            }
            return result;
        }
        public bool deleteLineTrip(LineTripBO lineTrip)
        {
            bool result;
            try
            {
                result = dal.deleteLineTrip(convertDAO(lineTrip));
            }
            catch (DO.LineTripExceptionDO ex)
            {
                throw new BO.LineTripExceptionBO("Does not exist in the system", ex);
            }
            return result;
        }
        public List<WayForPassBO> GetRelevantWays(int codeStation1, int codeStation2)
        {
            List<WayForPassBO> result = new List<WayForPassBO>();
            
            foreach (BusLineBO line in GetAllBusLinesBO())
            {
                bool ifFirstIn = false;
                int LocationFirst = 0;
                bool ifLastIn = false;
                int LocationLast = 0;
                foreach (var station in line.ListOfStations)
                {
                    if (station.CodeStation== codeStation1)
                    {
                        ifFirstIn = true;
                        LocationFirst = station.NumStationInTheLine;
                        break;
                    }
                }
                foreach (var station in line.ListOfStations)
                {
                    if (station.CodeStation == codeStation2)
                    {
                        ifLastIn = true;
                        LocationLast = station.NumStationInTheLine;
                        break;
                    }
                }
                if (ifFirstIn==true&& ifLastIn==true&& LocationFirst< LocationLast)//אם שתי התחנות קיימות בקו ותחנת המוצא לפני תחנת היעד
                {
                    TimeSpan count = new TimeSpan(0, 0, 0);
                    for (int i= LocationLast-1; i>= LocationFirst;i--)
                    {
                        count += line.ListOfStations.ToArray()[i].TimeDriving;
                    }
                    result.Add(new WayForPassBO { LineNumber=line.LineNumber,TimeOfTrip= count });
                }
            }
            IEnumerable<WayForPassBO> orderList =
                from way in result
                orderby way.TimeOfTrip
                select way;
            return orderList.ToList();
        }
    }
}

