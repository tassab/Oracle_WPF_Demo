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
        [Test]
        public void GetsAllEmployees()
        {
            var mockParent = new Mock<IEmployeeSelectorAdderVMParent>();
            var vm = new EmployeeSelectorAdderVM(mockParent.Object, FakeDatabase.GetDB().Object);
            var empList = vm.EmployeeList;

            Assert.That(empList.Count, Is.EqualTo(4));
        }
        [Test]
        public void FindsManagerBySearch()
        {
            var mockParent = new Mock<IEmployeeSelectorAdderVMParent>();
            var vm = new EmployeeSelectorAdderVM(mockParent.Object, FakeDatabase.GetDB().Object);
            vm.SearchQuery = "Mana";
            var empList = vm.EmployeeList;

            Assert.That(empList.Count, Is.EqualTo(1));
            Assert.That(empList.FirstOrDefault().FullName, Is.EqualTo("Manager Testerson"));
        }
        [Test]
        public void CallsRemove()
        {
            var mockParent = new Mock<IEmployeeSelectorAdderVMParent>();
            var vm = new EmployeeSelectorAdderVM(mockParent.Object, FakeDatabase.GetDB().Object);
            var employeeToDelete = vm.EmployeeList.FirstOrDefault();


            vm.SelectedEmployee = employeeToDelete;
            vm.RemoveEmployee.Execute(null);

            mockParent.Verify(m => m.OnRemoveEmployee(It.IsAny<object>()), Times.Once);
        }
    }
}
