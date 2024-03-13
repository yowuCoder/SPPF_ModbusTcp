// See https://aka.ms/new-console-template for more information

using System.Text;

using app1;
using AppModbusTcp.Helper.CsvHelper;
using AppModbusTcp.Helper.ApiHelper;
using AppModbusTcp.Models.fatek;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;
/*1.寫入csv檔5天
 *2.
 * 123
 */
string CIM_CTLPLC_IP = "192.168.2.3";
ushort startAddress_R = 1000;
string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config/config.csv");
NModbusRepository _NModbusRepository1 = new NModbusRepository();
MyCsvHelper _MyCsvHelper = new MyCsvHelper();

ApiHelper apiHelper = new ApiHelper("http://192.168.102.4:8081");
ThreadStart td_start2 = new ThreadStart(run2);
new Thread(td_start2).Start();
bool _Process = false;



void run2()
{
     void consolePrint()
    {
        Console.Clear();
        Console.WriteLine(DateTime.Now.ToString());
        Console.WriteLine($"********************************************************************************************************");
        Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory);
        //string data = await apiHelper.GetAsync("/api/User");
      
        //await Console.Out.WriteLineAsync(data);
    }
    
    async void postToApi(string endPoint,string jsonContent)
    {
        try
        {
            string data = await apiHelper.PostAsync(endPoint, jsonContent);
            await Console.Out.WriteLineAsync(data);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    
    }

     FatekRecord r_command(TagData line)
    {
        ushort[] _DataArray_R = _NModbusRepository1.ModbusTcpMasterReadHoldingRegisters(CIM_CTLPLC_IP, (ushort)(int.Parse(line.Address.ToString().Substring(1))), 1);
        // Console.WriteLine((ushort)(int.Parse(line.Address.ToString().Substring(1))));
        FatekRecord fatek = new()
        {
            Line = line.Line,
            Adress = line.Address.ToString(),
            Description = line.Description,
            Value = _DataArray_R[0].ToString(),
            Time = DateTime.Now,

        };
        Console.WriteLine($"{line.Line}線 {line.Description} value: {_DataArray_R[0]}");
        SaveToCSV($"{line.Line},{line.Description},{_DataArray_R[0]},{DateTime.Now}");
        return  fatek;
    }
    FatekRecord x_command(TagData line)
    {
        bool[] _DataArray_X = _NModbusRepository1.ModbusTcpMasterReadCoils(CIM_CTLPLC_IP, (ushort)line.Address, 1);
        Console.WriteLine($"{line.Line}線 {line.Description} value: {_DataArray_X[0]}");
        SaveToCSV($"{line.Line},{line.Description},{_DataArray_X[0]},{DateTime.Now}");
        FatekRecord fatek = new()
        {
            Line = line.Line,
            Adress = line.Address.ToString(),
            Description = line.Description,
            Value = _DataArray_X[0].ToString(),
            Time = DateTime.Now,

        };
        return fatek;
    }
    _Process = true;
    while (_Process)
    {
        List<FatekRecord> fatekRecords = new List<FatekRecord>();
        consolePrint();
       var file= _MyCsvHelper.ReadCsvFile(filePath);
        foreach (var line in file)
        {
            try
            {
               
                if (line.TagName[0]== 'R')
                {
                    fatekRecords.Add(r_command(line));
                }
                else if (line.TagName[0]== 'X')
                {
              
                    fatekRecords.Add(x_command(line));
                }
         
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        string json = JsonSerializer.Serialize(fatekRecords);
        Console.WriteLine(json);
        postToApi("/api/FatekRecord/list", json);
        Console.WriteLine($"********************************************************************************************************");
        System.Threading.Thread.Sleep(10000);
    }
    
    void SaveToCSV(string _txt)
    {
        string data = "";
        try
        {
            Console.WriteLine("save to ");
            StreamWriter wr = new StreamWriter($"fatek_{DateTime.Now.ToString("yyyyMMddHHmmss")}.CSV", true, CodePagesEncodingProvider.Instance.GetEncoding("Big5"));

            data += _txt;
            data += "\n";
            wr.Write(data);
            data = "";
            wr.Close();
        }
        catch(Exception e) { Console.WriteLine(e); }

    }
}


//ushort[] _DataArray_R = _NModbusRepository1.ModbusTcpMasterReadHoldingRegisters(CIM_CTLPLC_IP, 1000, 10);

//ushort[] _DataArray_R2 = _NModbusRepository1.ModbusTcpMasterReadHoldingRegisters(CIM_CTLPLC_IP, 2000, 10);

//ushort[] _DataArray_R3 = _NModbusRepository1.ModbusTcpMasterReadHoldingRegisters(CIM_CTLPLC_IP, 300, 5);

// bool[] _DataArray_X = _NModbusRepository1.ModbusTcpMasterReadCoils(CIM_CTLPLC_IP, 1001, 35);

//  bool[] _x2 = _NModbusRepository1.ModbusTcpMasterReadInputs(CIM_CTLPLC_IP, 1001, 35);

//  string _StrR = "";
/* for (int i = 0; i < _DataArray_R.Length - 1; i++)
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
 }*/

/* for (int i = 0; i < _DataArray_R2.Length - 1; i++)
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
 }*/
//SaveToCSV(_StrR);
// SaveToCSV(_StrR);
//  SaveToCSV("更新時間:" + DateTime.Now.ToString());
//SaveToCSV($"------------------------------------------------------------");
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