using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;
using ProjectoIHC.DataModel;
using System.ComponentModel;
using System.Globalization;

namespace ProjectoIHC
{
    public partial class AddSensor : PhoneApplicationPage
    {
        Sensor sen = new Sensor();
        
        SolidColorBrush SensorColor = new SolidColorBrush(Color.FromArgb(255, 0x16, 0xa0, 0x85));
        String SensorTitle = "asd";
        String SensorID = "asd";
        
        public AddSensor()
        {
            InitializeComponent();

            this.sen.Title = SensorTitle;
            this.sen.TitleColor = SensorColor.Color.ToString();
            this.sen.ID = SensorID;
            this.sen.Latitude = 0.00;
            this.sen.Longitude = 0.00;

            byte a = byte.Parse(sen.TitleColor.Substring(1, 2), NumberStyles.HexNumber);
            byte r = byte.Parse(sen.TitleColor.Substring(3, 2), NumberStyles.HexNumber);
            byte g = byte.Parse(sen.TitleColor.Substring(5, 2), NumberStyles.HexNumber);
            byte b = byte.Parse(sen.TitleColor.Substring(7, 2), NumberStyles.HexNumber);

            this.ColorSelected.Fill = new SolidColorBrush(Color.FromArgb(a,r,g,b));
            this.ID.Text = sen.ID;
            this.Title.Text = sen.Title;
           
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Cancel?", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        private void ChooseColor_Click(object sender, RoutedEventArgs e)
        {
            String colorName = ((Button)sender).Name;

            if (colorName.Equals("ColorGreen"))
            {
                SolidColorBrush SensorColor = new SolidColorBrush(Color.FromArgb(255, 0x16, 0xa0, 0x85));
                this.ColorSelected.Fill = SensorColor;
                this.sen.TitleColor = "#FF00FF00";
            }

            if (colorName.Equals("ColorBlue"))
            {
                SolidColorBrush SensorColor = new SolidColorBrush(Color.FromArgb(255, 0x29, 0x80, 0xb9));
                this.ColorSelected.Fill = SensorColor;
                this.sen.TitleColor = "#FF0000FF";
            }

            if (colorName.Equals("ColorOrange"))
            {
                SolidColorBrush SensorColor = new SolidColorBrush(Color.FromArgb(255, 0xf3, 0x9c, 0x12));
                this.ColorSelected.Fill = SensorColor;
                this.sen.TitleColor = "#FFFF9900";
            }

            if (colorName.Equals("ColorRed"))
            {
                SolidColorBrush SensorColor = new SolidColorBrush(Color.FromArgb(255, 0xc0, 0x39, 0x2b));
                this.ColorSelected.Fill = SensorColor ;
                this.sen.TitleColor = "#FFFF0000";
            }

            if (colorName.Equals("ColorGray"))
            {
                SolidColorBrush SensorColor = new SolidColorBrush(Color.FromArgb(255, 0xbd, 0xc3, 0xc7));
                this.ColorSelected.Fill = SensorColor;
                this.sen.TitleColor = "FF808080";
            }
        }

        private void Help_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Sensors ID's are like these\n XAS1-123A-AS22-XZC1");
        }

        private void CheckSensor_Click(object sender, EventArgs e)
        {
            SensorID = this.ID.Text;
            this.sen.ID = SensorID;

            if (SensorID.Equals("XX2A-DF2A-CS4E-12A3") || SensorID.Equals("12A3-DF2A-XX2A-CS4E") || SensorID.Equals("98AS-876S-ASD7-AD54") || SensorID.Equals("ZXDC-XDER-432D-3445"))
            {
                MessageBox.Show("Connected Successfully!");
            }
            else
            {
                SensorID = "";
                this.sen.ID = SensorID;

                MessageBox.Show("Connection Failed, try another Sensor ID");
            }
        }

        private void AddSensor_Click(object sender, EventArgs e)
        {
            App.DataModel.AddSensor(sen);
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));

        }
    }
}