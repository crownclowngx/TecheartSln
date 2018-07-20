using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecheartSln.Plug.Analysis.Domain.Boundary.Examination
{
    public class SimpleExamination
    {
        public IList<Voter> VoterScene { get; set; }

        public IList<Question> ExaminationQuestion { get; set; }
    }
}
