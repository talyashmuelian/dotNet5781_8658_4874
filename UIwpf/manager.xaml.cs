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
            RefreshLinesLB();
        }
        private void Button_ClickDeleteLine(object sender, RoutedEventArgs e)
        {
            deleteLine deleteLineWindow = new deleteLine(bl);
            deleteLineWindow.ShowDialog();
            bool a = false;
            try 
            {
                if (deleteLineWindow.ifDone)
                { a = bl.deleteBusLine(deleteLineWindow.newItem1); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);}
            RefreshLinesLB();
            if (a)
            {
                MessageBox.Show("!בוצע בהצלחה", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Button_ClickUpdateLine(object sender, RoutedEventArgs e)
        {
            updateLine updateLineWindow = new updateLine(bl);
            updateLineWindow.ShowDialog();
            bool a = false;
            try 
            {
                if (updateLineWindow.ifDone)
                { a = bl.updateBusLine(updateLineWindow.newItem1); }
                 
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error); }
            RefreshLinesLB();
            if (a)
            {
                MessageBox.Show("!בוצע בהצלחה", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Button_ClickAddStationToLine(object sender, RoutedEventArgs e)
        {
            addStationToLine addStationToLineWindow = new addStationToLine(bl);
            addStationToLineWindow.ShowDialog();
            RefreshLinesLB();
        }

        private void Button_ClickDeleteStationFromLine(object sender, RoutedEventArgs e)
        {
            delStationFromLine delStationFromLineWindow = new delStationFromLine(bl);
            delStationFromLineWindow.ShowDialog();
            RefreshLinesLB();
        }
        private void showDataLines_doubleClick(object sender, RoutedEventArgs e)//חלון נתוני קו
        {
            var fxElt = sender as ListBox;
            BusLineBO CurrentLine = fxElt.SelectedItem as BusLineBO;
            showLine showLineWindow = new showLine(bl, CurrentLine);
            showLineWindow.ShowDialog();
        }

        private void Button_ClickAddStation(object sender, RoutedEventArgs e)
        {
            addStation addStationWindow = new addStation(bl);
            addStationWindow.ShowDialog();
            bool a = false;
            try
            {
                if (addStationWindow.ifDone)
                { a = bl.addBusStation(addStationWindow.newItem1); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error); }
            RefreshStationsLB();
            if (a)
            {
                MessageBox.Show("!בוצע בהצלחה", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Button_ClickDeleteStation(object sender, RoutedEventArgs e)
        {
            deleteStation deleteStationWindow = new deleteStation(bl);
            deleteStationWindow.ShowDialog();
            bool a = false;
            try
            {
                if (deleteStationWindow.ifDone)
                { a = bl.deleteBusStation(deleteStationWindow.newItem1); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error); }
            RefreshStationsLB();
            if (a)
            {
                MessageBox.Show("!בוצע בהצלחה", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Button_ClickUpdateStation(object sender, RoutedEventArgs e)
        {
            updateStation updateStationWindow = new updateStation(bl);
            updateStationWindow.ShowDialog();
            bool a = false;
            try
            {
                if (updateStationWindow.ifDone)
                { a = bl.updateBusStation(updateStationWindow.newItem1); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error); }
            RefreshStationsLB();
            if (a)
            {
                MessageBox.Show("!בוצע בהצלחה", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void showDataStations_doubleClick(object sender, RoutedEventArgs e)//חלון נתוני תחנה
        {
            var fxElt = sender as ListBox;
            BusStationBO CurrentStation = fxElt.SelectedItem as BusStationBO;
            showStation showStationWindow = new showStation(bl, CurrentStation);
            showStationWindow.ShowDialog();
        }

        private void Button_ClickAddBus(object sender, RoutedEventArgs e)
        {
            addBus addBusWindow = new addBus(bl);
            addBusWindow.ShowDialog();
            bool a = false;
            try
            {
                if (addBusWindow.ifDone)
                { a = bl.addBus(addBusWindow.newItem1); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error); }
            RefreshBusesLB();
            if (a)
            {
                MessageBox.Show("!בוצע בהצלחה", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void Button_ClickDeleteBus(object sender, RoutedEventArgs e)
        {
            deleteBus deleteBusWindow = new deleteBus(bl);
            deleteBusWindow.ShowDialog();
            bool a = false;
            try
            {
                if (deleteBusWindow.ifDone)
                { a = bl.deleteBus(deleteBusWindow.newItem1); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error); }
            RefreshBusesLB();
            if (a)
            {
                MessageBox.Show("!בוצע בהצלחה", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Button_ClickUpdateBus(object sender, RoutedEventArgs e)
        {
            updateBus updateBusWindow = new updateBus(bl);
            updateBusWindow.ShowDialog();
            bool a = false;
            try
            {
                if (updateBusWindow.ifDone)
                { a = bl.updateBus(updateBusWindow.newItem1); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error); }
            RefreshBusesLB();
            if (a)
            {
                MessageBox.Show("!בוצע בהצלחה", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void showDataBuses_doubleClick(object sender, RoutedEventArgs e)//חלון נתוני אוטובוס
        {
            var fxElt = sender as ListBox;
            BusBO CurrentBus= fxElt.SelectedItem as BusBO;
            showBus showBusWindow = new showBus(bl, CurrentBus);
            showBusWindow.ShowDialog();
        }
        private void Button_ClickTreatment(object sender, RoutedEventArgs e)
        {
            var fxElt = sender as FrameworkElement;
            BusBO CurrentBus = fxElt.DataContext as BusBO;
            //var fxElt = sender as ListBox;
            //BusBO CurrentBus = fxElt.SelectedItem as BusBO;
            bl.treatment(CurrentBus.License);
            MessageBox.Show("!הטיפול בוצע בהצלחה", "", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void Button_ClickFuel(object sender, RoutedEventArgs e)
        {
            var fxElt = sender as FrameworkElement;
            BusBO CurrentBus = fxElt.DataContext as BusBO;
            //var fxElt = sender as ListBox;
            //BusBO CurrentBus = fxElt.SelectedItem as BusBO;
            bl.refuel(CurrentBus.License);
            MessageBox.Show("!התדלוק בוצע בהצלחה", "", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Button_ClickUpdatePair(object sender, RoutedEventArgs e)
        {
            updateAddPair updateAddPairWindow = new updateAddPair(bl);
            updateAddPairWindow.ShowDialog();
            bool a = false;
            try
            {
                if (updateAddPairWindow.ifDone)
                { a = bl.updatePairConsecutiveStations(updateAddPairWindow.newItem1); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error); }
            RefreshLinesLB();
            if (a)
            {
                MessageBox.Show("!בוצע בהצלחה", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void Button_ClickAddPair(object sender, RoutedEventArgs e)
        {
            updateAddPair updateAddPairWindow = new updateAddPair(bl);
            updateAddPairWindow.ShowDialog();
            bool a = false;
            try
            {
                if (updateAddPairWindow.ifDone)
                { a = bl.addPairConsecutiveStations(updateAddPairWindow.newItem1); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error); }
            RefreshLinesLB();
            if (a)
            {
                MessageBox.Show("!בוצע בהצלחה", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void Button_ClickShowPair(object sender, RoutedEventArgs e)//כפתור לתצוגת התחנות העוקבות של קו מסוים
        {
            var fxElt = sender as FrameworkElement;
            BusStationBO CurrentStation = fxElt.DataContext as BusStationBO;
            //var fxElt = sender as ListBox;
            //BusStationBO CurrentStation = fxElt.SelectedItem as BusStationBO;
            showPair showPairWindow = new showPair(bl, CurrentStation);
            showPairWindow.ShowDialog();
        }
    }
}
