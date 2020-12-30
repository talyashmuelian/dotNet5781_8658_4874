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
    /// Interaction logic for delStationFromLine.xaml
    /// </summary>
    public partial class delStationFromLine : Window
    {
        IBL bl;
        private stationToLine newItem = new stationToLine();
        public stationToLine newItem1 { get => newItem; set => newItem = value; }
        public delStationFromLine(IBL _bl)
        {
            InitializeComponent();
            bl = _bl;
            DataContext = newItem;
        }

        private void Button_ClickDelete(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.delStationToLine(newItem.CodeStation, newItem.IdentifyNumber);
                MessageBox.Show("!בוצע בהצלחה", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error); }
            Close();
        }
    }
}
