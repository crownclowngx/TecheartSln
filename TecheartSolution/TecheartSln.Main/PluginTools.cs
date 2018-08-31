using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TecheartSln.Core.Utils;
using TecheartSln.Core.ViewModel.Base;

namespace TecheartSln.Main
{
    public static class PluginTools
    {
        static string filePath = @".\plug\";

        static string commdllpath = @".\commondll\";

        public static IList<String> GetCommonDllsPath()
        {
            IList<String> filePaths = new List<String>();
            FileGet.clear();
            var files = FileGet.getFile(commdllpath, ".dll");
            foreach (var v in files)
            {
                filePaths.Add(v.FullName);
            }
            return filePaths;
        }
        public static IList<String> GetPlugsPath()
        {
            IList<String> filePaths = new List<String>();
            FileGet.clear();
            var files=FileGet.getFile(filePath, ".dll");
            foreach(var v in files)
            {
                filePaths.Add(v.FullName);
            }
            return filePaths;
        }

        public static Assembly GetAssembly(String path)
        {
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            byte[] bFile = br.ReadBytes((int)fs.Length);
            br.Close();
            fs.Close();
            var dll = Assembly.Load(bFile);
            return dll;
        }

        public static InitResponse Init()
        {
            
            InitResponse initResponse = GetInitPlug();
            return initResponse;
        }

        private static InitResponse GetInitPlug()
        {
            InitResponse initResponse = new InitResponse() { Inits = new List<TecheartSln.Core.IInit>(), TemplateBaseViewModels = new List<TemplateViewModelInit>(), ToolViewModels = new List<ToolViewModel>() };
            var paths = GetPlugsPath();
            foreach (var v in paths)
            {
                var dll = GetAssembly(v);
                foreach (var dlltype in dll.GetTypes())
                {
                    if (dlltype.Name == "Init")
                    {
                        var pluginit = (TecheartSln.Core.IInit)Activator.CreateInstance(dlltype);
                        initResponse.Inits.Add(pluginit);
                        foreach (var v3 in pluginit.GetToolViewModel())
                        {
                            initResponse.ToolViewModels.Add(v3);
                        }
                    }
                    if (dlltype.BaseType == typeof(TemplateBaseViewModel))
                    {
                        MethodInfo methodDesc = dlltype.GetMethod("TemplateGuid");
                        if (methodDesc == null)
                        {
                            continue;
                        }
                        TemplateViewModelInit templateViewModelInit = new TemplateViewModelInit() { TemplateType = dlltype, Identifier = methodDesc.Invoke(null, null).ToString() };
                        initResponse.TemplateBaseViewModels.Add(templateViewModelInit);
                    }
                }
            }

            return initResponse;
        }
    }
}
