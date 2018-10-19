using System;
using System.Collections.Generic;
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
using TecheartSln.Core.Utils;
using TecheartSln.Core.ViewModel.Base;

namespace TecheartSln.Plug.Analysis.ViewModel
{
    public class LoginViewModel : ToolViewModel
    {
        public const string ToolContentId = "LoginTool";
        private readonly String GuidRelationChange = Guid.NewGuid().ToString();
        public LoginViewModel() : base("登陆")
        {
            ContentId = ToolContentId;
            MessageSubscribeRelations.AddSubscribe(MessageType.RelationChangeEvent, new Relation()
            {
                CanUninstall = false,
                IsActive = true,
                RelationDescribe = "关系维护列表监听",
                RelationGuid = GuidRelationChange,
                IsKeep = true,
                RelationAction = (messagedata) =>
                {
                    base.BindingPropInOtherTask(() =>
                    {
                        
                    });
                }
            });
        }

        /// <summary>
        /// 登录名
        /// </summary>
        private String _loginName = String.Empty;

        public String LoginName
        {
            get { return _loginName; }
            private set
            {
                _loginName = value;
                RaisePropertyChanged("LoginName");
            }
        }

        /// <summary>
        /// 密码
        /// </summary>
        private String _password = String.Empty;

        public String Password
        {
            get { return _password; }
            private set
            {
                _password = value;
                RaisePropertyChanged("Password");
            }
        }

        /// <summary>
        /// 是否已经登陆
        /// </summary>
        private bool _isUnLogin = true;

        public bool IsUnLogin
        {
            get { return _isUnLogin; }
            private set
            {
                _isUnLogin = value;
                RaisePropertyChanged("IsUnLogin");
            }
        }

        RelayCommand _loginCommand = null;
        public ICommand LoginCommand
        {
            get
            {
                if (_loginCommand == null)
                {
                    _loginCommand = new RelayCommand((p) => 
                    {
                        var response=HttpUtils.PostJson("http://39.107.99.199:30000/api/User/Login", new { UserName= _loginName, PassWord= _password });
                        MessageBox.Show(response);
                    }, p => true);
                }
                return _loginCommand;
            }
        }
    }
}
