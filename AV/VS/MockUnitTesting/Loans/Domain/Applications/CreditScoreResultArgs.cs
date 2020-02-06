using System;

namespace Demo.Domain.Applications
{
    public class CreditScoreResultArgs : EventArgs
    {
        public int Score { get; set; }        
    }
}