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
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using dotNet5781_01_8658_4874;

namespace dotNet5781_03b_8658_4874
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window//חלון לצורך הוספת אוטובוס חדש וקליטת הנתונים שלו מהמשתמש
    {
        private Bus newItem=new Bus();
        public Bus newItem1 { get => newItem; set => newItem = value; }
        public Window1()
        {
            InitializeComponent();
            DataContext = newItem;
        }
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            /*int.TryParse(number.Text, out int num);
            DateTime.TryParse(date.ToString(), out DateTime start);
            newItem = new Bus(num, start);*/
            newItem.Flag1 = (state)1;
            newItem.dateTreatLast1 = DateTime.Now;
            newItem.dateOfStart1 = DateTime.Now;
            newItem.kilometersFromTreament1 = 0;
            MainWindow.collectionBuses.Add(newItem);
            Close();
        }
    }
}
