using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecheartSln.Plug.Editor.Domain
{
    /// <summary>
    /// 问题集合
    /// </summary>
    public class QuestionSet
    {
        /// <summary>
        /// 题目列表
        /// </summary>
        private IList<Question> questions = new List<Question>();

        /// <summary>
        /// 集合是否可排序
        /// </summary>
        public Boolean CanSort { get; private set; }

        /// <summary>
        /// 集合最大题目编号
        /// </summary>
        public int MaxNumber { get; private set; }

        public QuestionSet()
        {
            MaxNumber = 1;
        }

        public void AddQuestion(Question question)
        {
            if (question == default(Question))
            {
                return;
            }
            int number = 0;
            if(!Int32.TryParse(question.QuestionNumber,out number) && !String.IsNullOrEmpty(question.QuestionNumber))
            {
                CanSort = false;
            }

            if (String.IsNullOrEmpty(question.QuestionNumber))
            {

            }
        }
    }
}
