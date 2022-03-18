using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace THSensor
{
    public class Sensor : ObservableObject
    {
        private decimal temp = 0m;
        public decimal Temp
        {
            get => temp;
            set {
                SetProperty(ref temp, value, nameof(Temp));
                OnPropertyChanged(nameof(Temperature));
            }
        }

        public string Temperature
        {
            get { return $"{Temp:0.0}℃"; }
        }

        public string Humidity
        {
            get { return $"{Hum:0.0}%"; }
        }

        private decimal hum = 0m;
        public decimal Hum
        {
            get => hum;
            set
            {
                SetProperty(ref hum, value, nameof(Hum));
                OnPropertyChanged(nameof(Humidity));
            }
        }
    }
}

