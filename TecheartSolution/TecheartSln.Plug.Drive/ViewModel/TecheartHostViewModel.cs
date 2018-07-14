using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Input;
using TecheartSln.Core.Command;
using TecheartSln.Core.Message.DeliverProvider;
using TecheartSln.Core.Message.MessageTypeProvider;
using TecheartSln.Core.Message.RelationProvider;
using TecheartSln.Core.ViewModel.Base;
using TecheartVote;
using TecheartVote.UsbManager;

namespace TecheartSln.Plug.Drive.ViewModel
{
    /// <summary>
    /// 投票器状态监控类
    /// </summary>
    public class TecheartHostViewModel : ToolViewModel
    {
        public const string ToolContentId = "TecheartHostTool";
        private readonly String GuidDownloadAnswer = Guid.NewGuid().ToString();
        private readonly object objLockDataCome = new object();
        WsdePort post = null;
        public TecheartHostViewModel() : base("投票器主机状态监视窗口")
        {
            ContentId = ToolContentId;
            WsdeUsbManager manager = new WsdeUsbManager();
            manager.OnWsdeUsbComed += new WsdeUsbManager.OnWsdeUsbHandler(OnWsdeUsbComed);
            manager.OnWsdeUsbExited += new WsdeUsbManager.OnWsdeUsbHandler(OnWsdeUsbExitHandler);
            MessageSubscribeRelations.AddSubscribe(MessageType.DownloadAnswerToWSDEHost, new Relation()
            {
                IsKeep = true,
                IsActive = true,
                CanUninstall = true,
                RelationDescribe = "投票器主机下载答案监听",
                RelationGuid = GuidDownloadAnswer,
                RelationAction = (messagedata) =>
                {
                    lock (objLockDataCome)
                    {
                        JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                        var response = javaScriptSerializer.Deserialize<WSDEDownloadAnswerRequest>(messagedata.MessageData);
                        if (response.answerRequests != null)
                        {
                            response.answerRequests.ToList().ForEach(k => { post.subAnswerDic.SetAnswer(k.ProblemId, k.ProblemAnswer); });
                            post.PushAnswer();
                        }
                    }

                }
            });
        }

        public void OnWsdeUsbExitHandler(WsdePort wsdePort)
        {
            HostIsRegister = false;
            HostName = String.Empty;
        }
        public void OnWsdeUsbComed(WsdePort wsdePort)
        {
            HostIsRegister = true;
            HostName = wsdePort.wsdeName;
            post = wsdePort;
            post.OnDataCome += new WsdePort.OnDataComeHandler(OnDateComeHandler2);
            Thread.Sleep(1000);
            OnSetCipherList();
            Thread.Sleep(1000);
            OnSetBaseConfig();
            Thread.Sleep(1000);
            OnSetDynamicConfig();

            MessageBox.Show("系统已配置成功");
        }

        private void OnDateComeHandler2(WsdePort handshake, SubSelect subselect)
        {
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            MessageDeliverGroup.Delivery(MessageType.WSDEDataEvent, javaScriptSerializer.Serialize(new WSDESubVoterSelectRequest() { SubVoterNumber = subselect.address.ToString(), SubVoterResult = subselect.selectData, SubVoterSelectNumber = subselect.subjectNumber.ToString() }));
        }

        #region 属性

        #region 说明
        /// <summary>
        /// 密码表说明
        /// </summary>
        public String DescriptionCipherList
        {
            get
            {

                return "密码表详细说明" + Environment.NewLine
                    + "表示自己连入到现有系统所需要的密码" + Environment.NewLine
                    + "只要是其中之一即可" + Environment.NewLine
                    + "输入规则" + Environment.NewLine
                    + "一行一个密码，密码范围是1到9223372036854775806";
            }
        }
        public String DescriptionBaseConfig
        {
            get
            {
                return "基础信息设置说明" + Environment.NewLine
                    + "基础信息设置主要设置的是主机的基本配置，" + Environment.NewLine
                    + "该配置一般在初始化时配置一次之后就不在配置";
            }
        }
        #endregion


        /// <summary>
        /// 主机是否已被插入
        /// </summary>
        private bool _hostIsRegister = false;

        public bool HostIsRegister
        {
            get { return _hostIsRegister; }
            private set
            {
                _hostIsRegister = value;
                RaisePropertyChanged("HostIsRegister");
            }
        }

        /// <summary>
        /// 主机名称
        /// </summary>
        private String _hostName;
        public String HostName
        {
            get
            {
                if (String.IsNullOrWhiteSpace(_hostName))
                {
                    return "主机没有插入主机";
                }
                return _hostName;
            }
            private set
            {
                _hostName = value;
                RaisePropertyChanged("HostName");
            }
        }

