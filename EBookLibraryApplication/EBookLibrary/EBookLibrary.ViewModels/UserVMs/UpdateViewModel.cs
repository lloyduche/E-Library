using EBookLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EBookLibrary.ViewModels.UserVMs
{
    public class UpdateViewModel
    {

        [StringLength(maximumLength: 50, ErrorMessage = "The property should not have more than {1} characters")]
        public string FirstName { get; set; }

        [StringLength(maximumLength: 50, ErrorMessage = "The property should not have more than {1} characters")]
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

    }
}
