using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecheartSln.Plug.Drive.Scene.Response
{
    public class Anchor
    {
        /// <summary>
        /// 艺人ID，有别于userid 是一个艺人专属id
        /// </summary>
        public long AnchorId { get; set; }

        /// <summary>
        /// 艺人的userid 
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 艺人的YY号
        /// </summary>
        public String YYNumber { get; set; }

        /// <summary>
        /// 艺人的yy昵称
        /// </summary>
        public String AnchorName { get; set; }

        /// <summary>
        /// 艺人类型 普通还是金牌艺人等
        /// </summary>
        public int AnchorType { get; set; }

        /// <summary>
        /// 艺人的分成方式 
        /// </summary>
        public int DivisionType { get; set; }

        /// <summary>
        /// 艺人的分成比例 0-100  表示 0%-100%
        /// </summary>
        public int AnchorProportion { get; set; }


        /// <summary>
        /// 签约开始时间 
        /// </summary>
        public DateTime StartTime { get { return _StartTime; }set { _StartTime = value; } }

        /// <summary>
        /// 签约截止时间
        /// </summary>
        public DateTime EndTime { get { return _EndTime; } set { _EndTime = value; } }

        /// <summary>
        /// 艺人的真实姓名 
        /// </summary>
        public String RealName { get; set; }

        /// <summary>
        /// 艺人的其他扩展字段 
        /// </summary>
        public String Extend { get; set; }

        public String StartTimeStr { get { return StartTime.ToString(); } }

        public String EndTimeStr { get { return EndTime.ToString(); } }


        private DateTime _StartTime = DateTime.Now;
        private DateTime _EndTime = DateTime.Now;
        public override string ToString()
        {
            return $" YYNumbe={YYNumber}, AnchorName={AnchorName}, AnchorType={AnchorType}, DivisionType={DivisionType}, AnchorProportion={AnchorProportion}, StartTime={StartTime.ToString()}, EndTime={EndTime.ToString()}, RealName={RealName}, Extend={Extend}";
        }
    }
}
