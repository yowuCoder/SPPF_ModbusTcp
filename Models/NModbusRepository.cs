//using NLog;

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using kl_modbus.Models;
using NModbus;
using Npgsql;
using Serilog;
using WpfApp1.Models;

namespace app1.Models
{
    public class NModbusRepository : IModbusRepository
    {

    //    private readonly TagRepository _dr;
        public NModbusRepository()
        {
            string text = File.ReadAllText(Directory.GetCurrentDirectory() + @"\ConnectDB.txt");
           // _dr = new TagRepository(new NpgsqlConnection(text));
        }

        /// <summary>
        ///     Simple Modbus serial ASCII master read holding registers example.
        /// </summary>

        /// <summary>
        ///     Simple Modbus TCP master read inputs example.
        /// </summary>


        public void ModbusTcpMasterReadHoldingRegisters(string ip, ushort startAddress, ushort numInputs)
        {
            using (System.Net.Sockets.TcpClient client = new System.Net.Sockets.TcpClient(ip, 502) { SendTimeout = 2000, ReceiveTimeout = 2000 })
            {
                var factory = new ModbusFactory();
                IModbusMaster master = factory.CreateMaster(client);


                var inputs = master.ReadHoldingRegisters(0, startAddress, numInputs);
                

            }


        }
        public void ModbusTcpMasterReadInputRegisters(string ip, ushort startAddress, ushort numInputs)
        {
            using (System.Net.Sockets.TcpClient client = new System.Net.Sockets.TcpClient(ip, 502) { SendTimeout = 2000, ReceiveTimeout = 2000 })
            {
                var factory = new ModbusFactory();
                IModbusMaster master = factory.CreateMaster(client);


                var inputs = master.ReadInputRegisters(0, startAddress, numInputs);

            }


        }
        public void ModbusTcpMasterReadCoils(string ip, ushort startAddress, ushort numInputs)
        {
            using (System.Net.Sockets.TcpClient client = new System.Net.Sockets.TcpClient(ip, 502) { SendTimeout = 2000, ReceiveTimeout = 2000 })
            {
                var factory = new ModbusFactory();
                IModbusMaster master = factory.CreateMaster(client);


                var inputs = master.ReadCoils(0, startAddress, numInputs);

            }


        }
        public void ModbusTcpMasterReadInputs(string ip, ushort startAddress, ushort numInputs)
        {
            using (System.Net.Sockets.TcpClient client = new System.Net.Sockets.TcpClient(ip, 502) { SendTimeout = 2000, ReceiveTimeout = 2000 })
            {
                var factory = new ModbusFactory();
                IModbusMaster master = factory.CreateMaster(client);

                var inputs = master.ReadInputs(0, startAddress, numInputs);

            }


        }



        public async Task<ushort[]> ReadInputRegistersAsync(string ip, ushort startAddress, ushort numInputs)
        {
            try
            {
                var client = new System.Net.Sockets.TcpClient();

                if (!client.ConnectAsync(ip, 502).Wait(3000))
                {
                    client.Dispose();
                    return new ushort[numInputs];
                }
                var factory = new ModbusFactory();
                IModbusMaster master = factory.CreateMaster(client);
                var inputs = await master.ReadInputRegistersAsync(1, startAddress, numInputs);
                client.Close();
                return inputs;

            }
            catch (Exception e)
            {
                Log.Debug(e.ToString());
                Log.Debug("ReadInputRegistersAsync: " + ip + " " + startAddress + " " + numInputs + "" + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt"));
                throw;
            }


        }


        public async Task<ushort[]> ReadHoldingRegistersAsync(string ip, ushort startAddress, ushort numInputs)
        {
            try
            {

                var client = new System.Net.Sockets.TcpClient();
                if (!client.ConnectAsync(ip, 502).Wait(3000))
                {
                    client.Dispose();
                    return new ushort[numInputs];
                }


                var factory = new ModbusFactory();
                IModbusMaster master = factory.CreateMaster(client);
                var inputs = await master.ReadHoldingRegistersAsync(1, startAddress, numInputs);

                client.Close();

                return inputs;

            }
            catch (Exception e)
            {
                Log.Debug("ReadHoldingRegistersAsync: " + ip + " " + startAddress + " " + numInputs + "" + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt"));

                Log.Debug(e.ToString());
                throw;
            }




        }

        public async Task<bool[]> ReadCoilsAsync(string ip, ushort startAddress, ushort numInputs)
        {
            try
            {

                var client = new System.Net.Sockets.TcpClient();
                if (!client.ConnectAsync(ip, 502).Wait(3000))
                {
                    client.Dispose();

                    return new bool[numInputs];


                }
                var factory = new ModbusFactory();
                IModbusMaster master = factory.CreateMaster(client);


                //await _dr.UpdateHostList(ip, true);
                var inputs = await master.ReadCoilsAsync(1, startAddress, numInputs);


                client.Close();

                return inputs;

            }
            catch (Exception e)
            {
                Log.Debug(e.ToString());
                Log.Debug("ReadCoilsAsync: " + ip + " " + startAddress + " " + numInputs + "" + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt"));
                //   await _dr.UpdateHostList(ip, false);
                throw;
            }
        }
        public async Task<bool[]> ReadInputsAsync(string ip, ushort startAddress, ushort numInputs)
        {
            try
            {

                var client = new System.Net.Sockets.TcpClient();
                if (!client.ConnectAsync(ip, 502).Wait(3000))
                {
                    client.Dispose();
                    return new bool[numInputs];
                }
                var factory = new ModbusFactory();
                IModbusMaster master = factory.CreateMaster(client);


                var inputs = await master.ReadInputsAsync(1, startAddress, numInputs);
                client.Close();


                return inputs;

            }
            catch (Exception e)
            {
                //  await _dr.UpdateHostList(ip, false);
                Log.Debug("ReadInputsAsync: " + ip + " " + startAddress + " " + numInputs + " " + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt"));
                Log.Debug(e.ToString());
                throw;
            }



        }


    }
}
