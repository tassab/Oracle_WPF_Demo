using Autofac;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TestApp
{
    public class AppViewVM : VMBase, IMainMenuVMParent, IEmployeeConfiguratorVMParent, IOrderConfiguratorVMParent
    {
        private OracleDB _db;
        private IContainer _diContainer;
        private VMBase _currentVM;
        public VMBase CurrentVM { get { return _currentVM; } set { SetProperty(ref _currentVM, value); } }

        public AppViewVM()
        {
            CurrentVM = new MainMenuVM(this);
            _db = new OracleDB();
            _diContainer = new BootStrapper().BootStrap();
        }

        public void OnOpenEmployeeConfigurator(object param)
        {
            CurrentVM = new EmployeeConfiguratorVM(this, _db, _diContainer.Resolve<IMessageDialogService>());
        }

        public void OnOpenOrdersView(object param)
        {
            CurrentVM = new OrderConfiguratorVM(this, _db, _diContainer.Resolve<IMessageDialogService>());
        }

        public void OnExitEmployeeConfigurator(object param)
        {
            CurrentVM = new MainMenuVM(this);
        }

        public void OnExitOrderConfigurator(object param)
        {
            CurrentVM = new MainMenuVM(this);
        }
    }
}
