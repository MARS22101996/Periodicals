using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Periodicals.DAL.Entities
{
    public class Payment
    {
        public int PaymentId { get; set; }

        public DateTime OperationDate { get; set; }

        public double Amount { get; set; }

        public int PersonalAccountId { get; set; }

        public virtual PersonalAccount Account { get; set; }




    }
}
