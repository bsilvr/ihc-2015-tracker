﻿using System;
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
using System.Windows.Controls.Primitives;

namespace ProjectoIHC
{
    public partial class EditSensor : PhoneApplicationPage
    {
        Sensor sen = new Sensor();
        Sensor old = new Sensor();
        
        SolidColorBrush SensorColor = new SolidColorBrush(Color.FromArgb(255, 0x16, 0xa0, 0x85));
        String SensorTitle = "";
        String SensorID = "";
        
        public EditSensor()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e) 
        {
            
            base.OnNavigatedTo(e);
            double latitude = 0;
            double longitude = 0;
            string tmp;
            string color = "";

            //color = NavigationContext.QueryString["color"];
            if (NavigationContext.QueryString.TryGetValue("cor", out tmp))
            {
                color = "#" + tmp;
                old.TitleColor = color;
            }

            if (NavigationContext.QueryString.TryGetValue("sensorID", out tmp))
            {
                SensorID = tmp;
                old.ID = SensorID;
            }
            if (NavigationContext.QueryString.TryGetValue("label", out tmp))
            {
                SensorTitle = tmp;
                old.Title = SensorTitle;
            }

            if (NavigationContext.QueryString.TryGetValue("latitude", out tmp))
            {
                latitude = Convert.ToDouble(tmp);
                old.Latitude = latitude;
            }

            if (NavigationContext.QueryString.TryGetValue("longitude", out tmp))
            {
                longitude = Convert.ToDouble(tmp);
                old.Longitude = longitude;
            }

           

            this.sen.Title = SensorTitle;
            this.sen.TitleColor = color;
            this.sen.ID = SensorID;
            this.sen.Latitude = latitude;
            this.sen.Longitude = longitude;

            byte a = byte.Parse(sen.TitleColor.Substring(1, 2), NumberStyles.HexNumber);
            byte r = byte.Parse(sen.TitleColor.Substring(3, 2), NumberStyles.HexNumber);
            byte g = byte.Parse(sen.TitleColor.Substring(5, 2), NumberStyles.HexNumber);
            byte b = byte.Parse(sen.TitleColor.Substring(7, 2), NumberStyles.HexNumber);

            this.ColorSelected.Fill = new SolidColorBrush(Color.FromArgb(a, r, g, b));
            this.ID.Text = sen.ID;
            this.Title.Text = sen.Title;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Cancel?", MessageBoxButton.OKCancel) == MessageBoxResult.OK) 
            {
                App.DataModel.AddSensor(old);                
                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));                
            }
        }

        private void ChooseColor_Click(object sender, RoutedEventArgs e)
        {
            String colorName = ((Button)sender).Name;

            if (colorName.Equals("ColorGreen"))
            {
                SolidColorBrush SensorColor = new SolidColorBrush(Color.FromArgb(255, 0x16, 0xa0, 0x85));
                this.ColorSelected.Fill = SensorColor;
                this.sen.TitleColor = "#FF16a085";
            }

            if (colorName.Equals("ColorBlue"))
            {
                SolidColorBrush SensorColor = new SolidColorBrush(Color.FromArgb(255, 0x29, 0x80, 0xb9));
                this.ColorSelected.Fill = SensorColor;
                this.sen.TitleColor = "#FF2980b9";
            }

            if (colorName.Equals("ColorOrange"))
            {
                SolidColorBrush SensorColor = new SolidColorBrush(Color.FromArgb(255, 0xf3, 0x9c, 0x12));
                this.ColorSelected.Fill = SensorColor;
                this.sen.TitleColor = "#FFf39c12";
            }

            if (colorName.Equals("ColorRed"))
            {
                SolidColorBrush SensorColor = new SolidColorBrush(Color.FromArgb(255, 0xc0, 0x39, 0x2b));
                this.ColorSelected.Fill = SensorColor ;
                this.sen.TitleColor = "#FFc0392b";
            }

            if (colorName.Equals("ColorGray"))
            {
                SolidColorBrush SensorColor = new SolidColorBrush(Color.FromArgb(255, 0xbd, 0xc3, 0xc7));
                this.ColorSelected.Fill = SensorColor;
                this.sen.TitleColor = "FFbdc3c7";
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

            if (SensorID.Equals("XX2A-DF2A-CS4E-12A3") )
            {
                MessageBox.Show("Connected Successfully!");
            }
            else if (SensorID.Equals("12A3-DF2A-XX2A-CS4E"))
            {
                MessageBox.Show("Connected Successfully!");
            }
            else if (SensorID.Equals("98AS-876S-ASD7-AD54"))
            {
                MessageBox.Show("Connected Successfully!");
            }
            else if (SensorID.Equals("ZXDC-XDER-432D-3445"))
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

        private void EditSensor_Click(object sender, EventArgs e)
        {
            sen.Title = this.Title.Text;

            if (SensorID.Equals("XX2A-DF2A-CS4E-12A3"))
            {
                sen.Latitude = 40.603004;
                sen.Longitude = -8.642174;
            }
            else if (SensorID.Equals("12A3-DF2A-XX2A-CS4E"))
            {
                sen.Latitude = 40.603804;
                sen.Longitude = -8.642174;
            }
            else if (SensorID.Equals("98AS-876S-ASD7-AD54"))
            {
                sen.Latitude = 40.603850;
                sen.Longitude = -8.642024;
            }
            else if (SensorID.Equals("ZXDC-XDER-432D-3445"))
            {
                sen.Latitude = 40.603434;
                sen.Longitude = -8.643474;
            }
            App.DataModel.AddSensor(sen);
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            //var mySensor = myList.SelectedItem as Sensor;
        }

        private void BtnFindSensor_Click(object sender, RoutedEventArgs e)
        {
            Popup codePopup = new Popup();
            TextBlock popupText = new TextBlock();
            popupText.Text = "Popup Text";
            codePopup.Child = popupText;
        }
    }
}