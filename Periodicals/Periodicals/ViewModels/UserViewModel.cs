using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Periodicals.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Это поле обязательно к заполнению")]
        [DisplayName("Имя")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Это поле обязательно к заполнению")]
        [DisplayName("Фамилия")]
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
        [RegularExpression(@"\d{5}$", ErrorMessage = "Индекс состоит из 5 цифр")]
        [Display(Name = "Индекс")]
        public string Index { get; set; }
        [Required(ErrorMessage = "Это поле обязательно к заполнению")]
        [EmailAddress]
        [Display(Name = "Адрес электронной почты")]
        public string Email { get; set; }
        public byte[] ImageBytes { get; set; }
        public string ImgMimeType { get; set; }

    }
}