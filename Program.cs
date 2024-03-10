// See https://aka.ms/new-console-template for more information
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using CsvHelper;
using System.Text;
using kl_modbus.Command;
using kl_modbus.Helper.ModbusHelper.Factory;
using kl_modbus_pm.Models;
using NModbus;
using NModbus.Extensions.Enron;
using WpfApp1.Models;
using System;
using System.IO.Ports;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using app1;
using System.Data;
using AppModbusTcp.Helper.CsvHelper;
/*1.寫入csv檔5天
 *2.
 * 123
 */
string CIM_CTLPLC_IP = "192.168.2.3";
ushort startAddress_R = 1000;
string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config/config.csv");
NModbusRepository _NModbusRepository1 = new NModbusRepository();
MyCsvHelper _MyCsvHelper = new MyCsvHelper();

ThreadStart td_start2 = new ThreadStart(run2);
new Thread(td_start2).Start();
bool _Process = false;


void run2()
{
       
    _Process = true;
    while (_Process)
    {
        Console.Clear();
        Console.WriteLine(DateTime.Now.ToString());
        Console.WriteLine($"********************************************************************************************************");
        Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory);
       var file= _MyCsvHelper.ReadCsvFile(filePath);
    
        try
        {

        
            ushort[] _DataArray_R = _NModbusRepository1.ModbusTcpMasterReadHoldingRegisters(CIM_CTLPLC_IP, 1000, 10);

           ushort[] _DataArray_R2 = _NModbusRepository1.ModbusTcpMasterReadHoldingRegisters(CIM_CTLPLC_IP, 2000, 10);

            ushort[] _DataArray_R3 = _NModbusRepository1.ModbusTcpMasterReadHoldingRegisters(CIM_CTLPLC_IP, 300, 5);

            bool[] _DataArray_X = _NModbusRepository1.ModbusTcpMasterReadCoils(CIM_CTLPLC_IP,1001, 35);

          //  bool[] _x2 = _NModbusRepository1.ModbusTcpMasterReadInputs(CIM_CTLPLC_IP, 1001, 35);
            foreach (var i in _DataArray_X)
            {

            Console.WriteLine($"X: {i}");
            }
            string _StrR = "";
              for (int i = 0; i < _DataArray_R.Length - 1; i++)
              {


                  string _StrShow2 = "R" + i + $":4000{(startAddress_R + i + 1)}={(_DataArray_R[i].ToString())}  ";
                  if (i < 10)
                  {
                      _StrR = _StrR + _StrShow2;
                  }
                  else
                  {
                      //    _StrY = "";
                  }
              }
           // SaveToCSV(_StrR);
         for (int i = 0; i < _DataArray_R2.Length - 1; i++)
            {


                string _StrShow2 = "R" + i + $":4000{(startAddress_R + i + 1)}={(_DataArray_R[i].ToString())}  ";
                if (i < 10)
                {
                    _StrR = _StrR + _StrShow2;
                }
                else
                {
                    //    _StrY = "";
                }
            }
            for (int i = 0; i < _DataArray_R3.Length - 1; i++)
            {


                string _StrShow2 = "R" + i + $":4000{(startAddress_R + i + 1)}={(_DataArray_R[i].ToString())}  ";
                if (i < 10)
                {
                    _StrR = _StrR + _StrShow2;
                }
                else
                {
                    //    _StrY = "";
                }
            }
            SaveToCSV(_StrR);
            // SaveToCSV(_StrR);
            SaveToCSV("更新時間:" + DateTime.Now.ToString());
            SaveToCSV($"------------------------------------------------------------");
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        Console.WriteLine($"********************************************************************************************************");


        System.Threading.Thread.Sleep(5000);
    }
    
    void SaveToCSV(string _txt)
    {
        string data = "";
        try
        {
            Console.WriteLine("save to ");
            StreamWriter wr = new StreamWriter("data2.CSV", true, System.Text.Encoding.Default);

            data += _txt;
            data += "\n";
            wr.Write(data);
            data = "";
            wr.Close();
        }
        catch { }

    }
   /* void ExportJson()
    {
        using var Writer = new StreamWriter("123.json", false, CodePagesEncodingProvider.Instance.GetEncoding("Big5"));

        var j = 0;
        var k = 0;
        Writer.WriteLine("{");
        Writer.WriteLine(" \"members\": [");
        for (var i = 0; i < 10; i++)
        {
            Writer.WriteLine("{");

            Writer.WriteLine($" \"value\":\"-\",");
            Writer.WriteLine($"\"datetime\":\"{System.DateTime.Now.ToString()}\",");
            Writer.WriteLine($"\"wkno\":\"Wade\",");

            string host_id = "1";
            string tag_name = "TempME";
            Writer.WriteLine($"\"host_id\":\"{i}\",");
            string address = "40001";
            Writer.WriteLine($"\"tag_name\":\"{tag_name}\",");
            Writer.WriteLine($"\"address\":\"{address}\",");
            string tag_number = "123";
            string description = "Tapei";
            Writer.WriteLine($"\"tag_number\":\"{tag_number}\",");
            Writer.WriteLine($"\"description\":\"{description}\",");
            string unit = "度C";
            Writer.WriteLine($"\"unit\":\"{unit}\",");


            string group_name = "BLD";
            Writer.WriteLine($"\"group_name\":\"{group_name}\",");


            Writer.WriteLine($"\"tag_id\":\"{123 + i}\"");

            if (i == 9)
            {
                Writer.WriteLine("}");
            }
            else
            {
                Writer.WriteLine("},");
            }


        }
        Writer.WriteLine("]");
        Writer.WriteLine("}");




    }*/
}


