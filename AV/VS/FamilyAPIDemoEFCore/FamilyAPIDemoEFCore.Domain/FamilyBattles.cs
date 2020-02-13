using System;
using System.Collections.Generic;

namespace FamilyAPIDemoEFCore.Domain
{
    public partial class FamilyBattles
    {
        public int FamilyId { get; set; }
        public int BattleId { get; set; }
        public Battles Battle { get; set; }
        public Families Family { get; set; }
    }
}
