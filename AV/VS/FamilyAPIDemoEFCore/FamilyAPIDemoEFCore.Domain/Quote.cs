using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyAPIDemoEFCore.Domain
{
    public class Quote
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public Family Family { get; set; }
        public int FamilyId { get; set; }
    }
}
