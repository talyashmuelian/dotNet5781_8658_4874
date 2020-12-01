//neria farangi 211344874
//talya shmuelian 211378658
//donNet exe 3b
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using dotNet5781_01_8658_4874;

namespace dotNet5781_03b_8658_4874
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static public Random rand1 = new Random();
        public static ObservableCollection<Bus> collectionBuses = new ObservableCollection<Bus>();
        public MainWindow()
        {
            InitializeComponent();
            Bus bus1 = new Bus(rand1.Next(99999999), DateTime.Now);
            Bus bus2 = new Bus(rand1.Next(99999999), DateTime.Now);
            Bus bus3 = new Bus(rand1.Next(99999999), DateTime.Now);
            Bus bus4 = new Bus(rand1.Next(99999999), DateTime.Now); 
            Bus bus5 = new Bus(rand1.Next(99999999), DateTime.Now);
            Bus bus6 = new Bus(rand1.Next(99999999), DateTime.Now);
            Bus bus7 = new Bus(rand1.Next(99999999), DateTime.Now); 
            Bus bus8 = new Bus(rand1.Next(99999999), DateTime.Now); 
            Bus bus9 = new Bus(rand1.Next(99999999), DateTime.Now); 
            Bus bus10 = new Bus(rand1.Next(99999999), DateTime.Now);
            bus1.kilometers1 = 1000;//אתחול אוטובוס כך שיהיה לו מעט דלק
            DateTime myDate = new DateTime(2019, 9, 1);
            bus2.dateTreatLast1 = myDate;//אתחול אוטובוס כך שעברה שנה מאז הטיפול האחרון שלו
            bus3.kilometersFromTreament1 = 19000;//אתחול אוטובוס כך שהוא כמעט צריך ללכת לטיפול מבחינת קילומטרים
            collectionBuses.Add(bus1); collectionBuses.Add(bus2); collectionBuses.Add(bus3); collectionBuses.Add(bus4);
            collectionBuses.Add(bus5); collectionBuses.Add(bus6); collectionBuses.Add(bus7); collectionBuses.Add(bus8);
            collectionBuses.Add(bus9); collectionBuses.Add(bus10);
            lbBusesOnSystem.ItemsSource = collectionBuses;
        }
        private void button1_Click(object sender, RoutedEventArgs e)//חלון הוספת אוטובוס
        {
            Window1 secondWindow = new Window1();
            secondWindow.ShowDialog();
            //lbBusesOnSystem.Items.Refresh();
        }
        private void travelButton_Click(object sender, RoutedEventArgs e)//כפתור שליחה לנסיעה
        {

            var fxElt = sender as FrameworkElement;
            Bus CurrentBus = fxElt.DataContext as Bus;
            Window2 windowBusDrive = new Window2();
            windowBusDrive.ShowDialog();
            int.TryParse(windowBusDrive.ditance.Text, out int num);
            try
            {
                if (CurrentBus.checkIfReady(num) == false)
                    throw new ObjectCannotDoException("The bus cannot make the travel");//אם האוטובוס לא יכול לבצע את הנסיעה
                else
                {
                    CurrentBus.doingDriving(num);
                    MessageBox.Show("נשלח בהצלחה", "");
                }
            }
            catch (ObjectCannotDoException ex) { MessageBox.Show(ex.Message); }
        }
        private void fuelButton_Click(object sender, RoutedEventArgs e)//כפתור שליחה לתדלוק
        {
            var fxElt = sender as FrameworkElement;
            Bus CurrentBus = fxElt.DataContext as Bus;
            CurrentBus.setKilometers(0);//איפוס השדה שסופר את כמות הקילומטרים מאז התדלוק
            MessageBox.Show("התדלוק בוצע בהצלחה", "");
        }
        private void showData_doubleClick(object sender, RoutedEventArgs e)//חלון נתוני אוטובוס
        {
            var fxElt = sender as ListBox;
            Bus CurrentBus = fxElt.SelectedItem as Bus;
            WindowShowData showDataWindow = new WindowShowData(CurrentBus);
            //showDataWindow.MyData = CurrentBus;
            showDataWindow.ShowDialog();
        }

        
    }
}
