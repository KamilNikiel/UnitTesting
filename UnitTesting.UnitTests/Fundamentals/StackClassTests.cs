using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTesting.Fundamentals;

namespace UnitTesting.UnitTests.Fundamentals
{
    [TestFixture]
    public class StackClassTests
    {
        private StackClass<object> _stackClass;
        [SetUp]
        public void SetUp()
        {
            _stackClass = new StackClass<object>();
        }
        [Test]
        public void Push_ArgumentIsNull_ThrowsArgumentNullExeption()
        {
            Assert.That(() => _stackClass.Push(null), Throws.TypeOf<ArgumentNullException>());
        }
        [Test]
        public void Push_WhenCalled_AddsNewObject()
        {
            _stackClass.Push("test");

            Assert.That(_stackClass.Count, Is.EqualTo(1));
            Assert.That(_stackClass.Peek(), Is.EqualTo("test"));
        }
        [Test]
        public void Count_EmptyStack_ReturnsZero()
        {
            Assert.That(_stackClass.Count, Is.Zero);
        }
        [Test]
        public void Pop_EmptyStack_ThrowsInvalidOperationException()
        {
            Assert.That(() => _stackClass.Pop(), Throws.TypeOf<InvalidOperationException>());
        }
        [Test]
        public void Pop_TwoArgumentsAddedByPush_ReturnsObjectOnTheTopOfTheStackAndRemoveIt()
        {
            _stackClass.Push("first");
            _stackClass.Push("second");
            var result = _stackClass.Pop();

            Assert.That(_stackClass.Count, Is.EqualTo(1));
            Assert.That(result, Is.EqualTo("second"));
        }
        [Test]
        public void Peek_WhenCalled_ReturnsObjectOnTheTopOfTheStack()
        {
            _stackClass.Push("first");
            _stackClass.Push("second");
            var result = _stackClass.Peek();

            Assert.That(_stackClass.Count, Is.EqualTo(2));
            Assert.That(result, Is.EqualTo("second"));
        }
        [Test]
        public void Peek_WhenCalled_ThrowsInvalidOperationException()
        {
            Assert.That(() => _stackClass.Peek(), Throws.TypeOf<InvalidOperationException>());
        }
    }
}
