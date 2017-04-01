using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Periodicals.DAL;

namespace Periodicals.ViewModels
{
    public class PaymentViewModel
    {
        public int PaymentId { get; set; }

        public double Sum { get; set; }
    }
}