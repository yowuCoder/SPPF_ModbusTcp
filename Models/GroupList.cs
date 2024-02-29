//using EasyModbus.Exceptions;
using kl_modbus.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using WpfApp1;

using CsvHelper.Configuration.Attributes;

namespace WpfApp1.Models
{
    public class GroupList
    {

        public GroupList()
        {


        }

        public GroupList(int group_id, string host_id, string group_name, int interval)
        {



            this.group_id = group_id;
            this.host_id = host_id;
            this.group_name = group_name;
            this.interval = interval;



        }





        //public HostList host;
        [Name("group_id")]
        public int group_id { get; set; }
        [Name("group_name")]
        public string group_name { get; set; }
        [Name("interval")]
        public int interval { get; set; }
        //   public string host_name { get; set; }
        [Name("host_id")]

        public string host_id { get; set; }



    }
}
