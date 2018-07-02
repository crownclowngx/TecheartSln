using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecheartSln.Core.Message.RelationProvider;

namespace TecheartSln.Core.Message.MessageTypeProvider
{
    public class MessageRelationRequest:BaseMessageRequest
    {
        /// <summary>
        /// 消息类型
        /// </summary>
        public MessageType MessageType { get; set; }

        /// <summary>
        /// 消息关系
        /// </summary>
        public IList<Relation> Relations { get; set; }
    }
}
