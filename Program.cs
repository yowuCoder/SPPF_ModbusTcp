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
using Renci.SshNet;
using Renci.SshNet.Sftp;
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
        foreach (var line in file)
        {
            try
            {
               
                if (line.TagName[0]== 'R')
                {
                   //   ushort[] _DataArray_R = _NModbusRepository1.ModbusTcpMasterReadHoldingRegisters(CIM_CTLPLC_IP, (ushort)(int.Parse(line.Address.ToString().Substring(1))), 1);
                    Console.WriteLine((ushort)(int.Parse(line.Address.ToString().Substring(1))));
                    //Console.WriteLine($"{line.Line}線 {line.Description} value: {_DataArray_R[0]}");
                    SaveToCSV($"{line.Line},{line.Description},{123},{DateTime.Now}");   
                }
                else if (line.TagName[0]== 'X')
                {
                   //bool[] _DataArray_X = _NModbusRepository1.ModbusTcpMasterReadCoils(CIM_CTLPLC_IP, (ushort)line.Address, 1);
                   //Console.WriteLine($"{line.Line}線 {line.Description} value: {_DataArray_X[0]}");
                    SaveToCSV($"{line.Line},{line.Description},{123},{DateTime.Now}");
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
     
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
        catch { }

    }
    void ftp()
    {
        using (var sftp = new SftpClient("10.211.55.19", "tester", "password"))
        {
            //SFTP Server連線
            sftp.Connect();

            //顯示SFTP Server 資料夾目錄
         /*   Action<SftpFile> ShowDirOrFile = (item) => {
                if (item.IsDirectory)
                    Console.WriteLine($"[{item.Name}]");
                else
                    Console.WriteLine($"{item.Name:-32} {item.Length,8:N0} bytes");
            };

            Action<string> DirList = (path) =>
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"顯示 {path} 目錄: ");
                var list = sftp.ListDirectory(path)
                    //忽略 . 及 .. 目錄
                    .Where(o => !".,..".Split(',').Contains(o.Name))
                    .ToList();
                if (list.Any())
                    list.ForEach(ShowDirOrFile);
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("沒有檔案.");
                }
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();

            };*/

            //目前工作的資料夾
           // var working_directory = sftp.WorkingDirectory;
           // DirList(working_directory);

            //建立資料夾
           /* var folderA = $"/Test_" + DateTime.Now.ToString("yyyyMMdd") + "_A";
            var folderB = $"/Test_" + DateTime.Now.ToString("yyyyMMdd") + "_B";
            if (!sftp.Exists(folderA)) //驗證資料夾是否存在
            {
                Console.WriteLine("建立資料夾" + folderA);
                sftp.CreateDirectory(folderA);
            }
            if (!sftp.Exists(folderB))
            {

                Console.WriteLine("建立資料夾" + folderB);
                sftp.CreateDirectory(folderB);
            }

            DirList("/");*/

            //下載檔案
         /*   Console.WriteLine("下載檔案DownTest.txt");
            var WorkPath = $"C:\\Users\\yachyn\\Desktop\\SFTP";
            var destFilePath = Path.Combine(WorkPath, "DownTest_" + DateTime.Now.ToString("yyyymmddhhmmss") + ".txt");
            using (var file = new FileStream(destFilePath, FileMode.Create, FileAccess.Write))
            {
                sftp.DownloadFile($"/DownTest.txt", file);
            }
            Console.WriteLine("");*/

            //上傳檔案
          //  Console.WriteLine($"上傳檔案Uploadtest.txt到{folderA}");
          //  sftp.ChangeDirectory(folderA); //更換工作資料夾
            var sourcefile = "C:\\Users\\yachyn\\Desktop\\SFTP\\Uploadtest.txt";
            using (FileStream fs = new FileStream(sourcefile, FileMode.Open, FileAccess.Read))
            {
                sftp.BufferSize = 4 * 1024;
                sftp.UploadFile(fs, Path.GetFileName(sourcefile));
            }
           // DirList(folderA);

            //搬移檔案
           /* Console.WriteLine($"從{folderA}搬移檔案到{folderB}");
            if (!sftp.Exists($"/{folderB}/Uploadtest.txt"))
            {
                sftp.RenameFile($"/{folderA}/Uploadtest.txt", $"/{folderB}/Uploadtest.txt");
            }

            DirList(folderA);
            DirList(folderB);*/

            //刪除檔案
           /* Console.WriteLine("刪除檔案DeleteTest.txt");
            sftp.DeleteFile($"/DeleteTest.txt");

            //刪除資料夾
            Console.WriteLine($"刪除資料夾TestFolder");
            sftp.DeleteDirectory($"/TestFolder");

            DirList("/");*/
        }

        Console.Read();
    
}   /* void ExportJson()
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


