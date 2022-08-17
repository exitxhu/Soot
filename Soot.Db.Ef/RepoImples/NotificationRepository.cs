using Soot.Domain.Entities;
using Soot.Domain.Repositories;

namespace Soot.Db.Ef.RepoImples
{
    public class NotificationRepository : Repository<Notification>, INotificationRepository
    {
        public NotificationRepository(SootContext dbContext) : base(dbContext)
        {
        }

        public override async Task<Notification?> GetByIdAsync(object? id)
        {
            return await DbContext.Notifications.FindAsync(id);
        }
    }
}
