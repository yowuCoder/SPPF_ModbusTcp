using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppModbusTcp.Models.fatek
{
    internal class FatekRecord
    {
        public string Adress { get; set; } = null!;

        public string Line { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Value { get; set; } = null!;

        public DateTime Time { get; set; }
    }
}
