using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EBookLibrary.Models
{
    public class Review
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; } 

        [ForeignKey("UserId")]
        public string UserId { get; set; }

        public string BookId { get; set; }

        public string Comment { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public Book Book { get; set; }

        public User User { get; set; }


    }
}
