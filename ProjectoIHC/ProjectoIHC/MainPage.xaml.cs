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

<<<<<<< 65de9d08c44d1b978667b394cce0e25801939c59
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            string latitude = Convert.ToString(40.630339);
            string longitude = Convert.ToString(-8.657356);
            string sensor="Dog";

            string arg = String.Format("?label={0}&latitude={1}&longitude={2}", sensor, latitude, longitude);
            NavigationService.Navigate(new Uri("/ViewMap.xaml" + arg, UriKind.Relative));
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
=======
        private void AddSensor_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/AddSensor.xaml", UriKind.Relative));
        }
>>>>>>> 51b9af8374e5fb90dcde0fe599e79bb824eb7016
    }
}