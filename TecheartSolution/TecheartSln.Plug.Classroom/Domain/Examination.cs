using Aspose.Cells;
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
            if (String.IsNullOrEmpty(select))
            {
                return;
            }

            //找到第题目并且对题目进行统计
            var firstquestion = ExaminationQuestions.FirstOrDefault(k => k.QuestionNumber.Equals(questionNumber));
            if (firstquestion == null)
            {
                return;
            }
            firstquestion.AddStatistic(voterid, questionNumber, select);

            //找到人，根据题目答案判定对错在对人进行统计
            var firstVoter=Voters.FirstOrDefault(k => k.VoterId.Equals(voterid));
            if (firstVoter == null)
            {
                firstVoter = new Voter(voterid);
                Voters.Add(firstVoter);
            }
            //var selects = select.ToList().Select(k => k.ToString()).ToList();

            firstVoter.AddStatistic(voterid, questionNumber, select, GetAnswerScore(select, firstquestion));
        }

        /// <summary>
        /// 给出分数，如果未达对得0分
        /// </summary>
        /// <param name="select"></param>
        /// <param name="question"></param>
        /// <returns></returns>
        int GetAnswerScore(String select, ExaminationQuestion question)
        {
            int score = question.Score;
            var selects = select.ToList().Select(k => k.ToString()).ToList();
            selects.ForEach(k => 
            {
                if (!question.Answer.Any(m => k == m))
                {
                    score = 0;
                    return;
                }
            });
            return score;
        }




    }
}
