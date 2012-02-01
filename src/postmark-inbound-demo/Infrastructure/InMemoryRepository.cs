using System;
using System.Collections.Generic;
using System.Linq;

namespace postmark_inbound_demo.Infrastructure
{
    public abstract class InMemoryRepository<T> : IRepository<T> where T:new()
    {
        protected List<T> entities;

        public void Add(T entity)
        {
            OnAdd(entity);
            entities.Add(entity);
        }

        protected abstract void OnAdd(T entity);
    }
}