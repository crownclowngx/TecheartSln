using Aspose.Cells;
using Aspose.Cells.Charts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecheartSln.Plug.Classroom.Domain.Utils
{
    public static class ExaminationAnalysisUtils
    {
        /// <summary>
        /// 统计分数
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="voters"></param>
        public static void MakeStudentScore(Workbook workbook, IList<Voter> voters)
        {
            workbook.Worksheets.Add();
            Worksheet sheet = workbook.Worksheets[workbook.Worksheets.Count() - 1];
            sheet.Name = "成绩单";
            sheet.FreezePanes(1, 1, 1, 0); //冻结第一行
            sheet.Cells["A1"].PutValue("学号");
            sheet.Cells["B1"].PutValue("姓名");
            sheet.Cells["C1"].PutValue("学生id");
            sheet.Cells["D1"].PutValue("总答题数");
            sheet.Cells["E1"].PutValue("答对题目数");
            sheet.Cells["F1"].PutValue("答错题目数");
            sheet.Cells["G1"].PutValue("总分");

            int row = 1;
            foreach (var v in voters.OrderByDescending(k=>k.Score))
            {
                sheet.Cells[row, 0].PutValue(v.VoterMappingNumber);
                sheet.Cells[row, 1].PutValue(v.VoterName);
                sheet.Cells[row, 2].PutValue(v.VoterId);
                sheet.Cells[row, 3].PutValue(v.Count);
                sheet.Cells[row, 4].PutValue(v.StatisticsAnswer.Count(k => k.Value > 0));
                sheet.Cells[row, 5].PutValue(v.StatisticsAnswer.Count(k => k.Value <= 0));
                sheet.Cells[row, 6].PutValue(v.Score);
                row += 1;
            }
        }

        /// <summary>
        /// 柱状图
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="voters"></param>
        /// <param name="scores"></param>
        public static void MakeStudentDistribution(Workbook workbook, IList<Voter> voters,IList<int> scores)
        {
            workbook.Worksheets.Add();
            Worksheet sheet = workbook.Worksheets[workbook.Worksheets.Count() - 1];
            sheet.Cells["A1"].PutValue("成绩");
            sheet.Cells["B1"].PutValue("总数量");
            int row = 1;
            foreach (var v in scores)
            {
                sheet.Cells[row, 0].PutValue(v);
                sheet.Cells[row, 1].PutValue(voters.Count(k => k.Score >= v));
                row += 1;
            }
            sheet.Name = "统计图";
            sheet.Charts.Add(ChartType.Bar, 1, 1, 25, 10);
            Chart chart = sheet.Charts[0];

            chart.Title.Text = "Sales By Region For Years";
            chart.Title.Font.Color = Color.Gray;
            chart.Title.Font.IsBold = true;
            chart.Title.Font.Size = 12;

            chart.NSeries.Add(sheet.Name + "!" + "B2" + ":" + "B" + (scores.Count + 1), false);
            chart.NSeries.CategoryData = sheet.Name + "!A2:A"+ (scores.Count + 1);

            Cells cells = sheet.Cells;
        }
    }
}
