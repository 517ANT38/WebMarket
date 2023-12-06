using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebMarket.Models
{
    public class NewClient
    {
        
        [Required(ErrorMessage = "Пожалуйста, введите свое имя")]
        [Display(Name = "Имя")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите свою фамилию")]
        [Display(Name = "Фамилия")]
        public string Family { get; set; }
        [Display(Name = "Отечество")]
        public string Patronymic { get; set; } 

        [Required(ErrorMessage = "Пожалуйста, введите email")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Вы ввели некорректный email")]
        [Display(Name = "Почта")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите телефон")]
        [Display(Name = "Телефон")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Пожалуйста, введите пароль")]                                                                                                   // "3)Не менее 1 буквенного символа)\n")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        [Compare("Password", ErrorMessage = "Пароль и пароль подтверждения не совпадают.")]
        public string ConfirmPassword { get; set; }
        
    }
}