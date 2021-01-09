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
    /// Interaction logic for updateAddPair.xaml
    /// </summary>
    public partial class updateAddPair : Window
    {
        IBL bl;
        private PairConsecutiveStationsBO newItem = new PairConsecutiveStationsBO();
        public PairConsecutiveStationsBO newItem1 { get => newItem; set => newItem = value; }
        public bool ifDone { get; set; } = false;
        public updateAddPair(IBL _bl)
        {
            InitializeComponent();
            bl = _bl;
            DataContext = newItem;
            station1CB.ItemsSource = bl.GetAllMiniStationsBO();
            station2CB.ItemsSource = bl.GetAllMiniStationsBO();
        }
        private void station1CB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            newItem.StationNum1 = (station1CB.SelectedItem as MiniStationBO).CodeStation;
        }
        private void station2CB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if ((station2CB.SelectedItem as MiniStationBO).CodeStation == (station1CB.SelectedItem as MiniStationBO).CodeStation)
                    throw new BO.BusStationExceptionBO("לא ניתן לבחור את אותה תחנה פעמיים");
                newItem.StationNum2 = (station2CB.SelectedItem as MiniStationBO).CodeStation;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error); }

        }

        private void Button_ClickDoIt(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((station2CB.SelectedItem as MiniStationBO).CodeStation == (station1CB.SelectedItem as MiniStationBO).CodeStation)
                    throw new BO.BusStationExceptionBO("לא ניתן לבחור את אותה תחנה פעמיים");
                ifDone = true;
                Close();
            }
            catch (BusStationExceptionBO ex) { MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error); Close(); }

        }
    }
}
