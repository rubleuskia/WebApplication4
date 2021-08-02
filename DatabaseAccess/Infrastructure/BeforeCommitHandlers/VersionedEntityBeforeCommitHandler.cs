using System.Linq;
using System.Threading.Tasks;
using DatabaseAccess.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DatabaseAccess.Infrastructure.BeforeCommitHandlers
{
    public class VersionedEntityBeforeCommitHandler : IBeforeCommitHandler
    {
        public Task Execute(ApplicationContext context)
        {
            foreach (EntityEntry entry in context.ChangeTracker.Entries()
                .Where(x => x.State is EntityState.Modified or EntityState.Deleted))
            {
                if (entry.Entity is IHaveVersion versionedEntity)
                {
                    entry.Property(nameof(IHaveVersion.RowVersion)).OriginalValue = versionedEntity.RowVersion;
                }
            }

            return Task.CompletedTask;
        }
    }
}