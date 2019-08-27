using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TestApp
{
    public interface IEmployeeSelectorAdderVMParent
    {
        void OnAddNewEmployee(object param);
        void OnRemoveEmployee(object param);
    }
    public class EmployeeSelectorAdderVM : EmployeeSelectorVM
    {
        private ICommand _addNewEmployee;
        private ICommand _removeEmployee;
        private IEmployeeSelectorAdderVMParent _parent;

        public ICommand AddNewEmployee { get { return _addNewEmployee ?? new RelayCommand(OnAddNewEmployee); } set { _addNewEmployee = value; } }
        public ICommand RemoveEmployee { get { return _removeEmployee ?? new RelayCommand(OnRemoveEmployee); } set { _removeEmployee = value; } }
        public EmployeeSelectorAdderVM(IEmployeeSelectorAdderVMParent parent, OracleDB db): base(db)
        {
            _parent = parent;
        }
        private void OnAddNewEmployee(object param)
        {
            _parent.OnAddNewEmployee(param);
        }
        private void OnRemoveEmployee(object param)
        {
            _parent.OnRemoveEmployee(param);
            OnPropertyChanged("SearchQuery");
        }
    }
}
