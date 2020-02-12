using System.Collections.Generic;

namespace FamilyAPIDemoEFCore.Domain
{
    public class Family
    {
        public Family()
        {
            Quotes = new List<Quote>();
        }

        public int Id { get; set;}
        public string Name { get; set;}
        public List<Quote> Quotes { get; set; }
        public int BattleId { get; set; }
    }
}
