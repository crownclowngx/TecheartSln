using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecheartSln.Plug.Editor.Domain
{
    public class Question
    {
        /// <summary>
        /// 题目编号
        /// </summary>
        public String QuestionNumber { get; set; }

        /// <summary>
        /// 题干
        /// </summary>
        public String QuestionStem { get; set; }

        /// <summary>
        /// 选项描述
        /// </summary>
        public IList<String> SelectionDescription { get; set; }

        /// <summary>
        /// 选项总数
        /// </summary>
        public int CountSelection { get; set; }

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
