//using EasyModbus;
using kl_modbus.Helper;
using kl_modbus_pm.Helper.ModbusHelper;
using kl_modbus_pm.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Xml.Linq;
using WpfApp1;
using WpfApp1.Models;

namespace kl_modbus.Command
{
    public class InputRegisterCommand : ModbusCommand
    {
        public InputRegisterCommand(string ip, List<TaglistDTO> tagLists, DatabaseRepository dr) : base(ip, tagLists, dr)
        {

        }

        private readonly List<Task<ushort[]>> InputRegisterTasks = new List<Task<ushort[]>>();

        private readonly Dictionary<ushort, ushort> RegisterMap = new Dictionary<ushort, ushort>();

        private async Task HandleData()
        {


            foreach (var i in _tagList)
            {
                //var value = RegisterMap[Convert.ToUInt16(i.address.Substring(1))];
                bool success = RegisterMap.TryGetValue(Convert.ToUInt16(i.address.Substring(1)), out ushort value);

                if (!success)
                {
                    continue;
                }
                if (i.type.ToString() == "str")
                {



                    var strValue = RegisterConverter.ConvertRegistersToString(new int[] { Convert.ToInt32(value) }, 0, 2);

                    if (strValue.Contains("\u0000"))
                    {
                        strValue = strValue.Replace("\u0000", "");

                    }
                    // Console.WriteLine($"strValue {strValue}");
                    DateTime Time = DateTime.Now;
                    _Object.Add(new TagReal(Time.ToString("yyyyMMddHHmmss"), strValue, "", i.tag_id));

                }
                else if (i.type.ToString() == "int16")
                {
                    DateTime Time = DateTime.Now;

                    _Object.Add(new TagReal(Time.ToString("yyyyMMddHHmmss"), ((short)value / Math.Pow(10, i.point)).ToString(), "", i.tag_id));


                }
                else if (i.type.ToString() == "int32")
                {
                    var nextValue = RegisterMap[(ushort)(Convert.ToUInt16(i.address.Substring(1)) + 1)];

                    var int32Value = RegisterConverter.ConvertRegistersToInt(new int[] { Convert.ToInt32(value), Convert.ToInt32(nextValue) });


                    DateTime Time = DateTime.Now;
                    _Object.Add(new TagReal(Time.ToString("yyyyMMddHHmmss"), (int32Value / Math.Pow(10, i.point)).ToString(), "", i.tag_id));



                }
                else if (i.type.ToString() == "float32")
                {
                    var nextValue = RegisterMap[(ushort)(Convert.ToUInt16(i.address.Substring(1)) + 1)];


                    var int32Value = RegisterConverter.ConvertRegistersToFloat(new int[] { Convert.ToInt32(value), Convert.ToInt32(nextValue) });
                    string float32Value = "";
                    if (i.point == 2)
                        float32Value = int32Value.ToString("0.00");
                    else if (i.point == 0)
                        float32Value = int32Value.ToString("0");
                    else if (i.point == 1)
                        float32Value = int32Value.ToString("0.0");

                    DateTime Time = DateTime.Now;
                    _Object.Add(new TagReal(Time.ToString("yyyyMMddHHmmss"), float32Value, "", i.tag_id));
                }


            }

         //   await _dr.PostListAsync(_Object);
        }

        public override async Task Execute()
        {
            try
            {

                int count = 0;


                foreach (var address in _startAddressList)
                {
                    Log.Information($"_startAddressList   ReadInputRegistersAsync:  {address}  {DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt")}");
                    InputRegisterTasks.Add(_mr.ReadInputRegistersAsync(_ipAddress, (ushort)(address - 1), 100));
                    // await Task.Delay(300);
                }
                var results = await Task.WhenAll(InputRegisterTasks);

                var str = await _mr.ReadInputRegistersAsync("192.168.122.51", 200, 10);

                foreach (var result in results)
                {
                    int j = 0;
                    foreach (var i in result)
                    {

                        RegisterMap.Add((ushort)(_startAddressList[count] + j), i);
                        j++;
                    }
                    count++;
                }

                foreach (var i in RegisterMap)
                {
                    // Log.Information($"原始data ip {_ipAddress} address {i.Key}  value {i.Value}");

                }
                if (RegisterMap.Count > 0)
                {
                    await HandleData();
                  //  await _dr.RealToHistoryByMedthodAsync("3", _tagList[0].host_id);
                }
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                throw;
            }
        }
    }


}
