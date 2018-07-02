using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecheartSln.Core.Message.DeliverProvider;
using TecheartSln.Core.Message.MessageTypeProvider;
using TecheartSln.Core.Utils;

namespace TecheartSln.Core.Message.RelationProvider
{
    /// <summary>
    /// 订阅者关系维护，如果注册和注销动作时常发生 那么改类将带来巨大性能损失
    /// </summary>
    public class MessageSubscribeRelations
    {
        private static readonly object updateCacheLocker = new object();
        /// <summary>
        /// 关系维护的缓存类型 获取当前关系快照时如果 每次都重新开辟数组获取将会损失大量效率，
        /// 因为 关系修改不常常发生，
        /// 所以损失部分关系修改时的效率，
        /// 以加快获取关系列表快照时的效率
        /// 不能使用 Dictionary 因为如果多线程同时访问该集合的时，虽然Dictionary是线程安全的但是CPU使用率会大幅升高影响效率
        /// </summary>
        private static ConcurrentDictionary<MessageType, IList<Relation>> RelationCache = new ConcurrentDictionary<MessageType, IList<Relation>>();

        /// <summary>
        /// 关系维护的主要类型 因为其线程安全
        /// </summary>
        private static ConcurrentDictionary<MessageType, ConcurrentDictionary<String, Relation>> subscribeRelations = new ConcurrentDictionary<MessageType, ConcurrentDictionary<String, Relation>>();

        /// <summary>
        /// 因为类型是固定的，所以这里在预加载的时候处理完所有的类型关系
        /// </summary>
        static MessageSubscribeRelations()
        {
            subscribeRelations.TryAdd(MessageType.WSDEDataEvent, new ConcurrentDictionary<String, Relation>());
            subscribeRelations.TryAdd(MessageType.DownloadAnswerToWSDEHost, new ConcurrentDictionary<String, Relation>());
            subscribeRelations.TryAdd(MessageType.RelationChangeEvent, new ConcurrentDictionary<String, Relation>());
            subscribeRelations.TryAdd(MessageType.CloseTempleteEvemt, new ConcurrentDictionary<String, Relation>());
            subscribeRelations.TryAdd(MessageType.All, new ConcurrentDictionary<String, Relation>());

            RelationCache.TryAdd(MessageType.WSDEDataEvent, new List<Relation>());
            RelationCache.TryAdd(MessageType.DownloadAnswerToWSDEHost, new List<Relation>());
            RelationCache.TryAdd(MessageType.RelationChangeEvent, new List<Relation>());
            RelationCache.TryAdd(MessageType.CloseTempleteEvemt, new List<Relation>());
            RelationCache.TryAdd(MessageType.All, new List<Relation>());
        }

        /// <summary>
        /// 因为已经在static 的构造函数中处理了所有的键值关系，所以在这里不再对键值做判断，直接注册事件
        /// </summary>
        /// <param name="messageType"></param>
        /// <param name="action"></param>
        public static void AddSubscribe(MessageType messageType, Relation relation)
        {
            if (subscribeRelations[messageType].ContainsKey(relation.RelationGuid))
            {
                throw new Exception("设定关系时出现异常，该关系的RelationGuid已经存在，不能设置新的关系");
            }
            subscribeRelations[messageType].TryAdd(relation.RelationGuid, relation);
            UpdateCache(messageType);
        }

        public static void DeleteSubscribe(MessageType messageType, String relationGuid)
        {
            if (String.IsNullOrWhiteSpace(relationGuid))
            {
                throw new Exception("不能删除关系，因为传递的关系id 是null 或者是空");
            }
            Relation relation = new Relation();
            subscribeRelations[messageType].TryRemove(relationGuid, out relation);
            UpdateCache(messageType);
        }

        /// <summary>
        /// 考虑线程安全，那么将在这里 lock缓存更新操作
        /// </summary>
        private static void UpdateCache(MessageType messageType)
        {
            lock (updateCacheLocker)
            {
                RelationCache[messageType].Clear();
                foreach (var v in subscribeRelations[messageType].ToArray())
                {
                    RelationCache[messageType].Add(v.Value);
                }
                MessageDeliverGroup.Delivery(MessageType.RelationChangeEvent, JsonUtils.Serialize(new BaseMessageRequest() { TypeName= MessageType.RelationChangeEvent.ToString(), SubTypeName="", Desc="关系状态变更通知" }));
            }
        }

        /// <summary>
        /// 获取当先消息类型下的所有激活的订阅者
        /// </summary>
        /// <param name="messageType"></param>
        /// <returns></returns>
        public static IList<Relation> GetCurrentRelationsByMessageType(MessageType messageType)
        {
            return RelationCache[messageType].Where(k => k.IsActive).ToList();
        }

        /// <summary>
        /// 获取所有关系列表
        /// </summary>
        /// <returns></returns>
        public static IList<MessageRelationRequest> GetAllCurrentRelation()
        {
            List<MessageRelationRequest> relations = new List<MessageRelationRequest>();
            foreach (var v in RelationCache)
            {
                relations.Add(new MessageRelationRequest() { MessageType = v.Key, Relations = v.Value });
            }
            return relations;
        }


        /// <summary>
        /// 设置该类型下的所有关系为失活
        /// </summary>
        /// <param name="type"></param>
        public static void ClearRelationActive(MessageType type)
        {
            subscribeRelations[type].ToList().ForEach(k =>
            {
                if (!k.Value.IsKeep)
                {
                    k.Value.IsActive = false;
                }
            });
        }

        /// <summary>
        /// 设置一个关系为激活状态
        /// </summary>
        /// <param name="type"></param>
        public static void SetActive(MessageType type, String guidStr)
        {
            var response = subscribeRelations[type].FirstOrDefault(k => k.Key == guidStr);
            if (response.Value != null)
            {
                response.Value.IsActive = true;
            }
        }
    }
}
