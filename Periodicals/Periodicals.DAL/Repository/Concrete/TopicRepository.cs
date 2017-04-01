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
    public class TopicRepository : ITopicRepository
    {
        private readonly ApplicationDbContext _db;

        public TopicRepository(ApplicationDbContext context)
        {
            _db = context;
        }

        public void Add(Topic item)
        {
            _db.Topics.Add(item);
            _db.SaveChanges();
        }

        public void Edit(Topic item)
        {
            if (_db.Topics.Any(p => p.TopicId == item.TopicId))
            {
                _db.Entry(item).State = EntityState.Modified;
                _db.SaveChanges();
            }
        }

        public void Delete(int? id)
        {
            var item = FindById(id);
            if (item == null) return;
            _db.Topics.Remove(item);
            _db.SaveChanges();
        }

        public List<Topic> Get()
        {
            return _db.Topics.ToList();
        }


        public Topic FindById(int? id)
        {
            return _db.Topics.Find(id);
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
