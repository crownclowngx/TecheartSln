using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TecheartSln.Core.Command;
using TecheartSln.Core.Message.DeliverProvider;
using TecheartSln.Core.Message.MessageTypeProvider;
using TecheartSln.Core.Message.RelationProvider;
using TecheartSln.Core.Utils;
using TecheartSln.Core.ViewModel.Base;
using TecheartSln.Plug.Drive.Domain.Response;

namespace TecheartSln.Plug.Drive.ViewModel
{
    public class LoginViewModel : ToolViewModel
    {
        public const string ToolContentId = "LoginTool";
        public LoginViewModel() : base("登陆")
        {
            ContentId = ToolContentId;
        }

        /// <summary>
        /// 登录名
        /// </summary>
        private String _loginName = "";

        public String LoginName
        {
            get { return _loginName; }
            set
            {
                _loginName = value;
                RaisePropertyChanged("LoginName");
            }
        }


        /// <summary>
        /// 是否已经登陆
        /// </summary>
        private bool _isUnLogin = true;

        public bool IsUnLogin
        {
            get { return _isUnLogin; }
            set
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
                        var ps = (PasswordBox)p;
                        var responsestr=HttpUtils.PostJson("http://39.107.99.199:30000/api/User/Login", new { UserName= _loginName, PassWord= ps.Password });
                        var response=JsonUtils.Deserialize<Domain.Response.BaseResponse<UserResponse>>(responsestr);
                        if (response.Code != 0)
                        {
                            MessageBox.Show("用户名或密码错误");
                        }
                        else
                        {
                            TecheartSln.Core.Resource.CommonResource.SetValue("User", JsonUtils.Serialize(response.Data.User));
                            TecheartSln.Core.Resource.CommonResource.SetValue("Token", JsonUtils.Serialize(response.Data.Token));
                            MessageBox.Show("登陆成功");
                            IsUnLogin = false;
                        }
                    }, p => true);
                }
                return _loginCommand;
            }
        }
    }
}
