using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Periodicals.DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {   
      
        public string FirstName { get; set; }
      
        public string LastName { get; set; }     
       
        public string MobilePhone { get; set; }      
        
        public string City { get; set; }
       
        public string Index { get; set; }
        public byte[] ImageBytes { get; set; }
        public string ImgMimeType { get; set; }

        public virtual ICollection<UserPublication> UserPublications { get; set; }
        public virtual ICollection<PersonalAccount> PersonalAccounts { get; set; }
     
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
           
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
          
            return userIdentity;
        }
    }
    public class UserMetaData
    {
        [Required(ErrorMessage = "Это поле обязательно к заполнению")]
        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        public virtual string Email { get; set; }
        [Required(ErrorMessage = "Это поле обязательно к заполнению")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"\(\d\d\d\) \d\d\d-\d\d-\d\d", ErrorMessage = "Номер телефона должен иметь формат: (095) 111-11-11")]
        [DisplayName("Номер телефона")]
        public virtual string PhoneNumber { get; set; }
    }
}

