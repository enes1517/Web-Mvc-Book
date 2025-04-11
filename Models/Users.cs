using System.ComponentModel.DataAnnotations;

namespace web.Models
{
    public class Users
    {
        
        public int Id { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Firstname is Required")]

        public string Firstname { get; set; }
        [Required(ErrorMessage = "Lastname is Required")]

        public string Lastname { get; set; }



    }
}
