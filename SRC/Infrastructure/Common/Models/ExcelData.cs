using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models
{
    public class ExcelData
    {
        public byte[] Data { set; get; }
        public string ContentType { set; get; }
        public ExcelExtension  Extension { set; get; }
    }

    public class ExcelInputData
    {
        public string SheetName { set; get; }
        public string[] Header { set; get; }
        public List<string[]> Contents { set; get; }
    }

    public enum ExcelExtension
    {
        xlsx
    }
}
