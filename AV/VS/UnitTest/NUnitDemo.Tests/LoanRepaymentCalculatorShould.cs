using System;
using System.Collections.Generic;
using System.Text;
using Demo.Domain.Applications;
using NUnit.Framework;

namespace NUnitDemo.Tests
{
    [Category("Loan RePayment Calculator Comparison")]
    public class LoanRepaymentCalculatorShould
    {
        private LoanRepaymentCalculator sut;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            sut = new LoanRepaymentCalculator();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            sut = null;
        }

        [Test]
        [TestCase(200_000, 6.5, 30, ExpectedResult = 1264.14)] // Test case #1
        [TestCase(200_000, 10, 30, ExpectedResult = 1755.14)] // Test case #2
        [TestCase(500_000, 10, 30, ExpectedResult = 4387.86)] // Test case #3
        public decimal CalculateCorrectMonthlyRepayment_SimplifiedTestCase(decimal principal,
                                                     decimal interestRate,
                                                     int termInYears)
        {
            return sut.CalculateMonthlyRepayment(new LoanAmount("USD", principal),
                                                               interestRate,
                                                               new LoanTerm(termInYears));
        }

        [Test]
        [TestCaseSource(typeof(MonthlyRepaymentTestData), "TestCases")]
        public void CalculateCorrectMonthlyRepayment_Centralized(decimal principal,
                                                     decimal interestRate,
                                                     int termInYears,
                                                     decimal expectedMonthlyPayment)
        {
            var monthlyPayment = sut.CalculateMonthlyRepayment(new LoanAmount("USD", principal),
                                                               interestRate,
                                                               new LoanTerm(termInYears));

            Assert.That(monthlyPayment, Is.EqualTo(expectedMonthlyPayment));
        }

        [Test]
        [TestCaseSource(typeof(MonthlyRepaymentTestDataWithReturn), "TestCases")]
        public decimal CalculateCorrectMonthlyRepayment_CentralizedWithReturn(decimal principal,
                                                     decimal interestRate,
                                                     int termInYears)
        {
            return sut.CalculateMonthlyRepayment(new LoanAmount("USD", principal),
                                                               interestRate,
                                                               new LoanTerm(termInYears));
        }

        [Test]
        [TestCaseSource(typeof(MonthlyRepaymentCsvData), "GetTestCases", new Object[] { "Data.csv" })]
        public void CalculateCorrectMonthlyRepayment_CSV(decimal principal,
                                                     decimal interestRate,
                                                     int termInYears,
                                                     decimal expectedMonthlyPayment)
        {
            var monthlyPayment = sut.CalculateMonthlyRepayment(new LoanAmount("USD", principal),
                                                               interestRate,
                                                               new LoanTerm(termInYears));

            Assert.That(monthlyPayment, Is.EqualTo(expectedMonthlyPayment));
        }
    }
}
