using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using TecheartSln.Plug.Editor.Domain;
using TecheartSln.Plug.Editor.Scene;
using TecheartSln.Plug.Editor.Domain.Utils;

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
            var sc = javaScriptSerializer.Deserialize<QuestionEditorScence>(json);
            if (sc != default(QuestionEditorScence))
            {
                foreach(var v in sc.QuestionList)
                {
                    _quesionList.Add(v.Convert());
                }
            }
        }

        private ObservableCollection<VMQuesion> _quesionList = new ObservableCollection<VMQuesion>();
        public ObservableCollection<VMQuesion> QuesionList
        {
            get { return _quesionList; }
            set
            {
                _quesionList = value;
                RaisePropertyChanged("QuesionList");
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
                    _saveCommand = new RelayCommand((p) => 
                    {
                        QuestionEditorScence questionEditorScence = new QuestionEditorScence()
                        {
                            QuestionList = new List<Question>(),
                            TypeIdentity = TemplateGuid(),
                        };
                        foreach (var v in _quesionList)
                        {
                            questionEditorScence.QuestionList.Add(v.Convert());
                        }
                        base.SaveFile(questionEditorScence);
                    }, (p) => true);
                }

                return _saveCommand;
            }
        }

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
            return "题目信息编辑器";
        }

        /// <summary>
        /// 文档类，必须生成一个新的Guid不可直接使用
        /// </summary>
        /// <returns></returns>
        public new static string TemplateGuid()
        {
            return "67c77401-1e45-4eb9-973a-77ae0f32a664";
        }
    }

    public class VMQuesion
    {
        /// <summary>
        /// 题目编号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 选项个数
        /// </summary>
        public int CountSelection { get; set; }

        /// <summary>
        /// 选项，多个选项以,分割
        /// </summary>
        public String Answoer { get; set; }

        /// <summary>
        /// 分数
        /// </summary>
        public int Score { get; set; }
    }
}
