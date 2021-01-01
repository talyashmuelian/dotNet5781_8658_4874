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
    /// Interaction logic for showStation.xaml
    /// </summary>
    public partial class showStation : Window
    {
        IBL bl;
        public showStation(IBL _bl, BusStationBO currentStation)
        {
            InitializeComponent();
            bl = _bl;
            DataContext = currentStation;
            lbLinesInStationOnSystem.DataContext = currentStation.ListOfLines.ToList();
        }
    }
}
