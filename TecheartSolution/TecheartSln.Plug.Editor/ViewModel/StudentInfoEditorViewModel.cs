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
    public class StudentInfoEditorViewModel : TemplateBaseViewModel
    {
        public StudentInfoEditorViewModel(string title) : base(title)
        {
        }

        public StudentInfoEditorViewModel(BaseScene scene, String json):base(scene,json)
        {
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            var sc = javaScriptSerializer.Deserialize<StudentInfoEditorScence>(json);
            if (sc != default(StudentInfoEditorScence))
            {
                foreach (var v in sc.StudentInfoList)
                {
                    _studnetinfoList.Add(v.Convert());
                }
            }
        }

        public new static string GetDesc()
        {
            return "建立一个班级学生与终端的对应关系";
        }

        public new static string GetName()
        {
            return "学生信息编辑器";
        }

        /// <summary>
        /// 文档类，必须生成一个新的Guid不可直接使用
        /// </summary>
        /// <returns></returns>
        public new static string TemplateGuid()
        {
            return "61c8339b-27fe-49b5-bcac-cc20c62bab87";
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


        RelayCommand _saveCommand = null;
        override public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new RelayCommand((p) =>
                    {
                        StudentInfoEditorScence questionEditorScence = new StudentInfoEditorScence()
                        {
                            StudentInfoList = new List<StudentInfo>(),
                            TypeIdentity = TemplateGuid(),
                        };
                        foreach (var v in _studnetinfoList)
                        {
                            questionEditorScence.StudentInfoList.Add(v.Convert());
                        }
                        base.SaveFile(questionEditorScence);
                    }, (p) => true);
                }

                return _saveCommand;
            }
        }



        private ObservableCollection<VMStudentInfo> _studnetinfoList = new ObservableCollection<VMStudentInfo>();
        public ObservableCollection<VMStudentInfo> StudnetList
        {
            get { return _studnetinfoList; }
            set
            {
                _studnetinfoList = value;
                RaisePropertyChanged("StudnetList");
            }
        }


    }


    public class VMStudentInfo
    {
        /// <summary>
        /// 学号
        /// </summary>
        public String Number { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// 终端编号
        /// </summary>
        public String ClientNumber { get; set; }
    }
}
