﻿using System.Collections.Generic;

namespace Demo.Domain.Applications
{
    public class IdentityVerificationStatus : ValueObject
    {
        public bool Passed { get; }
        
        private IdentityVerificationStatus(){}

        public IdentityVerificationStatus(bool identityVerified)
        {
            Passed = identityVerified;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Passed;
        }
    }
}