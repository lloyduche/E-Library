using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibrary.ViewModels
{
    public class AdminDashboardViewModel
    {
        public int RegisteredUsers { get; set; }
        public int TotalBooks { get; set; }
        public int RecommendedReviews { get; set; }
    }
}
