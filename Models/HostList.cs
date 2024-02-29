//using EasyModbus.Exceptions;
using kl_modbus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace WpfApp1.Models
{
    public class HostList
    {



        public HostList()
        {

        }


        public string host_id { get; set; }


        public string host_name { get; set; }
        public string host_ip { get; set; }

        public int port { get; set; }

        public bool isEnabled { get; set; }
        public bool isconnected { get; set; }

    }
}
