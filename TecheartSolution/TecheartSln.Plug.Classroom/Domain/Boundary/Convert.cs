using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecheartSln.Plug.Classroom.Scene;
using TecheartSln.Plug.Classroom.ViewModel;

namespace TecheartSln.Plug.Classroom.Domain.Boundary
{
    public static class Convert
    {
        public static ExaminationQuestion convert(this Question question)
        {
            return new ExaminationQuestion()
            {
                Answer = question.Answer,
                QuestionNumber = question.QuestionNumber,
                Score = question.Score,
                Statistics = new Dictionary<string, int>(),
            };
        }

        public static Voter convert(this  StudentInfo studentInfo)
        {
            return new Voter(studentInfo.ClientNumber)
            {
                Statistics = new Dictionary<int, string>(),
                StatisticsAnswer = new Dictionary<int, int>(),
                VoterMappingNumber = studentInfo.Number,
                VoterName = studentInfo.Name,
            };
        }
        //VoterScene
        public static Voter convert(this VoterScene studentInfo)
        {
            return new Voter(studentInfo.VoterId)
            {
                Statistics = new Dictionary<int, string>(),
                StatisticsAnswer = new Dictionary<int, int>(),
                VoterMappingNumber = studentInfo.MappingNumber,
                VoterName = studentInfo.Name,
            };
        }

        public  static RegionVM convert(this RegionScene request)
        {
            return new RegionVM()
            {
                Score = request.Score,
                ScoreExplain = request.ScoreExplain,
            };
        }

        public static RegionScene convert(this RegionVM request)
        {
            return new RegionScene()
            {
                Score = request.Score,
                ScoreExplain = request.ScoreExplain,
            };
        }
    }
}
