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
    public class UserPublicationRepository : IUserPublicationRepository
    {
        private readonly ApplicationDbContext _db;

        public UserPublicationRepository(ApplicationDbContext context)
        {
            _db = context;
        }

        public void Add(UserPublication item)
        {
            _db.UserPublications.Add(item);
            _db.SaveChanges();
        }

        public void Edit(UserPublication item)
        {
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Delete(int? id)
        {
            var item = FindById(id);
            if (item == null) return;
            _db.UserPublications.Remove(item);
            _db.SaveChanges();
        }

        public List<UserPublication> Get()
        {
            return _db.UserPublications.Include(u => u.Publication).Include(u => u.User).OrderBy(o => o.Publication.NameOfPublication).ToList();
        }
        

        
        public UserPublication FindById(int? id)
        {
            return _db.UserPublications.Find(id);
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
