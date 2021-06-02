using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibrary.ViewModels.ReviewVMs
{
    public class AddReviewResponseVM
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public string BookId { get; set; }

        public string Comment { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
