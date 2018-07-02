using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecheartSln.Core.ViewModel.Base
{
    /// <summary>
    /// 所有工具类的基类 所有继承该工具类的 viewmodel 将是不能被新建的非文档 类型
    /// </summary>
    public class ToolViewModel : PaneViewModel
    {
        public ToolViewModel(string name)
        {
            Name = name;
            Title = name;
        }

        public string Name
        {
            get;
            private set;
        }


        #region IsVisible

        private bool _isVisible = true;
        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                if (_isVisible != value)
                {
                    _isVisible = value;
                    RaisePropertyChanged("IsVisible");
                }
            }
        }

        #endregion
    }
}
