using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Periodicals.DAL.Entities
{
    public class Publication
    {
        public int PublicationId { get; set; }
     
        public string NameOfPublication { get; set; }
       
        public string Description { get; set; }
       
        public string Periodicity { get; set; }
      
        public string Format { get; set; }
     
        public string Color { get; set; }
      
        public int Volume { get; set; }
      
        public double PricePerMonth { get; set; }

        public byte[] ImageBytes { get; set; }

        public string ImgMimeType { get; set; }


        public virtual ICollection<UserPublication> UserPublications { get; set; }
        public virtual ICollection<Topic> Topics { get; set; }




    }
}
