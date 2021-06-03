using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EBookLibrary.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string PublicId { get; set; }


        [ForeignKey("CategoryId")]
        public string CategoryId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Publisher { get; set; }

        [Required]
        public string Isbn { get; set; }

      
        public string AvatarUrl { get; set; }

        [Required]
        [StringLength(maximumLength: 150, ErrorMessage = "The property {0} should have not have more than {1} characters")]
        public string Description { get; set; }

        [Required]
        public string Pages { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string CopiesAvailable { get; set; }

        public DateTime DatePublished { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public ICollection<Review> Reviews { get; set; }

        public ICollection<Rating> Ratings { get; set; }

        public Category Category { get; set; }

    }
}
