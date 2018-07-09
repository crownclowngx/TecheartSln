﻿using Microsoft.Win32;
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
using TecheartSln.Plug.Classroom.Scene;
using Aspose.Cells;
using TecheartSln.Plug.Classroom.Domain.Utils;

namespace TecheartSln.Plug.Classroom.ViewModel
{
    public class SimpleExaminationViewModel : TemplateBaseViewModel
    {

        Examination examination = new Examination();

        public SimpleExaminationViewModel(string title) : base(title)
        {
            init();
        }

        public SimpleExaminationViewModel(BaseScene scene, String json) : base(scene, json)
        {
            CanAnalysis = true;
            init();
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            var response=javaScriptSerializer.Deserialize<SimpleExaminationScene>(json);
            examination = response.Convert();
            foreach (var v in examination.ExaminationQuestions)
            {
                QuestionVM vm = new QuestionVM();
                vm.Set(v);
                QuestionList.Add(vm);
            }

            foreach(var v in examination.Voters)
            {
                StudentVM studentNM = new StudentVM();
                studentNM.Set(v);
                StudentList.Add(studentNM);
            }
        }


        public new static string GetDesc()
        {
            return "根据一个已经编辑好的题目模板，在一段时间内，对学生的答案进行搜集，并计算总分，并保存到特定文件\n注意：该模板最多支持10个选项即从 0-9 不能是两位数或者字母，不能是小数";
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
        #region 进程列表
        private String _process = "";
        public String ProcessList
        {
            get { return _process; }
            set
            {
                _process = value;
                RaisePropertyChanged("ProcessList");
            }
        }
        #endregion
        #region 开始和停止考试标志
        private bool _start;
        public bool Start
        {
            get { return _start; }
            set
            {
                _start = value;
                if (value) ProcessList += "->开始考试";
                else ProcessList += "->结束考试";
                RaisePropertyChanged("Start");
            }
        }
        #endregion

        #region 考试题目VM
        private ObservableCollection<QuestionVM> _questionList = new ObservableCollection<QuestionVM>();
        public ObservableCollection<QuestionVM> QuestionList
        {
            get { return _questionList; }
            set
            {
                _questionList = value;
                RaisePropertyChanged("QuestionList");
                base.ViewChange();
            }
        }
        #endregion
        #region 考试学生VM
        private ObservableCollection<StudentVM> _studentList = new ObservableCollection<StudentVM>();
        public ObservableCollection<StudentVM> StudentList
        {
            get { return _studentList; }
            set
            {
                _studentList = value;
                RaisePropertyChanged("StudentList");
                base.ViewChange();
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
        RelayCommand _saveCommand = null;
        override public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new RelayCommand((p) =>
                    {
                        CanAnalysis = true;
                        SimpleExaminationScene simpleExaminationScene = new SimpleExaminationScene()
                        {
                            TypeIdentity = TemplateGuid(),
                            VoterScene = new List<VoterScene>(),
                            ExaminationQuestion = examination.ExaminationQuestions,
                        };
                        if(examination==null || examination.Voters == null)
                        {
                            return;
                        }
                        foreach(var v in examination.Voters)
                        {
                            foreach(var k in v.Statistics)
                            {
                                simpleExaminationScene.VoterScene.Add(new VoterScene() { VoterId=v.VoterId, Select=k.Value, QuestionNumber=k.Key });
                            }
                        }
                        base.SaveFile(simpleExaminationScene);
                    }, (p) => true);
                }

                return _saveCommand;
            }
        }
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
                        var respnose = OpenFileUtils.OpenFileByDialog();
                        JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                        dynamic dy = javaScriptSerializer.Deserialize<dynamic>(respnose);
                        var dydic = (IDictionary<string, object>)dy;
                        if (!(dydic.ContainsKey("TypeIdentity") && dydic.ContainsKey("QuestionList")))
                        {
                            MessageBox.Show("文件不能被打开，这里只能打开题目文件，不能是其他类型的文件");
                        }
                        var set = javaScriptSerializer.Deserialize<QuestionSet>(respnose);
                        examination.ExaminationQuestions = set.QuestionList.Select(k => k.convert()).ToList();
                        foreach (var v in examination.ExaminationQuestions)
                        {
                            QuestionVM vm = new QuestionVM();
                            vm.Set(v);
                            QuestionList.Add(vm);
                        }
                        ProcessList += "->已打开文件";
                    }, (p) => true);
                }

                return _openQuestionFileCommand;
            }

        }
        #endregion
        #region 分析按钮是否可用
        private bool _canAnalysis;
        public bool CanAnalysis
        {
            get { return _canAnalysis; }
            set
            {
                _canAnalysis = value;
                RaisePropertyChanged("CanAnalysis");
            }
        }
        #endregion

        #region 分析并导出到excel
        RelayCommand _analysisCommand = null;
        public ICommand AnalysisCommand
        {
            get
            {
                if (_analysisCommand == null)
                {
                    _analysisCommand = new RelayCommand(p =>
                    {
                        var dialog = new SaveFileDialog() { Filter = "2007以及之后Excel|*.xlsx", FileName = Title.Replace("*", "")+"成绩导出" };
                        if (dialog.ShowDialog() == true)
                        {
                            var path = dialog.FileName;
                            Workbook workbook = new Workbook();
                            workbook.Worksheets.Clear();
                            ExaminationAnalysisUtils.MakeStudentScore(workbook, examination.Voters);
                            workbook.Save(path, SaveFormat.Xlsx);
                        }
                    }, p => true);
                }
                return _analysisCommand;
            }
        }
        #endregion

        private void init()
        {
            MessageSubscribeRelations.AddSubscribe(MessageType.WSDEDataEvent, new Relation()
            {
                CanUninstall = true,
                IsActive = true,
                IsKeep = false,
                RelationDescribe = Title,
                RelationGuid = this.StrGuid,
                RelationAction = messageData =>
                {
                    base.BindingPropInOtherTask(() =>
                    {
                        if (!Start)
                        {
                            return;
                        }
                        JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                        var response = javaScriptSerializer.Deserialize<WSDESubVoterSelectRequest>(messageData.MessageData);
                        var number = System.Convert.ToInt32(response.SubVoterSelectNumber);
                        examination.Add(response.SubVoterNumber, number, response.SubVoterResult);
                        if (QuestionList.Any(k => k.QuestionNumber == response.SubVoterSelectNumber))
                        {
                            QuestionList.First(k => k.QuestionNumber == response.SubVoterSelectNumber).Set(examination.ExaminationQuestions.First(k => k.QuestionNumber == number));
                        }
                        if (!StudentList.Any(k => k.StudentNumber == response.SubVoterNumber))
                        {
                            StudentVM studentNM = new StudentVM();
                            studentNM.Set(examination.Voters.First(k => k.VoterId == response.SubVoterNumber));
                            StudentList.Add(studentNM);
                        }

                        StudentList.First(k => k.StudentNumber == response.SubVoterNumber).Set(examination.Voters.First(k => k.VoterId == response.SubVoterNumber));

                        base.ViewChange();
                    });
                },
            });
        }
    }
    public class StudentVM: ViewModelBase
    {
        /// <summary>
        /// 题目编号
        /// </summary>
        private String _studentNumber = "";
        public String StudentNumber
        {
            get { return _studentNumber; }
            set
            {
                _studentNumber = value;
                RaisePropertyChanged("StudentNumber");

            }
        }

        /// <summary>
        /// 总得分
        /// </summary>
        private string _score = "";
        public String Score
        {
            get { return _score; }
            set
            {
                _score = value;
                RaisePropertyChanged("Score");

            }
        }

        /// <summary>
        /// 总答题数
        /// </summary>
        public string _count = "0";
        public String Count
        {
            get { return _count; }
            set
            {
                _count = value;
                RaisePropertyChanged("Count");

            }
        }

        public void Set(Voter  voter)
        {
            StudentNumber = voter.VoterId;
            Score = "学生总分"+voter.Score.ToString();
            Count = "学生总答题数"+voter.Count.ToString();
        }
    }
    public class QuestionVM: ViewModelBase
    {
        /// <summary>
        /// 题目编号
        /// </summary>
        private String _questionNumber = "";
        public String QuestionNumber
        {
            get { return _questionNumber; }
            set
            {
                _questionNumber = value;
                RaisePropertyChanged("QuestionNumber");
                 
            }
        }
        /// <summary>
        /// 答案和分数
        /// </summary>
        private String _answerAndScore = "";
        public String AnswerAndScore
        {
            get { return _answerAndScore; }
            set
            {
                _answerAndScore = value;
                RaisePropertyChanged("AnswerAndScore");

            }
        }


        /// <summary>
        /// 答题分布统计
        /// </summary>
        private String _statistics = "";
        public String Statistics
        {
            get { return _statistics; }
            set
            {
                _statistics = value;
                RaisePropertyChanged("Statistics");

            }
        }
        
        public void Set(ExaminationQuestion examinationQuestion)
        {
            
            QuestionNumber = examinationQuestion.QuestionNumber.ToString();
            AnswerAndScore = "答案是："+String.Join("|", examinationQuestion.Answer) +" ，分数是："+ examinationQuestion.Score;//"答案是 A ，分数是 4";
            StringBuilder sb = new StringBuilder();
            foreach(var v in examinationQuestion.Statistics)
            {
                sb.Append("选择：");
                sb.Append(v.Key);
                sb.Append("人数：");
                sb.Append(v.Value);
                sb.Append(",  ");
            }
            Statistics = sb.ToString();
        }
    }
}
