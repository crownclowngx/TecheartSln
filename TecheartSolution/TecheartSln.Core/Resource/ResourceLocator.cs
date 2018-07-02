using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TecheartSln.Core.Resource
{
    /// <summary>
    /// 装载任意程序级下的对应类型的资源资源.
    /// 可用于XAML资源文件的加载.
    /// 将XAML的路径按照URI的形式传入
    /// 返回的实例可以在cs文件的上下文中使用.
    /// </summary>
    public static class ResourceLocator
    {

        /// <summary>
        /// 获取第一个资源类型.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assemblyName">Name of the assembly.</param>
        /// <param name="resourceFilename">The resource filename.</param>
        /// <returns></returns>
        public static T GetResource<T>(string assemblyName, string resourceFilename) where T : class
        {
            return GetResource<T>(assemblyName, resourceFilename, string.Empty);
        }

        /// <summary>
        /// 根据名字获取资源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assemblyName">Name of the assembly.</param>
        /// <param name="resourceFilename">The resource filename.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static T GetResource<T>(string assemblyName, string resourceFilename, string name) where T : class
        {
            try
            {
                if (string.IsNullOrEmpty(assemblyName) || string.IsNullOrEmpty(resourceFilename))
                    return default(T);

                string uriPath = string.Format("/{0};component/{1}", assemblyName, resourceFilename);
                Uri uri = new Uri(uriPath, UriKind.Relative);
                ResourceDictionary resource = Application.LoadComponent(uri) as ResourceDictionary;

                if (resource == null)
                    return default(T);

                if (!string.IsNullOrEmpty(name))
                {
                    if (resource.Contains(name))
                        return resource[name] as T;

                    return default(T);
                }

                return resource.Values.OfType<T>().FirstOrDefault();
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.ToString());
            }

            return default(T);
        }
    }
}
