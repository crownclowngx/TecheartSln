using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecheartSln.Core.Message.MessageTypeProvider
{
    public class BaseMessageRequest
    {
        /// <summary>
        /// 类型名称
        /// </summary>
        public String TypeName { get; set; }

        /// <summary>
        /// 子消息类型，比如其他消息，可以通过该类型区分
        /// </summary>
        public String SubTypeName { get; set; }

        /// <summary>
        /// 消息类型的友好说明
        /// </summary>
        public String Desc { get; set; }
    }
}
