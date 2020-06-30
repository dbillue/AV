using System;
using System.ComponentModel.DataAnnotations;

namespace FamilyApp.Model
{
    public class BirthState
    {
        [Key]
        public int StateId { get; set; }

        public string State {  get; set; }

        public string Abbreviation { get; set; }
    }
}
