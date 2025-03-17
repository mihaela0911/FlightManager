using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManager.ViewModels.User
{
    public class RegisterUserViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }

        [MinLength(10, ErrorMessage = "Length of field Egn must be 10!")]
        [MaxLength(10, ErrorMessage = "Length of field Egn must be 10!")]
        [Required]
        public string Egn { get; set; }
        [Required]
        public string Adress { get; set; }
        [MinLength(10)]
        [MaxLength(13)]
        [Required]
        public string Telephone { get; set; }
    }
}
