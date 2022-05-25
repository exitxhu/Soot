using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Soot.Db.Ef.RepoImples;
using Soot.Db.Ef.RepoImples.Queries;
using Soot.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soot.Db.Ef.Helpers
{
    public static class DiExtension
    {
        public static IServiceCollection AddSootDbEF(this IServiceCollection services,Action<DbContextOptionsBuilder> op)
        {
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IContactQueryRepository, ContactQueryRepository>();
            services.AddDbContext<SootContext>(op);
            return services;
        }
    }
}
