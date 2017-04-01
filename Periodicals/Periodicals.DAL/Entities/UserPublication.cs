using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Periodicals.DAL.Entities
{
    public class UserPublication
    {
        public UserPublication()
        {
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
        }
        public int UserPublicationId { get; set; }
        public int PublicationId { get; set; }
        public string UserId { get; set; }
    
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
      
        public int Period { get; set; }
        public bool PaymentState { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Publication Publication { get; set; }
    }
}
