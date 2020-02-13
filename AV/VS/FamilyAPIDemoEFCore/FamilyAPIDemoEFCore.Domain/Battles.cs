using System;
using System.Collections.Generic;

namespace FamilyAPIDemoEFCore.Domain
{
    public partial class Battles
    {
        public Battles()
        {
            FamilyBattles = new HashSet<FamilyBattles>();
        }

        public int Id { get; set; }
        public DateTime EndDate { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public ICollection<FamilyBattles> FamilyBattles { get; set; }
    }
}
