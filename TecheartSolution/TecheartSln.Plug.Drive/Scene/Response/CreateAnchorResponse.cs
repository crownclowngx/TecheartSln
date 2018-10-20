using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecheartSln.Plug.Drive.Domain.Response;

namespace TecheartSln.Plug.Drive.Scene.Response
{
    public class CreateAnchorResponse
    {
        /// <summary>
        /// 艺人的相关信息
        /// </summary>
        public Anchor Anchor { get; set; }

        /// <summary>
        /// 用户的相关信息
        /// </summary>
        public User User { get; set; }
    }
}
