﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TecheartSln.Core;
using TecheartSln.Core.Resource;
using TecheartSln.Core.View.Pane;
using TecheartSln.Core.ViewModel.Base;
using TecheartSln.Plug.Drive.ViewModel;

namespace TecheartSln.Plug.Drive
{
    /// <summary>
    /// 向Main 注册
    /// </summary>
    public class Init : IInit
    {
        public IList<ToolViewModel> GetToolViewModel()
        {
            return new List<ToolViewModel>() { new RelationListViewModel() { IsVisible = false }, new ProducerMonitorViewModel() { IsVisible = false }, new LoginViewModel() { IsVisible = true } };
        }

        public void RedisterStyles(PanesStyleSelectorDynamic selectPanesStyle)
        {
        }

        public void RegisterDataTemplates(PanesTemplateSelectorDynamic paneSel)
        {
            var template2 = ResourceLocator.GetResource<DataTemplate>(
                                    Assembly.GetAssembly(typeof(RelationListViewModel)).GetName().Name,
                                    "FoundationDataTemplate.xaml",
                                    "RelationListViewTemplate") as DataTemplate;

            paneSel.RegisterDataTemplate(typeof(RelationListViewModel), template2);

            var template3 = ResourceLocator.GetResource<DataTemplate>(
                                    Assembly.GetAssembly(typeof(ProducerMonitorViewModel)).GetName().Name,
                                    "FoundationDataTemplate.xaml",
                                    "ProducerMonitorViewTemplate") as DataTemplate;

            paneSel.RegisterDataTemplate(typeof(ProducerMonitorViewModel), template3);

            //var template4 = ResourceLocator.GetResource<DataTemplate>(
            //                        Assembly.GetAssembly(typeof(TecheartHostViewModel)).GetName().Name,
            //                        "FoundationDataTemplate.xaml",
            //                        "TecheartHostViewTemplate") as DataTemplate;

            //paneSel.RegisterDataTemplate(typeof(TecheartHostViewModel), template4);

            var template5 = ResourceLocator.GetResource<DataTemplate>(
                                 Assembly.GetAssembly(typeof(LoginViewModel)).GetName().Name,
                                 "FoundationDataTemplate.xaml",
                                 "LoginViewTemplate") as DataTemplate;

            paneSel.RegisterDataTemplate(typeof(LoginViewModel), template5);

            var template6 = ResourceLocator.GetResource<DataTemplate>(
                       Assembly.GetAssembly(typeof(SearchViewModel)).GetName().Name,
                       "FoundationDataTemplate.xaml",
                       "TecheartSlnPlugSearchViewTemplate") as DataTemplate;

            paneSel.RegisterDataTemplate(typeof(SearchViewModel), template6);

            var template7 = ResourceLocator.GetResource<DataTemplate>(
                        Assembly.GetAssembly(typeof(AddUserViewModel)).GetName().Name,
                        "FoundationDataTemplate.xaml",
                        "TecheartSlnPlugAddUserViewTemplate") as DataTemplate;

            paneSel.RegisterDataTemplate(typeof(AddUserViewModel), template7);


            var template8 = ResourceLocator.GetResource<DataTemplate>(
                    Assembly.GetAssembly(typeof(SearchUserViewModel)).GetName().Name,
                    "FoundationDataTemplate.xaml",
                    "TecheartSlnPlugSearchUserViewTemplate") as DataTemplate;

            paneSel.RegisterDataTemplate(typeof(SearchUserViewModel), template8);
 

        }
    }
}
