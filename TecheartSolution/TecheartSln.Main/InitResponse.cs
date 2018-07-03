using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecheartSln.Core.ViewModel.Base;

namespace TecheartSln.Main
{
    public class InitResponse
    {
        /// <summary>
        /// Init接口返回
        /// </summary>
        public IList<TecheartSln.Core.IInit> Inits { get; set; }

        /// <summary>
        /// TemplateBaseViewModels
        /// </summary>
        public IList<TemplateViewModelInit> TemplateBaseViewModels { get; set; }


        /// <summary>
        /// ToolViewModels
        /// </summary>
        public IList<ToolViewModel> ToolViewModels { get; set; }
    }

    public class TemplateViewModelInit
    {
        public Type TemplateType { get; set; }

        public String Identifier { get; set; }
    }
}
