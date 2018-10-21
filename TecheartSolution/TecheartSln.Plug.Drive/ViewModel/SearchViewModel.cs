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
using TecheartSln.Plug.Drive.Scene.Response;

namespace TecheartSln.Plug.Drive.ViewModel
{
    public class SearchViewModel : TemplateBaseViewModel
    {
        public SearchViewModel(string title) : base(title)
        {
            if (CommonResource.GetValue("User") == null)
            {
                RefusePeople = false;
                MessageBox.Show("未登录所以不能打开该页面");
                CloseThis();
                return;
            }
            
            var user = JsonUtils.Deserialize<User>(CommonResource.GetValue("User"));
            var responsestr = HttpUtils.PostJson("http://39.107.99.199:30000/api/User/GetSubUsersNew", new { UserIds = new List<long>() { user.UserId }, PageNumber=1, PageCount=10000, });
            var response = JsonUtils.Deserialize<BaseResponse<GetUsersNewResponse>>(responsestr);
            if (response.Code == 0)
            {
                foreach (var v  in response.Data.UserInfos)
                {
                    if (user.UserType == 0)
                    {
                        _OperationManagerList.Add(v.UserInfoUserId, v.UserInfoUserName);
                    }
                    if (user.UserType == 1)
                    {
                        _StarManagerList.Add(v.UserInfoUserId, v.UserInfoUserName);
                    }
                    if (user.UserType == 2)
                    {
                        _StarList.Add(v.UserInfoUserId, v.UserInfoUserName);
                    }

                }
            }
        }

        public SearchViewModel(BaseScene scene, String json) : base(scene, json)
        {

        }
        /// <summary>
        /// 是否已经登陆
        /// </summary>
        private bool _refusePeople = true;
        public bool RefusePeople
        {
            get { return _refusePeople; }
            set
            {
                _refusePeople = value;
                RaisePropertyChanged("RefusePeople");
            }
        }


        private String _yyNumber = "";

        public String YYNumber
        {
            get { return _yyNumber; }
            set
            {
                _yyNumber = value;
                RaisePropertyChanged("YYNumber");
            }
        }

        private String _realName = "";

        public String RealName
        {
            get { return _realName; }
            set
            {
                
                _realName = value;
                RaisePropertyChanged("RealName");
            }
        }


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

        private Dictionary<long, string> _OperationManagerList = new Dictionary<long, string>();

        public Dictionary<long, string> OperationManagerList
        {
            get { return _OperationManagerList; }
            set { _OperationManagerList = value; RaisePropertyChanged("OperationManagerList"); }
        }

        private long _OperationManager = 0;
        public long OperationManager
        {
            get { return _OperationManager; }
            set
            {
                _OperationManager = value;
                var responsestr = HttpUtils.PostJson("http://39.107.99.199:30000/api/User/GetSubUsersNew", new { UserIds = new List<long>() { value }, PageNumber = 1, PageCount = 10000, });
                var response = JsonUtils.Deserialize<BaseResponse<GetUsersNewResponse>>(responsestr);
                if (response.Code == 0)
                {
                    Dictionary<long, string> dic = new Dictionary<long, string>();
                    foreach (var v in response.Data.UserInfos)
                    {
                        dic.Add(v.UserInfoUserId, v.UserInfoUserName);
                    }
                    StarManagerList = dic;
                    StarManager = 0;
                }
                RaisePropertyChanged("OperationManager");
            }
        }

        private long _StarManager = 0;
        public long StarManager
        {
            get { return _StarManager; }
            set
            {
                _StarManager = value;
                var responsestr = HttpUtils.PostJson("http://39.107.99.199:30000/api/User/GetSubUsersNew", new { UserIds = new List<long>() { value }, PageNumber = 1, PageCount = 10000, });
                var response = JsonUtils.Deserialize<BaseResponse<GetUsersNewResponse>>(responsestr);
                if (response.Code == 0)
                {
                    Dictionary<long, string> dic = new Dictionary<long, string>();
                    foreach (var v in response.Data.UserInfos)
                    {
                        dic.Add(v.UserInfoUserId, v.UserInfoUserName);
                    }
                    StarList = dic;
                    Star = 0;
                }
               
                RaisePropertyChanged("StarManager");
            }
        }

