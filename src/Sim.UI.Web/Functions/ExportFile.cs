using OfficeOpenXml;
using System.ComponentModel;

namespace Sim.UI.Web.Functions
{
    public class ExportFile
    {
        public async Task<(MemoryStream StremFile, string ContentType, string Name)> ToExcel<T>(IEnumerable<T> list, string username){
            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using var epackage = new ExcelPackage(stream);
            var worksheet = epackage.Workbook.Worksheets.Add("Lista");
            worksheet.Cells.LoadFromCollection(list, true);
            await epackage.SaveAsync();

            stream.Position = 0;
            string excelname = $"lista-atend-{username}-{DateTime.Now:yyyyMMddHHmmss}.xlsx";

            return (stream, "application/vnd.openxmlformat-officedocument.spreadsheetml.sheet", excelname);
        }
        public void WriteTsv<T>(IEnumerable<T> data, TextWriter output)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            foreach (PropertyDescriptor prop in props)
            {
                output.Write(prop.DisplayName); // header
                output.Write("\t");
            }
            output.WriteLine();
            foreach (T item in data)
            {
                foreach (PropertyDescriptor prop in props)
                {
                    output.Write(prop.Converter.ConvertToString(
                         prop.GetValue(item)));
                    output.Write("\t");
                }
                output.WriteLine();
            }
        }

    }
}
