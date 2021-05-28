using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EBookLibrary.Models
{
    public class Category
    {
        public int Type { get; }

        public string Id 
        { 
            get { return Id; }
            private set { SetId(); }
        }

        public string Name { get; set; }

        private string SetId()
        {
            string Id;
            if (Type == 1)
                Id = "Fiction" + "*" + Guid.NewGuid().ToString();
            else if (Type == 2)
                Id = "Non-Fiction" + "*" + Guid.NewGuid().ToString();
            else
                Id = Guid.NewGuid().ToString();
            return Id;
        }

        public ICollection<Book> Book;
    }
}
