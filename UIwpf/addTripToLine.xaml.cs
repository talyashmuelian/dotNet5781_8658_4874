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
using BLAPI;
using BO;
namespace UIwpf
{
    /// <summary>
    /// Interaction logic for addTripToLine.xaml
    /// </summary>
    public partial class addTripToLine : Window
    {
        IBL bl;
        private BusLineBO newItem = new BusLineBO();
        public BusLineBO newItem1 { get => newItem; set => newItem = value; }
        public addTripToLine(IBL _bl, BusLineBO currentLine)
        {
            InitializeComponent();
            bl = _bl;
            DataContext = currentLine;
            newItem = currentLine;
        }

        private void Button_ClickAdd(object sender, RoutedEventArgs e)
        {
            try
            {
                int hours1 = Convert.ToInt32(hours.Text);
                int minutes1 = Convert.ToInt32(minutes.Text);
                int seconds1 = Convert.ToInt32(seconds.Text);
                if (hours1 < 0 || hours1 > 24 || minutes1 < 0 || minutes1 > 60 || seconds1 < 0 || seconds1 > 60)
                {
                    throw new LineTripExceptionBO("השעה שהכנסת אינה תקינה");
                }
                TimeSpan trip = new TimeSpan(hours1, minutes1, seconds1);
                bl.addLineTrip(new LineTripBO { IdentifyNumber = newItem.IdentifyNumber, TripStart = trip });
                MessageBox.Show("!בוצע בהצלחה", "", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
    }
}
