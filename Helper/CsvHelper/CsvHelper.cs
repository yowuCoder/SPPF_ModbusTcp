using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using CsvHelper;

namespace AppModbusTcp.Helper.CsvHelper
{
    public class TagData
    {
        public string Description { get; set; }
        public int Address { get; set; }
        public string TagName { get; set; }
        public string Line { get; set; }
    }
    internal class MyCsvHelper
    {
        public  List<TagData> ReadCsvFile(string filePath)
        {
            List<TagData> tagDataList = new List<TagData>();

            using (var reader = new StreamReader(filePath, CodePagesEncodingProvider.Instance.GetEncoding("Big5")))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {


                while (csv.Read())
                {
                    // Map CSV fields to TagData object
                    TagData tagData = csv.GetRecord<TagData>();
                    tagDataList.Add(tagData);
                }
            }

            return tagDataList;
        }
    }
}
