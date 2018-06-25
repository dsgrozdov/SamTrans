using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using OwenioNet;


namespace rsController
{
    class Program
    {
        static void Main(string[] args)
        {
            // стандартный интерфейс взаимодействия через ком порты
            string portName = "COM3";
            SerialPort serialPort = new SerialPort();
            //serialPort.BaudRate = 115200;
            ////serialPort.ReadTimeout = 500;
            //serialPort.DtrEnable = false;
            //serialPort.RtsEnable = true;
            try
            {
                serialPort.PortName = portName;
                serialPort.Open();
                // использование OwenioNet
                var owenProtocol = OwenProtocolMaster.Create(new SerialPort("COM3", 9600, Parity.Even, 5, StopBits.One));  // не открывает порт
                owenProtocol.OpenSerialPort(3, 9600, Parity.Even, 5, StopBits.One); // не открывает порт
                if (serialPort.IsOpen)
                    owenProtocol.OwenWrite(0x265, OwenioNet.Types.AddressLengthType.Bits11, "ab.L", new byte[] { 0x45, 0x87 });//без индекса 
                //owenProtocol.OwenRead(0x265, OwenioNet.Types.AddressLengthType.Bits11, "ab.L");//без индекса
                //owenProtocol.OwenWrite(0x265, OwenioNet.Types.AddressLengthType.Bits11, "ab.L", new byte[] { 0x45, 0x87 }, 0x3456);//с индексом
                owenProtocol.CloseSerialPort();
                Console.WriteLine("open");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.ReadLine();
            }
        }
    }
}
