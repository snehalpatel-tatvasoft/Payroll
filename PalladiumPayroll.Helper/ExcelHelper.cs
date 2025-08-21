using ClosedXML.Excel;
using System.Data;

namespace PalladiumPayroll.Helper
{
    public static class ExcelHelper
    {
        public static byte[] ExportToExcel(DataTable table, string sheetName = "Sheet1")
        {
            using (var workbook = new XLWorkbook())
            {
                workbook.Worksheets.Add(table, sheetName);
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }
    }
}
