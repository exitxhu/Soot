using Soot.Domain.Entities;
using Soot.Domain.Repositories;

namespace Soot.Db.Ef.RepoImples
{
    public class InboxRepository : Repository<Inbox>, IInboxRepository
    {
        public InboxRepository(SootContext dbContext) : base(dbContext)
        {
        }

        public override async Task<Inbox?> GetByIdAsync(object? id)
        {
            return await DbContext.Inbox.FindAsync(id);
        }
    }
}
