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
    /// Interaction logic for addBus.xaml
    /// </summary>
    public partial class addBus : Window
    {
        IBL bl;
        private BusBO newItem = new BusBO();
        public BusBO newItem1 { get => newItem; set => newItem = value; }
        public bool ifDone { get; set; } = false;
        public addBus(IBL _bl)
        {
            InitializeComponent();
            bl = _bl;
            DataContext = newItem;
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            newItem.DateTreatLast= DateTime.Now;
            newItem.Fuel = 1200;
            newItem.KmFromTreament = 0;
            newItem.Status = (Status)1;//מוכן
            ifDone = true;
            Close();
        }
    }
}
