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

        public static DataTable ImportFromExcel(IFormFile file)
        {
            var table = new DataTable();

            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                using (var workbook = new XLWorkbook(stream))
                {
                    var worksheet = workbook.Worksheets.First();
                    var rows = worksheet.RangeUsed()?.RowsUsed().ToList();

                    if(rows == null || !rows.Any())
                    {
                        return table;
                    }
                    else
                    {
                        // header - first row
                        foreach (var cell in rows.FirstOrDefault()?.Cells()!)
                        {
                            table.Columns.Add(cell.GetValue<string>());
                        }

                        // data - skip header row
                        foreach (var row in rows.Skip(1))
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
