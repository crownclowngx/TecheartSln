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
using TecheartSln.Core.Scene;
using TecheartSln.Core.ViewModel.Base;
using TecheartSln.Plug.Drive.Scene;

namespace TecheartSln.Plug.Drive.ViewModel
{
    public class TemplateViewModel : TemplateBaseViewModel
    {
        public TemplateViewModel(string title) : base(title)
        {
        }

        public TemplateViewModel(BaseScene scene,String json):base(scene,json)
        {
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            var sc = javaScriptSerializer.Deserialize<TemplateScene>(json);
            if (sc != default(TemplateScene))
            {
                
            }
        }

        RelayCommand _closeCommand = null;
        override public ICommand CloseCommand
        {
            get
            {
                if (_closeCommand == null)
                {
                    _closeCommand = new RelayCommand((p) => OnClose(), (p) => true);
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
                    _saveCommand = new RelayCommand((p) => OnSave(), (p) => true);
                }

                return _saveCommand;
            }
        }

        private void OnSave()
        {

            TemplateScene serial = new TemplateScene()
            {
                TextValue = "12354",
                 TypeIdentity= TemplateGuid(),
            };
            base.SaveFile(serial);
        }
        private void OnClose()
        {
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            MessageDeliverGroup.Delivery(MessageType.CloseTempleteEvemt, javaScriptSerializer.Serialize(new CloseTempleteRequest() { TempleteGuid = this.StrGuid }));
            MessageSubscribeRelations.DeleteSubscribe(MessageType.WSDEDataEvent, this.StrGuid);
        }

        public new static string GetDesc()
        {
            return "一个测试用的文档模板";
        }

        public new static string GetName()
        {
            return "TemplateViewModel";
        }

        public new static string TemplateGuid()
        {
            return "6b1561af-8a4b-4e54-8d78-857c057779ba";
        }
    }
}
