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
using System.Collections.ObjectModel;
using System.ComponentModel;
using BLAPI;
using BO;

namespace UIwpf
{
    /// <summary>
    /// Interaction logic for manager.xaml
    /// </summary>
    public partial class manager : Window
    {
        IBL bl;
        public manager(IBL _bl)
        {
            InitializeComponent();
            
            bl = _bl;
            RefreshBusesLB();
            RefreshLinesLB();
            RefreshStationsLB();
        }
        void RefreshBusesLB()
        {
            lbBusesOnSystem.DataContext = bl.GetAllBusesBO().ToList();
        }
        void RefreshLinesLB()
        {
            lbLinesOnSystem.DataContext = bl.GetAllBusLinesBO().ToList();
        }
        void RefreshStationsLB()
        {
            lbStationsOnSystem.DataContext = bl.GetAllBusStationsBO().ToList();
        }

        private void Button_ClickAddLine(object sender, RoutedEventArgs e)
        {
            addLine addLineWindow = new addLine(bl);
            addLineWindow.ShowDialog();
            try { bl.addBusLine(addLineWindow.newItem1); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "שגיאה"); }
            RefreshLinesLB();
        }

        private void Button_ClickDeleteLine(object sender, RoutedEventArgs e)
        {
            deleteLine deleteLineWindow = new deleteLine(bl);
            deleteLineWindow.ShowDialog();
            try { bl.deleteBusLine(deleteLineWindow.newItem1); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "שגיאה"); }
            RefreshLinesLB();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            updateLine updateLineWindow = new updateLine(bl);
            updateLineWindow.ShowDialog();
            try { bl.updateBusLine(updateLineWindow.newItem1); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "שגיאה"); }
            RefreshLinesLB();
        }
    }
}
