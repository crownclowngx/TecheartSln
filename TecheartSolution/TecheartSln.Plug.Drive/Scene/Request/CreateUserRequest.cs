using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecheartSln.Plug.Drive.Scene.Request
{
    public class CreateUserRequest
    {
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
        /// 父节点id 该用户的上层节点id 最终跟节点 编号 0 即如果是运营经理注册那么 这个属性就传递0 或者不传
        /// </summary>
        public long FatherId { get; set; }

        /// <summary>
        /// 明文的密码，在注册时候使用其他时候该字段无定义
        /// </summary>
        public String PassWord { get; set; }
    }
}
