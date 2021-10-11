using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models.Validation
{
    public class OrderConditionsAttribute : ValidationAttribute
    {
        
        public OrderConditionsAttribute()
        {
            
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;
            if (value is string input)
            {
                input = input.ToLower();
                
                if (input == "accepted" || input == "denied")
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("The only valid input is Accepted/Denied.");
                }

            }
            throw new NotImplementedException($"The attribute {nameof(UniqueCharactersAttribute)} is not implemented for object {value.GetType()}. {nameof(String)} only is implemented.");

        }
    }
}
