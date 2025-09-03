using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using System.Data;

namespace PalladiumPayroll.Helper
{
    public static class ExcelHelper
    {
        public static byte[] ExportToExcel(Dictionary<string, DataTable> tables)
        {
            using (var workbook = new XLWorkbook())
            {
                foreach (var kv in tables)
                {
                    workbook.Worksheets.Add(kv.Value, kv.Key);
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }

        /// <summary>
        /// Import data from Excel file
        /// </summary>
        /// <param name="file"></param>
        /// <param name="isFileHeader">To skip header row data</param>
        /// <param name="headerColumn">Add table column schema</param>
        /// <returns></returns>
        public static DataTable ImportFromExcel(IFormFile file, bool isFileHeader = true, DataColumn[]? headerColumn = null)
        {
            var table = new DataTable();

            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                using (var workbook = new XLWorkbook(stream))
                {
                    var worksheet = workbook.Worksheets.First();
                    var rows = worksheet.RangeUsed()?.RowsUsed().ToList();

                    if (rows == null || !rows.Any())
                    {
                        return table;
                    }
                    else
                    {
                        // header Column
                        if(headerColumn != null && headerColumn.Any())
                        {
                            table.Columns.AddRange(headerColumn);
                        }
                        else
                        {
                            if (isFileHeader)
                            {
                                foreach (var cell in rows.FirstOrDefault()?.Cells()!)
                                {
                                    table.Columns.Add(cell.GetValue<string>());
                                }
                            }
                        }

                        var rowData = isFileHeader ? rows.Skip(1) : rows;
                        foreach (var row in rowData)
                        {
                            var values = row.Cells().Select(c => c.GetValue<string>()).ToArray();
                            table.Rows.Add(values);
                        }
                    }
                }
            }
            return table;
        }
    }
}
