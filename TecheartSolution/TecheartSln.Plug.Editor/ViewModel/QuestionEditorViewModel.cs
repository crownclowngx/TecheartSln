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

namespace TecheartSln.Plug.Editor.ViewModel
{
    public class QuestionEditorViewModel : TemplateBaseViewModel
    {
        public QuestionEditorViewModel(string title) : base(title)
        {
        }

        public QuestionEditorViewModel(BaseScene scene, String json):base(scene,json)
        {
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            //var sc = javaScriptSerializer.Deserialize<TemplateScene>(json);
            //if (sc != default(TemplateScene))
            //{
            //
            //}
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

        public override ICommand SaveCommand => throw new NotImplementedException();

        private void OnClose()
        {
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            MessageDeliverGroup.Delivery(MessageType.CloseTempleteEvemt, javaScriptSerializer.Serialize(new CloseTempleteRequest() { TempleteGuid = this.StrGuid }));
            MessageSubscribeRelations.DeleteSubscribe(MessageType.WSDEDataEvent, this.StrGuid);
        }

        public new static string GetDesc()
        {
            return "一个简单的题目编辑器，可以设置题号，题目选项个数，题目正确答案(支持多选)，并最终支持保存成一个文档，以被其他程序使用。";
        }

        public new static string GetName()
        {
            return "简易问题编辑器";
        }

        /// <summary>
        /// 文档类，必须生成一个新的Guid不可直接使用
        /// </summary>
        /// <returns></returns>
        public new static string TemplateGuid()
        {
            return "6b1561af-8a4b-4e54-8d78-857c057779ba";
        }
    }
}
