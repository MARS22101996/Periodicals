using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Periodicals.DAL.Entities;

namespace Periodicals.ViewModels
{
    public class PublicationViewModel
    {
        public int PublicationId { get; set; }
        [Required(ErrorMessage = "Это поле обязательно к заполнению")]
        [DisplayName("Название")]
        public string NameOfPublication { get; set; }
        [Required(ErrorMessage = "Это поле обязательно к заполнению")]
        [DisplayName("Описание")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Это поле обязательно к заполнению")]
        [DisplayName("Периодичность")]
        public string Periodicity { get; set; }
        [Required(ErrorMessage = "Это поле обязательно к заполнению")]
        [DisplayName("Формат")]
        public string Format { get; set; }
        [Required(ErrorMessage = "Это поле обязательно к заполнению")]
        [DisplayName("Цвет")]
        public string Color { get; set; }
        [Required(ErrorMessage = "Это поле обязательно к заполнению")]
        [Range(1, 1000,
        ErrorMessage = "Значение {0} должно быть между {1} и {2}.")]
        [DisplayName("Объем, стр.")]
        public int Volume { get; set; }
        [Required(ErrorMessage = "Это поле обязательно к заполнению")]
        [Range(0.01, Double.MaxValue, ErrorMessage = "Сумма должна быть больше одной копейки")]
        [DisplayName("Цена, грн.")]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Currency)]
        [RegularExpression(@"^\d+(.\d\d)?$", ErrorMessage = "Сумма должна иметь формат 100.01")]
        public double PricePerMonth { get; set; }
 
        public virtual ICollection<Topic> AllTopics { get; set; }
        public string[] OwnTopics{ get; set; }
    }
}