using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TestApp
{
    public class EmployeeConfiguratorVM : BaseConfiguratorVM
    {
        private enum State
        {
            SELECT_EMPLOYEE_TO_CONFIGURE,
            CONFIGURE_EMPLOYEE,
            SELECT_MANAGER,
            SELECT_LOCATION
        };
        private State _state;

        public EmployeeConfiguratorVM(OracleDB db)
        {
            _db = db;
            ChangeState(State.SELECT_EMPLOYEE_TO_CONFIGURE);
        }
        protected override void OnForwardButton(object param)
        {
            Console.WriteLine("Forward!");
            switch (_state)
            {
                case State.SELECT_EMPLOYEE_TO_CONFIGURE:
                    ChangeState(State.CONFIGURE_EMPLOYEE);
                    break;
                case State.CONFIGURE_EMPLOYEE:
                    break;
                case State.SELECT_MANAGER:
                    ChangeState(State.CONFIGURE_EMPLOYEE);
                    break;
                default:
                    break;
            }
        }
        protected override void OnBackButton(object param)
        {
            Console.WriteLine("Retreat...");
            switch (_state)
            {
                case State.SELECT_EMPLOYEE_TO_CONFIGURE:
                    break;
                case State.CONFIGURE_EMPLOYEE:
                    ChangeState(State.SELECT_EMPLOYEE_TO_CONFIGURE);
                    break;
                case State.SELECT_MANAGER:
                    break;
                default:
                    break;
            }
        }
        public void OnSelectManager(object param)
        {
            ChangeState(State.SELECT_MANAGER);
        }
        public void OnSelectLocation(object param)
        {

        }
        private void ChangeState(State newState)
        {
            switch (newState)
            {
                case State.SELECT_EMPLOYEE_TO_CONFIGURE:
                    Content = new EmployeeSelectorVM(_db);
                    ForwardButtonText = "Configure Employee";
                    BackButtonText = "Back";
                    Title = "Select Employee to Configure";
                    _state = State.SELECT_EMPLOYEE_TO_CONFIGURE;
                    break;
                case State.CONFIGURE_EMPLOYEE:
                    EmployeeModifierVM newVM = null;
                    EmployeeSelectorVM vm;
                    switch (_state)
                    {
                        case State.SELECT_EMPLOYEE_TO_CONFIGURE:
                            vm = Content as EmployeeSelectorVM;
                            if (vm == null) { Console.WriteLine("This shouldnt happen"); return; }
                            if (vm.SelectedEmployee == null) { return; }
                            newVM = new EmployeeModifierVM(this, vm.SelectedEmployee);
                            break;
                        case State.SELECT_MANAGER:
                            newVM = _prevContent as EmployeeModifierVM;
                            vm = Content as EmployeeSelectorVM;
                            if (vm == null) { Console.WriteLine("This shouldnt happen"); return; }
                            if (vm.SelectedEmployee == null) { return; }
                            newVM.Manager = vm.SelectedEmployee;
                            break;
                        case State.SELECT_LOCATION:
                            newVM = _prevContent as EmployeeModifierVM;
                            break;
                    }
                    Title = $"Configuring {newVM.OriginalEmployee.FullName}";
                    ForwardButtonText = "Save";
                    BackButtonText = "Cancel";
                    Content = newVM;
                    _state = State.CONFIGURE_EMPLOYEE;
                    break;
                    
                case State.SELECT_MANAGER:
                    _prevContent = Content;
                    EmployeeModifierVM prevVM = _prevContent as EmployeeModifierVM;
                    Content = new EmployeeSelectorVM(_db);
                    Title = $"Select Manager For {prevVM.FirstName} {prevVM.LastName}";
                    ForwardButtonText = "Save";
                    BackButtonText = "Cancel";
                    _state = State.SELECT_MANAGER;
                    break;
                default:
                    break;
            }
        }
    }
}
