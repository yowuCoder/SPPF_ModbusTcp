using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using kl_modbus.Models;
using NModbus;
using Npgsql;
using Serilog;

namespace app1
{
    public class NModbusRepository : IModbusRepository
    {


        public NModbusRepository()
        {
     
        }


        public ushort[] ModbusTcpMasterReadHoldingRegisters(string ip, ushort startAddress, ushort numInputs)
        {
            try
            {
                using (System.Net.Sockets.TcpClient client = new System.Net.Sockets.TcpClient(ip, 502) { SendTimeout = 2000, ReceiveTimeout = 2000 })
                {
                    var factory = new ModbusFactory();
                    IModbusMaster master = factory.CreateMaster(client);


                    var inputs = master.ReadHoldingRegisters(0, startAddress, numInputs);
                    return inputs;
                }
            }
            catch (Exception ex) {
                throw ex;
            }
         
        }
        public ushort[] ModbusTcpMasterReadInputRegisters(string ip, ushort startAddress, ushort numInputs)
        {
            using (System.Net.Sockets.TcpClient client = new System.Net.Sockets.TcpClient(ip, 502) { SendTimeout = 2000, ReceiveTimeout = 2000 })
            {
                var factory = new ModbusFactory();
                IModbusMaster master = factory.CreateMaster(client);


                var inputs = master.ReadInputRegisters(1, startAddress, numInputs);
                Console.WriteLine("123");
                return inputs;
            }


        }
        public bool[] ModbusTcpMasterReadCoils(string ip, ushort startAddress, ushort numInputs)
        {
            try
            {
                using (System.Net.Sockets.TcpClient client = new System.Net.Sockets.TcpClient(ip, 502) { SendTimeout = 2000, ReceiveTimeout = 2000 })
                {
                    var factory = new ModbusFactory();
                    IModbusMaster master = factory.CreateMaster(client);


                    var inputs = master.ReadCoils(1, startAddress, numInputs);
                    //   Console.WriteLine("ReadCoils:" + startAddress + ",value:" + numInputs);
                    return inputs;
                }
            }
            catch (Exception ex) { 
                Console.WriteLine($"Could not read {ex.Message}");
                return [false];
            }
         
        }
        public void ModbusTcpMasterWriteHoldingRegisters(string ip, ushort startAddress, ushort _value)
        {
            using (System.Net.Sockets.TcpClient client = new System.Net.Sockets.TcpClient(ip, 502) { SendTimeout = 2000, ReceiveTimeout = 2000 })
            {
                var factory = new ModbusFactory();
                IModbusMaster master = factory.CreateMaster(client);


                 master.WriteSingleRegister(0, startAddress, _value);
            }
        }
        public void ModbusTcpMasterWriteCoils(string ip, ushort startAddress, bool value)
        {
            try
            {
                using (System.Net.Sockets.TcpClient client = new System.Net.Sockets.TcpClient(ip, 502) { SendTimeout = 2000, ReceiveTimeout = 2000 })
                {
                    var factory = new ModbusFactory();
                    IModbusMaster master = factory.CreateMaster(client);


                       //Console.WriteLine("startAddress:"+ startAddress+ ",value:"+ value);
                  master.WriteSingleCoil(1, startAddress, value);
                }
            }
            catch(Exception ex) {
                Console.WriteLine(ex.Message);
              
            };
         
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
