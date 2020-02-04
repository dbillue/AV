using System;
using System.ComponentModel.DataAnnotations;

namespace FamilyDemoAPIv2.Models
{
    /// <summary>
    /// DTO model for returning a person.
    /// </summary>
    public class GetPersonDTO
    {
        /// <summary>
        /// Database Id.
        /// </summary>
        public Guid PersonId { get; set; }

        /// <summary>
        /// Person's first name.
        /// </summary>
        [Required(ErrorMessage = "First name is required.")]
        [MaxLength(50)]
        public string FirstName { get; set; }

        /// <summary>
        /// Person's middle name.
        /// </summary>
        [Required(ErrorMessage = "Middle name is required.")]
        [MaxLength(50)]
        public string MIddleName { get; set; }

        /// <summary>
        /// Person's last name.
        /// </summary>
        [Required(ErrorMessage = "Last name is required.")]
        [MaxLength(50)]
        public string LastName { get; set; }

        /// <summary>
        /// Person's gender.
        /// </summary>
        [Required(ErrorMessage = "Gender is required.")]
        [MaxLength(50)]
        public string Gender { get; set; }

        /// <summary>
        /// Person's age.
        /// </summary>
        [Required(ErrorMessage = "Age is required.")]
        public int Age { get; set; }

        /// <summary>
        /// Person's country.
        /// </summary>
        [Required(ErrorMessage = "Country is required.")]
        [MaxLength(50)]
        public string Country { get; set; }

        /// <summary>
        /// Person's city.
        /// </summary>
        [Required(ErrorMessage = "City is required.")]
        [MaxLength(50)]
        public string City { get; set; }

        /// <summary>
        /// Person's state of birth.
        /// </summary>
        [Required(ErrorMessage = "State of birth is required.")]
        public int StateId { get; set; }

        /// <summary>
        /// Person's date of birth.
        /// </summary>
        [Required(ErrorMessage = "Date of birth is required.")]
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Database entry created date.
        /// </summary>
        public DateTime CreateDate { get; set; }
    }
}
