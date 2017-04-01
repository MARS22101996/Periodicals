using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Periodicals.DAL.Entities
{
    public class PersonalAccount
    {
      
        public int PersonalAccountId { get; set; }
        public double Balance { get; set; }
        public string ApplicationUserId { get; set; }

        public virtual ICollection<Replenishment> Replenishments { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
   }
}
