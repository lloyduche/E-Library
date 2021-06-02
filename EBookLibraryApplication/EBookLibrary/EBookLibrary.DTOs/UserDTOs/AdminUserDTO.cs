using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EBookLibrary.DTOs.UserDTOs
{
    public class AdminUserDTO
    {
         
        public string Id { get; set; }

        
        public string FirstName { get; set; }

        
        public string LastName { get; set; }

        
        public string Email { get; set; }

    }
}
