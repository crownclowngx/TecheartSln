using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using TecheartSln.Core.Message.MessageTypeProvider;
using TecheartSln.Core.Message.RelationProvider;
using TecheartSln.Core.ViewModel.Base;

namespace TecheartSln.Plug.Drive.ViewModel
{
    public class RelationListViewModel : ToolViewModel
    {
        public const string ToolContentId = "RelationListTool";
        private readonly String GuidRelationChange = Guid.NewGuid().ToString();
        private readonly object objLockDataCome = new object();
        public RelationListViewModel() : base("关系维护列表")
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
                        DynamicrelationList.Clear();
                        JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                        var relations = MessageSubscribeRelations.GetAllCurrentRelation();
                        foreach (var v in relations)
                        {
                            v.Relations.ToList().ForEach(k =>
                            {
                                DynamicrelationList.Add(k);
                            });
                        }
                    });
                }
            });
        }

        #region 属性
        private ObservableCollection<Relation> _dynamicrelationList = new ObservableCollection<Relation>();
        public ObservableCollection<Relation> DynamicrelationList
        {
            get { return _dynamicrelationList; }
            set
            {
                _dynamicrelationList = value;
                RaisePropertyChanged("DynamicrelationList");
            }
        }
        #endregion

        #region 命令
        #endregion

        #region 命令方法
        #endregion
    }
}
