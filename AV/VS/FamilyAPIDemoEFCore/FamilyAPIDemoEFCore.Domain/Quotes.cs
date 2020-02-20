using System;
using System.Collections.Generic;

namespace FamilyAPIDemoEFCore.Domain
{
    public partial class Quotes
    {
        public int Id { get; set; }
        public int FamilyId { get; set; }
        public string Text { get; set; }
        public Families Family { get; set; }
    }
}
