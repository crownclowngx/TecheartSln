using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecheartSln.Core.Message.MessageTypeProvider;

namespace TecheartSln.Core.Message.RelationProvider
{
    public class MessageDataBase
    {
        /// <summary>
        /// 用户消息类型
        /// </summary>
        public MessageType MessageDataType { get; set; }

        /// <summary>
        /// 用户消息的具体数据
        /// </summary>
        public String MessageData { get; set; }
    }
}
