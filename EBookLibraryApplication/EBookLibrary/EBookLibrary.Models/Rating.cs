using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EBookLibrary.Models
{
    public class Rating
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; } 

        [ForeignKey("UserId")]
        public string UserId { get; set; }

        [ForeignKey("BookId")]
        public string BookId { get; set; }

        public int Ratings { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public Book Book { get; set; }

        public User User { get; set; }
    }
}
