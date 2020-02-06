using Demo.Domain;
using System;
using System.Collections.Generic;

namespace Demo.Domain.Applications
{
    public class LoanAmount : ValueObject
    {
        public string CurrencyCode { get; }
        public decimal Principal { get; }

        // Explicitly stating to hide dealt constructor to indicate immutability
        private LoanAmount() { }

        public LoanAmount(string currencyCode, decimal principal)
        {
            CurrencyCode = currencyCode;
            Principal = principal;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return CurrencyCode;
            yield return Principal;
        }
    }
}
