using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NModbus;

namespace app1
{
    internal class Class1
    {
        public Class1() {

            using (SerialPort port = new SerialPort("COM3"))
            {
                // configure serial port
                port.BaudRate = 9600;
                port.DataBits = 8;
                port.Parity = Parity.None;
                port.StopBits = StopBits.One;
                port.Open();

                var factory = new NModbus.ModbusFactory();
                IModbusMaster master = factory.CreateRtuMaster((NModbus.IO.IStreamResource)port);

                byte slaveId = 2;
                ushort startAddress = 6001;
                ushort[] registers = new ushort[] { 666, 999, 777 };

                // write three registers
                master.WriteMultipleRegisters(slaveId, startAddress, registers);
            }


        }
    }
}
