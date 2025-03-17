using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public class Flight : BaseEntity
    {
        public Flight()
        {
            Reservations = new List<Reservation>();
        }
        public string LocationFrom { get; set; }
        public string LocationTo { get; set; }
        public DateTime TakeOff { get; set; }
        public DateTime Landing { get; set; }
        public string TypeOfPlane { get; set; }
        public int PlaneNumber { get; set; }
        public string PilotName { get; set; }
        public int CapacityOfPassengers { get; set; }
        public int CapacityOfBusinessClass { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }

        public string GetFlightTime()
        {
            TimeSpan result = Landing - TakeOff;
            return $"{(int)result.TotalHours} hours {result.TotalMinutes % 60} minutes";
            
        }
    }
}
