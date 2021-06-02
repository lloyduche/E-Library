using EBookLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibrary.ViewModels.ReviewVMs
{
    public class GetReviewVM
    {
        public string UserId { get; set; }

        public string BookId { get; set; }

        public string Comment { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public User User;

    }
}
