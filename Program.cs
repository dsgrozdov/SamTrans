using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using OwenioNet;
using OwenioNet.IO;
using OwenioNet.Exceptions;
using OwenioNet.Types;
using OwenioNet.DataConverter.Converter;

namespace rsController
{
    class Program
    {
        static void Main(string[] args)
        {
            SerialPortAdapter port1 = new SerialPortAdapter(3, 9600, Parity.None, 8, StopBits.One);
            port1.Open();
            var owenProtocol = OwenProtocolMaster.Create(port1);
            if (port1.IsOpened != true)
            {
                Console.WriteLine("Ошибка открытия порта COM3: {0}", port1.ToString());
            }
            else
            {
                try
                {   
                    var dataFromDevice = owenProtocol.OwenRead(16, AddressLengthType.Bits8, "dev");
                    Console.WriteLine($"Value - {BitConverter.ToString(dataFromDevice)}");
                    Console.ReadLine();
                    port1.Close();
                    try
                    {
                        var converterString = new ConverterAscii(16);
                        var value = converterString.ConvertBack(dataFromDevice);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        Console.ReadLine();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    Console.ReadLine();
                }
            }
        }
    }
}
