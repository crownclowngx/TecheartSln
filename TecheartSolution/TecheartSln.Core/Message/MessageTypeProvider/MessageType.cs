using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecheartSln.Core.Message.MessageTypeProvider
{
    public enum MessageType
    {
        /// <summary>
        /// WSDE类数据来临时触发事件
        /// </summary>
        WSDEDataEvent = 1,
        /// <summary>
        /// 通知主机下载答案列表 最多下载119 个答案
        /// </summary>
        DownloadAnswerToWSDEHost = 2,
        /// <summary>
        /// 当消息关系改变时触发的消息类型
        /// </summary>
        RelationChangeEvent = 3,

        /// <summary>
        /// 关闭一个模板的事件
        /// </summary>
        CloseTempleteEvemt = 4,

        /// <summary>
        /// 所有消息
        /// </summary>
        All = 998,
        /// <summary>
        /// 其他信息，暂不支持
        /// </summary>
        Other = 999,
    }
}
