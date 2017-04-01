using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Periodicals.ViewModels
{
    public class UserPublicationViewModel
    {
        public UserPublicationViewModel()
        {
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
        }      
        public int UserPublicationId { get; set; }
        public int PublicationId { get; set; }
        public string UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "Это поле обязательно к заполнению")]
        [Range(1, 12,
        ErrorMessage = "Значение {0} должно быть между {1} и {2}.")]
        [DisplayName("Период, мес.")]
        public int Period { get; set; }
        [DisplayName("Статус оплаты, мес.")]
        public bool PaymentState { get; set; }
        

    }
}