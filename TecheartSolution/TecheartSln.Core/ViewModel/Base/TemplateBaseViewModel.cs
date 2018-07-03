using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Input;
using TecheartSln.Core.Scene;

namespace TecheartSln.Core.ViewModel.Base
{
    /// <summary>
    /// 模板文件基础类，主要描述的已是一个可以临时新建的 文件类型 主要是和tools对比他可以关闭
    /// 该viewModel没有可用view请勿使用，如果需要请继承这个类型
    /// </summary>
    public abstract class TemplateBaseViewModel : PaneViewModel
    {

        public readonly String StrGuid = Guid.NewGuid().ToString();
        public TemplateBaseViewModel(String title)
        {
            Title = title;
        }

        public TemplateBaseViewModel(BaseScene scene,String json)
        {

        }

        abstract public ICommand CloseCommand
        {
            get;
        }

        abstract public ICommand SaveCommand
        {
            get;
        }


        Boolean _isChanged = false;
        public Boolean IsChanged { get { return _isChanged; } set { _isChanged = value; if (value) MakeChangeTitle(); } }

        public void ViewChange()
        {
            IsChanged = true;
        }

        String _path = String.Empty;
        /// <summary>
        /// 文件路径
        /// </summary>
        public String Path { get { return _path; } set { _path = value; SetTitle(); } }
        /// <summary>
        /// 保存一个字符串到文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="jsonObject">文件可以还原现场的序列化结果</param>
        public void SaveFile(BaseScene scene)
        {
            var dialog = new SaveFileDialog() { Filter = "Techeart自定义文件|*.Techeart", FileName = Title.Replace("*", "") };
            if (dialog.ShowDialog() == true)
            {
                this.Path = dialog.FileName;
                scene.Title = this.Title;
                FileStream fs = new FileStream(dialog.FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                StreamWriter sw = new StreamWriter(fs);
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                sw.WriteLine(javaScriptSerializer.Serialize(scene));
                sw.Flush();
                sw.Close();
            }
        }

        void SetTitle()
        {
            if (String.IsNullOrWhiteSpace(this.Path))
            {
                return;
            }
            String title = this.Path.Split('\\').LastOrDefault();
            if (String.IsNullOrWhiteSpace(title))
            {
                return;
            }
            this.Title = title.Split('.').First();
            return;
        }

        void MakeChangeTitle()
        {
            if (this.Title.LastOrDefault() == '*')
            {
                return;
            }
            this.Title += "*";
        }

        public static string GetDesc()
        {
            return "";
        }

        public static string GetName()
        {
            return "";
        }

        public static String TemplateGuid()
        {
            return "";
        }
    }
}
