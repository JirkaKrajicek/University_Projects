using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace eshop.Models
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = false)]
    internal class UniqueCharactersAttribute : ValidationAttribute
    {
        private readonly int CharCount;
        public UniqueCharactersAttribute(int charCount)
        {
            this.CharCount = charCount;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return new ValidationResult("This field is required!");

            if (value is string pswrd)
            {
                pswrd = pswrd.ToLower();                
                pswrd = String.Join("", pswrd.Distinct());

                if(pswrd.Length >= CharCount)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("Not enough unique charactars!");
                }
                
            }
            throw new NotImplementedException($"The attribute {nameof(UniqueCharactersAttribute)} is not implemented for object {value.GetType()}. {nameof(String)} only is implemented.");

        }

    }
}