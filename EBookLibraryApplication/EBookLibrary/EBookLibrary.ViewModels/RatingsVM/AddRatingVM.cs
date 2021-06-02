using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibrary.ViewModels.RatingsVM
{
    public class AddRatingVM
    {
       
        public string BookId { get; set; }

        public int Ratings { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
