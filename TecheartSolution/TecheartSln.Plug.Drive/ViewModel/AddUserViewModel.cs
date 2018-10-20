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
using TecheartSln.Core.Resource;
using TecheartSln.Core.Scene;
using TecheartSln.Core.Utils;
using TecheartSln.Core.ViewModel.Base;
using TecheartSln.Plug.Drive.Domain.Response;
using TecheartSln.Plug.Drive.Scene;
using TecheartSln.Plug.Drive.Scene.Response;

namespace TecheartSln.Plug.Drive.ViewModel
{
    public class AddUserViewModel : TemplateBaseViewModel
    {
        public AddUserViewModel(string title) : base(title)
        {
            if (CommonResource.GetValue("User") == null)
            {

                MessageBox.Show("未登录所以不能打开该页面");
                CloseThis();
                return;
            }
            CreateManagerSelectListByUserType();
            MinUser= GetMinSelectUser();
            UserSubType = ZhiBoUtils.GetUserSubTypeByUserType(MinUser.UserType);
            var user = JsonUtils.Deserialize<User>(CommonResource.GetValue("User"));
        }

       

        public AddUserViewModel(BaseScene scene, String json) : base(scene, json)
        {

        }
        //DivisionTypeList  AnchorProportionList
        /// <summary>
        /// 艺人类型
        /// </summary>
        private Dictionary<int, string> _DivisionTypeList = new Dictionary<int, string>() { {0, "普通艺人" },{1, "金牌艺人" } };

        public Dictionary<int, string> DivisionTypeList
        {
            get { return _DivisionTypeList; }
            set { _DivisionTypeList = value; RaisePropertyChanged("DivisionTypeList"); }
        }

        /// <summary>
        /// 分成方式
        /// </summary>
        private Dictionary<int, string> _AnchorProportionList = new Dictionary<int, string>() { { 0, "对私分成" }, { 1, "对公分成" } };

        public Dictionary<int, string> AnchorProportionList
        {
            get { return _AnchorProportionList; }
            set { _AnchorProportionList = value; RaisePropertyChanged("AnchorProportionList"); }
        }

        /// <summary>
        /// 运营经理列表
        /// </summary>
        private Dictionary<long, string> _OperationManagerList = new Dictionary<long, string>();

        public Dictionary<long, string> OperationManagerList
        {
            get { return _OperationManagerList; }
            set { _OperationManagerList = value; RaisePropertyChanged("OperationManagerList"); }
        }

        /// <summary>
        /// 星探组长列表
        /// </summary>
        private Dictionary<long, string> _StarManagerList = new Dictionary<long, string>();

        public Dictionary<long, string> StarManagerList
        {
            get { return _StarManagerList; }
            set { _StarManagerList = value; RaisePropertyChanged("StarManagerList"); }
        }

        /// <summary>
        /// 星探列表
        /// </summary>
        private Dictionary<long, string> _StarList = new Dictionary<long, string>();

        public Dictionary<long, string> StarList
        {
            get { return _StarList; }
            set
            {
                _StarList = value;
                RaisePropertyChanged("StarList");
            }
        }

        /// <summary>
        ///  选择的运营经理
        /// </summary>
        private long _OperationManager = 0;
        public long OperationManager
        {
            get { return _OperationManager; }
            set
            {
                _OperationManager = value;
                var response = ZhiBoUtils.GetSubUsersNew(value);
                if (response.Code == 0)
                {
                    Dictionary<long, string> dic = new Dictionary<long, string>();
                    foreach (var v in response.Data.UserInfos)
                    {
                        dic.Add(v.UserInfoUserId, v.UserInfoUserName);
                    }
                    StarManagerList = dic;
                    StarManager = 0;
                    MinUser = GetMinSelectUser();
                    UserSubType = ZhiBoUtils.GetUserSubTypeByUserType(MinUser.UserType);
                }
                RaisePropertyChanged("OperationManager");
            }
        }
        /// <summary>
        /// 选择的星探组长
        /// </summary>
        private long _StarManager = 0;
        public long StarManager
        {
            get { return _StarManager; }
            set
            {
                _StarManager = value;
                var response = ZhiBoUtils.GetSubUsersNew(value);
                if (response.Code == 0)
                {
                    Dictionary<long, string> dic = new Dictionary<long, string>();
                    foreach (var v in response.Data.UserInfos)
                    {
                        dic.Add(v.UserInfoUserId, v.UserInfoUserName);
                    }
                    StarList = dic;
                    Star = 0;
                    MinUser = GetMinSelectUser();
                    UserSubType = ZhiBoUtils.GetUserSubTypeByUserType(MinUser.UserType);
                }

                RaisePropertyChanged("StarManager");
            }
        }

        /// <summary>
        /// 选择的星探
        /// </summary>
        private long _Star = 0;
        public long Star
        {
            get { return _Star; }
            set
            {
                _Star = value;
                MinUser = GetMinSelectUser();
                UserSubType = ZhiBoUtils.GetUserSubTypeByUserType(MinUser.UserType);
                
                RaisePropertyChanged("Star");
            }
        }

        /// <summary>
        /// 修小选择的user
        /// </summary>
        private User _MinUser = new User ();
        public User MinUser
        {
            get { return _MinUser; }
            set
            {
                _MinUser = value;
                RaisePropertyChanged("MinUser");
            }
        }

        /// <summary>
        /// 用户子类型说明
        /// </summary>
        private UserTypeSub _UserSubType = new UserTypeSub();
        public UserTypeSub UserSubType
        {
            get { return _UserSubType; }
            set
            {
                _UserSubType = value;
                if (value.Id== ZhiBoUtils.Get主播Id())
                {
                    IsAnchor = true;
                }
                else
                {
                    IsAnchor = false;
                }
                RaisePropertyChanged("UserSubType");
            }
        }

