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
            Assert.IsNull(result);

            result = test.SusbstrToChars(' ');
            Assert.IsNull(result);

            result = test.SusbstrToChars(' ', ':', 'A');
            Assert.IsNull(result);

            test = string.Empty;
            result = test.SusbstrToChars();
            Assert.IsNull(result);

            result = test.SusbstrToChars(' ');
            Assert.IsNull(result);

            result = test.SusbstrToChars(' ', ':', 'A');
            Assert.IsNull(result);

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
            Assert.IsNull(result);
        }
    }
}