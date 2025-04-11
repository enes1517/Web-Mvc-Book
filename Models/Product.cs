using System.ComponentModel.DataAnnotations;

namespace web.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Author { get; set; }

        public string Category { get; set; }


        [Range(1,5,ErrorMessage = "The score must be between 1 and 5")]
        public int  Rating { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();


    }
}
