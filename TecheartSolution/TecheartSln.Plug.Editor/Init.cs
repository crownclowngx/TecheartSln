using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TecheartSln.Core;
using TecheartSln.Core.Resource;
using TecheartSln.Core.View.Pane;
using TecheartSln.Core.ViewModel.Base;
using TecheartSln.Plug.Editor.ViewModel;

namespace TecheartSln.Plug.Editor
{
    /// <summary>
    /// 向Main 注册
    /// </summary>
    public class Init : IInit
    {
        public IList<ToolViewModel> GetToolViewModel()
        {
            return new List<ToolViewModel>() { };
        }

        public void RedisterStyles(PanesStyleSelectorDynamic selectPanesStyle)
        {
        }

        public void RegisterDataTemplates(PanesTemplateSelectorDynamic paneSel)
        {
           
        }
    }
}
