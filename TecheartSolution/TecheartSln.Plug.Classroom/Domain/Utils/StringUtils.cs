using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecheartSln.Plug.Classroom.Domain.Utils
{
    public class StringUtils
    {
        public static String RemoveDuplicateCharacters(String str)
        {
            List<String> lst = new List<string>();
            foreach(var v in str)
            {
                if (!lst.Any(k => k == v.ToString()))
                {
                    lst.Add(v.ToString());
                }
            }
            StringBuilder sb = new StringBuilder();
            foreach(var v in lst)
            {
                sb.Append(v);
            }
            return sb.ToString();
        }

    }
}
