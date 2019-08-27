using Model;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace TestApp
{
    public class EmployeeSelectorVM : VMBase
    {
        private OracleDB _db;
        private string _search_Query;
        private Employee _selectedEmployee;
        private string _selectedEmployeeInfo;
        private List<Employee> _employeeList;


        public string SearchQuery { get { return _search_Query; } set { SetProperty(ref _search_Query, value); } }
        public Employee SelectedEmployee { get { return _selectedEmployee; } set { SetProperty(ref _selectedEmployee, value); } }
        public string SelectedEmployeeInfo { get { return _selectedEmployeeInfo; } set { SetProperty(ref _selectedEmployeeInfo, value); } }
        public List<Employee> EmployeeList { get { return _employeeList; } set { SetProperty(ref _employeeList, value); } }

        public EmployeeSelectorVM(OracleDB db)
        {
            _db = db;
            EmployeeList = _db.Employees.ToList();
            PropertyChanged += RespondToPropertyChanged;
        }

        private void RespondToPropertyChanged(object sender, PropertyChangedEventArgs a)
        {
            if (a.PropertyName == "SelectedEmployee")
            {
                SelectedEmployeeInfo = SelectedEmployee==null ? "" :
                    $"{SelectedEmployee.FullName}\n" +
                    $"Email: {SelectedEmployee.Email}\nLocation: {SelectedEmployee.Location}\n" +
                    $"Phone: {SelectedEmployee.Phone}\n" +
                    $"Hire date: {SelectedEmployee.Hire_Date.ToString("dd-MM-yyyy")}\n" +
                    $"Manager: {SelectedEmployee.Manager?.FullName ?? "None"}";
            }
            else if (a.PropertyName == "SearchQuery")
            {
                if (SearchQuery != "" && SearchQuery != null)
                {
                    EmployeeList = _db.Employees.Where(e => e.First_Name.ToLower().Contains(SearchQuery.ToLower()) || e.Last_Name.ToLower().Contains(SearchQuery.ToLower())).ToList();
                }
                else
                {
                    EmployeeList = _db.Employees.ToList();
                }
            }
        }
    }
}
