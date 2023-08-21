﻿using System.ComponentModel.DataAnnotations;

namespace ContactsManager.Core.DTO
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = ("Name can't be blank"))]
        public string PersonName { get; set; }

        [Required(ErrorMessage = ("E-mail can't be blank"))]
        [EmailAddress(ErrorMessage = "E-mail should be in a proper e-mail address format")]
        public string Email { get; set; }

        [Required(ErrorMessage = ("Phone can't be blank"))]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Phone number should contain numbers only")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessage = ("Password can't be blank"))]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = ("Confirm Password can't be blank"))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}