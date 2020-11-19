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
using System.Windows.Navigation;
using System.Windows.Shapes;
using dotNet5781_02_8658_4874;

namespace dotNet5781_03a_8658_4874
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ListLineBus list123 = new ListLineBus();
        private LineBus currentDisplayBusLine;
        public MainWindow()
        {
            list123.add10LinesToSystem();
            InitializeComponent();
            cbBusLines.ItemsSource = list123.Buses;
            cbBusLines.DisplayMemberPath = "BusLine1";
            cbBusLines.SelectedIndex = 0;

            ShowBusLine(list123.Buses[0].BusLine1);



        }
        private void ShowBusLine(int index)
        {
            currentDisplayBusLine = list123.Buses[index];
            UpGrid.DataContext = currentDisplayBusLine;
            lbBusLineStations.DataContext = currentDisplayBusLine.Stations;
        }

        private void cbBusLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowBusLine((cbBusLines.SelectedValue as LineBus).BusLine1);
        }
    }
}
