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
    /// Interaction logic for passenger.xaml
    /// </summary>
    public partial class passenger : Window
    {
        IBL bl;
        private int codeStation1;
        private int codeStation2;
        public passenger(IBL _bl)
        {
            InitializeComponent();
            bl = _bl;
            lastStationCB.ItemsSource = bl.GetAllMiniStationsBO();
            firstStationCB.ItemsSource = bl.GetAllMiniStationsBO();
        }
        private void firstStationCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //if ((lastStationCB.SelectedItem as MiniStationBO).CodeStation == (firstStationCB.SelectedItem as MiniStationBO).CodeStation)
                //    throw new BO.BusStationExceptionBO("לא ניתן לבחור את אותה תחנה פעמיים");
                codeStation1 = (firstStationCB.SelectedItem as MiniStationBO).CodeStation;
            dgWays.ItemsSource = bl.GetRelevantWays(codeStation1, codeStation2);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
        private void lastStationCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //if ((lastStationCB.SelectedItem as MiniStationBO).CodeStation == (firstStationCB.SelectedItem as MiniStationBO).CodeStation)
                //    throw new BO.BusStationExceptionBO("לא ניתן לבחור את אותה תחנה פעמיים");
                codeStation2 = (lastStationCB.SelectedItem as MiniStationBO).CodeStation;
                dgWays.ItemsSource = bl.GetRelevantWays(codeStation1, codeStation2);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error); }

        }
    }
}
