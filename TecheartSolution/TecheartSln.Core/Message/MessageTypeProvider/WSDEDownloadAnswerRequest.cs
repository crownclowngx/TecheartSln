using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecheartSln.Core.Message.MessageTypeProvider
{
    public class WSDEDownloadAnswerRequest:BaseMessageRequest
    {
        public IList<AnswerRequest> answerRequests { get; set; }
    }

    public class AnswerRequest
    {
        /// <summary>
        /// 问题id
        /// </summary>
        public int ProblemId { get; set; }

        /// <summary>
        /// 问题的标准答案
        /// </summary>
        public String ProblemAnswer { get; set; }
    }
}
