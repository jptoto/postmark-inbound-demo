using System;
using System.Collections.Generic;
using postmark_inbound_demo.Infrastructure;
using postmark_inbound_demo.Resources;

namespace postmark_inbound_demo.Repositories
{
    public class EmailRepository : InMemoryRepository<Email>, IEmailRepository
    {
        public EmailRepository()
        {
            entities = new List<Email>(){ };
        }

        protected override void OnAdd(Email entity){ }
    }
}