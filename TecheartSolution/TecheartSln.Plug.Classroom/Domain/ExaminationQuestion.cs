using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecheartSln.Plug.Classroom.Domain
{
    public class ExaminationQuestion
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

        /// <summary>
        /// 每个选项 有多少人投送的个数
        /// </summary>
        public IDictionary<String, int> Statistics = new Dictionary<String, int>();

        /// <summary>
        /// 已经选择的成员列表
        /// </summary>
        private IDictionary<String, IList<String>> Voterids = new Dictionary<String, IList<String>>();

        /// <summary>
        /// 投票总人数
        /// </summary>
        public int Count { get { return Voterids.Count(); } }
        /// <summary>
        /// 添加一个统计
        /// </summary>
        /// <param name="voterid"></param>
        /// <param name="questionNumber"></param>
        /// <param name="select"></param>
        public void AddStatistic(String voterid, int questionNumber, String select)
        {
            if (String.IsNullOrEmpty(select))
            {
                return;
            }
            var selects = select.ToList().Select(k => k.ToString()).ToList();
            if (!Voterids.ContainsKey(voterid))
            {
                Voterids.Add(voterid, selects);
                foreach(var v in selects)
                {
                    StatisticsAddOne(v);
                }
            }
            else
            {
                if (Voterids[voterid] == null)
                {
                    Voterids[voterid] = new List<String>();
                }

                foreach(var v in Voterids[voterid])
                {
                    StatisticsSubOne(v);
                }

                foreach (var v in selects)
                {
                    StatisticsAddOne(v);
                }

                Voterids[voterid] = selects;
            }
        }

        private void StatisticsAddOne(String select)
        {
            if (!Statistics.ContainsKey(select))
            {
                Statistics.Add(select, 0);
            }
            Statistics[select] += 1;
        }
        private void StatisticsSubOne(String select)
        {
            if (!Statistics.ContainsKey(select))
            {
                Statistics.Add(select, 0);
            }
            else
            {
                Statistics[select] -= 1;
            }
        }
    }
}
