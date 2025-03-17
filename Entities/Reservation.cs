using Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Reservation : BaseEntity
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Egn { get; set; }
        public string Telephone { get; set; }
        public string Nationality { get; set; }
        public TicketType TicketType { get; set; }
        public int FlightId { get; set; }
        public virtual Flight Flight { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {MiddleName} {LastName}";
        }
    }
}
