using System;

namespace EBookLibrary.ViewModels.BookVMs
{
    public class BookCardViewModel
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Category { get; set; }

        //[Required]
        public string? AvatarUrl { get; set; }

        public string Author { get; set; }

        public int Rating { get; set; }
    }
}
