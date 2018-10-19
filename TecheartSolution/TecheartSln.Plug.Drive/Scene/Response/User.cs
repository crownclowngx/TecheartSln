using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecheartSln.Plug.Drive.Domain.Response
{
    public class User
    {
        /// <summary>
        /// 用戶id，自增列查时候需要，插入时不需要传入
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 用户姓名 也就是之后用户的登录名
        /// </summary>
        public String UserName { get; set; }

        /// <summary>
        /// 扩展文本 任何关于该用户的 扩展均存于该文本中
        /// </summary>
        public String Extend { get; set; }

        /// <summary>
        /// 用户Type 0-超级管理员(不开放注册) 1-运营经历 2-星探组长 3-星探 999-主播
        /// </summary>
        public int UserType { get; set; }

        /// <summary>
        /// 父节点id 该用户的上层节点id 最终跟节点 编号 0
        /// </summary>
        public int FatherId { get; set; }

        /// <summary>
        /// 明文的密码，在注册时候使用其他时候该字段无定义
        /// </summary>
        public String PassWord { get; set; }

        /// <summary>
        /// 被加密的文本 在注册时使用
        /// </summary>
        public String EncryptPassWord { get; set; }
      
    }
}
