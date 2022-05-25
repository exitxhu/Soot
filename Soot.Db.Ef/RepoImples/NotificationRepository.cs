using Soot.Domain;
using Soot.Domain.Repositories;

namespace Soot.Db.Ef.RepoImples
{
    public class NotificationRepository : Repository<Notification>, INotificationRepository
    {
        public NotificationRepository(SootContext dbContext) : base(dbContext)
        {
        }

        public override async Task<Notification?> GetByIdAsync(object id)
        {
            return await _dbContext.Notifications.FindAsync(id);
        }
    }
    public class TagRepository : Repository<Tag>, ITagRepository
    {
        public TagRepository(SootContext dbContext) : base(dbContext)
        {
        }

        public override async Task<Tag?> GetByIdAsync(object id)
        {
            return await _dbContext.Tags.FindAsync(id);
        }
    }
}
