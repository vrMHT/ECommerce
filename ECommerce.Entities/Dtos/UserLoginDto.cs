﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ECommmerce.Entities.Dtos
{
    public class UserLoginDto
    {
        [Required]
        [EmailAddress(ErrorMessage = "Mail Adresiniz gecerli degil.")]
        [DisplayName("Mail")]
        public string Email { get; set; }
        [Required]
        [MaxLength(20,ErrorMessage = "Sifreniz maksimum 20 karakterden fazla olamaz.")]
        [MinLength(3,ErrorMessage = "Sifreniz minimum 3 karakterden fazla olamaz.")]
        [DisplayName("Sifreniz")]
        public string Password { get; set; }
    }
}
