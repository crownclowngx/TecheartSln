using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
using TecheartSln.Core.Message.MessageTypeProvider;
using TecheartSln.Core.Message.RelationProvider;
using TecheartSln.Core.View.Pane;
using TecheartSln.Core.ViewModel.Base;

namespace TecheartSln.Main
{
    class Workspace : ViewModelBase
    {
        PanesTemplateSelectorDynamic select = new PanesTemplateSelectorDynamic();
        PanesStyleSelectorDynamic selectStyle = new PanesStyleSelectorDynamic();
        TecheartSln.Core.Init init = new Core.Init();
        InitResponse plugInit = new InitResponse();
        static Workspace _this = new Workspace();
        public static Workspace This
        {
            get { return _this; }
        }


        ObservableCollection<TemplateBaseViewModel> _files = null;
        ReadOnlyObservableCollection<TemplateBaseViewModel> _readonyFiles = null;
        public ReadOnlyObservableCollection<TemplateBaseViewModel> Files
        {
            get
            {
                if (_readonyFiles == null)
                    _readonyFiles = new ReadOnlyObservableCollection<TemplateBaseViewModel>(_files);

                return _readonyFiles;
            }
        }


        ToolViewModel[] _tools = null;

        public IEnumerable<ToolViewModel> Tools
        {
            get
            {
                if (_tools == null)
                    _tools = plugInit.ToolViewModels.ToArray();
                return _tools;
            }
        }

        public void InitCommandBinding(Window win)
        {
            var mainwin = win as MainWindow;
            if (mainwin != null)
            {

                RegisterDataTemplates(select);
                mainwin.dockManager.LayoutItemTemplateSelector = select;

                RedisterStyles(selectStyle);
                mainwin.dockManager.LayoutItemContainerStyleSelector = selectStyle;
            }
        }

        protected Workspace()
        {
            _files = new ObservableCollection<TemplateBaseViewModel>();
            MessageSubscribeRelations.AddSubscribe(MessageType.CloseTempleteEvemt, new Relation()
            {
                CanUninstall = false,
                IsActive = true,
                IsKeep = true,
                RelationDescribe = "监听模板类窗口关闭事件",
                RelationGuid = Guid.NewGuid().ToString(),
                RelationAction = messageData =>
                {
                    base.BindingPropInOtherTask(() =>
                    {
                        JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                        var response = javaScriptSerializer.Deserialize<CloseTempleteRequest>(messageData.MessageData);
                        Close(response.TempleteGuid);
                    });
                },
            });
            plugInit = PluginTools.Init();
            
        }

        void Close(String StrGuid)
        {
            if (_files.Any(k => k.StrGuid == StrGuid))
            {
                _files.Remove(_files.FirstOrDefault(k => k.StrGuid == StrGuid));
            }
        }



        private void RedisterStyles(PanesStyleSelectorDynamic selectPanesStyle)
        {
           
            init.RedisterStyles(selectPanesStyle);
            foreach(var v in plugInit.Inits)
            {
                v.RedisterStyles(selectPanesStyle);
            }
        }

        private void RegisterDataTemplates(PanesTemplateSelectorDynamic paneSel)
        {
            init.RegisterDataTemplates(paneSel);
            foreach(var v in plugInit.Inits)
            {
                v.RegisterDataTemplates(paneSel);
            }
        }
    }
}
