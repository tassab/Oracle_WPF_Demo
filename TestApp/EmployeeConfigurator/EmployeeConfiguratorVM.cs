using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TestApp
{
    public class EmployeeConfiguratorVM : VMBase
    {
        private enum State
        {
            SELECT_EMPLOYEE_TO_CONFIGURE,
            CONFIGURE_EMPLOYEE,
            SELECT_MANAGER,
            SELECT_LOCATION
        };

        #region fields
        private State _state;
        private OracleDB _db;
        private VMBase _content;
        private VMBase _prevContent;

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
        public EmployeeConfiguratorVM(OracleDB db)
        {
            _db = db;
            ChangeState(State.SELECT_EMPLOYEE_TO_CONFIGURE);
        }
        private void OnForwardButton(object param)
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
        private void OnBackButton(object param)
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
