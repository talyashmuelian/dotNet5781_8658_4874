using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_8658_4874
{
	enum state { ready=1, onTravel, onRefueling, onTreatment };
	public class Bus
	{
		private int yearStart;//השנה שבה האוטובוס נכנס לפעילות The year the bus went into operation
		private int numOfBus;//מספר רישוי license number
		private DateTime dateOfStart;//תאריך תחילת פעילות Activity start date
		private DateTime dateTreatLast;//תאריך טיפול אחרון Last treatment date
		private int kilometersFromTreament;//מספר הקילומטרים מאז הטיפול האחרון Number of miles since last treatment
		private int kilometersUntilLastTreatment;//נסועה בה בוצע הטיפול האחרון A ride in which the last treatment was performed
		private int kilometraj;//נסועה כוללת Total travel
		private int kilometers;//כמות הקילומטרים מאז התדלוק The amount of miles since refueling
		private state flag;//שדה עבור סטטוס

        public int kilometers1 { get => kilometers; set => kilometers = value; }
		public int numOfBus1 { get => numOfBus; set => numOfBus = value; }

		public Bus()
        {
			DateTime myDate = new DateTime();
			numOfBus = Program.rand.Next(1000000, 99999999);
			dateOfStart = myDate;
			kilometraj = 0;
			kilometers = 0;
			kilometersFromTreament = 0;
			kilometersUntilLastTreatment = 0;
			dateTreatLast = DateTime.Now;
			yearStart = 0;
			flag = (state)1;//אתחול האוטובוס כמוכן לנסיעה
		}
		public Bus(int num, DateTime myDate)//c-tor
		{
			numOfBus = num;
			dateOfStart = myDate;
			kilometraj = 0;
			kilometers = 0;
			kilometersFromTreament = 0;
			kilometersUntilLastTreatment = 0;
			dateTreatLast = DateTime.Now;
			yearStart = 0;
			flag = (state)1;//אתחול האוטובוס כמוכן לנסיעה

		}
		public void setYearStart(int num) { yearStart = num; }
		public int getNumOfBus() { return numOfBus; }
		public void setNumOfBus(int num) { numOfBus = num; }
		public double getKilometers() { return kilometers; }
		public void setKilometers(int num) { kilometers = num; }
		public void treatment()
		{
			dateTreatLast = DateTime.Now;
			kilometersFromTreament = 0;
			kilometersUntilLastTreatment = kilometraj;
		}
		public void print()//הצגת הנסועה מאז הטיפול האחרון Presentation of the passenger since the last treatment
		{
			int start, middle, end;
			if (yearStart > 2017)//8 ספרות numbers
			{
				start = numOfBus / 100000;
				middle = (numOfBus % 100000) / 1000;
				end = numOfBus % 1000;
				Console.WriteLine("Registration Number:" + start + "-" + middle + "-" + end + ", Km since last treatment:" + kilometersFromTreament);
			}
			else//7 ספרות numbers
			{
				start = numOfBus / 100000;
				middle = (numOfBus % 100000) / 100;
				end = numOfBus % 100;
				Console.WriteLine("Registration Number:" + start + "-" + middle + "-" + end + ", Km since last treatment:" + kilometersFromTreament);
			}

		}
		public bool checkIfReady(int kilimeterForTravel)//מתודה שבודקת אם אוטבוס יכול לצאת לנסיעה מסוימת A method that checks if a bus can go on a particular trip
		{
			DateTime date1 = DateTime.Now;
			TimeSpan t = date1 - dateTreatLast;
			int space = Convert.ToInt32(t.TotalDays);//casting to int
			if (space > 365)//עברה שנה מאז הטיפול האחרון It has been a year since the last treatment
				return false;
			int sum = kilimeterForTravel + kilometersFromTreament;//סכום הקילומטרים שהיו מאז הטיפול האחרון פלוס הנסיעה שהוגרלה The amount of miles that have been since the last treatment plus the raffle ride
			if (sum > 20000)//הנסיעה ארוכה מידי מבחינת קילומטרז' שנשאר לטיפול The journey is too long in terms of mileage left for treatment
				return false;
			sum = kilometers + kilimeterForTravel;// סכום הקילומטרים שהיו מאז התדלוק האחרון פלוס הנסיעה שהוגרלה The amount of miles that have been since the last refueling plus the raffle ride

			if (sum > 1200)//הנסיעה ארוכה מידי מבחינת דלק The trip is too long in terms of fuel
				return false;
			return true;
		}
		public void doingDriving(int kilimeterForTravel)
		{
			kilometersFromTreament += kilimeterForTravel;//עדכון מספר הקילומטרים מאז הטיפול האחרון Update the number of miles since the last treatment
			kilometraj += kilimeterForTravel;//עדכון נסועה כוללת Overall travel update
			kilometers += kilimeterForTravel;//עדכון כמות הקילומטרים מאז התדלוק Update the amount of miles since refueling
		}
		public override string ToString()
		{
			{ return "Licensing number: " + numOfBus + ", Activity start date: " + dateOfStart + ", Last treatment date: " + dateTreatLast
				+", Number of miles since last treatment: " + kilometersFromTreament
				+", Total travel: " + kilometraj
				+ ", The amount of miles since refueling: " + kilometers
				+ ", Status: " + flag
			;
			}
		}

	}
}
