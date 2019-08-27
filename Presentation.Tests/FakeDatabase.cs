using Model;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Tests
{
    public static class FakeDatabase
    {
        public static Mock<OracleDB> GetDB()
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
    }
}
