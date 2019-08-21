using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TestApp
{
    public class EmployeeModifierVM : VMBase
    {
        private string _firstName;
        private string _lastName;
        private string _title;
        private string _phone;
        private string _email;
        private Employee _manager;
        private Location _location;
        private Employee _originalEmployee;
        private EmployeeConfiguratorVM _parent;

        private ICommand _selectManager;
        private ICommand _selectLocation;
        private ICommand _removeManager;
        private ICommand _reset;

        public string FirstName { get { return _firstName; } set { SetProperty(ref _firstName, value); } }
        public string LastName { get { return _lastName; } set { SetProperty(ref _lastName, value); } }
        public string Title { get { return _title; } set { SetProperty(ref _title, value); } }
        public string Phone { get { return _phone; } set { SetProperty(ref _phone, value); } }
        public string Email { get { return _email; } set { SetProperty(ref _email, value); } }
        public Employee Manager { get { return _manager; } set { SetProperty(ref _manager, value, "ManagerName"); } }
        public Location Location { get { return _location; } set { SetProperty(ref _location, value, "LocationName"); } }
        public string ManagerName { get { return Manager == null ? "None" : Manager.FullName; } }
        public string LocationName { get { return Location == null ? "None" : Location.ToString(); } }
        public Employee OriginalEmployee { get { return _originalEmployee; } }

        public ICommand SelectManager { get { return _selectManager ?? new RelayCommand(OnSelectManager); } set { _selectManager = value; } }
        public ICommand SelectLocation { get { return _selectLocation ?? new RelayCommand(OnSelectLocation); } set { _selectLocation = value; } }
        public ICommand RemoveManager { get { return _removeManager ?? new RelayCommand(OnRemoveManager); } set { _removeManager = value; } }
        public ICommand Reset { get { return _reset ?? new RelayCommand(OnReset); } set { _reset = value; } }

        public EmployeeModifierVM(EmployeeConfiguratorVM parent)
        {
            _parent = parent;
        }
        public EmployeeModifierVM(EmployeeConfiguratorVM parent, Employee e): this(parent)
        {
            _originalEmployee = e;
            FillEmployeeFields(e);
        }
        private void FillEmployeeFields(Employee e)
        {
            FirstName = e.First_Name;
            LastName = e.Last_Name;
            Title = e.Job_Title;
            Phone = e.Phone;
            Email = e.Email;
            Manager = e.Manager;
            Location = e.Location;
        }
        public Employee GetEmployee()
        {
            return new Employee
            {
                First_Name = FirstName,
                Last_Name = LastName,
                Email = Email,
                Phone = Phone,
                Job_Title = Title,
                Location = Location,
                Manager = Manager
            };
        }
        private void OnSelectManager(object param){ _parent.OnSelectManager(param); }
        private void OnSelectLocation(object param){ _parent.OnSelectLocation(param); }
        private void OnRemoveManager(object param) { Manager = null; }
        private void OnReset(object param) { if (_originalEmployee != null) FillEmployeeFields(_originalEmployee); }
    }
}
