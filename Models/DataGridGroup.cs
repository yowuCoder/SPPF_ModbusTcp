using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kl_modbus.Models
{
    public class DataGridGroup
    {
        public DataGridGroup()
        {

        }
        public DataGridGroup(int group_id, int host_id, string group_name, int interval, string device_id)
        {


            this.group_id = group_id;
            this.host_id = host_id;
            this.group_name = group_name;
            this.interval = interval;
            this.device_id = device_id;
        }

        public int group_id { get; set; }
        public string group_name { get; set; }
        public int interval { get; set; }
        public string device_id { get; set; }

        public int host_id { get; set; }
        public string isdeleted { get; set; }
    }
}
