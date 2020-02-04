using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FamilyDemoAPIv2.Entities
{
    /// <summary>
    /// Database entity for BirthState.
    /// </summary>
    [Table("BirthState")]
    public class BirthState
    {
        [Key]
        public int StateId { get; set; }

        [Required]
        [MaxLength(50)]
        public string State { get; set; }

        [Required]
        [MaxLength(50)]
        public string Abbreviation { get; set; }
    }
}
