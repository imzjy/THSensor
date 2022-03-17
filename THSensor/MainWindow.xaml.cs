using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NModbus;
using NModbus.IO;
using NModbus.Serial;

namespace THSensor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Sensor sensor = new Sensor { Temp = 0m, Hum = 0m };
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            Unloaded += MainWindow_Unloaded;
            Main.DataContext = sensor;
        }

        private void MainWindow_Unloaded(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            port.Close();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            SetupModbus();
            StartTimer();
        }

        SerialPort port = null;
        IModbusMaster master = null;
        private void SetupModbus()
        {
            var portName = ConfigurationManager.AppSettings["port"] ?? "COM3";
            port = new SerialPort(portName);
            // configure serial port
            port.DataBits = 8;
            port.Parity = Parity.None;
            port.StopBits = StopBits.One;
            port.Open();

            var factory = new ModbusFactory();
            master = factory.CreateRtuMaster(port);
            master.Transport.ReadTimeout = 5000; //5s
            master.Transport.WriteTimeout = 5000; //5s
        }

        Timer timer = new Timer(500);
        public void StartTimer()
        {
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            ReadRegisters();
        }

        public void ReadRegisters()
        {
            //timer.Stop();

            byte slaveId = 1;
            ushort startAddress = 0x0001;
            ushort numberOfPoints = 0x0002;

            // read registers
            var temp = master.ReadInputRegisters(slaveId, startAddress, numberOfPoints);
            if (temp.Length == 2)
            {
                this.sensor.Temp = (temp[0] / 10m);
                this.sensor.Hum = (temp[1] / 10m);
            }
        }
    }
}
