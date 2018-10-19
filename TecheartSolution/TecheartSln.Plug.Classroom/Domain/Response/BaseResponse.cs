using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecheartSln.Plug.Classroom.Domain.Response
{
    /// 基本的返回类型 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseResponse<T>
    {
        /// <summary>
        /// 返回状态吗 200是成功  其他是失败
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 返回的状态说明 如果是成功 不需要关注该问题，否则如果失败在里面将会返回详细信息，该信息不可用于展示 主要用于调试
        /// </summary>
        public String Detail { get; set; }

        /// <summary>
        /// 泛型的Data 里面包含了正式的业务数据
        /// </summary>
        public T Data { get; set; }
    }
}
