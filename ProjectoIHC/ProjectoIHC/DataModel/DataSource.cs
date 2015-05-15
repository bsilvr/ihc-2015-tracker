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

namespace ProjectoIHC.DataModel
{
    public class Sensor
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public SolidColorBrush TitleColor { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class DataSource
    {
        private ObservableCollection<Sensor> _sensors;

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
            if (_sensors.Count == 0)
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
