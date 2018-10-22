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
    public class ArgsPatserTests
    {
        [Test]
        public void ParsingTest1()
        {
            var args = new string[] 
            {
                @"-firstCmd"
            };
            var argsParser = new ArgsParser(args);

            Assert.IsNotNull(argsParser);
            Assert.AreEqual(1, argsParser.ArgParameterSet.Count);
            Assert.AreEqual(0, argsParser.Commands.Count());
            Assert.AreEqual(1, argsParser.Switches.Count());
            Assert.IsTrue(argsParser.CommandExist("firstCmd"));
            Assert.IsFalse(argsParser.CommandExist("FirstCmd"));
            Assert.IsFalse(argsParser.CommandExist("-firstCmd"));
        }


        [Test]
        public void ParsingTest2()
        {
            var args = new string[]
            {
                "--verbose",
                "--timeout=1000",
                "--src",
                "one",
                "--src",
                "two"
            };
            var argsParser = new ArgsParser(args);

            Assert.IsNotNull(argsParser);
            Assert.AreEqual(6, argsParser.ArgParameterSet.Count);
            Assert.AreEqual(2, argsParser.Commands.Count());
            Assert.AreEqual(4, argsParser.Switches.Count());
            Assert.IsTrue(argsParser.CommandExist("one"));
            Assert.IsTrue(argsParser.CommandExist("two"));
            Assert.IsFalse(argsParser.CommandExist("three"));
            Assert.IsNotNull(argsParser.GetSwitch("verbose"));
            Assert.IsNull(argsParser.GetSwitch("Verbose"));
        }


        [Test]
        public void ParsingTest3()
        {
            var args = new string[]
            {
                @"-cdlFile:""c:\tmp\file.txt""",
            };
            var argsParser = new ArgsParser(args);

            Assert.IsNotNull(argsParser);
            Assert.AreEqual(1, argsParser.ArgParameterSet.Count);
            Assert.AreEqual(0, argsParser.Commands.Count());
            Assert.AreEqual(1, argsParser.Switches.Count());
            Assert.IsNotNull(argsParser.GetSwitch("cdlFile"));
            Assert.AreEqual(@"""c:\tmp\file.txt""", argsParser.GetSwitch("cdlFile")?.Value);
        }
    }
}
