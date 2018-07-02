using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TecheartSln.Core.View.Pane
{
    public class PanesTemplateSelectorDynamic : DataTemplateSelector
    {
        #region fields
        private readonly Dictionary<Type, DataTemplate> _TemplateDirectory = null;
        #endregion fields

        #region constructor
        /// <summary>
        /// 实例化
        /// </summary>
        public PanesTemplateSelectorDynamic()
        {
            _TemplateDirectory = new Dictionary<Type, DataTemplate>();
        }
        #endregion constructor

        #region methods
        /// <summary>
        /// 返回一个DataTemplate如果已经在缓存里面出现过
        /// </summary>
        /// <param name="item">The data object for which to select the template.</param>
        /// <param name="container">The data-bound object.</param>
        /// <returns>Returns a System.Windows.DataTemplate or null. The default value is null.</returns>
        public override System.Windows.DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
        {
            if (_TemplateDirectory == null)
                return null;

            if (item == null)
                return null;

            DataTemplate o;
            _TemplateDirectory.TryGetValue(item.GetType(), out o);

            if (o != null)
                return o;

            return base.SelectTemplate(item, container);
        }

        /// <summary>
        /// 注册一个数据模板 <seealso cref="DataTemplate"/> for a view.
        /// </summary>
        /// <param name="typeOfViewmodel"></param>
        /// <param name="view"></param>
        public void RegisterDataTemplate(Type typeOfViewmodel, DataTemplate view)
        {
            _TemplateDirectory.Add(typeOfViewmodel, view);
        }
        #endregion methods
    }
}
