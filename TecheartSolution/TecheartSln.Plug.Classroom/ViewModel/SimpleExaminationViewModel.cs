using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using TecheartSln.Core.Utils;
using TecheartSln.Core.ViewModel.Base;
using System.Dynamic;
using System.Windows;
using TecheartSln.Plug.Classroom.Domain.Boundary;
using System.Collections.ObjectModel;
using TecheartSln.Plug.Classroom.Domain;

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


        public new static string GetDesc()
        {
            return "根据一个已经编辑好的题目模板，在一段时间内，对学生的答案进行搜集，并计算总分，并保存到特定文件";
        }

        public new static string GetName()
        {
            return "在线考试模板";
        }

        /// <summary>
        /// 文档类，必须生成一个新的Guid不可直接使用
        /// </summary>
        /// <returns></returns>
        public new static string TemplateGuid()
        {
            return "be3209cd-d564-4d59-b9ca-e850b98bccac";
        }

        #region 问题列表
        private ObservableCollection<ExaminationQuestion> _quesionList = new ObservableCollection<ExaminationQuestion>();
        public ObservableCollection<ExaminationQuestion> QuesionList
        {
            get { return _quesionList; }
            set
            {
                _quesionList = value;
                RaisePropertyChanged("QuesionList");
            }
        }
        #endregion

        #region 窗口关闭命令
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
        #endregion

        #region 窗口保存命令
        public override ICommand SaveCommand => throw new NotImplementedException();
        #endregion

        #region 打开题目模板
        RelayCommand _openQuestionFileCommand = null;
        public ICommand OpenQuestionFileCommand
        {
            get
            {
                if (_openQuestionFileCommand == null)
                {
                    _openQuestionFileCommand = new RelayCommand((p) => 
                    {
                        var respnose=OpenFileUtils.OpenFileByDialog();
                        JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                        dynamic dy = javaScriptSerializer.Deserialize<dynamic>(respnose);
                        var dydic = (IDictionary<string, object>)dy;
                        if (!(dydic.ContainsKey("TypeIdentity") && dydic.ContainsKey("QuestionList")))
                        {
                            MessageBox.Show("文件不能被打开，这里只能打开题目文件，不能是其他类型的文件");
                        }
                        var set=javaScriptSerializer.Deserialize<QuestionSet>(respnose);
                        foreach(var v in set.QuestionList)
                        {
                            _quesionList.Add(v);
                        }
                    }, (p) => true);
                }

                return _openQuestionFileCommand;
            }
                    
        }
        #endregion

    }
}
