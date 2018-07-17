using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecheartSln.Core.ViewModel.Base;
using TecheartSln.Plug.Classroom.Domain;

namespace TecheartSln.Plug.Classroom.ViewModel
{
    public class StudentVM : ViewModelBase
    {
        /// <summary>
        /// 学生编号
        /// </summary>
        private String _studentNumber = "";
        public String StudentNumber
        {
            get { return _studentNumber; }
            set
            {
                _studentNumber = value;
                RaisePropertyChanged("StudentNumber");

            }
        }

        /// <summary>
        /// 总得分
        /// </summary>
        private string _score = "";
        public String Score
        {
            get { return _score; }
            set
            {
                _score = value;
                RaisePropertyChanged("Score");

            }
        }

        /// <summary>
        /// 总答题数
        /// </summary>
        public string _count = "0";
        public String Count
        {
            get { return _count; }
            set
            {
                _count = value;
                RaisePropertyChanged("Count");

            }
        }

        /// <summary>
        /// 总答题数
        /// </summary>
        public string _mappingNumber = "0";
        public String MappingNumber
        {
            get { return _mappingNumber; }
            set
            {
                _mappingNumber = value;
                RaisePropertyChanged("MappingNumber");

            }
        }

        /// <summary>
        /// 总答题数
        /// </summary>
        public string _name = "";
        public String Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged("Name");

            }
        }

        /// <summary>
        /// 是否登陆
        /// </summary>
        public bool _isLogin = false;
        public bool IsLogin
        {
            get { return _isLogin; }
            set
            {
                _isLogin = value;
                RaisePropertyChanged("IsLogin");

            }
        }

        public void Set(Voter voter)
        {
            StudentNumber = voter.VoterId;
            Score = voter.Score.ToString();
            Count = voter.Count.ToString();
            MappingNumber = voter.VoterMappingNumber;
            Name = voter.VoterName;
        }
    }
    public class QuestionVM : ViewModelBase
    {
        /// <summary>
        /// 题目编号
        /// </summary>
        private String _questionNumber = "";
        public String QuestionNumber
        {
            get { return _questionNumber; }
            set
            {
                _questionNumber = value;
                RaisePropertyChanged("QuestionNumber");

            }
        }
        /// <summary>
        /// 答案和分数
        /// </summary>
        private String _answerAndScore = "";
        public String AnswerAndScore
        {
            get { return _answerAndScore; }
            set
            {
                _answerAndScore = value;
                RaisePropertyChanged("AnswerAndScore");

            }
        }


        /// <summary>
        /// 答题分布统计
        /// </summary>
        private String _statistics = "";
        public String Statistics
        {
            get { return _statistics; }
            set
            {
                _statistics = value;
                RaisePropertyChanged("Statistics");

            }
        }

        public void Set(ExaminationQuestion examinationQuestion)
        {

            QuestionNumber = examinationQuestion.QuestionNumber.ToString();
            AnswerAndScore = "答案是：" + String.Join("|", examinationQuestion.Answer) + " ，分数是：" + examinationQuestion.Score;//"答案是 A ，分数是 4";
            StringBuilder sb = new StringBuilder();
            foreach (var v in examinationQuestion.Statistics)
            {

                sb.Append(v.Key);
                sb.Append("/");
                sb.Append(v.Value);
                sb.Append(",  ");
            }
            Statistics = sb.ToString();
        }
    }

    public class RegionVM : ViewModelBase
    {
        private int _score;
        /// <summary>
        /// 分数
        /// </summary>
        public int Score
        {
            get { return _score; }
            set
            {
                _score = value;
                RaisePropertyChanged("Score");

            }
        }

        private String _scoreExplain = "";
        /// <summary>
        /// 对于分数的说明
        /// </summary>
        public String ScoreExplain
        {
            get { return _scoreExplain; }
            set
            {
                _scoreExplain = value;
                RaisePropertyChanged("ScoreExplain");

            }
        }

    }
}