        /// <summary>
        /// 密码表
        /// </summary>
        private IList<String> _cipherList = new List<String>() { "1","2","3","4"};
        public String CipherListText
        {
            get { return String.Join(Environment.NewLine, _cipherList); }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    _cipherList = new List<string>();
                }
                else
                {
                    _cipherList = value.Split(new String[] { Environment.NewLine }, StringSplitOptions.None).ToList();
                    RaisePropertyChanged("CipherListText");
                }
            }
        }

        private Dictionary<int, string> _selChannelList = new Dictionary<int, string>()
        {
            {1,"1" },
            {2,"2" },
            {3,"3" },
            {4,"4" },
            {5,"5" },
            {6,"6" },
            {7,"7" },
            {8,"8" },
            {9,"9" },
            {10,"10" },
        };
        public Dictionary<int, string> SelChannelList
        {
            get { return _selChannelList; }
        }
        private int _channel=1;
        public int Channel
        {
            get { return _channel; }
            set
            {
                _channel = value;
                RaisePropertyChanged("Channel");
            }
        }

        private Dictionary<int, string> _selFrequencyList = new Dictionary<int, string>() { { 1, "dBMNegative6" }, { 2, "dBMNegative4" }, { 3, "dBM0" }, { 4, "dBM1" }, { 5, "dBM3" }, { 6, "dBM4" }, { 7, "dBM7" }, { 0, "dBMNegative12" }, };
        public Dictionary<int, string> SelFrequencyList
        {
            get { return _selFrequencyList; }
        }
        private int _frequency;
        public int Frequency
        {
            get { return _frequency; }
            set
            {
                _frequency = value;
                RaisePropertyChanged("Frequency");
            }
        }

        private ObservableCollection<DynamicConfigBox> _dynamicConfigList = new ObservableCollection<DynamicConfigBox>
        {
            new DynamicConfigBox (){ IsSelect=true, ShareId="persistenceConfiguration", ShareName="固化主机配置" , TrueAction=(post)=>post.shareAction1P.persistenceConfiguration=true, FalseAction=(post)=>post.shareAction1P.persistenceConfiguration=false },
            new DynamicConfigBox (){ IsSelect=true, ShareId="clientCanClearSoon", ShareName="子机是否可以快速清除" , TrueAction=(post)=>post.shareAction1P.clientCanClearSoon=true ,FalseAction=(post)=>post.shareAction1P.clientCanClearSoon=false},
            new DynamicConfigBox (){ IsSelect=true, ShareId="clientCanAnswer", ShareName="子机是否可以答题" , TrueAction=(post)=>post.shareAction1P.clientCanAnswer=true ,FalseAction=(post)=>post.shareAction1P.clientCanAnswer=false},
            new DynamicConfigBox (){ IsSelect=true, ShareId="eraseClientMemory", ShareName="终端擦除内存" , TrueAction=(post)=>post.shareAction1P.eraseClientMemory=true ,FalseAction=(post)=>post.shareAction1P.eraseClientMemory=false},
            new DynamicConfigBox (){ IsSelect=true, ShareId="clientCanSeeSolution", ShareName="终端是否可以查看答案" , TrueAction=(post)=>post.shareAction1P.clientCanSeeSolution=true ,FalseAction=(post)=>post.shareAction1P.clientCanSeeSolution=false},

            new DynamicConfigBox (){ IsSelect=true, ShareId="clientCanWriteNumber" , ShareName="客户端是否允许输入数字", TrueAction=(post)=>post.shareAction2P.clientCanWriteNumber=true ,FalseAction=(post)=>post.shareAction2P.clientCanWriteNumber=false },
            new DynamicConfigBox (){ IsSelect=true, ShareId="clientCanWriteABC" , ShareName="是否允许输入英语字母", TrueAction=(post)=>post.shareAction2P.clientCanWriteABC=true ,FalseAction=(post)=>post.shareAction2P.clientCanWriteABC=false },
            new DynamicConfigBox (){ IsSelect=true, ShareId="clientCanChangeChannel" , ShareName="是否允许客户端修改信道", TrueAction=(post)=>post.shareAction2P.clientCanChangeChannel=true ,FalseAction=(post)=>post.shareAction2P.clientCanChangeChannel=false },
            new DynamicConfigBox (){ IsSelect=true, ShareId="clientCanErase" , ShareName="是否允许擦除内存数据", TrueAction=(post)=>post.shareAction2P.clientCanErase=true ,FalseAction=(post)=>post.shareAction2P.clientCanErase=false },
            new DynamicConfigBox (){ IsSelect=true, ShareId="clientCanChangeDate" , ShareName="是否能够修改时间", TrueAction=(post)=>{post.shareAction2P.clientCanChangeDate=true; post.shareAction2P.clientCanCnahgeTime=true; } ,FalseAction=(post)=>{post.shareAction2P.clientCanChangeDate=false; post.shareAction2P.clientCanCnahgeTime=false; } },
            new DynamicConfigBox (){ IsSelect=true, ShareId="clinetCanSeeAnswer" , ShareName="客户端是否可以查看答案", TrueAction=(post)=>post.shareAction2P.clinetCanSeeAnswer=true ,FalseAction=(post)=>post.shareAction2P.clinetCanSeeAnswer=false },
        };
        public ObservableCollection<DynamicConfigBox> DynamicConfigList
        {
            get { return _dynamicConfigList; }
            set
            {
                _dynamicConfigList = value;
                RaisePropertyChanged("DynamicConfigList");
            }
        }

        #endregion
        #region 命令
        RelayCommand _setDefaultCommand = null;
        public ICommand SetDefaultCommand
        {
            get
            {
                if (_setDefaultCommand == null)
                {
                    _setDefaultCommand = new RelayCommand(p => { OnSetCipherList(); Thread.Sleep(1000); OnSetBaseConfig(); Thread.Sleep(1000); OnSetDynamicConfig(); }, p => true);
                }
                return _setDefaultCommand;
            }
        }

        RelayCommand _setDynamicConfigCommand = null;
        public ICommand SetDynamicConfigCommand
        {
            get
            {
                if (_setDynamicConfigCommand == null)
                {
                    _setDynamicConfigCommand = new RelayCommand(p => OnSetDynamicConfig(), p => true);
                }
                return _setDynamicConfigCommand;
            }
        }

        RelayCommand _setCipherListCommand = null;
        public ICommand SetCipherListCommand
        {
            get
            {
                if (_setCipherListCommand == null)
                {
                    _setCipherListCommand = new RelayCommand(p => { OnSetCipherList(); }, p => true);
                }
                return _setCipherListCommand;
            }
        }

        RelayCommand _setBaseConfigfCommand = null;
        public ICommand SetBaseConfigfCommand
        {
            get
            {
                if (_setBaseConfigfCommand == null)
                {
                    _setBaseConfigfCommand = new RelayCommand(p => { OnSetBaseConfig(); }, p => true);
                }
                return _setBaseConfigfCommand;
            }
        }

        RelayCommand _testCreateHostCommand = null;
        public ICommand TestCreateHostCommand
        {
            get
            {
                if (_testCreateHostCommand == null)
                {
                    _testCreateHostCommand = new RelayCommand(p => { TestCreateHost(); }, p => true);

                }
                return _testCreateHostCommand;
            }
        }

        RelayCommand _testCreateSubDataComeCommand = null;
        public ICommand TestCreateSubDataComeCommand
        {
            get
            {
                if (_testCreateSubDataComeCommand == null)
                {
                    _testCreateSubDataComeCommand = new RelayCommand(p => { TestCreateSubDataCome(); }, p => true);

                }
                return _testCreateSubDataComeCommand;
            }
        }

        RelayCommand _testDeleteHostCommand = null;
        public ICommand TestDeleteHostCommand
        {
            get
            {
                if (_testDeleteHostCommand == null)
                {
                    _testDeleteHostCommand = new RelayCommand(p => { OnWsdeUsbExitHandler(new WsdePort("COM3", true)); }, p => true);
                }
                return _testDeleteHostCommand;
            }
        }



        #endregion

        #region 命令方法

        public void OnSetDynamicConfig()
        {
            foreach (var v in DynamicConfigList)
            {
                if (v.IsSelect)
                {
                    v.TrueAction(post);
                }
                else
                {
                    v.FalseAction(post);
                }
            }
            post.UpdateDynamicConf();
        }
        public void TestCreateHost()
        {
            OnWsdeUsbComed(new WsdePort("COM3", true));
            post.Handshake();
            post.handshakeRespone = new TecheartVote.Response.HandshakeResponse() { Address = 1, Channel = 1, Remark = "测试主机", SecretKey = 520 };
            post.channel = 1;
        }

        public void TestCreateSubDataCome()
        {
            Random random = new Random();
            OnDateComeHandler2(new WsdePort("COM3", true) { }, new SubSelect() { address = Userid, selectData = QueSelect, subjectNumber = Queid });
        }

        public void OnSetCipherList()
        {
            post.SetAccessPasswords(_cipherList.Where(k => !String.IsNullOrWhiteSpace(k)).ToList().ConvertAll(k => Convert.ToUInt64(k)));
        }

        public void OnSetBaseConfig()
        {
            post.InitConf(new ConfAction() { channel = Channel, frequency = (FrequencyEnum)Frequency });
        }
        #endregion


        private int _userid;
        public int Userid
        {
            get { return _userid; }
            set
            {
                _userid = value;
                RaisePropertyChanged("Userid");
            }
        }

        private int _queid;
        public int Queid
        {
            get { return _queid; }
            set
            {
                _queid = value;
                RaisePropertyChanged("Queid");
            }
        }

        private String _queSelect;
        public String QueSelect
        {
            get { return _queSelect; }
            set
            {
                _queSelect = value;
                RaisePropertyChanged("QueSelect");
            }
        }
    }

    public class DynamicConfigBox
    {
        /// <summary>
        /// 标志位id
        /// </summary>
        public String ShareId { get; set; }

        /// <summary>
        /// 标志位名称
        /// </summary>
        public String ShareName { get; set; }

        /// <summary>
        /// 是否是被选中的状态
        /// </summary>
        public Boolean IsSelect { get; set; }

        /// <summary>
        /// 选中需要执行的动作
        /// </summary>
        public Action<WsdePort> TrueAction { get; set; }

        /// <summary>
        /// 没有选中的情况下应该执行的操作
        /// </summary>
        public Action<WsdePort> FalseAction { get; set; }
    }
}
