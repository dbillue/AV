using System;
using System.Collections.Generic;
using System.Text;
using Demo.Domain.Applications;
using NUnit.Framework;

namespace NUnitDemo.Tests
{
    [Category("Product Comparison")]
    public class ProductComparerShould
    {
        private List<LoanProduct> products;
        private ProductComparer sut;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            products = new List<LoanProduct>
            {
                new LoanProduct(1, "a", 1),
                new LoanProduct(2, "b", 1),
                new LoanProduct(3, "c", 1),
            };
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            // Use for disposing objects or any other cleanup at end of test run.
        }

        [SetUp]
        public void Setup()
        {
            sut = new ProductComparer(new LoanAmount("USD", 200_000m), products);
        }

        [TearDown]
        public void Teardown()
        {

        }

        [Test]
        public void ReturnCorrectNumberOfComparisons()
        {
            List<MonthlyRepaymentComparison> comparisons = sut.CompareMonthlyRepayments(new LoanTerm(30));

            Assert.That(comparisons, Has.Exactly(3).Items);
        }

        [Test]
        public void NotReturnCorrectNumberOfComparisons()
        {
            List<MonthlyRepaymentComparison> comparisons = sut.CompareMonthlyRepayments(new LoanTerm(30));

            Assert.That(comparisons, Is.Unique);
        }

        [Test]
        public void ReturnComparisonForFirstProduct()
        {
            List<MonthlyRepaymentComparison> comparisons = sut.CompareMonthlyRepayments(new LoanTerm(30));

            var expectedProduct = new MonthlyRepaymentComparison("a", 1, 643.28m);

            Assert.That(comparisons, Does.Contain(expectedProduct));
        }

        [Test]
        public void ReturnComparisonForFirstProduct_WithPartialKnownExpectedValues()
        {
            List<MonthlyRepaymentComparison> comparisons = sut.CompareMonthlyRepayments(new LoanTerm(30));

            var expectedProduct = new MonthlyRepaymentComparison("a", 1, 643.28m);

            Assert.That(comparisons, Has.Exactly(1).Property("ProductName").EqualTo("a")
                                        .And
                                        .Property("InterestRate").EqualTo(1)
                                        .And
                                        .Property("MonthlyRepayment").GreaterThan(0));

            Assert.That(comparisons, Has.Exactly(1)
                            .Matches<MonthlyRepaymentComparison>(
                                item => item.ProductName == "a" &&
                                        item.InterestRate == 1 &&
                                        item.MonthlyRepayment > 0));
        }
    }
}
