using Microsoft.AspNet.Identity.EntityFramework;
using Periodicals.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Periodicals.DAL.Initializer;

namespace Periodicals.DAL.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        static ApplicationDbContext()
        {
            Database.SetInitializer<ApplicationDbContext>(new PeriodicalsDatabaseInitializer());
        }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
       
        public DbSet<PersonalAccount> PersonalAccounts{ get; set; }
        public DbSet<Payment> Payments{ get; set; }
        public DbSet<Publication> Publications { get; set; }
        public DbSet<Replenishment> Replenishments { get; set; }
        public DbSet<Topic>Topics { get; set; }
        public DbSet<UserPublication> UserPublications { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        
    }
}
