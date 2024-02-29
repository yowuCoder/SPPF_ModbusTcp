using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kl_modbus_pm.Helper.ModbusHelper
{
    public static class RegisterConverter
    {
        public static string ConvertRegistersToString(int[] registers, int offset, int stringLength)
        {
            byte[] array = new byte[stringLength];
            byte[] array2 = new byte[2];
            checked
            {
                for (int i = 0; i < unchecked(stringLength / 2); i++)
                {
                    array2 = BitConverter.GetBytes(registers[offset + i]);
                    array[i * 2] = array2[0];
                    array[i * 2 + 1] = array2[1];
                }

                return Encoding.Default.GetString(array);
            }
        }

        public static int ConvertRegistersToInt(int[] registers)
        {
            if (registers.Length != 2)
            {
                throw new ArgumentException("Input Array length invalid - Array langth must be '2'");
            }

            int value = registers[1];
            int value2 = registers[0];
            byte[] bytes = BitConverter.GetBytes(value);
            byte[] bytes2 = BitConverter.GetBytes(value2);
            byte[] value3 = new byte[4]
            {
                bytes2[0],
                bytes2[1],
                bytes[0],
                bytes[1]
            };
            return BitConverter.ToInt32(value3, 0);
        }
        public static float ConvertRegistersToFloat(int[] registers)
        {
            if (registers.Length != 2)
            {
                throw new ArgumentException("Input Array length invalid - Array langth must be '2'");
            }

            int value = registers[1];
            int value2 = registers[0];
            byte[] bytes = BitConverter.GetBytes(value);
            byte[] bytes2 = BitConverter.GetBytes(value2);
            byte[] value3 = new byte[4]
            {
                bytes2[0],
                bytes2[1],
                bytes[0],
                bytes[1]
            };
            return BitConverter.ToSingle(value3, 0);
        }
    }
}
