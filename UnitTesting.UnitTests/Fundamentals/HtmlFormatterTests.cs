using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTesting.Fundamentals;

namespace UnitTesting.UnitTests.Fundamentals
{
    [TestFixture]
    public class HtmlFormatterTests
    {
        [Test]
        public void FormatAsBold_WhenCalled_ShouldEncloseTheStringWithStrongElement()
        {
            var formatter = new HtmlFormatter();

            var result = formatter.FormatAsBold("test");

            //Specific
            Assert.That(result, Is.EqualTo("<strong>test</strong>").IgnoreCase);

            //More general
            //Assert.That(result, Does.StartWith("<strong>"));
            //Assert.That(result, Does.EndWith("</strong>"));
            //Assert.That(result, Does.Contain("test"));
        }
    }
}
