using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManager.Attributes
{
    [AttributeUsage(AttributeTargets.Property |
  AttributeTargets.Field, AllowMultiple = false)]
    public class ValidateLandingDate : ValidationAttribute
    {
        private DateTime TakeOff { get; set; }
        public ValidateLandingDate(DateTime takeoff)
        {
            TakeOff = takeoff;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if ((DateTime)value < TakeOff)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Landing date must be after take off date!");
            }
        }
    }
}
