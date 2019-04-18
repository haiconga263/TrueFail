using Common.Models;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Common.Helpers
{
    public class ExcelHelper
    {
        public static ExcelData Create(IEnumerable<ExcelInputData> datas, ExcelExtension extension)
        {
            if(datas == null)
            {
                return null;
            }

            using (var fs = new MemoryStream())
            {

                IWorkbook workbook = new XSSFWorkbook();

                int sheetIndex = 1;
                foreach (var data in datas)
                {
                    if(data == null || data.Header == null || data.Contents == null)
                    {
                        continue;
                    }
                    ISheet sheet = workbook.CreateSheet(data.SheetName ?? $"Sheet{sheetIndex}");
                    var rowIndex = 0;

                    //Create header
                    var header = sheet.CreateRow(rowIndex);
                    for (int i = 0; i < data.Header.Length; i++)
                    {
                        header.CreateCell(i).SetCellValue(data.Header[i]);
                        sheet.AutoSizeColumn(i);
                    }
                    rowIndex++;

                    //Create Content
                    foreach (var content in data.Contents)
                    {
                        if(content != null)
                        {
                            var row = sheet.CreateRow(rowIndex);
                            for (int i = 0; i < content.Length; i++)
                            {
                                row.CreateCell(i).SetCellValue(content[i]);
                            }
                            rowIndex++;
                        }
                    }

                    sheetIndex++;
                }
                workbook.Write(fs);

                return new ExcelData()
                {
                    Data = fs.GetBuffer(),
                    ContentType = GetContentType(extension),
                    Extension = extension
                };
            }
        }

        private static string GetContentType(ExcelExtension extension)
        {
            switch(extension)
            {
                case ExcelExtension.xlsx:
                    return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                default:
                    return string.Empty;
            }
        }
    }
}
