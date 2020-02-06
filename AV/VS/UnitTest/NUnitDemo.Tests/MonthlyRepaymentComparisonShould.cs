﻿using System;
using System.Collections.Generic;
using System.Text;
using Demo.Domain.Applications;
using NUnit.Framework;

namespace Loans.Tests
{
    [Category("Monthly Payment Comparison")]
    public class MonthlyRepaymentComparisonShould
    {

        [Test]
        public void RespectValueEquality()
        {
            var a = new MonthlyRepaymentComparison("a", 42.42m, 22.22m);
            var b = new MonthlyRepaymentComparison("a", 42.42m, 22.22m);

            Assert.That(a, Is.EqualTo(b));
        }

        [Test]
        public void RespectValueInequality()
        {
            var a = new MonthlyRepaymentComparison("a", 42.42m, 22.22m);
            var b = new MonthlyRepaymentComparison("a", 30.42m, 22.22m);

            Assert.That(a, Is.Not.EqualTo(b));
        }
    }
}
