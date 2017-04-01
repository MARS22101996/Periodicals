using System;
using System.Collections.Generic;

namespace Periodicals.DAL.Repository.Abstract
{
    public interface IRepository<T> : IDisposable
        where T : class
    {
        void Add(T item);
        void Edit(T item);
        void Delete(int? id);
        List<T> Get();
        T FindById(int? id);

    }
}
