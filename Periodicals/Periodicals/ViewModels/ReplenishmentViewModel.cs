using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Periodicals.DAL;

namespace Periodicals.ViewModels
{
    public class ReplenishmentViewModel
    {
        public int ReplenishmentId { get; set; }


        [Required(ErrorMessage = "Это поле обязательно к заполнению")]
        [Range(0.01, Double.MaxValue, ErrorMessage = "Сумма должна быть больше одной копейки")]
        [DisplayName("Сумма")]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Currency)]
        [RegularExpression(@"^\d+(.\d\d)?$", ErrorMessage = "Сумма должна иметь формат 100.01")]
        public double Amount { get; set; }

        [Required(ErrorMessage = "Это поле обязательно к заполнению")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"\(\d\d\d\) \d\d\d-\d\d-\d\d",
            ErrorMessage = "Номер телефона должен иметь формат: (095) 111-11-11")]
        [DisplayName("Номер телефона")]
        public string MobileNumber { get; set; }

        [Required(ErrorMessage = "Это поле обязательно к заполнению")]
        [MinLength(14, ErrorMessage = "Длина номера карты должна быть больше 14 символов")]
        [MaxLength(23, ErrorMessage = "Длина номера карты не должна превышать 23 символов")]
        [RegularExpression(@"[\d\s]+", ErrorMessage = "Номер карты имеет неверный формат.")]
        [DisplayName("Номер карты")]
        public string NumberOfCard { get; set; }

        [Required(ErrorMessage = "Это поле обязательно к заполнению")]
        [RegularExpression(@"\d\d / \d{4}", ErrorMessage = "Дата должна иметь формат мм/гггг")]
        [CustomValidation(typeof(MyValidation), "ValidateExpirationDate", ErrorMessage = "Дата должна быть больше текущего числа.")]
        [DisplayName("Срок действия")]
        public string ExpirationDate { get; set; }

        [Required(ErrorMessage = "Это поле обязательно к заполнению")]
        [Range(100, 9999, ErrorMessage = "Код должен быть в диапазоне чисел от 100 до 9999")]
        [DisplayName("CVC код")]
        public int Cvc { get; set; }

        [Required(ErrorMessage = "Это поле обязательно к заполнению")]
        [DisplayName("Название карты")]
        public string NameOfCard { get; set; }

    }
}