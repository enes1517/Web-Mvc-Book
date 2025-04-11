using System;
using System.ComponentModel.DataAnnotations;

namespace web.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Comment text is required")]
        public string Text { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Foreign key for Product
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}