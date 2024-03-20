using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppModbusTcp.Helper.TxtHelper
{
    internal class TxtHelper
    {
        public TxtHelper()
        {
        }

        public string[] Read(string filePath)
        {
            try
            {
                string line = System.IO.File.ReadAllText(filePath);
                string ip = line.Split('\n')[0];
                string port = line.Split('\n')[1];
                string[] result = {ip, port};
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }   
        
    }
}
