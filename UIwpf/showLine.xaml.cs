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
    /// Interaction logic for showLine.xaml
    /// </summary>
    public partial class showLine : Window
    {
        IBL bl;
        public showLine(IBL _bl, BusLineBO currentLine)
        {
            InitializeComponent();
            bl = _bl;
            DataContext = currentLine;
            lbStationsInLineOnSystem.DataContext = currentLine.ListOfStations.ToList();
            loozDG.ItemsSource = currentLine.ListOfTrips.ToList();
        }
    }
}

