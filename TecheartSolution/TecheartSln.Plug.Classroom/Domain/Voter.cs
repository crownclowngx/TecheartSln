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
        }
        /// <summary>
        /// 学生编号（投票器编号）
        /// </summary>
        public String VoterId { get; set; }

        /// <summary>
        /// 选择列表 (题目，选项)
        /// </summary>
        public IDictionary<int,String> Statistics { get; set; }

        public void AddStatistic(String voterid, int questionNumber, String select)
        {
            if (!Statistics.ContainsKey(questionNumber))
            {
                Statistics.Add(questionNumber, select);
            }
            else
            {
                Statistics[questionNumber] = select;
            }
        }
    }
}
