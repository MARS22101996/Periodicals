using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Periodicals.DAL.Context;
using Periodicals.DAL.Entities;
using Periodicals.DAL.Repository.Abstract;

namespace Periodicals.DAL.Repository.Concrete
{
    public class PersonalAccountRepository : IPersonalAccountRepository
    {
        private readonly ApplicationDbContext _db;

        public PersonalAccountRepository(ApplicationDbContext context)
        {
            _db = context;
        }

        public List<PersonalAccount> GetUserAccount(string userId)
        {
            return _db.PersonalAccounts.AsEnumerable().Where(p => p.ApplicationUserId == userId).ToList();
        }

        public void Add(PersonalAccount item)
        {
            _db.PersonalAccounts.Add(item);
            _db.SaveChanges();
        }

        public void Edit(PersonalAccount item)
        {
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Delete(int? id)
        {
            var item = FindById(id);
            if (item == null) return;
            _db.PersonalAccounts.Remove(item);
            _db.SaveChanges();
        }

        public List<PersonalAccount> Get()
        {
            return _db.PersonalAccounts.ToList();
        }


        public PersonalAccount FindById(int? id)
        {
            return _db.PersonalAccounts.Find(id);
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
