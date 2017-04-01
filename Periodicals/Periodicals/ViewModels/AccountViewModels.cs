﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Periodicals.ViewModels
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Адрес электронной почты")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Код")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Запомнить браузер?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Адрес электронной почты")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Адрес электронной почты")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить меня")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Это поле обязательно к заполнению")]
        [EmailAddress]
        [Display(Name = "Адрес электронной почты")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Это поле обязательно к заполнению")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Это поле обязательно к заполнению")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

       
        [Required(ErrorMessage = "Это поле обязательно к заполнению")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"\(\d\d\d\) \d\d\d-\d\d-\d\d", ErrorMessage = "Номер телефона должен иметь формат: (095) 111-11-11")]
        [Display(Name = "Номер телефона")]
        public string MobilePhone { get; set; }

        [Required(ErrorMessage = "Это поле обязательно к заполнению")]
        [RegularExpression(@"^[А-Я][а-яА-ЯёЁ\s]{1,25}$", ErrorMessage = "Неверный формат ввода! Пишите город с большой буквы, не используйте цифры! Вводите значение на русском!")]
        [Display(Name = "Город")]
        public string City { get; set; }

        [Required(ErrorMessage = "Это поле обязательно к заполнению")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "Индекс состоит из 5 цифр")]
        [Display(Name = "Индекс")]
        public string Index { get; set; }

        [Required(ErrorMessage = "Это поле обязательно к заполнению")]
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        [Compare("Password", ErrorMessage = "Пароль и его подтверждение не совпадают.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Адрес электронной почты")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        [Compare("Password", ErrorMessage = "Пароль и его подтверждение не совпадают.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Почта")]
        public string Email { get; set; }
    }
}
