using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibrary.ViewModels.RatingsVM
{
    public class AddRatingResponseVM
    {
        public string Id { get; set; }
        public string UserId { get; set; }

        public string BookId { get; set; }

        public int Ratings { get; set; }
    }
}
