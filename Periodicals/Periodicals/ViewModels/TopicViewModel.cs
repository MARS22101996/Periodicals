using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Periodicals.ViewModels
{
    public class TopicViewModel
    {
        public int TopicId { get; set; }
        [Required(ErrorMessage = "Это поле обязательно к заполнению")]
        [DisplayName("Название")]
        public string NameOfTopic { get; set; }
        [Required(ErrorMessage = "Это поле обязательно к заполнению")]
        [DisplayName("Описание")]
        public string Description { get; set; }
    }
}