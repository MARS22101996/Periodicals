using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Periodicals.DAL.Entities
{
    public class Topic
    {
        public int TopicId { get; set; }   
        public string NameOfTopic { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Publication> Publications { get; set; }
    }
}
