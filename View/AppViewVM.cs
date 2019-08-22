using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TestApp
{
    public class AppViewVM : VMBase, IMainMenuVMParent, IEmployeeConfiguratorVMParent
    {
        private VMBase _currentVM;
        public VMBase CurrentVM { get { return _currentVM; } set { SetProperty(ref _currentVM, value); } }
        public AppViewVM()
        {
            CurrentVM = new MainMenuVM(this);
        }

        public void OnOpenEmployeeConfigurator(object param)
        {
            CurrentVM = new EmployeeConfiguratorVM(this, new OracleDB());
        }

        public void OnOpenOrdersView(object param)
        {
            Console.WriteLine("This view is not available yet");
        }

        public void OnExitEmployeeConfigurator(object param)
        {
            CurrentVM = new MainMenuVM(this);
        }
    }
}
