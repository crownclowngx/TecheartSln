using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecheartSln.Plug.Classroom.Domain
{
    public class Question
    {
        /// <summary>
        /// 题目编号
        /// </summary>
        public int QuestionNumber { get; set; }

        /// <summary>
        /// 题目答案
        /// </summary>
        public IList<String> Answer { get; set; }

        /// <summary>
        /// 题目分数
        /// </summary>
        public int Score { get; set; }
    }
}
