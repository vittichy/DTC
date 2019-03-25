using Dtc.Common.Extensions;
using NUnit.Framework;
using System.Linq;

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
            var result = test.SusbstrFromToChar(' ', ':');
            Assert.IsNull(result);

            test = string.Empty;
            result = test.SusbstrFromToChar(' ', ':');
            Assert.IsNull(result);

            test = "AB";
            result = test.SusbstrFromToChar('A', 'B');
            Assert.AreEqual("AB", result);

            result = test.SusbstrFromToChar('A', 'C');
            Assert.AreEqual("AB", result);

            result = test.SusbstrFromToChar('X', 'B');
            Assert.AreEqual("AB", result);

            result = test.SusbstrFromToChar('Z', 'Z');
            Assert.AreEqual("AB", result);

            // parsovani CDL
            // asi nejslozitejsi priklad:
            // viewmodel PayrollAbsenceViewModel : PayrollAbsence [parent = payroll]

            test = "viewmodel PayrollAbsenceViewModel : PayrollAbsence [parent = payroll]";
            result = test.SusbstrFromToChar(' ', ':');
            Assert.IsNotNull(result);
            Assert.AreEqual(" PayrollAbsenceViewModel :", result);

            result = test.SusbstrFromToChar(':', '[');
            Assert.IsNotNull(result);
            Assert.AreEqual(": PayrollAbsence [", result);

            result = test.SusbstrFromToChar('[', ']');
            Assert.IsNotNull(result);
            Assert.AreEqual("[parent = payroll]", result);

            // service FinanceService
            // concept ReceivedInvoice: BookkeepingDocument
            // concept VatBreakDownSheetReceivedInvoice[parent = receivedInvoice]
            // concept CarLog[parent = car]
            // concept CarDrivingSettings[settings]
            // concept ECar : Car
            // concept PayrollTaxAllowance [parent = payroll, audit = no]    

        }


        /// <summary>
        /// SusbstrToChars
        /// </summary>
        [Test]
        public void SusbstrToAnyChars_Tests()
        {
            string test = null;
            var result = test.SusbstrToChars();
            Assert.IsEmpty(result);

            result = test.SusbstrToChars(' ');
            Assert.IsEmpty(result);

            result = test.SusbstrToChars(' ', ':', 'A');
            Assert.IsEmpty(result);

            test = string.Empty;
            result = test.SusbstrToChars();
            Assert.IsEmpty(result);

            result = test.SusbstrToChars(' ');
            Assert.IsEmpty(result);

            result = test.SusbstrToChars(' ', ':', 'A');
            Assert.IsEmpty(result);

            test = "ABCDE";
            result = test.SusbstrToChars('A');
            Assert.AreEqual("", result);

            result = test.SusbstrToChars('A', 'C');
            Assert.AreEqual("", result);

            result = test.SusbstrToChars('C', 'C', 'D');
            Assert.AreEqual("AB", result);

            result = test.SusbstrToChars('X', 'D', 'C', 'Y');
            Assert.AreEqual("ABC", result);

            result = test.SusbstrToChars('X', 'E', 'C', 'Y');
            Assert.AreEqual("ABCD", result);

            result = test.SusbstrToChars('X', 'Y');
            Assert.IsEmpty(result);
        }



        /// <summary>
        /// SusbstrTo
        /// </summary>
        [Test]
        public void SusbstrTo_CHR_Tests()
        {
            string test = null;
            var result = test.SusbstrTo(' ');
            Assert.IsEmpty(result);

            result = test.SusbstrTo('A');
            Assert.IsEmpty(result);

            test = "A";

            result = test.SusbstrTo(' ');
            Assert.IsEmpty(result);

            result = test.SusbstrTo('A');
            Assert.IsEmpty(result);

            test = "ABCDE";

            result = test.SusbstrTo(' ');
            Assert.IsEmpty(result);

            result = test.SusbstrTo('A');
            Assert.IsEmpty(result);

            result = test.SusbstrTo('B');
            Assert.AreEqual("A", result);

            result = test.SusbstrTo('C');
            Assert.AreEqual("AB", result);

            result = test.SusbstrTo('E');
            Assert.AreEqual("ABCD", result);
        }


        /// <summary>
        /// SusbstrTo - str
        /// </summary>
        [Test]
        public void SusbstrTo_STR_Tests()
        {
            string test = null;
            var result = test.SusbstrTo(null);
            Assert.IsEmpty(result);

            result = test.SusbstrTo(string.Empty);
            Assert.IsEmpty(result);

            result = test.SusbstrTo("A");
            Assert.IsEmpty(result);

            test = "A";

            result = test.SusbstrTo(" ");
            Assert.IsEmpty(result);

            result = test.SusbstrTo("A");
            Assert.IsEmpty(result);

            test = "ABCDE";

            result = test.SusbstrTo(" ");
            Assert.IsEmpty(result);

            result = test.SusbstrTo("---");
            Assert.IsEmpty(result);

            result = test.SusbstrTo("A");
            Assert.IsEmpty(result);

            result = test.SusbstrTo("ABC");
            Assert.IsEmpty(result);

            result = test.SusbstrTo("B");
            Assert.AreEqual("A", result);

            result = test.SusbstrTo("BC");
            Assert.AreEqual("A", result);

            result = test.SusbstrTo("C");
            Assert.AreEqual("AB", result);

            result = test.SusbstrTo("CDE");
            Assert.AreEqual("AB", result);

            result = test.SusbstrTo("E");
            Assert.AreEqual("ABCD", result);

            result = test.SusbstrTo("DE");
            Assert.AreEqual("ABC", result);
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
        public void SusbstrFrom_Tests()
        {
            string test = "ABCDE";

            var result = test.SusbstrFrom("BCD");
            Assert.AreEqual("E", result);

            result = test.SusbstrFrom("A");
            Assert.AreEqual("BCDE", result);

            result = test.SusbstrFrom("B");
            Assert.AreEqual("CDE", result);

            result = test.SusbstrFrom("E");
            Assert.AreEqual("", result);

            result = test.SusbstrFrom("DE");
            Assert.AreEqual("", result);

            result = test.SusbstrFrom("ABCDE");
            Assert.AreEqual("", result);

            result = test.SusbstrFrom(null);
            Assert.AreEqual("", result);

            result = test.SusbstrFrom(string.Empty);
            Assert.AreEqual("", result);

            result = test.SusbstrFrom("XXXXXX");
            Assert.AreEqual("", result);
        }
    }
}