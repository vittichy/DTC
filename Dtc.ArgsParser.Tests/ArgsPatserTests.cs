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
            Assert.AreEqual(@"c:\tmp\file.txt", argsParser.GetSwitch("cdlFile")?.Value);
        }


        [Test]
        public void ParsingTest4MutliValues()
        {
            var args = new string[]
            {
                @"/size:300,444444,aaaa,!!!,@",
            };
            var argsParser = new ArgsParser(args);

            Assert.IsNotNull(argsParser);
            Assert.AreEqual(1, argsParser.ArgParameterSet.Count);
            Assert.AreEqual(0, argsParser.Commands.Count());
            Assert.AreEqual(1, argsParser.Switches.Count());
            var sizeSwitch = argsParser.GetSwitch("size");
            Assert.IsNotNull(sizeSwitch);
            Assert.AreEqual("300,444444,aaaa,!!!,@", sizeSwitch.Value);
            Assert.AreEqual(5, sizeSwitch.Values.Count());
            Assert.AreEqual("300", sizeSwitch.Values[0]);
            Assert.AreEqual("444444", sizeSwitch.Values[1]);
            Assert.AreEqual("aaaa", sizeSwitch.Values[2]);
            Assert.AreEqual("!!!", sizeSwitch.Values[3]);
            Assert.AreEqual("@", sizeSwitch.Values[4]);
        }


        [Test]
        public void ParsingTest5()
        {
            var args = new string[]
            {
                @"/size:444444",
            };
            var argsParser = new ArgsParser(args);

            Assert.IsNotNull(argsParser);
            Assert.AreEqual(1, argsParser.ArgParameterSet.Count);
            Assert.AreEqual(0, argsParser.Commands.Count());
            Assert.AreEqual(1, argsParser.Switches.Count());
            var sizeSwitch = argsParser.GetSwitch("size");
            Assert.IsNotNull(sizeSwitch);
            Assert.AreEqual("444444", sizeSwitch.Value);
            Assert.AreEqual("444444", argsParser.GetSwitchValue("size"));
        }

            
        [Test]
        public void ParsingTest6()
        {
            var args = new string[]
            {
                @"-file:""C:\_C1Sources\C1\Services\FinanceService,a:\;""e:\___""""",
            };
            var argsParser = new ArgsParser(args);

            Assert.IsNotNull(argsParser);
            Assert.AreEqual(1, argsParser.ArgParameterSet.Count);
            Assert.AreEqual(0, argsParser.Commands.Count());
            Assert.AreEqual(1, argsParser.Switches.Count());
            var fileSwitch = argsParser.GetSwitch("file");
            Assert.IsNotNull(fileSwitch);
            Assert.IsNotNull(fileSwitch.Values);
            Assert.AreEqual(3, fileSwitch.Values.Count());
            Assert.AreEqual(@"C:\_C1Sources\C1\Services\FinanceService", fileSwitch.Values[0]);
            Assert.AreEqual(@"a:\", fileSwitch.Values[1]);
            Assert.AreEqual(@"""e:\___""", fileSwitch.Values[2]);
        }


    }
}
