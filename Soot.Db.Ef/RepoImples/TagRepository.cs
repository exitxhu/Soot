using Soot.Domain.Entities;
using Soot.Domain.Repositories;

namespace Soot.Db.Ef.RepoImples;

public class TagRepository : Repository<Tag>, ITagRepository
{
    public TagRepository(SootContext dbContext) : base(dbContext)
    {
    }
    public override async Task<Tag?> GetByIdAsync(object? id)
    {
        return await DbContext.Tags.FindAsync(id);
    }
}