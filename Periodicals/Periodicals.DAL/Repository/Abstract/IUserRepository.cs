using System.Collections.Generic;

using Microsoft.AspNet.Identity;

using Periodicals.DAL.Entities;

namespace Periodicals.DAL.Repository.Abstract
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        void Add(ApplicationUser item, string password, string role="User");
        void Edit(ApplicationUser item, string userId);
        void Delete(string id);
        ApplicationUser FindById(string id);
        List<ApplicationUser> Get(string role);
        void SetLock(string id, bool block);
        void AddAccount(PersonalAccount item);
    }
}
