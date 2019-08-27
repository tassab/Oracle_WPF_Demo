using Model;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp;

namespace Presentation.Tests
{
    [TestFixture]
    class EmployeeConfiguratorTests
    {
        [Test]
        public void CallsDelete()
        {
            var fakeDB = FakeDatabase.GetDB();
            var fakeMsgService = new Mock<IMessageDialogService>();
            fakeMsgService.Setup(s => s.ShowYesNoDialog(It.IsAny<string>(), It.IsAny<string>())).Returns(MessageDialogResult.Yes);
            var vm = new EmployeeConfiguratorVM(new Mock<IEmployeeConfiguratorVMParent>().Object, fakeDB.Object, fakeMsgService.Object);

            var empSelAdd = vm.Content as EmployeeSelectorAdderVM;
            var employeeToDelete = empSelAdd.EmployeeList.FirstOrDefault();
            empSelAdd.SelectedEmployee = employeeToDelete;
            empSelAdd.RemoveEmployee.Execute(null);

            fakeMsgService.Verify(s => s.ShowYesNoDialog(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            fakeDB.Verify(e => e.Employees.Remove(It.Is<Employee>(p => p.FullName == employeeToDelete.FullName)), Times.Once);
        }
        [Test]
        public void DoesNotCallDeleteWhenNoIsSelected()
        {
            var fakeDB = FakeDatabase.GetDB();
            var fakeMsgService = new Mock<IMessageDialogService>();
            fakeMsgService.Setup(s => s.ShowYesNoDialog(It.IsAny<string>(), It.IsAny<string>())).Returns(MessageDialogResult.No);
            var vm = new EmployeeConfiguratorVM(new Mock<IEmployeeConfiguratorVMParent>().Object, fakeDB.Object, fakeMsgService.Object);

            var empSelAdd = vm.Content as EmployeeSelectorAdderVM;
            var employeeToDelete = empSelAdd.EmployeeList.FirstOrDefault();
            empSelAdd.SelectedEmployee = employeeToDelete;
            empSelAdd.RemoveEmployee.Execute(null);

            fakeMsgService.Verify(s => s.ShowYesNoDialog(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            fakeDB.Verify(e => e.Employees.Remove(It.Is<Employee>(p => p.FullName == employeeToDelete.FullName)), Times.Never);
        }
    }
}
