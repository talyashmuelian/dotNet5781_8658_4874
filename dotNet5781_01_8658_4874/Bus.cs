using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


namespace dotNet5781_01_8658_4874
{
	public enum state { ready=1, onTravel, onRefueling, onTreatment, notReady };
	public class Bus : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		private int time;
		private int yearStart;//השנה שבה האוטובוס נכנס לפעילות The year the bus went into operation
		private int numOfBus;//מספר רישוי license number
		private string numOfBusString;//מספר רישוי במחרוזת
		private DateTime dateOfStart;//תאריך תחילת פעילות Activity start date
		private DateTime dateTreatLast;//תאריך טיפול אחרון Last treatment date
		private int kilometersFromTreament;//מספר הקילומטרים מאז הטיפול האחרון Number of miles since last treatment
		private int kilometersUntilLastTreatment;//נסועה בה בוצע הטיפול האחרון A ride in which the last treatment was performed
		private int kilometraj;//נסועה כוללת Total travel
		private int kilometers;//כמות הקילומטרים מאז התדלוק The amount of miles since refueling
		private state flag;//שדה עבור סטטוס
						   //private bool ifReady;
		private int numOfKmInTheLastTime;//שדה עבור שמירת מס הקילוממטרים של הנססיעה האחרונה שהתבצעה
		public bool ifInTravel()
		{
			if (flag == (state)2)
				return true;
			return false;
		}
		public bool ifInTravel1 { get => ifInTravel(); }
		public bool ifInFuel()
		{
			if (flag == (state)3)
				return true;
			return false;
		}
		public bool ifInFuel1 { get => ifInFuel(); }
		public bool ifInTraetment()
		{
			if (flag == (state)4)
				return true;
			return false;
		}
		public bool ifInTraetment1 { get => ifInTraetment(); }
		public bool ifCanToFuel()
		{
			if (kilometers == 0)
				return false;
			if (flag == (state)2)
				return false;
			if (flag == (state)3)
				return false;
			return true;
		}
		public bool ifCanToFuel1 { get => ifCanToFuel(); }
		public bool ifCanToTreat()
		{
			if (flag == (state)2)
				return false;
			if (flag == (state)4)
				return false;
			return true;
		}
		public bool ifCanToTreat1 { get => ifCanToTreat(); }
		public bool ifReady()//מתודה שבודקת האם האוטובוס מוכן לנסיעה
		{

			DateTime date1 = DateTime.Now;
			TimeSpan t = date1 - dateTreatLast;
			int space = Convert.ToInt32(t.TotalDays);//casting to int
			if (space > 365)//עברה שנה מאז הטיפול האחרון It has been a year since the last treatment
				return false;
			if (kilometersFromTreament >= 20000)//עברו מספר הקילומטרים הדרוש לטיפול
				return false;
			if (kilometers >= 1200)//אין דלק
				return false;
			if (Flag1 != (state)1)
				return false;
			//if (flag == (state)1)
			//	return true;
			return true;
		}
		//public bool ifReady1 { get => ifReady; set => ifReady = value; }
		public bool ifReady1 { get => ifReady(); }
		public int numOfKmInTheLastTime1 { get => numOfKmInTheLastTime; set => numOfKmInTheLastTime = value; }
		public int kilometers1 { get => kilometers; set => kilometers = value; }
		public string numOfBusString1
		{
			get
			{
				if (yearStart > 2017)//8 ספרות numbers
				{
					return numOfBusString.Substring(0, 3) +"-" +numOfBusString[3] + numOfBusString[4] + "-" + numOfBusString[5] + numOfBusString[6] + numOfBusString[7];

				}
				else//7 ספרות numbers
				{
					return numOfBusString.Substring(0, 2) + "-" + numOfBusString[2] + numOfBusString[3] + numOfBusString[4] + "-" + numOfBusString[5] + numOfBusString[6];
				}
			}
			set => numOfBusString = value;
		}
		public int numOfBus1 
		{
			get 
			{
				return numOfBus;
			}
			set => numOfBus = value; 
		}
		public int time1 { get => time;
			set { time = value;
				if (PropertyChanged != null) 
					{ PropertyChanged(this, new PropertyChangedEventArgs("time")); }
			}
		}
		public int kilometraj1 { get => kilometraj; set => kilometraj = value; }
		public int yeartSart1 { get => yearStart; set => yearStart = value; }
		public int kilometersFromTreament1 { get => kilometersFromTreament; set => kilometersFromTreament = value; }
		public DateTime dateTreatLast1 { get => dateTreatLast; set => dateTreatLast = value; }
		public DateTime dateOfStart1 { get => dateOfStart; set => dateOfStart = value; }
		public state Flag1 { get => flag; set => flag = value; }
		public Bus() { yearStart = DateTime.Now.Year; }
		public Bus(int num, DateTime myDate)//c-tor
		{
			numOfBus = num;
			numOfBusString = num.ToString();
			dateOfStart = myDate;
			kilometraj = 0;
			kilometers = 0;
			kilometersFromTreament = 0;
			kilometersUntilLastTreatment = 0;
			dateTreatLast = DateTime.Now;
			yearStart = myDate.Year;
			numOfKmInTheLastTime = 0;
			flag = (state)1;//אתחול האוטובוס כמוכן לנסיעה
			//ifReady = true;

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
				throw new ObjectNotAllowedException("The bus cannot make the travel, treatment is needed");//צריך טיפול
			int sum = kilimeterForTravel + kilometersFromTreament;//סכום הקילומטרים שהיו מאז הטיפול האחרון פלוס הנסיעה שהוגרלה The amount of miles that have been since the last treatment plus the raffle ride
			if (sum > 20000)//הנסיעה ארוכה מידי מבחינת קילומטרז' שנשאר לטיפול The journey is too long in terms of mileage left for treatment
				throw new ObjectNotAllowedException("The bus cannot make the travel, treatment is needed");//צריך טיפול
			sum = kilometers + kilimeterForTravel;// סכום הקילומטרים שהיו מאז התדלוק האחרון פלוס הנסיעה שהוגרלה The amount of miles that have been since the last refueling plus the raffle ride

			if (sum > 1200)//הנסיעה ארוכה מידי מבחינת דלק The trip is too long in terms of fuel
				throw new ObjectNotAllowedException("The bus cannot make the travel, need to refuel");//צריך תדלוק
			if (Flag1 != (state)1)
				throw new ObjectNotAllowedException("The bus cannot make the travel, Status does not allow");//סטטוס לא מאפשר
			return true;
		}
		public void doingDriving(int kilimeterForTravel)
		{
			kilometersFromTreament += kilimeterForTravel;//עדכון מספר הקילומטרים מאז הטיפול האחרון Update the number of miles since the last treatment
			kilometraj += kilimeterForTravel;//עדכון נסועה כוללת Overall travel update
			kilometers += kilimeterForTravel;//עדכון כמות הקילומטרים מאז התדלוק Update the amount of miles since refueling
		}
		//public override string ToString()
		//{
		//	{ return "Licensing number: " + numOfBus + ", Activity start date: " + dateOfStart + ", Last treatment date: " + dateTreatLast
		//		+", Number of miles since last treatment: " + kilometersFromTreament
		//		+", Total travel: " + kilometraj
		//		+ ", The amount of miles since refueling: " + kilometers
		//		+ ", Status: " + flag
		//	;
		//	}
		//}

	}
}
