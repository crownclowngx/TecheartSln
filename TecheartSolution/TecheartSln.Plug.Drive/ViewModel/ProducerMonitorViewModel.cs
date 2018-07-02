using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Input;
using TecheartSln.Core.Command;
using TecheartSln.Core.Message.MessageTypeProvider;
using TecheartSln.Core.Message.RelationProvider;
using TecheartSln.Core.ViewModel.Base;

namespace TecheartSln.Plug.Drive.ViewModel
{
    public class ProducerMonitorViewModel : ToolViewModel
    {
        public const string ToolContentId = "ProducerMonitorTool";
        private readonly String GuidWSDEDataComeEvent = Guid.NewGuid().ToString();
        private readonly object objLockDataCome = new object();
        public ProducerMonitorViewModel() : base("生产者数据监控")
        {
            ContentId = ToolContentId;
            MessageSubscribeRelations.AddSubscribe(MessageType.All, new Relation()
            {

                CanUninstall = false,
                IsActive = true,
                IsKeep = true,
                RelationDescribe = "数据监视器WSDE类消息监听",
                RelationGuid = GuidWSDEDataComeEvent,
                RelationAction = messageData =>
                {
                    base.BindingPropInOtherTask(() =>
                    {
                        JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                        //var response = javaScriptSerializer.Deserialize<WSDESubVoterSelectRequest>(messageData.MessageData);
                        if (DataList.Count() > _maxKeep)
                        {
                            for (int i = 0; i < DataList.Count() - _maxKeep; i++)
                            {
                                DataList.RemoveAt(0);
                            }
                        }
                        DataList.Add(messageData);
                    });
                },
            });
        }


        private ObservableCollection<MessageDataBase> _dataList = new ObservableCollection<MessageDataBase>();
        public ObservableCollection<MessageDataBase> DataList
        {
            get { return _dataList; }
            set
            {
                _dataList = value;
                RaisePropertyChanged("DataList");
            }
        }

        private int _maxKeep = 1000;
        public int MaxKeep
        {
            get { return _maxKeep; }
            set
            {
                _maxKeep = value;
                RaisePropertyChanged("MaxKeep");
            }
        }

        private Dictionary<int, string> _selKeepCountList = new Dictionary<int, string>() { { 100, "Keep100" }, { 1000, "Keep1000" }, { 5000, "Keep5000" }, { 10000, "Keep10000" }, { Int32.MaxValue, "KeepAll" }, { 10, "Keep10" } };
        public Dictionary<int, string> SelKeepCountList
        {
            get { return _selKeepCountList; }
        }


        private RelayCommand _maxKeepChangeCommand = null;
        public ICommand MaxKeepChangeCommand
        {
            get
            {
                if (_maxKeepChangeCommand == null)
                {
                    _maxKeepChangeCommand = new RelayCommand(p => { ChangeKeepSize(); }, p => true);
                }
                return _maxKeepChangeCommand;
            }

        }

        void ChangeKeepSize()
        {

        }

    }
}
