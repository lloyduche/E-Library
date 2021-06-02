using EBookLibrary.ViewModels.RatingsVM;
using EBookLibrary.ViewModels.ReviewVMs;
using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibrary.ViewModels.BookVMs
{
    public class GetBookDetailsResponseVM
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Publisher { get; set; }


        public string Isbn { get; set; }

        public string Description { get; set; }


        public int Pages { get; set; }

        public string Author { get; set; }


        public string CopiesAvailable { get; set; }

        public DateTime DatePublished { get; set; }
        public string AvatarUrl { get; set; }

        
        public string Category { get; set; }

        public ICollection<GetReviewVM> Reviews { get; set; }

        public ICollection<GetRatingVM> Ratings { get; set; }

        public AddReviewVM AddReviewVM { get; set; }

        public AddRatingVM AddRatingVM { get; set; }
    }
}
