using System;
using System.Collections.Generic;
using System.Text;
using Demo.Domain.Applications;
using NUnit.Framework;

namespace Loans.Tests
{
    [TestFixture]
    //[Ignore("Not ready for test implementation.")]
    [Category("Loan Payment Test")]
    public class LoanTermShould
    {
        [Test]
        [Ignore("Not ready for test implementation.")]
        public void ReturnTermInMonths()
        {
            var sut = new LoanTerm(1);

            Assert.That(sut.ToMonths(), Is.EqualTo(12), "Test failed - Months should be 12!");
        }

        [Test]
        public void StoreYears()
        {
            var sut = new LoanTerm(1);

            Assert.That(sut.Years, Is.EqualTo(1));
        }

        [Test]
        public void RespectValueQuality()
        {
            var a = new LoanTerm(1);
            var b = new LoanTerm(1);

            Assert.That(a, Is.EqualTo(b));
        }

        [Test]
        public void RespectValueInequality()
        {
            var a = new LoanTerm(1);
            var b = new LoanTerm(420);

            Assert.That(a, Is.Not.EqualTo(b));
        }

        [Test]
        public void ReferenceEqualityExample()
        {
            var a = new LoanTerm(1);
            var b = a;
            var c = new LoanTerm(1);

            Assert.That(a, Is.SameAs(b));
            Assert.That(a, Is.Not.SameAs(c));

            var x = new List<string> { "a", "b" };
            var y = x;
            var z = new List<string> { "a", "b" };

            Assert.That(y, Is.SameAs(x));
            Assert.That(z, Is.Not.SameAs(x));
        }

        [Test]
        public void TestDouble()
        {
            double a = 1.0 / 3.0;

            Assert.That(a, Is.EqualTo(0.33).Within(0.004));
            Assert.That(a, Is.EqualTo(0.33).Within(10).Percent);
        }

        [Test]
        public void NotAllowedZeroYears()
        {
            Assert.That(() => new LoanTerm(0), Throws.TypeOf<ArgumentOutOfRangeException>());

            //Assert.That(() => new LoanTerm(0), Throws.TypeOf<ArgumentOutOfRangeException>()
            //                   .With
            //                   .Property("Message")
            //                   .EqualTo("Please specify a value greater than 0."));

            //Assert.That(() => new LoanTerm(0), Throws.TypeOf<ArgumentOutOfRangeException>()
            //                   .With
            //                   .Message
            //                   .EqualTo("Please specify a value greater than 0."));

            Assert.That(() => new LoanTerm(0), Throws.TypeOf<ArgumentOutOfRangeException>()
                               .With
                               .Property("ParamName")
                               .EqualTo("years"));

            Assert.That(() => new LoanTerm(0), Throws.TypeOf<ArgumentOutOfRangeException>()
                               .With
                               .Matches<ArgumentOutOfRangeException>(
                                    ex => ex.ParamName == "years"));
        }
    }
}
