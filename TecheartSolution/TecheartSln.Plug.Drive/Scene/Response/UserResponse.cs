using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecheartSln.Plug.Drive.Domain.Response
{
    /// <summary>
    /// 添加用户的返回值
    /// </summary>
    public class UserResponse : CommonResponse
    {
        /// <summary>
        /// 用户的基本信息
        /// </summary>
        public User User { get; set; }
    }
}
