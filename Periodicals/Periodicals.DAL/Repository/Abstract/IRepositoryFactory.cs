using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using Periodicals.DAL.Entities;

namespace Periodicals.DAL.Repository.Abstract
{
    public interface IRepositoryFactory
    {
        IPaymentRepository PaymentRepository { get; }
        IPersonalAccountRepository PersonalAccountRepository { get; }
        IPublicationRepository PublicationRepository { get; }
        IReplenishmentRepository ReplenishmentRepository { get; }
        ITopicRepository TopicRepository { get; }
        IUserPublicationRepository UserPublicationRepository { get; }
        IUserRepository GetUserRepository(UserManager<ApplicationUser> userManager);
        

        void ConfigAuthorization(IAppBuilder app);
    }
}
