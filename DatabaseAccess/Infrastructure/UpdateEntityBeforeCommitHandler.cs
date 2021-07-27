using System;
using System.Linq;
using System.Threading.Tasks;
using Common.Extensions;
using DatabaseAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DatabaseAccess.Infrastructure
{
    public class EntityUpdateBeforeCommitHandler : IBeforeCommitHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EntityUpdateBeforeCommitHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Task Execute(ApplicationContext context)
        {
            var entries = context.ChangeTracker
                .Entries()
                .Where(e => e.State is EntityState.Modified);

            foreach (var entityEntry in entries)
            {
                if (entityEntry.Entity is BaseEntity entity)
                {
                    var userId = _httpContextAccessor.HttpContext.User.GetUserId();
                    entity.LastUpdatedById = userId;
                    entity.LastUpdatedAt = DateTime.UtcNow;
                }
            }

            return Task.CompletedTask;
        }
    }
}