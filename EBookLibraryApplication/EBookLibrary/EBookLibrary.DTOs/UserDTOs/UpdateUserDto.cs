using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EBookLibrary.DTOs.UserDTOs
{
    public class UpdateUserDto
    {
        [Required]
        public string Id { get; set; }

        [StringLength(maximumLength: 50, ErrorMessage = "The property should not have more than {1} characters")]
        public string FirstName { get; set; }

        [StringLength(maximumLength: 50, ErrorMessage = "The property should not have more than {1} characters")]
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Gender { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
