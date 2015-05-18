using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Windows.Devices.Geolocation;
using System.Device.Location;
using Microsoft.Phone.Maps.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ProjectoIHC
{
    public partial class ViewMap : PhoneApplicationPage
    {
        string label;
        double latitude;
        double longitude;
        GeoCoordinate pos = new GeoCoordinate();

        public ViewMap()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            string tmp;
            if(NavigationContext.QueryString.TryGetValue("label",out tmp))
            {
                label = tmp;
            }

            if (NavigationContext.QueryString.TryGetValue("latitude", out tmp))
            {
                latitude = Convert.ToDouble(tmp);

            }

            if (NavigationContext.QueryString.TryGetValue("longitude", out tmp))
            {
                longitude = Convert.ToDouble(tmp);
            }

            title.Text = label;

            pos.Latitude = latitude;
            pos.Longitude = longitude;

            myMap.SetView(pos, 17D);

            MapOverlay overlay = new MapOverlay
            {
                GeoCoordinate = new GeoCoordinate(latitude, longitude),
                Content = new Ellipse
                {
                    Fill = new SolidColorBrush(Colors.Red),
                    Width = 40,
                    Height = 40
                }
            };
            MapLayer layer = new MapLayer();
            layer.Add(overlay);

            myMap.Layers.Add(layer);
        }
    }
}