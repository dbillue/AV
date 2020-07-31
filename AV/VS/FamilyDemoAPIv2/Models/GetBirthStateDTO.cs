using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyDemoAPIv2.Models
{
    public class GetBirthStateDTO
    {
        public int StateId { get; set; }
        
        public string State { get; set; }

        public string Abbreviation { get; set; }
    }
}
