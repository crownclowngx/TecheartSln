using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Input;
using TecheartSln.Core.Command;
using TecheartSln.Core.Message.DeliverProvider;
using TecheartSln.Core.Message.MessageTypeProvider;
using TecheartSln.Core.Message.RelationProvider;
using TecheartSln.Core.Scene;
using TecheartSln.Core.ViewModel.Base;

namespace TecheartSln.Plug.Classroom.ViewModel
{
    public class SimpleExaminationViewModel : TemplateBaseViewModel
    {
        public SimpleExaminationViewModel(string title) : base(title)
        {
        }

        public SimpleExaminationViewModel(BaseScene scene, String json) : base(scene, json)
        {
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            
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

        private void OnClose()
        {
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            MessageDeliverGroup.Delivery(MessageType.CloseTempleteEvemt, javaScriptSerializer.Serialize(new CloseTempleteRequest() { TempleteGuid = this.StrGuid }));
            MessageSubscribeRelations.DeleteSubscribe(MessageType.WSDEDataEvent, this.StrGuid);
        }

        public override ICommand SaveCommand => throw new NotImplementedException();
    }
}
