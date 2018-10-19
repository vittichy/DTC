using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using NUnit.Framework;

namespace Dtc.ArgsParser.Tests
{
    [TestFixture]
    public class Test1
    {
        [Test]
        public void PassingTest()
        {
            Assert.AreEqual(4, Add(2, 2));
        }

        [Test]
        public void FailingTest()
        {
            Assert.AreEqual(5, Add(2, 2));
        }


        int Add(int x, int y)
        {
            return x + y;
        }
    }
}
