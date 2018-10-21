using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Input;
using TecheartSln.Core.Command;
using TecheartSln.Core.Message.DeliverProvider;
using TecheartSln.Core.Message.MessageTypeProvider;
using TecheartSln.Core.Message.RelationProvider;
using TecheartSln.Core.Resource;
using TecheartSln.Core.Scene;
using TecheartSln.Core.Utils;
using TecheartSln.Core.ViewModel.Base;
using TecheartSln.Plug.Drive.Domain.Response;
using TecheartSln.Plug.Drive.Scene;
using TecheartSln.Plug.Drive.Scene.Response;

namespace TecheartSln.Plug.Drive.ViewModel
{
    public class SearchUserViewModel : TemplateBaseViewModel
    {
        public SearchUserViewModel(string title) : base(title)
        {
            if (CommonResource.GetValue("User") == null)
            {

                MessageBox.Show("未登录所以不能打开该页面");
                CloseThis();
                return;
            }
            var user = JsonUtils.Deserialize<User>(CommonResource.GetValue("User"));
            Dictionary<int, string> dic = new Dictionary<int, string>();
            if (user.UserType == 0)
            {
                dic.Add(1, "运营经理");
                dic.Add(2, "星探组长");
                dic.Add(3, "星探");
                dic.Add(999, "主播");
            }
            if (user.UserType == 1)
            { 
                dic.Add(2, "星探组长");
                dic.Add(3, "星探");
                dic.Add(999, "主播");
            }
            if (user.UserType == 2)
            { 
                dic.Add(3, "星探");
                dic.Add(999, "主播");
            }
            if (user.UserType == 3)
            {
                dic.Add(999, "主播");
            }
            UserTypeList = dic;
        }



        public SearchUserViewModel(BaseScene scene, String json) : base(scene, json)
        {

        }

        private IList<UserDetailNew> _AllQuesionList = new List<UserDetailNew>();
        /// <summary>
        /// 绑定的数据集合
        /// </summary>
        private ObservableCollection<UserDetailNew> _quesionList = new ObservableCollection<UserDetailNew>();
        public ObservableCollection<UserDetailNew> QuesionList
        {
            get { return _quesionList; }
            set
            {
                _quesionList = value;
                RaisePropertyChanged("QuesionList");
            }
        }

        /// <summary>
        /// 艺人类型
        /// </summary>
        private Dictionary<int, string> _UserTypeList = new Dictionary<int, string>() { };

        public Dictionary<int, string> UserTypeList
        {
            get { return _UserTypeList; }
            set { _UserTypeList = value; RaisePropertyChanged("UserTypeList"); }
        }


        /// <summary>
        ///  选择的用户类型
        /// </summary>
        private int _UserType = 0;
        public int UserType
        {
            get { return _UserType; }
            set
            {
                _UserType = value;
                var user = JsonUtils.Deserialize<User>(CommonResource.GetValue("User"));
                var response = ZhiBoUtils.GetAnyUserByFilter(user.UserId, 1, 10000, value);
                if (response.Code == 0)
                {
                    SetSearchValue(response.Data.UserInfos);
                }
                RaisePropertyChanged("UserType");
            }
        }


        /// <summary>
        /// 输入的用户信息
        /// </summary>
        private String _InputSearch = String.Empty;
        public String InputSearch
        {
            get { return _InputSearch; }
            set
            {
                _InputSearch = value;
                QuesionList = ConvertToObservableCollection(_AllQuesionList.Where(k => k.UserInfoUserName.Contains(value)).ToList());
                RaisePropertyChanged("InputSearch");
            }
        }

        RelayCommand _closeCommand = null;
        override public ICommand CloseCommand
        {
            get
            {
                if (_closeCommand == null)
                {
                    _closeCommand = new RelayCommand((p) =>
                    {
                        CloseThis();
                    }, (p) => true);
                }

                return _closeCommand;
            }
        }

        RelayCommand _saveCommand = null;
        override public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new RelayCommand((p) =>
                    {

                    }, (p) => true);
                }

                return _saveCommand;
            }
        }
        public new static string GetDesc()
        {
            return "根据当前登陆的账号和选择的类型查询 他所有已注册下属";
        }

        public new static string GetName()
        {
            return "下属查询";
        }

        /// <summary>
        /// 文档类，必须生成一个新的Guid不可直接使用
        /// </summary>
        /// <returns></returns>
        public new static string TemplateGuid()
        {
            return "f7aa26e5-4947-4444-bac1-f8e0a4e17e44";
        }
        private void CloseThis()
        {
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            MessageDeliverGroup.Delivery(MessageType.CloseTempleteEvemt, javaScriptSerializer.Serialize(new CloseTempleteRequest() { TempleteGuid = this.StrGuid }));
            MessageSubscribeRelations.DeleteSubscribe(MessageType.WSDEDataEvent, this.StrGuid);
        }

        private void SetSearchValue(IList<UserDetailNew> request)
        {
            QuesionList = ConvertToObservableCollection(request);
            _AllQuesionList = (request);
        }

        private ObservableCollection<T> ConvertToObservableCollection<T>(IList<T> ts)
        {
            ObservableCollection<T> ot = new ObservableCollection<T>(ts);
            return ot;
        }
    }
}
