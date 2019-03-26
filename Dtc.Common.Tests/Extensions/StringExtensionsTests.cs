using Dtc.Common.Extensions;
using NUnit.Framework;
using System.Linq;

namespace Dtc.Common.Tests.Extensions
{
    [TestFixture]
    public class StringExtensionsTests
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


        [Test]
        public void ExtendToLength_Tests()
        {
            string test = null;
            var result = test.ExtendToLength(10, '=');
            Assert.IsNotNull(result);
            Assert.AreEqual(10, result.Length);
            Assert.IsTrue(result.All(p => p == '='));

            test = string.Empty;
            result = test.ExtendToLength(10, '=');
            Assert.IsNotNull(result);
            Assert.AreEqual(10, result.Length);
            Assert.IsTrue(result.All(p => p == '='));

            test = "a";
            result = test.ExtendToLength(10, '=');
            Assert.IsNotNull(result);
            Assert.AreEqual(10, result.Length);
            Assert.IsTrue(result.Remove(0, test.Length).All(p => p == '='));

            test = "ab";
            result = test.ExtendToLength(10, '=');
            Assert.IsNotNull(result);
            Assert.AreEqual(10, result.Length);
            Assert.IsTrue(result.Remove(0, test.Length).All(p => p == '='));

            test = "ababababababababababab";
            result = test.ExtendToLength(10, '=');
            Assert.IsNotNull(result);
            Assert.AreEqual(test, result);
        }


        /// <summary>
        /// </summary>
        [Test]
        public void SusbstrFromToChar_Tests()
        {
            string test = null;
            var result = test.SubstrFromToChar(' ', ':');
            Assert.IsNull(result);

            test = string.Empty;
            result = test.SubstrFromToChar(' ', ':');
            Assert.IsNull(result);

            test = "AB";
            result = test.SubstrFromToChar('A', 'B');
            Assert.AreEqual("AB", result);

            result = test.SubstrFromToChar('A', 'C');
            Assert.AreEqual("AB", result);

            result = test.SubstrFromToChar('X', 'B');
            Assert.AreEqual("AB", result);

            result = test.SubstrFromToChar('Z', 'Z');
            Assert.AreEqual("AB", result);

            // parsovani CDL
            // asi nejslozitejsi priklad:
            // viewmodel PayrollAbsenceViewModel : PayrollAbsence [parent = payroll]

            test = "viewmodel PayrollAbsenceViewModel : PayrollAbsence [parent = payroll]";
            result = test.SubstrFromToChar(' ', ':');
            Assert.IsNotNull(result);
            Assert.AreEqual(" PayrollAbsenceViewModel :", result);

            result = test.SubstrFromToChar(':', '[');
            Assert.IsNotNull(result);
            Assert.AreEqual(": PayrollAbsence [", result);

            result = test.SubstrFromToChar('[', ']');
            Assert.IsNotNull(result);
            Assert.AreEqual("[parent = payroll]", result);
        }


        /// <summary>
        /// SusbstrToChars
        /// </summary>
        [Test]
        public void SusbstrToAnyChars_Tests()
        {
            string test = null;
            var result = test.SubstrToChars();
            Assert.IsEmpty(result);

            result = test.SubstrToChars(' ');
            Assert.IsEmpty(result);

            result = test.SubstrToChars(' ', ':', 'A');
            Assert.IsEmpty(result);

            test = string.Empty;
            result = test.SubstrToChars();
            Assert.IsEmpty(result);

            result = test.SubstrToChars(' ');
            Assert.IsEmpty(result);

            result = test.SubstrToChars(' ', ':', 'A');
            Assert.IsEmpty(result);

            test = "ABCDE";
            result = test.SubstrToChars('A');
            Assert.IsEmpty(result);

            result = test.SubstrToChars('A', 'C');
            Assert.AreEqual("", result);

            result = test.SubstrToChars('C', 'C', 'D');
            Assert.AreEqual("AB", result);

            result = test.SubstrToChars('X', 'D', 'C', 'Y');
            Assert.AreEqual("ABC", result);

            result = test.SubstrToChars('X', 'E', 'C', 'Y');
            Assert.AreEqual("ABCD", result);

            result = test.SubstrToChars('X', 'Y');
            Assert.AreEqual(test, result);

            result = test.SubstrToChars('C', 'X', 'Y');
            Assert.AreEqual("AB", result);
        }



        /// <summary>
        /// SusbstrTo
        /// </summary>
        [Test]
        public void SusbstrTo_CHR_Tests()
        {
            string test = null;
            var result = test.SubstrTo(' ');
            Assert.IsEmpty(result);

            result = test.SubstrTo('A');
            Assert.IsEmpty(result);

            test = "A";

            result = test.SubstrTo(' ');
            Assert.AreEqual(test, result);

            result = test.SubstrTo('A');
            Assert.IsEmpty(result);

            test = "ABCDE";

            result = test.SubstrTo(' ');
            Assert.AreEqual(test, result);

            result = test.SubstrTo('A');
            Assert.IsEmpty(result);

            result = test.SubstrTo('B');
            Assert.AreEqual("A", result);

            result = test.SubstrTo('C');
            Assert.AreEqual("AB", result);

            result = test.SubstrTo('E');
            Assert.AreEqual("ABCD", result);
        }


