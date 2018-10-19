using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecheartSln.Plug.Classroom.Domain.Response
{
    public class CommonResponse
    {
        /// <summary>
        /// 用户token，用于其他业务接口识别 用户id 的标识
        /// </summary>
        public String Token { get; set; }
    }
}
