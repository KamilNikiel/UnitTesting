using UnitTesting.Fundamentals;
using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace UnitTesting.UnitTests.Fundamentals
{
    [TestFixture]
    internal class MathClassTests
    {
        private MathClass _math;
        [SetUp]
        public void SetUp()
        {
            _math = new MathClass();
        }
        [Test]
        //[Ignore("Because I wanted to.")]
        public void Add_WhenCalled_ReturnsSumOfArguments()
        {
            var result = _math.Add(1, 2);

            Assert.That(result, Is.EqualTo(1 + 2));
        }
        [Test]
        [TestCase(2, 1, 2)]
        [TestCase(1, 2, 2)]
        [TestCase(1, 1, 1)]
        public void Max_WhenCalled_ReturnTheGreaterArgument(int a, int b, int expectedResult)
        {
            var result = _math.Max(a, b);

            Assert.That(result, Is.EqualTo(expectedResult));
        }
        [Test]
        [TestCase(5, new[] { 1, 3, 5 })]
        [TestCase(-5, new[] { -5, -3, -1 })]
        [TestCase(0, new int[0])]
        public void GetOddNumbers_WhenCalled_RetuenOddNumbersUpToLimit(int a, IEnumerable<int> expectedResult)
        {
            var result = _math.GetOddNumbers(a);

            //Assert.That(result, Is.Not.Empty);

            //Assert.That(result.Count(), Is.EqualTo(3));

            //Assert.That(result, Does.Contain(1));
            //Assert.That(result, Does.Contain(3));
            //Assert.That(result, Does.Contain(5));

            Assert.That(result, Is.EquivalentTo(expectedResult));

            //Assert.That(result, Is.Ordered);
            //Assert.That(result, Is.Unique);
        }
    }
}
