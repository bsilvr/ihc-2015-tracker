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

        private void OnNavigatedTo(object sender, EventArgs e)
        {
            string latitude = Convert.ToString(40.630339);
            string longitude = Convert.ToString(-8.657356);
            string sensor="Dog";

            string arg = String.Format("?label={0}&latitude={1}&longitude={2}", sensor, latitude, longitude);
            NavigationService.Navigate(new Uri("/ViewMap.xaml" + arg, UriKind.Relative));
        }

        private void AddSensor_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/AddSensor.xaml", UriKind.Relative));
        }
    }
}