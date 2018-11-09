using Dtc.Common.Extensions;
using NUnit.Framework;

namespace Dtc.Common.Tests.Extensions
{
    [TestFixture]
    public class ArgsPatserTests
    {
        [Test]
        public void RemoveEndTextTo_Tests()
        {
            string test = null;
            var result = test.RemoveEndTextTo('x');
            Assert.IsNull(result);

            test = string.Empty;
            result = test.RemoveEndTextTo('x');
            Assert.AreEqual(test, result);

            test = "werdtwrt354t3ft5f";
            result = test.RemoveEndTextTo('|');
            Assert.AreEqual(test, result);

            test = "abc|";
            result = test.RemoveEndTextTo('|');
            Assert.AreEqual("abc", result);

            test = "|abc|";
            result = test.RemoveEndTextTo('|');
            Assert.AreEqual("|abc", result);

            test = "a|bc|";
            result = test.RemoveEndTextTo('|');
            Assert.AreEqual("a|bc", result);

            test = "a|bc||";
            result = test.RemoveEndTextTo('|');
            Assert.AreEqual("a|bc|", result);

            test = "|";
            result = test.RemoveEndTextTo('|');
            Assert.AreEqual("", result);

            test = "||";
            result = test.RemoveEndTextTo('|');
            Assert.AreEqual("|", result);
        }


        [Test]
        public void RemoveStartTextTo_Tests()
        {
            string test = null;
            var result = test.RemoveStartTextTo('x');
            Assert.IsNull(result);

            test = string.Empty;
            result = test.RemoveStartTextTo('x');
            Assert.AreEqual(test, result);

            test = "werdtwrt354t3ft5f";
            result = test.RemoveStartTextTo('|');
            Assert.AreEqual(test, result);

            test = "abc|";
            result = test.RemoveStartTextTo('|');
            Assert.AreEqual("", result);

            test = "|abc|";
            result = test.RemoveStartTextTo('|');
            Assert.AreEqual("abc|", result);

            test = "||abc|";
            result = test.RemoveStartTextTo('|');
            Assert.AreEqual("|abc|", result);

            test = "a|bc|";
            result = test.RemoveStartTextTo('|');
            Assert.AreEqual("bc|", result);

            test = "bc||";
            result = test.RemoveStartTextTo('|');
            Assert.AreEqual("|", result);

            test = "|";
            result = test.RemoveStartTextTo('|');
            Assert.AreEqual("", result);

            test = "||";
            result = test.RemoveStartTextTo('|');
            Assert.AreEqual("|", result);
        }
    }
}
