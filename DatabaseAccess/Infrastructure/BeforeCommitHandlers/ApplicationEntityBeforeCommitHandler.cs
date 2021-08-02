using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DatabaseAccess.Infrastructure.BeforeCommitHandlers
{
    public abstract class ApplicationEntityBeforeCommitHandler : IBeforeCommitHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        protected ApplicationEntityBeforeCommitHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Task Execute(ApplicationContext context)
        {
            var userId = _httpContextAccessor.HttpContext.User.Claims
                .Single(x => x.Type == ClaimTypes.NameIdentifier)
                .Value;

            var now = DateTime.UtcNow;

            foreach (EntityEntry entry in context.ChangeTracker.Entries().Where(IsApplicable))
            {
                Execute(entry, userId, now);
            }

            return Task.CompletedTask;
        }

        protected abstract bool IsApplicable(EntityEntry entry);
        protected abstract void Execute(EntityEntry entry, string userId, DateTime now);
    }
}