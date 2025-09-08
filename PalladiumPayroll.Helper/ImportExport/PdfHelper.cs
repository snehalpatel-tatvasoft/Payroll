using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data;

namespace PalladiumPayroll.Helper.ImportExport
{
    public static class PdfHelper
    {
        public static byte[] ExportToPdfTable(DataTable Dt, float margin = 0)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Document document = new Document(PageSize.A4, margin, margin, margin, margin);
                PdfWriter writer = PdfWriter.GetInstance(document, ms);
                document.Open();

                PdfPTable table = new PdfPTable(Dt.Columns.Count);
                table.WidthPercentage = 100;

                // Add headers
                foreach (DataColumn column in Dt.Columns)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(column.ColumnName));
                    cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                    table.AddCell(cell);
                }

                // Add data rows
                foreach (DataRow row in Dt.Rows)
                {
                    foreach (var cell in row.ItemArray)
                    {
                        table.AddCell(new Phrase(cell?.ToString()));
                    }
                }

                document.Add(table);
                document.Close();
                writer.Close();
                return ms.ToArray();
            }
        }

        public static bool Merge(List<string> InFiles, string OutFile)
        {
            bool merged = true;
            List<PdfReader> readerList = new List<PdfReader>();
            foreach (string filePath in InFiles)
            {
                PdfReader pdfReader = new PdfReader(filePath);
                readerList.Add(pdfReader);
            }

            //Define a new output document and its size, type
            Document document = new Document(PageSize.A4, 0, 0, 0, 0);
            //Create blank output pdf file and get the stream to write on it.
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(OutFile, FileMode.Create));
            document.Open();

            foreach (PdfReader reader in readerList)
            {
                PdfReader.unethicalreading = true;
                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    PdfImportedPage page = writer.GetImportedPage(reader, i);
                    document.Add(Image.GetInstance(page));
                }
            }
            document.Close();
            foreach (PdfReader reader in readerList)
            {
                reader.Close();
            }

            foreach (string filePath in InFiles)
            {
                File.Delete(filePath);
            }
            return merged;
        }
    }
}
