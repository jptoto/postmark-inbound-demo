using System.Linq;

namespace postmark_inbound_demo.Infrastructure
{
    public interface IRepository<T>
    {
        void Add(T entity);
    }
}