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
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _db;

        public PaymentRepository(ApplicationDbContext context)
        {
            _db = context;
        }

        public void Add(Payment item)
        {
            _db.Payments.Add(item);
            _db.SaveChanges();
        }

        public void Edit(Payment item)
        {
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Delete(int? id)
        {
            var item = FindById(id);
            if (item == null) return;
            _db.Payments.Remove(item);
            _db.SaveChanges();
        }

        public List<Payment> Get()
        {
            return _db.Payments.ToList();
        }



        public Payment FindById(int? id)
        {
            return _db.Payments.Find(id);
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
