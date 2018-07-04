using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecheartSln.Plug.Editor.ViewModel;

namespace TecheartSln.Plug.Editor.Domain.Utils
{
    /// <summary>
    /// 类型转换工具类
    /// </summary>
    public static class ConvertUtils
    {
        public static Question Convert(this VMQuesion vMQuesion)
        {
            var answer = vMQuesion.Answoer;
            if (String.IsNullOrEmpty(answer))
            {
                answer = "";
            }
            return new Question()
            {
                Answer = answer.Split(',').ToList(),
                CountSelection = vMQuesion.CountSelection,
                QuestionNumber = vMQuesion.Id,
                Score = vMQuesion.Score,

            };
        }

        public static VMQuesion Convert(this Question question)
        {
            return new VMQuesion()
            {
                Answoer = String.Join(",", question.Answer),
                CountSelection = question.CountSelection,
                Id = question.QuestionNumber,
                Score = question.Score,
            };
        }
    }
}
