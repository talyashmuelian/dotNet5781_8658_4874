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
        //הוספה, עדכון ומחיקת קו
        bool addBusLine(BusLineBO busLine);
        bool updateBusLine(BusLineBO busLine);
        void deleteBusLine(BusLineBO busLine);
        void addStationToLine(int codeStation, int identifyNumber, int location);//הוספת תחנה קיימת לקו קיים
        IEnumerable<BusBO> GetAllBusesBO();//הדפסת כל האוטבוסים
        BusBO GetBusBO(int license);//קבלת פרטי אוטובוס בודד
        //הוספה, עדכון ומחיקת אוטובוס
        bool addBus(BusBO bus);
        bool updateBus(BusBO bus);
        void deleteBus(BusBO bus);
        //שליחת אוטבוס לטיפול ותדלוק
        void refuel(int license);
        void treatment(int license);
        IEnumerable<BusStationBO> GetAllBusStationsBO();//הדפסת כל התחנות
        BusStationBO GetBusStationBO(int codeStation);//קבלת פרטים עבור תחנה מסוימת
        //הוספה עדכון ומחיקת תחנה
        bool addBusStation(BusStationBO busStation);//הוספת תחנה חדשה לגמרי שחייבת להיות לפחות בקו אחד
        bool updateBusStation(BusStationBO busStation);
        void deleteBusStation(BusStationBO busStation);
        void addExsistStationToLine(BusStationBO busStation);//הוספת תחנה קיימת לקו כלשהו, צריך לעדכן ברשימת התחנות של הקו, וברשימת הקווים של התחנה
        void updatePairConsecutiveStations(int numStation1, int numStation2, int distance, int timeDriving);//עדכון מרחק וזמן נסיעה בין זוג תחנות עוקבות


    }
}

