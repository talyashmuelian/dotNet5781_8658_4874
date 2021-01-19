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
    /// Interaction logic for deleteTripFromLine.xaml
    /// </summary>
    public partial class deleteTripFromLine : Window
    {
        IBL bl;
        private BusLineBO newItem = new BusLineBO();
        private TimeSpan timeTrip = new TimeSpan();
        public BusLineBO newItem1 { get => newItem; set => newItem = value; }
        public deleteTripFromLine(IBL _bl, BusLineBO currentLine)
        {
            InitializeComponent();
            bl = _bl;
            DataContext = currentLine;
            newItem = currentLine;
            tripsInLineCB.ItemsSource = currentLine.ListOfTrips;
        }
        private void tripsInLineCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            timeTrip = (tripsInLineCB.SelectedItem as LineTripBO).TripStart;
        }

        private void Button_ClickDel(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.deleteLineTrip(new LineTripBO { IdentifyNumber = newItem.IdentifyNumber, TripStart = timeTrip });
                MessageBox.Show("!בוצע בהצלחה", "", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
    }
}
