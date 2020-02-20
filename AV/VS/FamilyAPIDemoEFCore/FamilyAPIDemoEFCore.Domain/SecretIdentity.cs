using System;
using System.Collections.Generic;

namespace FamilyAPIDemoEFCore.Domain
{
    public partial class SecretIdentity
    {
        public int Id { get; set; }
        public int FamilyId { get; set; }
        public string RealName { get; set; }
        public Families Family { get; set; }
    }
}
