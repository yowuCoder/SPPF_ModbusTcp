using kl_modbus_pm.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1;
using WpfApp1.Models;

namespace kl_modbus.Command
{
    public class CoilsCommand : ModbusCommand
    {
        public CoilsCommand(string ip, List<TaglistDTO> tagLists, DatabaseRepository dr) : base(ip, tagLists, dr)
        {

        }
        private readonly List<Task<bool[]>> InputTasks = new List<Task<bool[]>>();
        private readonly Dictionary<ushort, bool> RegisterMap = new Dictionary<ushort, bool>();
        private async Task HandleData()
        {
            foreach (var i in _tagList)
            {

                bool success = RegisterMap.TryGetValue(Convert.ToUInt16(i.address.Substring(1)), out bool value);
                if (success)
                {
                    DateTime Time = DateTime.Now;
                    _Object.Add(new TagReal(Time.ToString("yyyyMMddHHmmss"), value.ToString(), "", i.tag_id));
                }
                else
                {
                    DateTime Time = DateTime.Now;
                    _Object.Add(new TagReal(Time.ToString("yyyyMMddHHmmss"), "null", "", i.tag_id));
                }

            }
            _Object.ForEach((x) =>
            {
                //  Log.Information($"ip {_ipAddress} tag_id {x.tag_id} value {x.value}");
            });
       //     await _dr.PostListAsync(_Object);
        }

        public override async Task Execute()
        {
            try
            {
                int count = 0;


                foreach (var address in _startAddressList)
                {
                    InputTasks.Add(_mr.ReadCoilsAsync(_ipAddress, (ushort)(address - 1), 100));
                }
                var results = await Task.WhenAll(InputTasks);

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



                if (RegisterMap.Count > 0)
                {
                    await HandleData();
                //    await _dr.RealToHistoryByMedthodAsync("0", _tagList[0].host_id);
                }

            }
            catch (Exception e)
            {
                Log.Debug(e.ToString());

                throw;
            }
        }
    }

}



