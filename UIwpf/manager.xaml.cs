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
        private List<string> areas = new List<string>();
        public manager(IBL _bl)
        {
            InitializeComponent();
            
            bl = _bl;
            RefreshBusesLB();
            RefreshLinesLB();
            RefreshStationsLB();
            areas.Add("כל האיזורים");
            areas.Add("ירושלים");
            areas.Add("מרכז");
            areas.Add("דרום");
            areas.Add("צפון");
            areaCB.ItemsSource = areas;
            areaCB.SelectedIndex = 0;
        }
        private void areaCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (areaCB.SelectedItem.ToString())
            {
                case "כל האיזורים":
                    lbLinesOnSystem.DataContext = bl.GetAllBusLinesBO().ToList();
                    break;
                case "ירושלים":
                    foreach (var group in bl.orderLinesByArea())
                    {
                        if (group.Key== "ירושלים")
                            lbLinesOnSystem.DataContext = group.ListOfLinesInArea;
                    }
                    break;
                case "דרום":
                    foreach (var group in bl.orderLinesByArea())
                    {
                        if (group.Key == "דרום")
                            lbLinesOnSystem.DataContext = group.ListOfLinesInArea;
                    }
                    break;
                case "צפון":
                    foreach (var group in bl.orderLinesByArea())
                    {
                        if (group.Key == "צפון")
                            lbLinesOnSystem.DataContext = group.ListOfLinesInArea;
                    }
                    break;
                case "מרכז":
                    foreach (var group in bl.orderLinesByArea())
                    {
                        if (group.Key == "מרכז")
                            lbLinesOnSystem.DataContext = group.ListOfLinesInArea;
                    }
                    break;
            }

        }
        void RefreshBusesLB()
        {
            lbBusesOnSystem.DataContext = bl.GetAllBusesBO().ToList();
        }
        void RefreshLinesLB()
        {
            areaCB.SelectedIndex = 0;
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
        
        private void Button_ClickDeleteLine1(object sender, RoutedEventArgs e)
        {
            var fxElt = sender as FrameworkElement;
            BusLineBO CurrentLine = fxElt.DataContext as BusLineBO;
            try
            {
                MessageBoxResult result = MessageBox.Show("?האם אתה בטוח שברצונך למחוק את הקו", "אישור מחיקה", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    bl.deleteBusLine(CurrentLine);
                    MessageBox.Show("!בוצע בהצלחה", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error); }
            RefreshLinesLB();
        }

        private void Button_ClickUpdateLine1(object sender, RoutedEventArgs e)
        {
            var fxElt = sender as FrameworkElement;
            BusLineBO CurrentLine = fxElt.DataContext as BusLineBO;
            updateLine updateLineWindow = new updateLine(bl, CurrentLine);
            updateLineWindow.ShowDialog();
            RefreshLinesLB();
        }

        private void Button_ClickAddStationToLine1(object sender, RoutedEventArgs e)
        {
            var fxElt = sender as FrameworkElement;
            BusLineBO CurrentLine = fxElt.DataContext as BusLineBO;
            addStationToLine addStationToLineWindow = new addStationToLine(bl, CurrentLine);
            addStationToLineWindow.ShowDialog();
            RefreshLinesLB();
        }

        private void Button_ClickDeleteStationFromLine1(object sender, RoutedEventArgs e)
        {
            var fxElt = sender as FrameworkElement;
            BusLineBO CurrentLine = fxElt.DataContext as BusLineBO;
            delStationFromLine delStationFromLineWindow = new delStationFromLine(bl, CurrentLine);
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

        
        private void Button_ClickDeleteStation1(object sender, RoutedEventArgs e)
        {
            var fxElt = sender as FrameworkElement;
            BusStationBO CurrentStation = fxElt.DataContext as BusStationBO;
            try
            {
                MessageBoxResult result = MessageBox.Show("האם אתה בטוח שברצונך למחוק את התחנה? מחיקה זו תמחק את התחנה מכל הקווים שעוברים בה", "אישור מחיקה", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    foreach (var line in CurrentStation.ListOfLines)//מחיקה התחנה מכל הקווים שעוברים בה, כמובן תבקש מידע על זוג תחנות חדשות אם יהיה צורך
                    {
                        BusLineBO CurrentLine = bl.GetBusLineBO(line.IdentifyNumber);
                        bl.chekIfCanToDelStationFromLine(CurrentStation.CodeStation, CurrentLine.IdentifyNumber);
                        PairConsecutiveStationsBO currentPair = bl.ifNeedToGetDataBetweenTwoStation(CurrentLine.IdentifyNumber, CurrentStation.CodeStation);
                        if (currentPair != null)//אין מידע עבור התחנה הקודמת והעוקבת לזו שרוצים למחוק
                        {
                            //נפתח חלון חדש שמבקש מידע עבור המרחק בין התחנות האלה
                            addDataToPaitStation addDataToPaitStationWindow = new addDataToPaitStation(bl, currentPair.StationNum1, currentPair.StationNum2);
                            addDataToPaitStationWindow.ShowDialog();
                        }
                        bl.delStationToLine(CurrentStation.CodeStation, CurrentLine.IdentifyNumber);
                        MessageBox.Show("!בוצע בהצלחה", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    bl.deleteBusStation(CurrentStation);
                    RefreshStationsLB();
                    MessageBox.Show("!בוצע בהצלחה", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error); }
            RefreshStationsLB();
            RefreshLinesLB();
        }

        private void Button_ClickUpdateStation1(object sender, RoutedEventArgs e)
        {
            var fxElt = sender as FrameworkElement;
            BusStationBO CurrentStation = fxElt.DataContext as BusStationBO;
            updateStation updateStationWindow = new updateStation(bl, CurrentStation);
            updateStationWindow.ShowDialog();
            RefreshStationsLB();
            //bool a = false;
            //try
            //{
            //    if (updateStationWindow.ifDone)
            //    { a = bl.updateBusStation(updateStationWindow.newItem1); }
            //}
            //catch (Exception ex) { MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error); }
            //RefreshStationsLB();
            //if (a)
            //{
            //    MessageBox.Show("!בוצע בהצלחה", "", MessageBoxButton.OK, MessageBoxImage.Information);
            //}
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
        private void Button_ClickUpdateBus1(object sender, RoutedEventArgs e)//כפתור עדכון מתוך רשימת האוטובוסים
        {
            var fxElt = sender as FrameworkElement;
            BusBO CurrentBus = fxElt.DataContext as BusBO;
            updateBus updateBusWindow = new updateBus(bl, CurrentBus);
            updateBusWindow.ShowDialog();
            RefreshBusesLB();
        }
        private void Button_ClickDeleteBus1(object sender, RoutedEventArgs e)//כפתור מחיקה מתוך הרשימה
        {
            var fxElt = sender as FrameworkElement;
            BusBO CurrentBus = fxElt.DataContext as BusBO;
            try
            {
                MessageBoxResult result = MessageBox.Show("?האם אתה בטוח שברצונך למחוק את האוטובוס", "אישור מחיקה", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    bl.deleteBus(CurrentBus);
                    MessageBox.Show("!בוצע בהצלחה", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error); }
            RefreshBusesLB();
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
        private void Button_ClickAddTripToLine(object sender, RoutedEventArgs e)
        {
            var fxElt = sender as FrameworkElement;
            BusLineBO CurrentLine = fxElt.DataContext as BusLineBO;
            addTripToLine addTripToLineWindow = new addTripToLine(bl, CurrentLine);
            addTripToLineWindow.ShowDialog();
            RefreshLinesLB();
        }
        private void Button_ClickDeleteTripFromLine(object sender, RoutedEventArgs e)
        {
            var fxElt = sender as FrameworkElement;
            BusLineBO CurrentLine = fxElt.DataContext as BusLineBO;
            deleteTripFromLine deleteTripFromLineWindow = new deleteTripFromLine(bl, CurrentLine);
            deleteTripFromLineWindow.ShowDialog();
            RefreshLinesLB();
        }
    }
}
