using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecheartSln.Core.Message.MessageTypeProvider;
using TecheartSln.Core.Message.RelationProvider;

namespace TecheartSln.Core.Message.DeliverProvider
{
    /// <summary>
    /// 可服用消息发送者
    /// </summary>
    public class ReusableMessageDeliverer
    {

        /// <summary>
        /// 是否是忙碌的
        /// </summary>
        public bool IsBusy { get; set; }
        public ReusableMessageDeliverer()
        { 
        }

        public void Delivery(MessageType type, String messageData)
        {
            MessageDataBase message = new MessageDataBase();
            message.MessageData = messageData;
            message.MessageDataType = type;
            var consumers = MessageSubscribeRelations.GetCurrentRelationsByMessageType(message.MessageDataType);
            foreach (var v in consumers)
            {
                try
                {
                    v.RelationAction(message);
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                    //异常日志记录
                }
            }

            var consumersAll = MessageSubscribeRelations.GetCurrentRelationsByMessageType(MessageType.All);
            foreach (var v in consumersAll)
            {
                try
                {
                    v.RelationAction(message);
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                    //异常日志记录
                }
            }
        }
    }
}
