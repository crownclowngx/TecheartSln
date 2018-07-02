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
    /// 消息投递者，生产者生产一个消息 交给一个消息投递者，该投递者负责对该类型已注册所有消费者进行投递
    /// 不考了直接使用一个manager的情况是，如果某一类消息产生拥堵，或者某一个实际的消费者产生拥堵，那么manager将不能在有效时间内做正确投递，
    /// 所以牺牲内存占用而保证效率，并且在接收者的Action中，该Action也将不在是线程安全的
    /// </summary>
    public class SpecialMessageDeliverer
    {

        private MessageDataBase message { get; set; }
        public SpecialMessageDeliverer(MessageType type, String messageData)
        {
            message = new MessageDataBase();
            message.MessageData = messageData;
            message.MessageDataType = type;
        }

        public void Delivery()
        {
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
