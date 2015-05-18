using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using System.IO;
using System.Windows.Media;
using System.ComponentModel;

namespace ProjectoIHC.DataModel
{
    public class Sensor : INotifyPropertyChanged
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public string TitleColor { get; set; }

        
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        private bool _is_selected;

        public Sensor() 
        {
            this.IsSelected = false;
            this.NonHighlightColor = new SolidColorBrush(Colors.Transparent);
            this.HighLightColor = new SolidColorBrush(Colors.Red);
        }

        public bool IsSelected
        {
            get { return _is_selected; }
            set
            {
                _is_selected = value;
                OnPropertyChanged("HighlightBackgroundColor");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public SolidColorBrush HighlightBackgroundColor
        {
            get { if (IsSelected) return HighLightColor; else return NonHighlightColor; }
        }

        private SolidColorBrush HighLightColor { get; set; }

        private SolidColorBrush NonHighlightColor { get; set; }
    }

    public class DataSource
    {
        private ObservableCollection<Sensor> _sensors = new ObservableCollection<Sensor>();

        const string fileName = "sensors_list.json";

        public async Task<ObservableCollection<Sensor>> GetSensors()
        {
            await ensureDataLoaded();
            return _sensors;
        }

        private async Task ensureDataLoaded()
        {
            if (_sensors.Count == 0)
                await getSensorsDataAsync();

            return;
        }

        private async Task getSensorsDataAsync()
        {
            if (_sensors.Count != 0)
                return;

            var jsonSerializer = new DataContractJsonSerializer(typeof(ObservableCollection<Sensor>));

            try
            {
                using (var stream = await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync(fileName))
                {
                    _sensors = (ObservableCollection<Sensor>)jsonSerializer.ReadObject(stream);
                }
            }
            catch
            {
                _sensors = new ObservableCollection<Sensor>();
            }
        }

        public async void AddSensor(Sensor sensor)
        {
            if (_sensors == null)
                _sensors = new ObservableCollection<Sensor>();
            _sensors.Add(sensor);
            await saveSensorDataAsync();
        }

        public async void DeleteSensor(Sensor sensor)
        {
            _sensors.Remove(sensor);
            await saveSensorDataAsync();
        }

        private async Task saveSensorDataAsync()
        {
            var jsonSerializer = new DataContractJsonSerializer(typeof(ObservableCollection<Sensor>));

            using(var stream = await ApplicationData.Current.LocalFolder.OpenStreamForWriteAsync(fileName, CreationCollisionOption.ReplaceExisting))
            {
                jsonSerializer.WriteObject(stream, _sensors);
            }
        }
    }
}
