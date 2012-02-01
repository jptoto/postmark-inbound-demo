using postmark_inbound_demo.Infrastructure;
using postmark_inbound_demo.Resources;

namespace postmark_inbound_demo.Repositories
{
    public interface IEmailRepository : IRepository<Email>
    {
    }
}