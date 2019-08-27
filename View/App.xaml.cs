using Autofac;
using Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace TestApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            IContainer container = new BootStrapper().BootStrap();
            AppView app = new AppView();
            AppViewVM appViewVM = new AppViewVM(new OracleDB(), container.Resolve<IMessageDialogService>());
            app.DataContext = appViewVM;
            app.Show();
        }
    }
}
