using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecheartSln.Plug.Classroom.Domain.Boundary
{
    public static class Convert
    {
        public static ExaminationQuestion convert(this Question question)
        {
            return new ExaminationQuestion()
            {
                Answer = question.Answer,
                QuestionNumber = question.QuestionNumber,
                Score = question.Score,
                Statistics = new Dictionary<string, int>(),
            };
        }
    }
}
