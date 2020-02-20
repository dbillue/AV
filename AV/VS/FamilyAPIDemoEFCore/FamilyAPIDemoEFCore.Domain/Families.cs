using System;
using System.Collections.Generic;

namespace FamilyAPIDemoEFCore.Domain
{
    public partial class Families
    {
        public Families()
        {
            FamilyBattles = new HashSet<FamilyBattles>();
            Quotes = new HashSet<Quotes>();
        }

        public int Id { get; set; }
        public int BattleId { get; set; }
        public string Name { get; set; }
        public SecretIdentity SecretIdentity { get; set; }
        public ICollection<FamilyBattles> FamilyBattles { get; set; }
        public ICollection<Quotes> Quotes { get; set; }
    }
}
