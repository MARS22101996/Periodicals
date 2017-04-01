using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using Periodicals.DAL.Context;
using Periodicals.DAL.Entities;
using Periodicals.DAL.Identity;
using Periodicals.DAL.Repository.Abstract;

namespace Periodicals.DAL.Repository.Concrete
{
    public class EFRepositoryFactory : IRepositoryFactory
    {
        private readonly ApplicationDbContext _context;

        public IPaymentRepository PaymentRepository
        {
            get { return new PaymentRepository(_context); }
        }
        public IPersonalAccountRepository PersonalAccountRepository
        {
            get { return new PersonalAccountRepository(_context); }
        }
        public  IPublicationRepository PublicationRepository
        {
            get { return new PublicationRepository(_context); }
        }
        public IReplenishmentRepository ReplenishmentRepository
        {
            get { return new ReplenishmentRepository(_context); }
        }
        public ITopicRepository TopicRepository
        {
            get { return new TopicRepository(_context); }
        }
        public IUserPublicationRepository UserPublicationRepository
        {
            get { return new UserPublicationRepository(_context); }
        }
        public IUserRepository GetUserRepository(UserManager<ApplicationUser> userManager)
        {
            return new UserRepository(_context, (ApplicationUserManager)userManager);
        }
        public EFRepositoryFactory(string connectionName)
        {
            _context = new ApplicationDbContext();
        }

        public void ConfigAuthorization(IAppBuilder app)
        {
            app.CreatePerOwinContext(() => _context);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);
        }

 
    }
}
