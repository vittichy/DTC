using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtc.IO.Tests
{
    [TestFixture]
    public class Test1
    {
        [Test]
        public void PassingTest()
        {
            Assert.AreEqual(4, 4);
        }
    }
}
