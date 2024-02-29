using NModbus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace kl_modbus.Models
{
    public interface IModbusRepository
    {

        Task<ushort[]> ReadInputRegistersAsync(string ip, ushort startAddress, ushort numInputs);


        Task<ushort[]> ReadHoldingRegistersAsync(string ip, ushort startAddress, ushort numInputs);


        Task<bool[]> ReadCoilsAsync(string ip, ushort startAddress, ushort numInputs);

        Task<bool[]> ReadInputsAsync(string ip, ushort startAddress, ushort numInputs);

    }
}
