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

/*1.寫入csv檔5天
 *2.
 * 123
 */
string CIM_CTLPLC_IP = "192.168.2.3";
ushort BitStartAddress = 00000;
ushort startAddress_D = 6000;
ushort startAddress_R = 1000;

NModbusRepository _NModbusRepository1 = new NModbusRepository();

ThreadStart td_start1 = new ThreadStart(_Command);
new Thread(td_start1).Start();
ThreadStart td_start2 = new ThreadStart(run2);
new Thread(td_start2).Start();
bool _Process = false;
void _Command()
{
    while (2 > 1)
    {
        string _StrConsole = "";
        _StrConsole = Console.ReadLine().Trim().Replace(" ","");
        Console.BackgroundColor = ConsoleColor.Black;
        if (_StrConsole.Length >= 3)
        {
            if ((_StrConsole.Substring(0, 1) == "D")&&(_StrConsole.IndexOf('=')>0))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(_StrConsole);

                int xx= _StrConsole.Trim().IndexOf('=');
                string[] _array = _StrConsole.Split('=');
                string _tagName = _StrConsole.Trim().Substring(xx - 1, 1);
                _Process = false;
                startAddress_D = ushort.Parse("600" + ushort.Parse(_tagName));
                _NModbusRepository1.ModbusTcpMasterWriteHoldingRegisters(CIM_CTLPLC_IP, startAddress_D, UInt16.Parse(_array[1]));
              //  _NModbusRepository1.ModbusTcpMasterWriteHoldingRegisters(CIM_CTLPLC_IP, ushort.Parse("6000" + _array[0].Substring(1, 1)), ushort.Parse(System.DateTime.Now.Second.ToString()));

            }
            if ((_StrConsole.Substring(0, 1) == "Y") && (_StrConsole.IndexOf('=') > 0))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(_StrConsole);

                int xx = _StrConsole.Trim().IndexOf('=');
                string[] _array = _StrConsole.Split('=');
                string _tagName = _StrConsole.Trim().Substring(xx - 1, 1);
                _Process = false;
                startAddress_D = ushort.Parse("000" + ushort.Parse(_tagName));
                _NModbusRepository1.ModbusTcpMasterWriteHoldingRegisters(CIM_CTLPLC_IP, startAddress_D, UInt16.Parse(_array[1]));
                if (_array[1] == "1")
                {
                    _NModbusRepository1.ModbusTcpMasterWriteCoils(CIM_CTLPLC_IP, startAddress_D, true);
                }
                else
                {
                    _NModbusRepository1.ModbusTcpMasterWriteCoils(CIM_CTLPLC_IP, startAddress_D, false);
                }
                //  _NModbusRepository1.ModbusTcpMasterWriteHoldingRegisters(CIM_CTLPLC_IP, ushort.Parse("6000" + _array[0].Substring(1, 1)), ushort.Parse(System.DateTime.Now.Second.ToString()));

            }
            _Process = true;
            Console.ForegroundColor = ConsoleColor.White;

        }
    }
}


void run2()
{
       
    _Process = true;
    while (_Process)
    {
        Console.Clear();
        Console.WriteLine(DateTime.Now.ToString());
        Console.WriteLine($"********************************************************************************************************");
      
        try
        {

            //_NModbusRepository1.ModbusTcpMasterWriteCoils(CIM_CTLPLC_IP, BitStartAddress, true);//Y2
          bool[] _BitArray_Y = _NModbusRepository1.ModbusTcpMasterReadCoils(CIM_CTLPLC_IP, BitStartAddress, 30);
            ushort[] _DataArray_D = _NModbusRepository1.ModbusTcpMasterReadHoldingRegisters(CIM_CTLPLC_IP, startAddress_D, 10);
            ushort[] _DataArray_R = _NModbusRepository1.ModbusTcpMasterReadHoldingRegisters(CIM_CTLPLC_IP, startAddress_R, 10);

            //  _NModbusRepository1.ModbusTcpMasterWriteHoldingRegisters(CIM_CTLPLC_IP, DatastartAddress, 777);

            //  _NModbusRepository1.ModbusTcpMasterWriteHoldingRegisters(CIM_CTLPLC_IP, 9500, 12345);


           


            string _StrY = "";
            for (int i = 0; i < _BitArray_Y.Length - 1; i++)
              {
                  string _StrShow = "Y" + i + $":0000{(BitStartAddress + i + 1)}={(_BitArray_Y[i] ? 1 : 0)} ";

                  if (i < 10)
                  {
                      _StrY = _StrY + _StrShow + ","; ;
                  }
                  else
                  {
                      //    _StrY = "";
                  }
                  //  Console.WriteLine(_StrShow);
              }
            //  SaveToCSV(_StrY);
            //  Console.WriteLine(_StrY);

              Console.WriteLine($"                                                    ");

              // TagList _TagList1=new TagList();
              //_TagList1.


              string _StrD = "";
              for (int i = 0; i < _DataArray_D.Length - 1; i++)
              {

                  //   csv.WriteRecords(('\t' + dataarray[i].ToString()));
                  // csv.NextRecord();
                  string _StrShow2 = "D" + i + $":4{(startAddress_D + i + 1)}={(_DataArray_D[i].ToString())}  ";
                  if (i < 10)
                  {
                      _StrD = _StrD + _StrShow2 + ",";
                  }
                  else
                  {
                      //    _StrY = "";
                  }

              }
             // SaveToCSV(_StrD);
              // csv.WriteRecords(_StrD);
              // csv.NextRecord();
             // Console.WriteLine(DateTime.Now.ToString() + "," + _StrD);
              
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
            SaveToCSV(_StrR);
            int _dr = startAddress_D;

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
    void ExportJson()
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




    }
}


