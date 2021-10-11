using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="This form is required.")]
        [StringLength(256)]
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        [Required]
        [StringLength(256)]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [UniqueCharacters(6, ErrorMessage = "Not enough unique characters.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z0-9]).{8,}$", ErrorMessage = RegisterViewModel.ErrorMessagePassword)]
        public string Password { get; set; }
        private const string ErrorMessagePassword = "Password requires 6 unique characters \n" +
                                                    "Password's length must be at least 8 characters \n" +
                                                    "Password requires Upper and Lower case \n" +
                                                    "Password requires number \n" +
                                                    "Password requires special character like '#$^+=!*()@%&'";


        [Required]
        [Compare(nameof(Password), ErrorMessage = "Passwords don't match")]
        public string RepeatedPassword { get; set; }

        public string[] ErrorsDuringRegister { get; set; }
    }
}
