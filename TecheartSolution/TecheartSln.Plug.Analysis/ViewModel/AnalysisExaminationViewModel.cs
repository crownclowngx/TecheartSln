using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TecheartSln.Core.Scene;
using TecheartSln.Core.ViewModel.Base;

namespace TecheartSln.Plug.Analysis.ViewModel
{
    public class AnalysisExaminationViewModel : TemplateBaseViewModel
    {
        public AnalysisExaminationViewModel(string title) : base(title)
        {

        }

        public AnalysisExaminationViewModel(BaseScene scene, String json) : base(scene, json)
        {
           
        }

        public override ICommand CloseCommand => throw new NotImplementedException();

        public override ICommand SaveCommand => throw new NotImplementedException();
    }
}
