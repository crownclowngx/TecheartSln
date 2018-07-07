using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecheartSln.Plug.Classroom.Domain
{
    public class Voter
    {
        public Voter(String voterId)
        {
            this.VoterId = voterId;
            Statistics = new Dictionary<int, String>();
            StatisticsAnswer = new Dictionary<int, int>();
        }
        /// <summary>
        /// 学生编号（投票器编号）
        /// </summary>
        public String VoterId { get; set; }

        /// <summary>
        /// 选择列表 (题目，选项)
        /// </summary>
        public IDictionary<int,String> Statistics { get; set; }

        /// <summary>
        /// 考生总分
        /// </summary>
        public int Score { get { int score = 0; foreach (var v in StatisticsAnswer.Values) { score += v; } return score; } }

        /// <summary>
        /// 学生答题总数
        /// </summary>
        public int Count { get { return StatisticsAnswer.Count(); } }
        /// <summary>
        /// 答题对错的统计(题目，分数)，如果未答对分数是0 否则分数有值
        /// </summary>
        public IDictionary<int,int> StatisticsAnswer { get; set; }

        /// <summary>
        /// 添加 统计，默认socre是0即未达对
        /// </summary>
        /// <param name="voterid"></param>
        /// <param name="questionNumber"></param>
        /// <param name="select"></param>
        /// <param name="score"></param>
        public void AddStatistic(String voterid, int questionNumber, String select,int score=0)
        {
            if (!Statistics.ContainsKey(questionNumber))
            {
                Statistics.Add(questionNumber, select);
            }
            else
            {
                Statistics[questionNumber] = select;
            }

            if (!StatisticsAnswer.ContainsKey(questionNumber))
            {
                StatisticsAnswer.Add(questionNumber, score);
            }
            else
            {
                StatisticsAnswer[questionNumber] = score;
            }
        }
    }
}
