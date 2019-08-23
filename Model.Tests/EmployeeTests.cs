using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Tests
{
    [TestFixture]
    public class EmployeeTests
    {
        [Test]
        public void ReturnsFullName()
        {
            var sut = new Employee()
            {
                First_Name = "Espen",
                Last_Name = "Bjørnsen"
            };

            Assert.That(sut.FullName, Is.EqualTo("Espen Bjørnsen"));


        }
    }
}