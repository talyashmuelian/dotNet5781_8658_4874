using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace BLAPI
{
    public interface IBL
    {
        IEnumerable<BusLineBO> GetAllBusLinesBO();//הדפסת כל הקווים
        BusLineBO GetBusLineBO(int identifyNumber);//קבלת פרטים על קו בודד
        IEnumerable<LineInAreaBO> orderLinesByArea();
        //הוספה, עדכון ומחיקת קו
        bool addBusLine(BusLineBO busLine);
        bool updateBusLine(BusLineBO busLine);
        bool deleteBusLine(BusLineBO busLine);
        void chekIfCanToDelStationFromLine(int codeStation, int identifyNumber);
        PairConsecutiveStationsBO ifNeedToGetDataBetweenTwoStation(int identifyNumber, int codeStation);
        void delStationToLine(int codeStation, int identifyNumber);//מחיקת תחנה קיימת מקו קיים
        void chekIfCanToddStationToLine(int codeStation, int identifyNumber, int location);
        int ifNeedToGetDataToBeforeStation(int identifyNumber, int codeStation, int location);
        int ifNeedToGetDataToAfterStation(int identifyNumber, int codeStation, int location);
        void addStationToLine(int codeStation, int identifyNumber, int location);//הוספת תחנה קיימת לקו קיים
        IEnumerable<BusBO> GetAllBusesBO();//הדפסת כל האוטבוסים
        BusBO GetBusBO(string license);//קבלת פרטי אוטובוס בודד
        //הוספה, עדכון ומחיקת אוטובוס
        bool addBus(BusBO bus);
        bool updateBus(BusBO bus);
        bool deleteBus(BusBO bus);
        //שליחת אוטבוס לטיפול ותדלוק
        void refuel(string license);
        void treatment(string license);
        IEnumerable<BusStationBO> GetAllBusStationsBO();//הדפסת כל התחנות
        IEnumerable<MiniStationBO> GetAllMiniStationsBO();
        BusStationBO GetBusStationBO(int codeStation);//קבלת פרטים עבור תחנה מסוימת
        //הוספה עדכון ומחיקת תחנה
        bool addBusStation(BusStationBO busStation);//הוספת תחנה חדשה לגמרי שחייבת להיות לפחות בקו אחד
        bool updateBusStation(BusStationBO busStation);
        bool deleteBusStation(BusStationBO busStation);
        //זוג תחנות עוקבות
        PairConsecutiveStationsBO GetPairConsecutiveStationsBO(int stationNum1, int stationNum2);
        bool addPairConsecutiveStations(PairConsecutiveStationsBO pair);
        bool updatePairConsecutiveStations(PairConsecutiveStationsBO pair);
        bool deletePairConsecutiveStations(PairConsecutiveStationsBO pair);
        IEnumerable<PairConsecutiveStationsBO> GetPairThatConnect(int codeStation);
        void updatePairConsecutiveStations(int numStation1, int numStation2, int distance, int timeDriving);//עדכון מרחק וזמן נסיעה בין זוג תחנות עוקבות
        IEnumerable<UserBO> GetAllUsersBO();//הדפסת כל המשתמשים
        UserBO GetUserBO(string userName);//קבלת פרטי משתמש בודד
        //הוספה, עדכון ומחיקת משתמש
        bool addUser(UserBO user);
        bool updateUser(UserBO user);
        bool deleteUser(UserBO user);
        string forgetPassWord(string userName, string checkAsk);//שחזור סיסמה לפי שם משתמש ושאלת אימות
        bool ifUserAndPassCorrect(string userName, string passWord);

    }
}

