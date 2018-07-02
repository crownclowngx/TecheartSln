using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TecheartSln.Core.Resource;
using TecheartSln.Core.View.Pane;
using TecheartSln.Core.ViewModel.Base;

namespace TecheartSln.Core
{
    public class Init : IInit
    {
        public IList<ToolViewModel> GetToolViewModel()
        {
            return new List<ToolViewModel>() { };
        }

        public void RedisterStyles(PanesStyleSelectorDynamic selectPanesStyle)
        {
            var newStyle = ResourceLocator.GetResource<Style>(
                "TecheartSln.Core",
                "FoundationStyle.xaml",
                "ToolStyle");
            var fileStyle = ResourceLocator.GetResource<Style>(
                "TecheartSln.Core",
                "FoundationStyle.xaml",
                "FileStyle");
            selectPanesStyle.RegisterStyle(typeof(ToolViewModel), newStyle);
            selectPanesStyle.RegisterStyle(typeof(TemplateBaseViewModel), fileStyle);
        }

        public void RegisterDataTemplates(PanesTemplateSelectorDynamic paneSel)
        {
            
        }

    }
}
