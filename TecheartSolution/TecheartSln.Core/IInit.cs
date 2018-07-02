using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecheartSln.Core.View.Pane;
using TecheartSln.Core.ViewModel.Base;

namespace TecheartSln.Core
{
    public interface IInit
    {
        void RedisterStyles(PanesStyleSelectorDynamic selectPanesStyle);

        void RegisterDataTemplates(PanesTemplateSelectorDynamic paneSel);

        IList<ToolViewModel> GetToolViewModel();


    }
}