        private long _Star = 0;
        public long Star
        {
            get { return _Star; }
            set
            {
                _Star = value;

                RaisePropertyChanged("Star");
            }
        }

        private Dictionary<long, string> _StarManagerList = new Dictionary<long, string>();

        public Dictionary<long, string> StarManagerList
        {
            get { return _StarManagerList; }
            set { _StarManagerList = value; RaisePropertyChanged("StarManagerList"); }
        }

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

        private void CloseThis()
        {
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            MessageDeliverGroup.Delivery(MessageType.CloseTempleteEvemt, javaScriptSerializer.Serialize(new CloseTempleteRequest() { TempleteGuid = this.StrGuid }));
            MessageSubscribeRelations.DeleteSubscribe(MessageType.WSDEDataEvent, this.StrGuid);
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


        RelayCommand _searchCommand = null;
        public ICommand SearchCommand
        {
            get
            {
                if (_searchCommand == null)
                {
                    _searchCommand = new RelayCommand((p) => 
                    {
                        if (CommonResource.GetValue("User") == null)
                        {
                            MessageBox.Show("未登陆");
                            return;
                        }
                        var enable = true;
                        if(String.IsNullOrEmpty(RealName) && string.IsNullOrEmpty(YYNumber))
                        {
                            enable = false;
                        }
                        var user=JsonUtils.Deserialize<User>(CommonResource.GetValue("User"));
                        var responsestr=HttpUtils.PostJson("http://39.107.99.199:30000/api/User/GetAnchorByFilter", new { UserIds =new List<long>() { user.UserId}, Filter=new { Enable= enable, RealName= RealName, YYNumber= YYNumber } });
                        var response=JsonUtils.Deserialize<BaseResponse<GetUsersNewResponse>>(responsestr);
 
                        //QuesionList = response.Data.UserInfos;
                        QuesionList.Clear();
                        if (response.Data == null || response.Data.UserInfos == null)
                        {
                            return;
                        }
                        foreach (var v in response.Data.UserInfos)
                        {
                            QuesionList.Add(v);
                        }
                    }, (p) => true);
                }

                return _searchCommand;
            }
        }


        RelayCommand _SearchArrangementCommand = null;
        public ICommand SearchArrangementCommand
        {
            get
            {
                if (_SearchArrangementCommand == null)
                {
                    _SearchArrangementCommand = new RelayCommand((p) =>
                    {
                        if (CommonResource.GetValue("User") == null)
                        {
                            MessageBox.Show("未登陆");
                            return;
                        }
                        var user = JsonUtils.Deserialize<User>(CommonResource.GetValue("User"));
                        long userid = user.UserId;
                        if (OperationManager != 0)
                        {
                            userid = OperationManager;
                        }
                        if (_StarManager != 0)
                        {
                            userid = _StarManager;
                        }
                        if (_Star != 0)
                        {
                            userid = _Star;
                        }
                        //_StarManager _Star
                        var responsestr = HttpUtils.PostJson("http://39.107.99.199:30000/api/User/GetAnchorByFilter", new { UserIds = new List<long>() { userid }, Filter = new { Enable = false } });
                        var response = JsonUtils.Deserialize<BaseResponse<GetUsersNewResponse>>(responsestr);
 
                        //QuesionList = response.Data.UserInfos;
                        QuesionList.Clear();
                        if (response.Data == null || response.Data.UserInfos == null)
                        {
                            return;
                        }
                        foreach (var v in response.Data.UserInfos)
                        {
                            QuesionList.Add(v);
                        }
                    }, (p) => true);
                }

                return _SearchArrangementCommand;
            }
        }

        public new static string GetDesc()
        {
            return "主播详细信息的查询，不同于用户管理，这里查询的是主播的详细信息";
        }

        public new static string GetName()
        {
            return "主播详细信息查询";
        }

        /// <summary>
        /// 文档类，必须生成一个新的Guid不可直接使用
        /// </summary>
        /// <returns></returns>
        public new static string TemplateGuid()
        {
            return "b4d427cd-81a5-480b-b33c-a7081551c2e7";
        }
    }
}
