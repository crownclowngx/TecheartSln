using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TecheartSln.Core.Command;
using TecheartSln.Core.ViewModel.Base;

namespace TecheartSln.Main.Utils
{
    public class FileTemplateListViewModel: ViewModelBase
    {
        public FileTemplateListViewModel()
        {
            var templates = Workspace.This.FileTemplateTypes;
            foreach (var type in templates)
            {
                MethodInfo methodDesc = type.TemplateType.GetMethod("GetDesc");
                
                MethodInfo methodName = type.TemplateType.GetMethod("GetName");

                MethodInfo methodGuid = type.TemplateType.GetMethod("TemplateGuid");
                _fileTemplates.Add(new FileTemplate() { FileDesc = methodDesc.Invoke(null, null).ToString(), FileName = methodName.Invoke(null, null).ToString(), TemplateType = type.TemplateType , TemplateGuid= methodGuid.Invoke(null, null).ToString() });
            }
        }

        ObservableCollection<FileTemplate> _fileTemplates = new ObservableCollection<FileTemplate>();

        public ObservableCollection<FileTemplate> FileTemplates
        {
            get { return _fileTemplates; }
            set
            {
                _fileTemplates = value;
                RaisePropertyChanged("FileTemplates");
            }
        }

        private FileTemplate p_SelectedItem;
        public FileTemplate SelectedItem
        {
            get { return p_SelectedItem; }

            set
            {
                p_SelectedItem = value;
                RaisePropertyChanged("SelectedItem");
            }
        }

        private String _fileName;

        public String FileName
        {
            get { return _fileName; }

            set
            {
                _fileName = value;
                RaisePropertyChanged("FileName");
            }
        }

        private RelayCommand _createCommand = null;
        public ICommand CreateCommand
        {
            get
            {
                if (_createCommand == null)
                {
                    
                    _createCommand = new RelayCommand(p => {
                        if (p_SelectedItem == null)
                        {
                            return;
                        }
                        if (String.IsNullOrWhiteSpace(FileName))
                        {
                            MessageBox.Show("需要起个名字啊喂(╯‵□′)╯︵┻━┻");
                            return;
                        }
                        Workspace.This.CreateFile((TemplateBaseViewModel)Activator.CreateInstance(SelectedItem.TemplateType,new object[] { _fileName }));
                        
                    }, 
                    p => true);
                }
                return _createCommand;
            }

        }

    }

    public class FileTemplate
    {

        /// <summary>
        /// 模板名称
        /// </summary>
        public String FileName { get; set; }

        /// <summary>
        /// 模板说明
        /// </summary>
        public String FileDesc { get; set; }

        /// <summary>
        /// 模板对应的 Type类型 ，方便实例化
        /// </summary>
        public Type TemplateType { get; set; }

        public String TemplateGuid { get; set; }
    }
}
