﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FamilyDemoAPIv2.Entities
{
    /// <summary>
    /// Database entity for Person.
    /// </summary>
    [Table("Persons")]
    public class Person
    {
        [Key]
        public Guid PersonId { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string MIddleName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(50)]
        public string Gender { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        [MaxLength(50)]
        public string Country { get; set; }

        [Required]
        [MaxLength(50)]
        public string City { get; set; }

        [Required]
        public int StateId { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        public ICollection<Pet> Pets { get; set; }
            = new List<Pet>();

        public ICollection<BirthState> birthState { get; set; }
            = new List<BirthState>();
    }
}
