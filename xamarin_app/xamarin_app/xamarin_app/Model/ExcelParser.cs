using Syncfusion.XlsIO;
using System.Data;
using System.IO;
using System.Reflection;

namespace xamarin_app.Model
{
    class ExcelParser
    {
        ExcelEngine excelEngine;

        public IWorkbook createWorkbook(string resourcePath)
        {
            excelEngine = new ExcelEngine();
            IApplication application = excelEngine.Excel;
            application.DefaultVersion = ExcelVersion.Excel2013;

            //"App" is the class of Portable project.
            Assembly assembly = typeof(App).GetTypeInfo().Assembly;
            Stream fileStream = assembly.GetManifestResourceStream(resourcePath);

            //Opens the workbook 
            IWorkbook workbook = application.Workbooks.Open(fileStream);

            return workbook;
        }

        public DataTable createDataTable(IWorkbook workbook, int worksheetIndex)
        {
            //Access selected worksheet from the workbook.
            //worksheet is zero-based indexed
            IWorksheet worksheet = workbook.Worksheets[worksheetIndex];

            //Read data from the worksheet and Export to the DataTable
            DataTable dataTable = worksheet.ExportDataTable(worksheet.UsedRange, ExcelExportDataTableOptions.ColumnNames);

            workbook.Close(); //vielleicht kann man dann das workbook nicht mehr in der anderen Methode öffnen
            excelEngine.Dispose();

            return dataTable;
        }
    }
}