        /// <summary>
        /// SusbstrTo - str
        /// </summary>
        [Test]
        public void SusbstrTo_STR_Tests()
        {
            string test = null;
            var result = test.SubstrTo(null);
            Assert.IsEmpty(result);

            result = test.SubstrTo(string.Empty);
            Assert.IsEmpty(result);

            result = test.SubstrTo("A");
            Assert.IsEmpty(result);

            test = "A";

            result = test.SubstrTo(" ");
            Assert.AreEqual(test, result);

            result = test.SubstrTo("A");
            Assert.IsEmpty(result);

            test = "ABCDE";

            result = test.SubstrTo(" ");
            Assert.AreEqual(test, result);

            result = test.SubstrTo("---");
            Assert.AreEqual(test, result);

            result = test.SubstrTo("A");
            Assert.IsEmpty(result);

            result = test.SubstrTo("ABC");
            Assert.IsEmpty(result);

            result = test.SubstrTo("B");
            Assert.AreEqual("A", result);

            result = test.SubstrTo("BC");
            Assert.AreEqual("A", result);

            result = test.SubstrTo("C");
            Assert.AreEqual("AB", result);

            result = test.SubstrTo("CDE");
            Assert.AreEqual("AB", result);

            result = test.SubstrTo("E");
            Assert.AreEqual("ABCD", result);

            result = test.SubstrTo("DE");
            Assert.AreEqual("ABC", result);

            result = test.SubstrTo("ABCDE");
            Assert.IsEmpty(result);
        }


        /// <summary>
        /// Split2Half
        /// </summary>
        [Test]
        public void Split2Half_CHR_Tests()
        {
            string test = null;

            var result = test.Split2Half(' ');
            Assert.IsNotNull(result);
            Assert.IsEmpty(result.Item1);
            Assert.IsEmpty(result.Item2);

            test = "";
            result = test.Split2Half(' ');
            Assert.IsNotNull(result);
            Assert.IsEmpty(result.Item1);
            Assert.IsEmpty(result.Item2);

            test = " ";
            result = test.Split2Half(' ');
            Assert.IsNotNull(result);
            Assert.IsEmpty(result.Item1);
            Assert.IsEmpty(result.Item2);

            test = "ABCDE";
            result = test.Split2Half(' ');
            Assert.IsNotNull(result);
            Assert.AreEqual("ABCDE", result.Item1);
            Assert.IsEmpty(result.Item2);

            result = test.Split2Half('C');
            Assert.IsNotNull(result);
            Assert.AreEqual("AB", result.Item1);
            Assert.AreEqual("DE", result.Item2);

            result = test.Split2Half('A');
            Assert.IsNotNull(result);
            Assert.AreEqual("", result.Item1);
            Assert.AreEqual("BCDE", result.Item2);

            result = test.Split2Half('E');
            Assert.IsNotNull(result);
            Assert.AreEqual("ABCD", result.Item1);
            Assert.AreEqual("", result.Item2);
        }


        /// <summary>
        /// Split2Half
        /// </summary>
        [Test]
        public void Split2Half_STR_Tests()
        {
            string test = null;

            var result = test.Split2Half(" ");
            Assert.IsNotNull(result);
            Assert.IsEmpty(result.Item1);
            Assert.IsEmpty(result.Item2);

            test = "";
            result = test.Split2Half(" ");
            Assert.IsNotNull(result);
            Assert.IsEmpty(result.Item1);
            Assert.IsEmpty(result.Item2);

            test = " ";
            result = test.Split2Half(" ");
            Assert.IsNotNull(result);
            Assert.IsEmpty(result.Item1);
            Assert.IsEmpty(result.Item2);

            test = "ABCDE";
            result = test.Split2Half(" ");
            Assert.IsNotNull(result);
            Assert.AreEqual("ABCDE", result.Item1);
            Assert.IsEmpty(result.Item2);

            result = test.Split2Half("C");
            Assert.IsNotNull(result);
            Assert.AreEqual("AB", result.Item1);
            Assert.AreEqual("DE", result.Item2);

            result = test.Split2Half("A");
            Assert.IsNotNull(result);
            Assert.AreEqual("", result.Item1);
            Assert.AreEqual("BCDE", result.Item2);

            result = test.Split2Half("E");
            Assert.IsNotNull(result);
            Assert.AreEqual("ABCD", result.Item1);
            Assert.AreEqual("", result.Item2);

            result = test.Split2Half("BCD");
            Assert.IsNotNull(result);
            Assert.AreEqual("A", result.Item1);
            Assert.AreEqual("E", result.Item2);

            result = test.Split2Half("ABCDE");
            Assert.IsNotNull(result);
            Assert.AreEqual("", result.Item1);
            Assert.AreEqual("", result.Item2);

        }


        /// <summary>
        /// SusbstrFrom
        /// </summary>
        [Test]
        public void SubstrFrom_Tests()
        {
            string test = "ABCDE";

            var result = test.SubstrFrom("BCD");
            Assert.AreEqual("E", result);

            result = test.SubstrFrom("A");
            Assert.AreEqual("BCDE", result);

            result = test.SubstrFrom("B");
            Assert.AreEqual("CDE", result);

            result = test.SubstrFrom("E");
            Assert.AreEqual("", result);

            result = test.SubstrFrom("DE");
            Assert.AreEqual("", result);

            result = test.SubstrFrom("ABCDE");
            Assert.AreEqual("", result);

            result = test.SubstrFrom(null);
            Assert.AreEqual("", result);

            result = test.SubstrFrom(string.Empty);
            Assert.AreEqual("", result);

            result = test.SubstrFrom("XXXXXX");
            Assert.AreEqual("", result);
        }
    }
}