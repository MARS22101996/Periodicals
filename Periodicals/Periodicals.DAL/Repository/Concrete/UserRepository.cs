using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Periodicals.DAL.Context;
using Periodicals.DAL.Entities;
using Periodicals.DAL.Identity;
using Periodicals.DAL.Repository.Abstract;

namespace Periodicals.DAL.Repository.Concrete
{
    class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly ApplicationUserManager _userManager;

        public UserRepository(ApplicationDbContext context, 
            ApplicationUserManager userManager)
        {
            _db = context;
            _userManager = userManager;
        }
        public void AddAccount(PersonalAccount item)
        {
            _db.PersonalAccounts.Add(item);
            _db.SaveChanges();
        }
        public void Add(ApplicationUser item)
        {
            _userManager.Create(item);
        }

        public void Edit(ApplicationUser item, string userId)
        {
            var old = FindById(userId);
            old.LastName = item.LastName;
            old.FirstName = item.FirstName;
            old.MobilePhone = item.MobilePhone;
            old.Index = item.Index;
            old.City = item.City;
            if (item.ImageBytes != null)
            {
                old.ImageBytes = item.ImageBytes;
                old.ImgMimeType = item.ImgMimeType;
            }
            //item.Id = userId;
            //_userManager.Update(item);
            _db.Entry(old).State = EntityState.Modified;
            _db.SaveChanges();
            //_userManager.
        }

        public void Delete(int? id)
        {
            throw new NotSupportedException();
        }

        public void Delete(string id)
        {
            _userManager.Delete(FindById(id));
        }

        public List<ApplicationUser> Get()
        {
            return _db.Users.ToList();
        }

        public List<ApplicationUser> Get(int skip, int take)
        {
            return Get().Skip(skip).Take(take).ToList();
        }

        public List<ApplicationUser> Get(string roleName)
        {
            var role = _db.Roles.FirstOrDefault(r => r.Name == roleName);
            if (role == null) 
                return new List<ApplicationUser>();
            return Get().Where(u => u.Roles.Any(r => r.RoleId == role.Id)).ToList();
        }

        public ApplicationUser FindById(int? id)
        {
            throw new NotSupportedException();
        }

        public ApplicationUser FindById(string id)
        {
            return _db.Users.Find(id);
        }

        public List<ApplicationUser> Find(Func<ApplicationUser, bool> predicate)
        {
            return Get().Where(predicate).ToList();
        }

        public void Add(ApplicationUser item, string password, string role = "User")
        {
            _userManager.Create(item, password);
            _userManager.AddToRole(item.Id, role);
        }
        
        public void SetLock(string id, bool block)
        {
            _userManager.SetLockoutEnabled(id, block);
        }


        public void Edit(ApplicationUser item)
        {
            _userManager.Update(item);
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
