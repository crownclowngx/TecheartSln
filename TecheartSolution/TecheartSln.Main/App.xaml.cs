using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace TecheartSln.Main
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {

        public App()
        {
            this.Startup += new StartupEventHandler(App_Startup);
            
        }

        void App_Startup(object sender, StartupEventArgs e)
        {
            var name = Assembly.GetExecutingAssembly().Location;
            var path = Path.GetDirectoryName(name);

            AppDomainSetup ads = new AppDomainSetup();
            ads.PrivateBinPath = path + @"\Libs";
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            this.DispatcherUnhandledException += new DispatcherUnhandledExceptionEventHandler(App_DispatcherUnhandledException);
        }

        void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
        }
    }
}
