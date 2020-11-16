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
        ListLineBus cbHostList = new ListLineBus();
        private LineBus currentDisplayBusLine;
        public MainWindow()
        {
            cbHostList.add10LinesToSystem();
            InitializeComponent();
            cbHostList.ItemsSource = cbHostList.Buses;
            cbHostList.DisplayMemberPath = " BusLine ";
            cbHostList.SelectedIndex = 0;
            ShowBusLine();


        }
        private void ShowBusLine(int index)
        {
            currentDisplayBusLine = busLines[index];
            UpGrid.DataContext = currentDisplayBusLine;
            lbBusLineStations.DataContext = currentDisplayBusLine.Stations;
        }

        private void cbBusLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowBusLine((cbBusLines.SelectedValue as LineBus).BusLine);
        }
    }
}
