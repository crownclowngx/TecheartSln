using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecheartSln.Plug.Analysis.Domain.Boundary.Examination;

namespace TecheartSln.Plug.Analysis.Domain
{
    /// <summary>
    /// 考试分析主体类
    /// 应支持，groupby,where,top
    /// </summary>
    public class ExaminationAnalysis
    {
        /// <summary>
        /// 所需要分析的考试列表 
        /// </summary>
        IList<SimpleExamination> simpleExaminations { get; set; }

        public ExaminationAnalysis()
        {
            simpleExaminations = new List<SimpleExamination>();
        }

        public void AddExamination(SimpleExamination simpleExamination)
        {
            simpleExaminations.Add(simpleExamination);
        }


    }
}
