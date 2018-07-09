using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecheartSln.Plug.Classroom.Domain.Utils
{
    public static class ExaminationAnalysisUtils
    {
        public static void MakeStudentScore(Workbook workbook, IList<Voter> voters)
        {
            workbook.Worksheets.Add();
            Worksheet sheet = workbook.Worksheets[workbook.Worksheets.Count() - 1];
            sheet.Name = "成绩单";
            sheet.FreezePanes(1, 1, 1, 0); //冻结第一行
            sheet.Cells["A1"].PutValue("学生id");
            sheet.Cells["B1"].PutValue("总分");
            sheet.Cells["C1"].PutValue("总答题数");
            sheet.Cells["D1"].PutValue("答对题目数");
            sheet.Cells["E1"].PutValue("答错题目数");
            int row = 2;
            foreach (var v in voters.OrderByDescending(k=>k.Score))
            {
                sheet.Cells[row, 0].PutValue(v.VoterId);
                sheet.Cells[row, 1].PutValue(v.Score);
                sheet.Cells[row, 2].PutValue(v.Count);
                sheet.Cells[row, 3].PutValue(v.StatisticsAnswer.Count(k => k.Value > 0));
                sheet.Cells[row, 4].PutValue(v.StatisticsAnswer.Count(k => k.Value <= 0));
                row += 1;
            }
        }
    }
}
