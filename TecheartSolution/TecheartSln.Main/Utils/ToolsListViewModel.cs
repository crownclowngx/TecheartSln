using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecheartSln.Core.ViewModel.Base;

namespace TecheartSln.Main
{
    public class ToolsListViewModel : ViewModelBase
    {
        public IEnumerable<ToolViewModel> tools = null;
        public ToolsListViewModel()
        {
            tools = Workspace.This.Tools;
            base.BindingPropInOtherTask(() => 
            {
                Tools.Clear();
                foreach(var v in tools)
                {
                    _tools.Add(v);
                }
            });
        }

        ObservableCollection<ToolViewModel> _tools = new ObservableCollection<ToolViewModel>();

        public ObservableCollection<ToolViewModel> Tools
        {
            get { return _tools; }
            set
            {
                _tools = value;
                RaisePropertyChanged("Tools");
            }
        }

    } 
}
