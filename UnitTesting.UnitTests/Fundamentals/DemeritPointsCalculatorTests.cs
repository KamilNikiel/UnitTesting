using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTesting.Fundamentals;

namespace UnitTesting.UnitTests.Fundamentals
{
    [TestFixture]
    public class DemeritPointsCalculatorTests
    {
        private DemeritPointsCalculator _demeritPointsCalculator;
        [SetUp]
        public void SetUp()
        {
            _demeritPointsCalculator = new DemeritPointsCalculator();
        }
        [Test]
        [TestCase(-10)]
        [TestCase(400)]
        public void CalculateDemeritPoints_WhenCalled_ReturnArgumentOutOfRangeExeption(int a)
        {
            Assert.That(() => _demeritPointsCalculator.CalculateDemeritPoints(a),
                Throws.TypeOf<ArgumentOutOfRangeException>());
        }
        [Test]
        [TestCase(60, 0)]
        [TestCase(80, 3)]
        public void CalculateDemeritPoints_WhenCalled_ReturnDemeritPoints(int a, int expectedResult)
        {
            var result = _demeritPointsCalculator.CalculateDemeritPoints(a);

            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
