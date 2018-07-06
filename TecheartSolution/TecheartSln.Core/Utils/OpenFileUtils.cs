using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecheartSln.Core.Utils
{
    public static class OpenFileUtils
    {
        public static String OpenFileByDialog()
        {
            var dlg = new OpenFileDialog();
            if (dlg.ShowDialog().GetValueOrDefault())
            {
                FileStream fsRead = new FileStream(dlg.FileName, FileMode.OpenOrCreate, FileAccess.Read);
                int fsLen = (int)fsRead.Length;
                byte[] heByte = new byte[fsLen];
                int r = fsRead.Read(heByte, 0, heByte.Length);
                string myStr = System.Text.Encoding.UTF8.GetString(heByte);
                return myStr;
            }
            return String.Empty;
        }
    }
}
