using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using kl_modbus.Command;
using kl_modbus_pm.Models;
using Serilog;
using WpfApp1.Models;

namespace kl_modbus.Helper.ModbusHelper.Factory
{
    public class SimpleFactory
    {


        public ModbusCommand Create(char str, List<TaglistDTO> taglist, string ip, DatabaseRepository dr)
        {
            try
            {
                switch (str)
                {
                    case '0':
                        return new CoilsCommand(ip, taglist, dr);
                    case '1':
                        return new InputCommand(ip, taglist, dr);
                    case '3':
                        return new InputRegisterCommand(ip, taglist, dr);
                    case '4':
                        return new HoldingRegisterCommand(ip, taglist, dr);
                    default:
                        return null;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                throw ex;
            }


        }
    }
}
