using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DatabaseAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DatabaseAccess.Infrastructure
{
    public class UpdateEntityAsyncBeforeCommitHandler : IAsyncBeforeCommitHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateEntityAsyncBeforeCommitHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Task Execute(ApplicationContext context)
        {
            var userId = _httpContextAccessor.HttpContext.User.Claims
                .Single(x => x.Type == ClaimTypes.NameIdentifier)
                .Value;

            var now = DateTime.UtcNow;

            foreach (EntityEntry entry in context.ChangeTracker.Entries())
            {
                if (entry.Entity is not BaseEntity entity)
                {
                    continue;
                }

                if (entry.State == EntityState.Modified)
                {
                    entity.UpdatedAt = now;
                    entity.UpdatedById = userId;
                }
            }

            return Task.CompletedTask;
        }
    }
}