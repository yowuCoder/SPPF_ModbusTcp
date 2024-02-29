using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1;

namespace kl_modbus_pm.Models
{

    public class TaglistDTO
    {

        public enum TagType { 請輸入type, bit, int16, int32, float32, str };
        public TaglistDTO() { }
        public TaglistDTO(string tag_name, string tag_number, string address, string description, TagType? type, string host_id, int tag_id, int group_id)
        {
            this.tag_name = tag_name;
            this.tag_number = tag_number;
            this.type = type;
            this.address = address;
            this.host_id = host_id;
            this.description = description;
            this.tag_id = tag_id;

            this.group_id = group_id;
        }
        [Name("tag_name")]
        public string tag_name { get; set; }
        [Name("tag_number")]
        public string? tag_number { get; set; }
        [Name("description")]
        public string? description { get; set; }
        [Name("address")]
        public string address { get; set; }

        [Name("type")]
        public TagType? type { get; set; }

        [Name("host_id")]
        public string host_id { get; set; }

        [Name("tag_id")]
        public int tag_id { get; set; }
        [Name("group_id")]
        public int group_id { get; set; }
        [Name("point")]
        public int point { get; set; }

        [Name("unit")]
        public string unit { get; set; }

    }

}
