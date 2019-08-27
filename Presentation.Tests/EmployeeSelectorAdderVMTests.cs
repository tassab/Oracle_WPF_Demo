using Model;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp;

namespace Presentation.Tests
{
    [TestFixture]
    public class EmployeeSelectorAdderVMTests
    {
        private Mock<OracleDB> GetFakeDatabase()
        {
            var data = new List<Employee>
            {
                new Employee {
                    First_Name = "Test",
                    Last_Name = "Testerson_1",
                },
                new Employee {
                    First_Name = "Test",
                    Last_Name = "Testerson_2",
                },
                new Employee {
                    First_Name = "Test",
                    Last_Name = "Testerson_3",
                },
                new Employee {
                    First_Name = "Manager",
                    Last_Name = "Testerson",
                }
            };
            data[0].Manager = data[3];

            var dataAsQueryable = data.AsQueryable();

            var mockEmployees = new Mock<DbSet<Employee>>();
            mockEmployees.As<IQueryable<Employee>>().Setup(m => m.Provider).Returns(dataAsQueryable.Provider);
            mockEmployees.As<IQueryable<Employee>>().Setup(m => m.Expression).Returns(dataAsQueryable.Expression);
            mockEmployees.As<IQueryable<Employee>>().Setup(m => m.ElementType).Returns(dataAsQueryable.ElementType);
            mockEmployees.As<IQueryable<Employee>>().Setup(m => m.GetEnumerator()).Returns(dataAsQueryable.GetEnumerator());

            var mockDB = new Mock<OracleDB>();
            mockDB.Setup(m => m.Employees).Returns(mockEmployees.Object);

            return mockDB;
        }
        [Test]
        public void GetsAllEmployees()
        {
            var mockParent = new Mock<IEmployeeSelectorAdderVMParent>();
            var vm = new EmployeeSelectorAdderVM(mockParent.Object, GetFakeDatabase().Object);
            var empList = vm.EmployeeList;

            Assert.That(empList.Count, Is.EqualTo(4));
        }
        [Test]
        public void FindsManagerBySearch()
        {
            var mockParent = new Mock<IEmployeeSelectorAdderVMParent>();
            var vm = new EmployeeSelectorAdderVM(mockParent.Object, GetFakeDatabase().Object);
            vm.SearchQuery = "Mana";
            var empList = vm.EmployeeList;

            Assert.That(empList.Count, Is.EqualTo(1));
            Assert.That(empList.FirstOrDefault().FullName, Is.EqualTo("Manager Testerson"));
        }
        [Test]
        public void CallsRemove()
        {
            var mockParent = new Mock<IEmployeeSelectorAdderVMParent>();
            var vm = new EmployeeSelectorAdderVM(mockParent.Object, GetFakeDatabase().Object);
            var employeeToDelete = vm.EmployeeList.FirstOrDefault();


            vm.SelectedEmployee = employeeToDelete;
            vm.RemoveEmployee.Execute(null);

            mockParent.Verify(m => m.OnRemoveEmployee(It.IsAny<object>()), Times.Once);
        }
    }
}
