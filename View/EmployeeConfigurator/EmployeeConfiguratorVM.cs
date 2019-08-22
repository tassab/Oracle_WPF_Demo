using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace TestApp
{
    public interface IEmployeeConfiguratorVMParent
    {
        void OnExitEmployeeConfigurator(object param);
    }
    public class EmployeeConfiguratorVM : VMBase, IEmployeeSelectorAdderVMParent, IEmployeeModifierVMParent
    {
        private enum State
        {
            EMPLOYEE_LIST,
            CONFIGURE_EMPLOYEE,
            SELECT_MANAGER,
            SELECT_LOCATION
        };

        #region fields
        private State _state;
        private OracleDB _db;
        private VMBase _content;
        private VMBase _prevContent;
        private IEmployeeConfiguratorVMParent _parent;

        private ICommand _forwardButton;
        private ICommand _backButton;
        private string _forwardButttonText;
        private string _backButttonText;
        private string _title;
        #endregion fields

        #region properties
        public VMBase Content { get { return _content; } set { SetProperty(ref _content, value); } }
        public string ForwardButtonText { get { return _forwardButttonText;} set { SetProperty(ref _forwardButttonText, value); } }
        public string BackButtonText { get { return _backButttonText;} set { SetProperty(ref _backButttonText, value); } }
        public string Title { get { return _title; } set { SetProperty(ref _title, value); } }
        public ICommand ForwardButton {
            get { return _forwardButton ?? new RelayCommand(OnForwardButton); }
            set { _forwardButton = value; }
        }
        public ICommand BackButton
        {
            get { return _backButton ?? new RelayCommand(OnBackButton); }
            set { _backButton = value; }
        }
        #endregion properties
        public EmployeeConfiguratorVM(IEmployeeConfiguratorVMParent parent, OracleDB db)
        {
            _parent = parent;
            _db = db;
            ChangeState(State.EMPLOYEE_LIST);
        }

        private void OnForwardButton(object param)
        {
            switch (_state)
            {
                case State.EMPLOYEE_LIST:
                    EmployeeSelectorVM vm = Content as EmployeeSelectorVM;
                    if (vm == null) { Console.WriteLine("This shouldnt happen"); return; }
                    if (vm.SelectedEmployee == null) { return; }
                    ChangeState(State.CONFIGURE_EMPLOYEE, new EmployeeModifierVM(this, vm.SelectedEmployee));
                    break;
                case State.CONFIGURE_EMPLOYEE:
                    EmployeeModifierVM vm2 = Content as EmployeeModifierVM;
                    Employee configuredEmployee = vm2.GetEmployee();
                    if (configuredEmployee.Employee_Id == 0)
                    {
                        _db.Employees.Add(configuredEmployee);
                    }
                    else
                    {
                        Employee originalEmployee = _db.Employees.Find(configuredEmployee.Employee_Id);
                        _db.Entry(originalEmployee).CurrentValues.SetValues(configuredEmployee);
                    }
                    _db.SaveChanges();
                    ChangeState(State.EMPLOYEE_LIST);
                    break;
                case State.SELECT_MANAGER:
                    EmployeeModifierVM newVM = _prevContent as EmployeeModifierVM;
                    vm = Content as EmployeeSelectorVM;
                    if (vm == null) { Console.WriteLine("This shouldnt happen"); return; }
                    if (vm.SelectedEmployee == null) { return; }
                    newVM.Manager = vm.SelectedEmployee;
                    ChangeState(State.CONFIGURE_EMPLOYEE, newVM);
                    break;
                default:
                    break;
            }
        }
        private void OnBackButton(object param)
        {
            switch (_state)
            {
                case State.EMPLOYEE_LIST:
                    _parent.OnExitEmployeeConfigurator(param);
                    break;
                case State.CONFIGURE_EMPLOYEE:
                    ChangeState(State.EMPLOYEE_LIST);
                    break;
                case State.SELECT_MANAGER:
                    ChangeState(State.CONFIGURE_EMPLOYEE, _prevContent as EmployeeModifierVM);
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
        public void OnAddNewEmployee(object param)
        {
            ChangeState(State.CONFIGURE_EMPLOYEE, new EmployeeModifierVM(this));
        }
        public void OnRemoveEmployee(object param)
        {
            var vm = Content as EmployeeSelectorVM;
            if (vm == null) { Console.WriteLine("This shouldnt happen"); return; }
            if (vm.SelectedEmployee == null) { return; }
            if(MessageBox.Show($"Do you want to remove {vm.SelectedEmployee.FullName} from the database?",
                "Confirm removal",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning)
                == MessageBoxResult.No)
            {
                return;
            }
            _db.Employees.Remove(vm.SelectedEmployee);
            _db.SaveChanges();
        }
        private void ChangeState(State newState, VMBase newVM = null)
        {
            switch (newState)
            {
                case State.EMPLOYEE_LIST:
                    Content = new EmployeeSelectorAdderVM(this, _db);
                    ForwardButtonText = "Configure Employee";
                    BackButtonText = "Back";
                    Title = "Select Employee to Configure";
                    _state = State.EMPLOYEE_LIST;
                    break;
                case State.CONFIGURE_EMPLOYEE:
                    var newVMCast = newVM as EmployeeModifierVM;
                    Title = $"Configuring {newVMCast.OriginalEmployee?.FullName ?? "New Employee"}";
                    ForwardButtonText = "Save";
                    BackButtonText = "Cancel";
                    Content = newVMCast;
                    _state = State.CONFIGURE_EMPLOYEE;
                    break;
                    
                case State.SELECT_MANAGER:
                    _prevContent = Content;
                    EmployeeModifierVM prevVM = _prevContent as EmployeeModifierVM;
                    Content = new EmployeeSelectorVM(_db);
                    Title = $"Select Manager For {prevVM.OriginalEmployee?.FullName ?? "New Employee"}";
                    ForwardButtonText = "Save";
                    BackButtonText = "Cancel";
                    _state = State.SELECT_MANAGER;
                    break;
            }
        }
    }
}
