using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecheartSln.Plug.Classroom.Domain
{
    public class Examination
    {
        public Examination()
        {
            Voters = new List<Voter>();
            ExaminationQuestions = new List<ExaminationQuestion>();
        }
        /// <summary>
        /// 题目
        /// </summary>
        public IList<ExaminationQuestion> ExaminationQuestions { get; set; }

        /// <summary>
        /// 投票者列表
        /// </summary>
        public IList<Voter> Voters { get; set; }

        /// <summary>
        /// 问题个数
        /// </summary>
        public int QuestionCount { get { if (ExaminationQuestions == null) { return 0; }return ExaminationQuestions.Count(); } }

        /// <summary>
        /// 添加一个答案
        /// </summary>
        /// <param name="voterid">投票者id</param>
        /// <param name="questionNumber">投票者选择的题号</param>
        /// <param name="select">选择的选项</param>
        public void Add(String voterid,int questionNumber,String select)
        {
            var firstquestion = ExaminationQuestions.FirstOrDefault(k => k.QuestionNumber.Equals(questionNumber));
            if (firstquestion == null)
            {
                return;
            }
            firstquestion.AddStatistic(voterid, questionNumber, select);
            var firstVoter=Voters.FirstOrDefault(k => k.VoterId.Equals(voterid));
            if (firstVoter == null)
            {
                firstVoter = new Voter(voterid);
                Voters.Add(firstVoter);
            }
            firstVoter.AddStatistic(voterid, questionNumber, select);
        }
    }
}
