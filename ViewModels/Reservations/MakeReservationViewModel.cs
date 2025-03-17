using Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManager.ViewModels.Reservations
{
    public class MakeReservationViewModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [MinLength(10, ErrorMessage = "Length of field Egn must be 10!")]
        [MaxLength(10, ErrorMessage = "Length of field Egn must be 10!")]
        public string Egn { get; set; }
        
        [Required]
        [MinLength(10)]
        [MaxLength(13)]
        public string Telephone { get; set; }
        [Required]
        public string Nationality { get; set; }
        [Required]
        public TicketType TicketType { get; set; }
    }
}
