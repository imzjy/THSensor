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
        public string Temperature
        {
            get { return $"{Temp:0.0}℃"; }
        }

        private decimal temp = 0m;
        public decimal Temp
        {
            get => temp;
            set {
                SetProperty(ref temp, value, nameof(Temp));
                OnPropertyChanged(nameof(Temperature));
            }
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

        public string Luminace
        {
            get { return $"{Lux:0.0}L"; }
        }

        private decimal lux = 0m;
        public decimal Lux
        {
            get => lux;
            set
            {
                SetProperty(ref lux, value, nameof(Lux));
                OnPropertyChanged(nameof(Luminace));
            }
        }
    }
}

