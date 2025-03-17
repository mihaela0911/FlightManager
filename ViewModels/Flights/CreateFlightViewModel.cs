using FlightManager.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManager.ViewModels.Flights
{
    public class CreateFlightViewModel
    {
        [Required]
        public string LocationFrom { get; set; }
        [Required]
        public string LocationTo { get; set; }
        [Required]
        public DateTime TakeOff { get; set; }
        [Required]
        //[ValidateLandingDate(TakeOff)]
        public DateTime Landing { get; set; }
        [Required]
        public string TypeOfPlane { get; set; }
        [Required]
        public int PlaneNumber { get; set; }
        [Required]
        public string PilotName { get; set; }
        [Required]
        //[Range(50, 150)]
        public int CapacityOfPassengers { get; set; }
        [Required]
        //[Range(1,20)]
        public int CapacityOfBusinessClass { get; set; }
        
    }
}
