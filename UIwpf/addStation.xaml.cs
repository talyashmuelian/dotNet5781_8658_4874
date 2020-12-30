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
    /// Interaction logic for addStation.xaml
    /// </summary>
    public partial class addStation : Window
    {
        IBL bl;
        private BusStationBO newItem = new BusStationBO();
        public BusStationBO newItem1 { get => newItem; set => newItem = value; }
        public addStation(IBL _bl)
        {
            InitializeComponent();
            bl = _bl;
            DataContext = newItem;
        }

        private void Button_ClickAddStation(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
