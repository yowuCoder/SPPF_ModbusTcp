using app1.Models;
using kl_modbus.Models;
using kl_modbus_pm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WpfApp1;
using WpfApp1.Models;

namespace kl_modbus.Command
{
    public abstract class ModbusCommand
    {
        protected readonly DatabaseRepository _dr;
        protected readonly IModbusRepository _mr = new NModbusRepository();
        protected readonly string _ipAddress;
        protected readonly ushort _startAddress;
        protected List<TaglistDTO> _tagList;
        protected readonly ushort _numInputs;
        protected List<TagReal> _Object = new List<TagReal>();
        protected List<int> _startAddressList = new List<int>();
        protected ushort _maxAddress;
        protected ushort _minAddress;
        protected List<ushort> _addressList = new List<ushort>();


        public ModbusCommand(string ip, List<TaglistDTO> tagList, DatabaseRepository dr)
        {
            _dr = dr;
            _ipAddress = ip;
            _tagList = tagList;
            SetMaxAddress();
            SetMinAddress();
            SetAddressList();
            SetStartAddressList();

        }
        public abstract Task Execute();
        protected bool IsExistAddressInRange(int address)
        {
            return _addressList.Contains((ushort)address);
        }

        private void SetAddressList()
        {
            _tagList.ForEach((tag) =>
            {
                _addressList.Add(Convert.ToUInt16(tag.address.Substring(1)));
            });

        }

        private void SetStartAddressList()
        {
            _addressList.Sort();
            _addressList.ForEach((x) =>
            {
                if (_startAddressList.Count == 0)
                {
                    _startAddressList.Add(x);
                }
                else if (x - _startAddressList.Last() >= 100)
                {
                    _startAddressList.Add(x);

                }
            });

        }

        private void SetMaxAddress()
        {
            _maxAddress = Convert.ToUInt16(_tagList.Max((tag) =>
            {
                return tag.address;
            }).Substring(1));
        }

        private void SetMinAddress()
        {
            _minAddress = Convert.ToUInt16(_tagList.Min((tag) =>
            {
                return tag.address;
            }).Substring(1));
        }
    }
}
