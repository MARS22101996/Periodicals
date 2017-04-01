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
    public class PublicationRepository : IPublicationRepository
    {
        private readonly ApplicationDbContext _db;

        public PublicationRepository(ApplicationDbContext context)
        {
            _db = context;
        }

       

        public void Add(Publication item)
        {
            _db.Publications.Add(item);
            _db.SaveChanges();
        }

        public void Edit(Publication item)
        {
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
        }
 
        public void Delete(int? id)
        {
            var item = FindById(id);
            if (item == null) return;
            _db.Publications.Remove(item);
            _db.SaveChanges();
        }

        public List<Publication> Get()
        {
            return _db.Publications.OrderBy(x=>x.NameOfPublication).ToList();
        }

        public Publication FindById(int? id)
        {
            return _db.Publications.Find(id);
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
