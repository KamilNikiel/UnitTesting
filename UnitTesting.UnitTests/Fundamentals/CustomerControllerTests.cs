using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTesting.Fundamentals;

namespace UnitTesting.UnitTests.Fundamentals
{
    [TestFixture]
    public class CustomerControllerTests
    {
        private CustomerController _customerController;
        [SetUp]
        public void SetUp()
        {
            _customerController = new CustomerController();
        }
        [Test]
        public void GetCustomer_IdIsZero_ReturnNotFound()
        {
            var result = _customerController.GetCustomer(0);
            //Not Found
            Assert.That(result, Is.TypeOf<NotFound>());

            //Not Found || derivatives
            //Assert.That(result, Is.InstanceOf<NotFound>());
        }
        [Test]
        public void GetCustomer_IdIsZero_ReturnOk()
        {
            var result = _customerController.GetCustomer(1);

            Assert.That(result, Is.TypeOf<Ok>());
        }
    }
}
