using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibrary.DTOs.RatingDTOs
{
    public class AddRatingResponseDto
    {
        public string Id { get; set; }
        public string UserId { get; set; }

        public string BookId { get; set; }

        public int Ratings { get; set; }
    }
}
