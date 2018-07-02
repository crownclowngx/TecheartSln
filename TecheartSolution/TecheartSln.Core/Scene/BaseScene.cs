using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecheartSln.Core.Scene
{
    /// <summary>
    /// 场景，决定如何实例化
    /// </summary>
    public abstract class BaseScene
    {
        public String Title { get; set; }

        public String TypeIdentity { get; set; }


    }
}
