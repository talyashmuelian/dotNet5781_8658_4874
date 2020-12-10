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
            newItem.numOfBusString1 = numOfBus1TextBox.Text;
            newItem.Flag1 = (state)1;
            newItem.dateTreatLast1 = DateTime.Now;
            newItem.dateOfStart1 = DateTime.Now;
            newItem.kilometersFromTreament1 = 0;
            newItem.numOfKmInTheLastTime1 = 0;
            try
            {
                foreach (Bus item in MainWindow.collectionBuses)
                {
                    if (item.numOfBus1 == newItem.numOfBus1)
                        throw new ObjectNotAllowedException("The license number already exists in the system");
                }
                if (newItem.numOfBus1 < 100000 || newItem.numOfBus1 > 99999999)
                    throw new ObjectNotAllowedException("The number is not allow. Enter a number of digits that will be compatible for a year.");
                if (newItem.yeartSart1 < 2018 && newItem.numOfBus1 > 9999999)
                    throw new ObjectNotAllowedException("The number is not allow. Enter a number of digits that will be compatible for a year.");
                if (newItem.yeartSart1 >= 2018 && newItem.numOfBus1 < 10000000)
                    throw new ObjectNotAllowedException("The number is not allow. Enter a number of digits that will be compatible for a year.");
                MainWindow.collectionBuses.Add(newItem);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR"); }
            Close();
        }
    }
}
