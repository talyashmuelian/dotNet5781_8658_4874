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
    /// Interaction logic for addStationToLine.xaml
    /// </summary>
    public partial class addStationToLine : Window
    {
        IBL bl;
        private stationToLine newItem = new stationToLine();
        public stationToLine newItem1 { get => newItem; set => newItem = value; }
        public addStationToLine(IBL _bl)
        {
            InitializeComponent();
            bl = _bl;
            DataContext = newItem;
        }

        private void Button_ClickAdd(object sender, RoutedEventArgs e)
        {
            try 
            { 
                bl.addStationToLine(newItem.CodeStation, newItem.IdentifyNumber, newItem.Location);
                MessageBox.Show("!בוצע בהצלחה", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error); }
            Close();
        }
    }
}
