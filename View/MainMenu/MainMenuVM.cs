using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TestApp
{
    public interface IMainMenuVMParent
    {
        void OnOpenEmployeeConfigurator(object param);
        void OnOpenOrdersView(object param);
    }
    public class MainMenuVM: VMBase
    {
        private ICommand _openEmployeeConfigurator;
        private ICommand _openOrdersView;
        private IMainMenuVMParent _parent;

        public ICommand OpenEmployeeConfigurator { get { return _openEmployeeConfigurator ?? new RelayCommand(OnOpenEmployeeConfigurator); } set { _openEmployeeConfigurator = value; } }
        public ICommand OpenOrdersView { get { return _openOrdersView ?? new RelayCommand(OnOpenOrdersView); } set { _openOrdersView = value; } }

        public MainMenuVM(IMainMenuVMParent parent)
        {
            _parent = parent;
        }
        private void OnOpenEmployeeConfigurator(object param)
        {
            _parent.OnOpenEmployeeConfigurator(param);
        }
        private void OnOpenOrdersView(object param)
        {
            _parent.OnOpenOrdersView(param);
        }
    }
}
