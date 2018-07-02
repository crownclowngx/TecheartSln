using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecheartSln.Core.Message.MessageTypeProvider
{
    public class WSDESubVoterSelectRequest
    {
        /// <summary>
        /// 身份编号
        /// </summary>
        public String SubVoterNumber { get; set; }

        /// <summary>
        /// 输入的结果
        /// </summary>
        public String SubVoterResult { get; set; }

        /// <summary>
        /// 对应的题号
        /// </summary>
        public String SubVoterSelectNumber { get; set; }
    }
}
