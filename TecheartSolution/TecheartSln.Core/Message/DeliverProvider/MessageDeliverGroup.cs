using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecheartSln.Core.Message.MessageTypeProvider;

namespace TecheartSln.Core.Message.DeliverProvider
{
    public static class MessageDeliverGroup
    {
        static List<ReusableMessageDeliverer> reusableMessageDeliverers = new List<ReusableMessageDeliverer>() { };
        static Random random = new Random();
        static MessageDeliverGroup()
        {
            for(var i = 1; i <= 20; i++)
            {
                reusableMessageDeliverers.Add(new ReusableMessageDeliverer());
            }
        }
        /// <summary>
        /// 该方法由于投递者只投递一次就被废弃，所以会产生大量密集的GC
        /// </summary>
        /// <param name="type"></param>
        /// <param name="messageData"></param>
        public static void SpecialDelivery(MessageType type, String messageData)
        {
            new Task(() => {
                SpecialMessageDeliverer messageDeliverer = new SpecialMessageDeliverer(type, messageData);
                messageDeliverer.Delivery();
            }).Start();

        }

        public static void Delivery(MessageType type, String messageData)
        {
            //公用列表算法需要调整暂时使用 独立投递
            SpecialDelivery(type, messageData);
        }
    }
}
