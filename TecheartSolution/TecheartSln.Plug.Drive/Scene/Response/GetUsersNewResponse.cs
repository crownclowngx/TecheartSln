using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecheartSln.Plug.Drive.Scene.Response
{
    /// <summary>
    /// 
    /// </summary>
    public class GetUsersNewResponse
    {
        /// <summary>
        /// 用户信息
        /// </summary>
        public IList<UserDetailNew> UserInfos { get; set; }

        /// <summary>
        /// 页面个数
        /// </summary>
        public int PageCount { get; set; }
    }

    public class UserDetailNew
    {
        /// <summary>
        /// 用戶id，自增列查时候需要，插入时不需要传入
        /// </summary>
        public long UserInfoUserId { get; set; }

        /// <summary>
        /// 用户姓名 也就是之后用户的登录名
        /// </summary>
        public String UserInfoUserName { get; set; }

        /// <summary>
        /// 扩展文本 任何关于该用户的 扩展均存于该文本中
        /// </summary>
        public String UserInfoExtend { get; set; }

        /// <summary>
        /// 用户Type 0-超级管理员(不开放注册) 1-运营经历 2-星探组长 3-星探 999-主播
        /// </summary>
        public int UserInfoUserType { get; set; }

        /// <summary>
        /// 父节点id 该用户的上层节点id 最终跟节点 编号 0
        /// </summary>
        public int UserInfoFatherId { get; set; }

        /// <summary>
        /// 艺人ID，有别于userid 是一个艺人专属id
        /// </summary>
        public long AnchorInfoAnchorId { get; set; }

        /// <summary>
        /// 艺人的userid 
        /// </summary>
        public long AnchorInfoUserId { get; set; }

        /// <summary>
        /// 艺人的YY号
        /// </summary>
        public String AnchorInfoYYNumber { get; set; }

        /// <summary>
        /// 艺人的yy昵称
        /// </summary>
        public String AnchorInfoAnchorName { get; set; }

        /// <summary>
        /// 艺人类型 普通还是金牌艺人等
        /// </summary>
        public int AnchorInfoAnchorType { get; set; }

        /// <summary>
        /// 艺人的分成方式 
        /// </summary>
        public int AnchorInfoDivisionType { get; set; }

        /// <summary>
        /// 艺人的分成比例 0-100  表示 0%-100%
        /// </summary>
        public int AnchorInfoAnchorProportion { get; set; }

        /// <summary>
        /// 签约开始时间 
        /// </summary>
        public DateTime AnchorInfoStartTime { get; set; }

        /// <summary>
        /// 签约截止时间
        /// </summary>
        public DateTime AnchorInfoEndTime { get; set; }

        /// <summary>
        /// 艺人的真实姓名 
        /// </summary>
        public String AnchorInfoRealName { get; set; }

        /// <summary>
        /// 艺人的其他扩展字段 
        /// </summary>
        public String AnchorInfoExtend { get; set; }

        public String StartTimeStr { get { return AnchorInfoStartTime.ToString(); } }

        public String EndTimeStr { get { return AnchorInfoEndTime.ToString(); } }

        public String UserInfoUserTypeStr { get { if (UserInfoUserType == 0) { return "超级管理员"; } if (UserInfoUserType == 1) { return "运营经理"; } if (UserInfoUserType == 2) { return "星探组长"; } if (UserInfoUserType == 3) { return "星探"; } if (UserInfoUserType == 999) { return "主播"; } return ""; } }

        public String AnchorInfoAnchorTypeStr
        {
            get
            {
                if (AnchorInfoAnchorType == 0)
                {
                    return "普通艺人";
                }
                return "金牌艺人";
            }
        }

        public String AnchorInfoDivisionTypeStr
        {
            get
            {
                if (AnchorInfoDivisionType == 0)
                {
                    return "对私分成";
                }
                return "对共分成";
            }
        }
    }
}