using System;
using System.ComponentModel.DataAnnotations;

namespace FamilyDemoAPIv2.Models
{
    /// <summary>
    /// DTO model for updating an existing person.
    /// </summary>
    public class UpdatePersonDTO
    {
        /// <summary>
        /// Database Id.
        /// </summary>
        public Guid PersonId { get; set; }

        /// <summary>
        /// Person's first name.
        /// </summary>
        [MaxLength(50)]
        public string FirstName { get; set; }

        /// <summary>
        /// Person's middle name.
        /// </summary>
        [MaxLength(50)]
        public string MIddleName { get; set; }

        /// <summary>
        /// Person's last name.
        /// </summary>
        [MaxLength(50)]
        public string LastName { get; set; }

        /// <summary>
        /// Person's gender.
        /// </summary>
        [MaxLength(50)]
        public string Gender { get; set; }

        /// <summary>
        /// Person's age.
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Person's country.
        /// </summary>
        [MaxLength(50)]
        public string Country { get; set; }

        /// <summary>
        /// Person's city.
        /// </summary>
        [MaxLength(50)]
        public string City { get; set; }

        /// <summary>
        /// Person's state of birth.
        /// </summary>
        public int StateId { get; set; }

        /// <summary>
        /// Person's date of birth.
        /// </summary>
        public DateTime DateOfBirth { get; set; }
    }
}
