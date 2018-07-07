using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecheartSln.Plug.Classroom.Scene
{
    public class VoterScene
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
    }
}
