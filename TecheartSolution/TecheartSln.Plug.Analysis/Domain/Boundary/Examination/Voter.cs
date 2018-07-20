using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecheartSln.Plug.Analysis.Domain.Boundary.Examination
{
    public class Voter
    {
        /// <summary>
        /// 学生编号（投票器编号）
        /// </summary>
        public String VoterId { get; set; }

        /// <summary>
        /// 题目编号
        /// </summary>
        public int QuestionNumber { get; set; }

        /// <summary>
        /// 选择结果
        /// </summary>
        public String Select { get; set; }

        /// <summary>
        /// 学生对应学号
        /// </summary>
        public String MappingNumber { get; set; }

        /// <summary>
        /// 学生姓名
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// 选择答案是正确的
        /// </summary>
        public Boolean SelectIsRight { get; set; }
    }
}
