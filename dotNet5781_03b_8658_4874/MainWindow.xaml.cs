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
using dotNet5781_01_8658_4874;

namespace dotNet5781_03b_8658_4874
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Bus> collectionBuses= new List<Bus>();
        public MainWindow()
        {
            Bus bus1 = new Bus(); Bus bus2 = new Bus(); Bus bus3 = new Bus(); Bus bus4 = new Bus(); Bus bus5 = new Bus();
            Bus bus6 = new Bus(); Bus bus7 = new Bus(); Bus bus8 = new Bus(); Bus bus9 = new Bus(); Bus bus10 = new Bus();
            bus1.kilometers1 = 1000;//אתחול אוטובוס כך שיהיה לו מעט דלק
            collectionBuses.Add(bus1); collectionBuses.Add(bus2); collectionBuses.Add(bus3); collectionBuses.Add(bus4);
            collectionBuses.Add(bus5); collectionBuses.Add(bus6); collectionBuses.Add(bus7); collectionBuses.Add(bus8);
            collectionBuses.Add(bus9); collectionBuses.Add(bus10);
            InitializeComponent();
            lbBusesOnSystem.ItemsSource = collectionBuses;
            lbBusesOnSystem.DisplayMemberPath = "numOfBus1";
        }
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Window1 secondWindow = new Window1();
            secondWindow.Show();

        }

        private void lbBusesOnSystem_SelectionChanged(object sender, SelectionChangedEventArgs e)//מה קורה כשבוחרים אוטובוס מהרשימה
        {
            //צריך להוציא פה חלון עם נתוני האוטובוסMessageBox.Show("דוגמה", "שגיאה");
        }
    }
}
