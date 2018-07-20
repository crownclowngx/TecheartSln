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
            if (examination.ExaminationQuestions != null)
                foreach (var v in examination.ExaminationQuestions)
                {
                    QuestionVM vm = new QuestionVM();
                    vm.Set(v);
                    QuestionList.Add(vm);
                    TotalScore += v.Score;
                }
            if (examination.Voters != null)
                foreach (var v in examination.Voters)
                {
                    StudentVM studentNM = new StudentVM();
                    studentNM.Set(v);
                    StudentList.Add(studentNM);
                }
            if (response.RegionScenes != null)
            {
                RegionList.Clear();
                foreach (var v in response.RegionScenes)
                {
                    RegionList.Add(v.convert());
                }
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
        #region 不欢迎外来者
        private bool _refuseOutOfStudentList=true;
        public bool RefuseOutOfStudentList
        {
            get { return _refuseOutOfStudentList; }
            set
            {
                _refuseOutOfStudentList = value;
                RaisePropertyChanged("RefuseOutOfStudentList");
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

        #region 成绩范围
        private ObservableCollection<RegionVM> _regionList = new ObservableCollection<RegionVM>() { new RegionVM() { Score=90, ScoreExplain="优秀" },new RegionVM() { Score = 75, ScoreExplain = "良好" }, new RegionVM() { Score = 60, ScoreExplain = "及格" }, new RegionVM() { Score = 0, ScoreExplain = "不及格" } };
        public ObservableCollection<RegionVM> RegionList
        {
            get { return _regionList; }
            set
            {
                _regionList = value;
                RaisePropertyChanged("RegionList");
                base.ViewChange();
            }
        }
        #endregion

        #region 最大分数
        private int _totalScore = 0;
        public int TotalScore
        {
            get { return _totalScore; }
            set
            {
                _totalScore = value;
                RaisePropertyChanged("TotalScore");
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
                            RegionScenes = new List<RegionScene>(),
                        };
                        if(examination==null || examination.Voters == null)
                        {
                            return;
                        }
                        foreach(var v in examination.Voters)
                        {
                            if(v.Statistics==null || v.Statistics.Count() == 0)
                            {
                                v.Statistics.Add(0, "0");
                            }
                            foreach(var k in v.Statistics)
                            {
                                Boolean right = false;
                                if (v.StatisticsAnswer!=null && v.StatisticsAnswer.ContainsKey(k.Key))
                                {
                                    right= v.StatisticsAnswer[k.Key] > 0; 
                                }
                                simpleExaminationScene.VoterScene.Add(new VoterScene() { VoterId=v.VoterId, Select=k.Value, QuestionNumber=k.Key, MappingNumber=v.VoterMappingNumber, Name=v.VoterName, SelectIsRight= right });
                            }
                        }
                        foreach(var v in RegionList)
                        {
                            simpleExaminationScene.RegionScenes.Add(v.convert());
                        }
                        base.SaveFile(simpleExaminationScene);
                        ProcessList += "->已保存";
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
                        if (dydic == null || !(dydic.ContainsKey("TypeIdentity") && dydic.ContainsKey("QuestionList")))
                        {
                            MessageBox.Show("文件不能被打开，这里只能打开题目文件，不能是其他类型的文件");
                            return;
                        }
                        var set = javaScriptSerializer.Deserialize<QuestionSet>(respnose);
                        examination.ExaminationQuestions = set.QuestionList.Select(k => k.convert()).ToList();
                        foreach (var v in examination.ExaminationQuestions)
                        {
                            QuestionVM vm = new QuestionVM();
                            vm.Set(v);
                            QuestionList.Add(vm);
                            TotalScore += v.Score;
                        }
                        ProcessList += "->已打开考试文件";
                       
                    }, (p) => true);
                }

                return _openQuestionFileCommand;
            }

        }
        #endregion
        #region 打开学生模板
        RelayCommand _openStudentInfoFileCommand = null;
        public ICommand OpenStudentInfoFileCommand
        {
            get
            {
                if (_openStudentInfoFileCommand == null)
                {
                    _openStudentInfoFileCommand = new RelayCommand((p) =>
                    {
                        var respnose = OpenFileUtils.OpenFileByDialog();
                        JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                        dynamic dy = javaScriptSerializer.Deserialize<dynamic>(respnose);
                        var dydic = (IDictionary<string, object>)dy;
                        if (dydic == null || !(dydic.ContainsKey("TypeIdentity") && dydic.ContainsKey("StudentInfoList")))
                        {
                            MessageBox.Show("文件不能被打开，这里只能打开学生关系文件，不能是其他类型的文件");
                            return;
                        }
                        var set = javaScriptSerializer.Deserialize<StudentSet>(respnose);
                        examination.Voters = set.StudentInfoList.Select(k => k.convert()).ToList();
                        foreach (var v in examination.Voters)
                        {
                            StudentVM vm = new StudentVM();
                            vm.Set(v);
                            StudentList.Add(vm);
                        }
                        ProcessList += "->已打开学生文件";
                    }, (p) => true);
                }

                return _openStudentInfoFileCommand;
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
                        //throw new Exception("这是一个测试一场");
                        try
                        {
                            
                            var dialog = new SaveFileDialog() { Filter = "2007以及之后Excel|*.xlsx", FileName = Title.Replace("*", "") + "成绩导出" };
                            if (dialog.ShowDialog() == true)
                            {
                                var path = dialog.FileName;
                                Workbook workbook = new Workbook();
                                workbook.Worksheets.Clear();
                                ExaminationAnalysisUtils.MakeStudentScore(workbook, examination.Voters);
                                ExaminationAnalysisUtils.MakeStudentDistribution(workbook, examination.Voters, _regionList.Select(k => k.convert()).ToList());
                                workbook.Save(path, SaveFormat.Xlsx);
                                ProcessList += "->已导出到Excel";
                            }
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show("Excel处于打开状态，请关闭Excel文件");
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
                        JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                        var response = javaScriptSerializer.Deserialize<WSDESubVoterSelectRequest>(messageData.MessageData);
                        if (!Start)
                        {
                            StudentLogin(response);
                            return;
                        }
                        StudentLogin(response);
                        if (String.IsNullOrWhiteSpace(response.SubVoterResult) || response.SubVoterResult.Equals("login"))
                        {
                            return;
                        }
                        if(!StudentList.Any(k => k.StudentNumber == response.SubVoterNumber) && RefuseOutOfStudentList)
                        {
                            return;
                        }
                        response.SubVoterResult = response.SubVoterResult.Replace(".", "");
                        response.SubVoterResult = StringUtils.RemoveDuplicateCharacters(response.SubVoterResult);
                        var number = System.Convert.ToInt32(response.SubVoterSelectNumber);
                        examination.Add(response.SubVoterNumber, number, response.SubVoterResult);
                        if (QuestionList.Any(k => k.QuestionNumber == response.SubVoterSelectNumber))
                        {
                            QuestionList.First(k => k.QuestionNumber == response.SubVoterSelectNumber).Set(examination.ExaminationQuestions.First(k => k.QuestionNumber == number));
                        }
                        if (!StudentList.Any(k => k.StudentNumber == response.SubVoterNumber) && examination.Voters!=null && examination.Voters.Count()>0)
                        {
                            StudentVM studentNM = new StudentVM();
                            studentNM.Set(examination.Voters.First(k => k.VoterId == response.SubVoterNumber));
                            StudentList.Add(studentNM);
                        }
                        if(examination.Voters != null && examination.Voters.Count() > 0)
                        {
                            StudentList.First(k => k.StudentNumber == response.SubVoterNumber).Set(examination.Voters.First(k => k.VoterId == response.SubVoterNumber));

                        }
                        
                        base.ViewChange();
                    });
                },
            });
        }

        private void StudentLogin(WSDESubVoterSelectRequest response)
        {
            if (response.SubVoterResult.Equals("login") && StudentList.Any(k => k.StudentNumber == response.SubVoterNumber))
            {
                StudentList.First(k => k.StudentNumber == response.SubVoterNumber).IsLogin = true;
            }
        }
    }

}
