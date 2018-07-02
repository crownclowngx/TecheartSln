using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecheartSln.Core.Message.MessageTypeProvider;

namespace TecheartSln.Core.Message.RelationProvider
{
    public class Relation
    {
        /// <summary>
        /// 关系ID
        /// </summary>
        public String RelationGuid { get; set; }

        /// <summary>
        /// 关系说明
        /// </summary>
        public String RelationDescribe { get; set; }

        /// <summary>
        /// 该关系的具体动作
        /// </summary>
        public Action<MessageDataBase> RelationAction { get; set; }

        /// <summary>
        /// 是否是一个激活的关系（如果不是激活的关系在投递者进行投递时将不进行投递）
        /// </summary>
        public Boolean IsActive { get; set; }

        /// <summary>
        /// 是否可以卸载激活的 即该关系是否能够选择不激活
        /// </summary>
        public Boolean CanUninstall { get; set; }

        /// <summary>
        /// 是否需要保持监听，如果是一个需要保持的监听，在每次自动失活所有激活的监听的时候 将不失活该监听
        /// </summary>
        public Boolean IsKeep { get; set; }
    }
}
