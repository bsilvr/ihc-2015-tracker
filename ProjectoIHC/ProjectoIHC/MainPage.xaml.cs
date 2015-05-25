using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ProjectoIHC.Resources;
using System.Windows.Media;
using ProjectoIHC.DataModel;
using System.ComponentModel;

namespace ProjectoIHC
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            ApplicationBar = (Microsoft.Phone.Shell.ApplicationBar)Resources["DefaultAppBar"];
            
            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            var sensors = await App.DataModel.GetSensors();

            this.DataContext = sensors;
            //string arg = String.Format("?label={0}&latitude={1}&longitude={2}", sensor, latitude, longitude);
            //NavigationService.Navigate(new Uri("/ViewMap.xaml" + arg, UriKind.Relative));
        }

        private void AddSensor_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/AddSensor.xaml", UriKind.Relative));
        }

        private void LongListSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                LongListSelector ls = sender as LongListSelector;
                Sensor si = ls.SelectedItem as Sensor;
                foreach (Sensor sd in ls.ItemsSource)
                {
                    if (sd != si)
                    {
                        sd.IsSelected = false;
                    }
                }
                si.IsSelected = true;
            }
            catch 
            {
                
            }
            ApplicationBar = (Microsoft.Phone.Shell.ApplicationBar)Resources["SelectedAppBar"];

        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Delete?", MessageBoxButton.OKCancel) == MessageBoxResult.OK) 
            {
                var mySensor = myList.SelectedItem as Sensor;
                App.DataModel.DeleteSensor(mySensor);
                ApplicationBar = (Microsoft.Phone.Shell.ApplicationBar)Resources["DefaultAppBar"];
                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            }
                
        }

        private void myList_LostFocus(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Are you sure?");
            ApplicationBar = (Microsoft.Phone.Shell.ApplicationBar)Resources["DefaultAppBar"];
        }

        private void ApplicationBarIconButton_Click_1(object sender, EventArgs e)
        {
            var mySensor = myList.SelectedItem as Sensor;
            string arg = String.Format("?label={0}&latitude={1}&longitude={2}", mySensor.Title, mySensor.Latitude, mySensor.Longitude);
            NavigationService.Navigate(new Uri("/ViewMap.xaml" + arg, UriKind.Relative));
        }

        private void selected_Click(object sender, RoutedEventArgs e)
        {
            //botao desselecionar
        }

        private void ApplicationBarIconButton_Click_2(object sender, EventArgs e)
        {
            var mySensor = myList.SelectedItem as Sensor;
            try 
            {
                App.DataModel.DeleteSensor(mySensor);
                string arg = String.Format("?label={0}&latitude={1}&longitude={2}&cor={3}&sensorID={4}", mySensor.Title, mySensor.Latitude, mySensor.Longitude, mySensor.TitleColor.Substring(1, 8), mySensor.ID);
                NavigationService.Navigate(new Uri("/EditSensor.xaml" + arg, UriKind.Relative));
            }
            catch 
            {
                App.DataModel.AddSensor(mySensor);
            }
            
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            e.Cancel = true;
            if (MessageBox.Show("Are you sure you want to exit?", "Confirm Exit?",
                                    MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                
                Application.Current.Terminate();
            }
        }
    }
}