        /// <summary>
        /// 输入的用户信息
        /// </summary>
        private User _InputUser = new User() ;
        public User InputUser
        {
            get { return _InputUser; }
            set
            {
                _InputUser = value;
                
                RaisePropertyChanged("InputUser");
            }
        }


        /// <summary>
        /// 输入的艺人名称
        /// </summary>
        private Anchor _InputAnchor = new Anchor();

        public Anchor InputAnchor
        {
            get { return _InputAnchor; }
            set
            {
                _InputAnchor = value;

                RaisePropertyChanged("InputAnchor");
            }
        }

        /// <summary>
        /// 表示是否是艺人
        /// </summary>
        private bool _IsAnchor = false;
        public bool IsAnchor
        {
            get { return _IsAnchor; }
            set
            {
                _IsAnchor = value;

                RaisePropertyChanged("IsAnchor");
            }
        }

        RelayCommand _ClearAllSelectCommand = null;
        public ICommand ClearAllSelectCommand
        {
            get
            {
                if (_ClearAllSelectCommand == null)
                {
                    _ClearAllSelectCommand = new RelayCommand((p) =>
                    {
                        ClearSelect();
                    }, (p) => true);
                }

                return _ClearAllSelectCommand;
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


        //CreateUserCommand
        RelayCommand _CreateUserCommand = null;
        public ICommand CreateUserCommand
        {
            get
            {
                if (_CreateUserCommand == null)
                {
                    _CreateUserCommand = new RelayCommand((p) =>
                    {
                        if (UserSubType.Id != 999)
                        {
                            var response = ZhiBoUtils.CreateUser(new Scene.Request.CreateUserRequest()
                            {
                                Extend = InputUser.Extend,
                                PassWord = InputUser.PassWord,
                                UserName = InputUser.UserName,
                                UserType = UserSubType.Id,
                                FatherId = MinUser.UserId,
                            });
                            if (response.Code != 0)
                            {
                                MessageBox.Show("用户添加失败，可能是用户名重复了");
                            }
                            else
                            {
                                MessageBox.Show("添加成功");

                            }
                        }
                        else
                        {
                            //MessageBox.Show($"{InputAnchor.ToString()}");
                            var response=ZhiBoUtils.CreateAnchor(InputAnchor, new User() {
                                Extend = InputUser.Extend,
                                PassWord = InputUser.PassWord,
                                UserName = InputUser.UserName,
                                UserType = UserSubType.Id,
                                FatherId = MinUser.UserId,
                            });
                            if (response.Code != 0)
                            {
                                MessageBox.Show("用户添加失败，可能是用户名重复了");
                            }
                            else
                            {
                                MessageBox.Show("添加成功");

                            }
                        }
                        //ClearSelect();
                        ClearInput();
                    }, (p) => true);
                }

                return _CreateUserCommand;
            }
        }
        public new static string GetDesc()
        {
            return "添加一个账号，根据您登陆的方式进行相关的添加";
        }

        public new static string GetName()
        {
            return "添加账号";
        }

        /// <summary>
        /// 文档类，必须生成一个新的Guid不可直接使用
        /// </summary>
        /// <returns></returns>
        public new static string TemplateGuid()
        {
            return "7fd98e25-bd0f-4f48-84d2-a7be82f32ebb";
        }


        private void CreateManagerSelectListByUserType()
        {
            var user = JsonUtils.Deserialize<User>(CommonResource.GetValue("User"));
            var response = ZhiBoUtils.GetSubUsersNew(user.UserId);
            if (response.Code == 0)
            {
                foreach (var v in response.Data.UserInfos)
                {
                    if (user.UserType == 0)
                    {
                        OperationManagerList.Add(v.UserInfoUserId, v.UserInfoUserName);
                    }
                    if (user.UserType == 1)
                    {
                        StarManagerList.Add(v.UserInfoUserId, v.UserInfoUserName);
                    }
                    if (user.UserType == 2)
                    {
                        StarList.Add(v.UserInfoUserId, v.UserInfoUserName);
                    }

                }
            }
        }

        private void CloseThis()
        {
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            MessageDeliverGroup.Delivery(MessageType.CloseTempleteEvemt, javaScriptSerializer.Serialize(new CloseTempleteRequest() { TempleteGuid = this.StrGuid }));
            MessageSubscribeRelations.DeleteSubscribe(MessageType.WSDEDataEvent, this.StrGuid);
        }

        /// <summary>
        /// 获取最小的选择节点
        /// </summary>
        /// <returns></returns>
        private User GetMinSelectUser()
        {
            var user = JsonUtils.Deserialize<User>(CommonResource.GetValue("User"));
            if (Star != 0)
            {
                user=ZhiBoUtils.GetUserByUserId(Star).Data.User;
            }
            else if (StarManager != 0)
            {
                user = ZhiBoUtils.GetUserByUserId(StarManager).Data.User;
            }
            else if(OperationManager != 0)
            {
                user = ZhiBoUtils.GetUserByUserId(OperationManager).Data.User;
            }
            return user;
        }

        private void ClearSelect()
        {
            OperationManagerList.Clear();
            StarManagerList.Clear();
            StarList.Clear();
            Star = 0;
            StarManager = 0;
            OperationManager = 0;
            CreateManagerSelectListByUserType();
            MinUser = GetMinSelectUser();
            UserSubType = ZhiBoUtils.GetUserSubTypeByUserType(MinUser.UserType);
        }

        private void ClearInput()
        {
            InputUser = new User();
            InputAnchor = new Anchor();
        }
    }
}
