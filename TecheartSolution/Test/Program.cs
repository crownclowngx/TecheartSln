using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            {

                Console.WriteLine(Guid.NewGuid().ToString());
            }

            Workbook BuildReport_WorkBook = new Workbook();
            Worksheet BuildReport_WorkSheet = BuildReport_WorkBook.Worksheets[0];
            BuildReport_WorkSheet.FreezePanes(1, 1, 1, 0); //冻结第一行
            Cells BuildReportCells = BuildReport_WorkSheet.Cells;
            BuildReportCells[0, 0].PutValue("姓名");
            BuildReportCells[0, 1].PutValue("Id");
            BuildReportCells[0, 2].PutValue("出生年月");
            BuildReportCells[0, 3].PutValue("年龄");
            BuildReport_WorkSheet.Name = "users";
            BuildReport_WorkSheet.AutoFitColumns();
            BuildReport_WorkBook.Save("test.xlsx", SaveFormat.Xlsx);

            Console.Read();
        }
    }
}
