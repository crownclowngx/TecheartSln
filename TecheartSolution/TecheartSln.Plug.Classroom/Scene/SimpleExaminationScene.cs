﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecheartSln.Core.Scene;
using TecheartSln.Plug.Classroom.Domain;
using TecheartSln.Plug.Classroom.Domain.Boundary;

namespace TecheartSln.Plug.Classroom.Scene
{
    public class SimpleExaminationScene : BaseScene
    {
        public IList<VoterScene> VoterScene { get; set; }

        public IList<ExaminationQuestion> ExaminationQuestion { get; set; }

        public IList<RegionScene> RegionScenes { get; set; }

        public Examination Convert()
        {
            Examination examination = new Examination();
            if (ExaminationQuestion != null)
                examination.ExaminationQuestions = ExaminationQuestion;
            if (VoterScene != null)
            {
                foreach (var v in VoterScene)
                {
                    if (!examination.Voters.Any(k => k.VoterId == v.VoterId))
                    {
                        examination.Voters.Add(v.convert());
                    }
                    
                }
                foreach (var v in VoterScene)
                {
                    examination.Add(v.VoterId, v.QuestionNumber, v.Select);
                }
            }

            return examination;
        }
    }
}
