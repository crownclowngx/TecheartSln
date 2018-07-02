using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace TecheartSln.Core.Utils
{
    public static class JsonUtils
    {
        static JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();

        public static String Serialize<T>(T obj)
        {
            return javaScriptSerializer.Serialize(obj);
        }

        public static String Serialize(object obj)
        {
            return javaScriptSerializer.Serialize(obj);
        }

        public static T Deserialize<T>(String jsonstr)
        {
            return javaScriptSerializer.Deserialize<T>(jsonstr);
        }
    }
}
