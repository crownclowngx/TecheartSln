using Aspose.Cells;
using Aspose.Cells.Charts;
using System;
using System.Collections.Generic;
using System.Drawing;
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
                Console.Read();
            }
            //Workbook workbook = new Workbook();
            //workbook.Worksheets.Clear();
            //workbook.Worksheets.Add();
            //Worksheet sheet = workbook.Worksheets[workbook.Worksheets.Count() - 1];
            //sheet.Name = "统计图";
            //sheet.Cells["A1"].PutValue("成绩");
            //sheet.Cells["B1"].PutValue("总数量");
            //int row = 1;
            //foreach (var v in new List<String>() { "90分", "70分", "60分", "0分" })
            //{
            //    sheet.Cells[row, 0].PutValue(v);
            //    sheet.Cells[row, 1].PutValue(random.Next(10, 50));
            //    row += 1;
            //}
            //Cells cells = workbook.Worksheets[0].Cells;


            //workbook.Worksheets[0].Charts.Add(ChartType.Column3DStacked, 1, 2, 25, 10);
            //Chart chart = workbook.Worksheets[0].Charts[0];
            //chart.Title.Text = "Sales By Region For Years";
            //chart.Title.Font.Color = Color.Gray;
            //chart.Title.Font.IsBold = true;
            //chart.Title.Font.Size = 12;
            //chart.NSeries.Add("统计图!B2:B5", true);
            //chart.NSeries.CategoryData = "统计图!A2:A5";
            //workbook.Save("test.xlsx", SaveFormat.Xlsx);

            //FileStream fstream = new FileStream("test1.xlsx", FileMode.Open);
            //Workbook workbook = new Workbook(fstream);
            //Chart chart = workbook.Worksheets[0].Charts[0];
            //Console.Read();
        }
    }
}
