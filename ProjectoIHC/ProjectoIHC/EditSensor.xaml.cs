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
using System.Windows.Controls.Primitives;
using Windows.Phone.UI.Input;

namespace ProjectoIHC
{
    public partial class EditSensor : PhoneApplicationPage
    {
        Sensor sen = new Sensor();
        Sensor old = new Sensor();
        
        SolidColorBrush SensorColor = new SolidColorBrush(Color.FromArgb(255, 0x16, 0xa0, 0x85));
        String SensorTitle = "";
        String SensorID = "";

        ListPicker lp;
        StackPanel sp;
        CustomMessageBox cmb;
        
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
            if (MessageBox.Show("Are you sure? Changes will be discarded", "Cancel?", MessageBoxButton.OKCancel) == MessageBoxResult.OK) 
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

            if (string.Equals(SensorID, "XX2A-DF2A-CS4E-12A3", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Connected Successfully!");
            }
            else if (string.Equals(SensorID, "12A3-DF2A-XX2A-CS4E", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Connected Successfully!");
            }
            else if (string.Equals(SensorID, "98AS-876S-ASD7-AD54", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Connected Successfully!");
            }
            else if (string.Equals(SensorID, "ZXDC-XDER-432D-3445", StringComparison.OrdinalIgnoreCase))
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

            if (string.Equals(SensorID, "XX2A-DF2A-CS4E-12A3", StringComparison.OrdinalIgnoreCase))
            {
                sen.Latitude = 40.603004;
                sen.Longitude = -8.642174;
            }
            else if (string.Equals(SensorID, "12A3-DF2A-XX2A-CS4E", StringComparison.OrdinalIgnoreCase))
            {
                sen.Latitude = 40.603804;
                sen.Longitude = -8.642174;
            }
            else if (string.Equals(SensorID, "98AS-876S-ASD7-AD54", StringComparison.OrdinalIgnoreCase))
            {
                sen.Latitude = 40.603850;
                sen.Longitude = -8.642024;
            }
            else if (string.Equals(SensorID, "ZXDC-XDER-432D-3445", StringComparison.OrdinalIgnoreCase))
            {
                sen.Latitude = 40.603434;
                sen.Longitude = -8.643474;
            }
            App.DataModel.AddSensor(sen);
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            //var mySensor = myList.SelectedItem as Sensor;
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Are you sure? Changes will be discarded", "Cancel?", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                App.DataModel.AddSensor(old);
                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            }
            e.Cancel = true;
        }

        private void FindSensors_Click(object sender, RoutedEventArgs e)
        {
            sp = new StackPanel() { Orientation = System.Windows.Controls.Orientation.Vertical };

            lp = new ListPicker();

            lp.Items.Add(new ListPickerItem() { Content = "XX2A-DF2A-CS4E-12A3" });
            lp.Items.Add(new ListPickerItem() { Content = "12A3-DF2A-XX2A-CS4E" });
            lp.Items.Add(new ListPickerItem() { Content = "ZXDC-XDER-432D-3445" });

            sp.Children.Add(lp);

            TiltEffect.SetIsTiltEnabled(sp, true);

            cmb = new CustomMessageBox()
            {
                Content = sp,
                Opacity = 0.98,


                RightButtonContent = "Clear",
                LeftButtonContent = "Select",
            };

            cmb.Dismissing += cmb_Dismissing;
            lp.SelectionChanged += lp_SelectionChanged;

            cmb.Show();
        }

        void lp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lp.SelectedItem != null)
            {
                this.ID.Text = ((ListPickerItem)lp.SelectedItem).Content.ToString();
            }
        }

        void cmb_Dismissing(object sender, DismissingEventArgs e)
        {
            if (e.Result == CustomMessageBoxResult.RightButton)
            {
                this.ID.Text = "";
            }
            if (e.Result == CustomMessageBoxResult.LeftButton)
            {
                this.ID.Text = ((ListPickerItem)lp.SelectedItem).Content.ToString();
            }
        }

    }
}