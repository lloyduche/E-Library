using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibrary.DTOs.ReviewDTOs
{
    public class AddReviewResponseDto
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public string BookId { get; set; }

        public string Comment { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
