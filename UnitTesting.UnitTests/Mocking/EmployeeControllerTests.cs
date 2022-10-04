using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTesting.Mocking;

namespace UnitTesting.UnitTests.Mocking
{
    internal class EmployeeControllerTests
    {
        private EmployeeController _employeeController;
        private Mock<IEmployeeStorage> _employeeStorage;

        [SetUp]
        public void SetUp()
        {
            _employeeStorage = new Mock<IEmployeeStorage>();
            _employeeController = new EmployeeController(_employeeStorage.Object);
        }

        [Test]
        public void DeleteEmployee_WhenCalled_DeleteTheEmployeeFromDatabase()
        {
            _employeeController.DeleteEmployee(1);

            _employeeStorage.Verify(es => es.DeleteEmployee(1));
        }
        [Test]
        public void DeleteEmployee_WhenCalled_RedirectsToAction()
        {
            Assert.That(_employeeController.DeleteEmployee(1), Is.InstanceOf<ActionResult>());
        }
    }
}
