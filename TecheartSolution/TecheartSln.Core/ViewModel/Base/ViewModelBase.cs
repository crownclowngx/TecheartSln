using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TecheartSln.Core.ViewModel.Base
{
    /// <summary>
    /// 所有viewmodel 应该继承的基类，该基类继承并实现了 INotifyPropertyChanged 接口
    /// 除非Main项目中的特别地方
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 更改属性将引发该事件，
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// 告诉通过WPF绑定的控件刷新显示
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="property"></param>
        public void NotifyPropertyChanged<TProperty>(Expression<Func<TProperty>> property)
        {
            var lambda = (LambdaExpression)property;
            MemberExpression memberExpression;

            if (lambda.Body is UnaryExpression)
            {
                var unaryExpression = (UnaryExpression)lambda.Body;
                memberExpression = (MemberExpression)unaryExpression.Operand;
            }
            else
                memberExpression = (MemberExpression)lambda.Body;

            this.RaisePropertyChanged(memberExpression.Member.Name);
        }

        public void BindingPropInOtherTask(Action action)
        {
            ThreadPool.QueueUserWorkItem(delegate
            {
                System.Threading.SynchronizationContext.SetSynchronizationContext(new
                    System.Windows.Threading.DispatcherSynchronizationContext(System.Windows.Application.Current.Dispatcher));
                System.Threading.SynchronizationContext.Current.Post(pl =>
                {
                    //里面写真正的业务内容
                    action();
                }, null);
            });
        }
    }
}
