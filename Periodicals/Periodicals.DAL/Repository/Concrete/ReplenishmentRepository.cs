using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Periodicals.DAL.Repository.Abstract;
using Periodicals.DAL.Context;
using Periodicals.DAL.Entities;

namespace Periodicals.DAL.Repository.Concrete
{
    public class ReplenishmentRepository : IReplenishmentRepository
    {
        private readonly ApplicationDbContext _db;

        public ReplenishmentRepository(ApplicationDbContext context)
        {
            _db = context;
        }

        public void Add(Replenishment item)
        {
            _db.Replenishments.Add(item);
            _db.SaveChanges();
        }

        public void Edit(Replenishment item)
        {
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Delete(int? id)
        {
            var item = FindById(id);
            if (item == null) return;
            _db.Replenishments.Remove(item);
            _db.SaveChanges();
        }

        public List<Replenishment> Get()
        {
            return _db.Replenishments.ToList();
        }


        public Replenishment FindById(int? id)
        {
            return _db.Replenishments.Find(id);
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
