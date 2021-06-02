using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibrary.ViewModels.BookVMs
{
    public class SearchTermViewModel
    {
        public string Category { get; set; }
        public string ISBN { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public DateTime YearPublished { get; set; }
    }
}
