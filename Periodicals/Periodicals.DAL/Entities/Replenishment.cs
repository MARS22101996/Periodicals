using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Periodicals.DAL.Entities
{
    public class Replenishment
    {
        public int ReplenishmentId { get; set; }

        public DateTime OperationDate { get; set; }

        public double Amount { get; set; }

        public string MobileNumber { get; set; }
     
        public string NumberOfCard { get; set; }

        public string ExpirationDate { get; set; }

        public int Cvc { get; set; }

        public string NameOfCard { get; set; }

        public int PersonalAccountId { get; set; }
        public virtual PersonalAccount Account { get; set; }

      


    }
}
