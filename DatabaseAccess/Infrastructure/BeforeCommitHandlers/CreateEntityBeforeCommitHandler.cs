using System;
using DatabaseAccess.Entities.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DatabaseAccess.Infrastructure.BeforeCommitHandlers
{
    public class CreateEntityBeforeCommitHandler : ApplicationEntityBeforeCommitHandler
    {

        public CreateEntityBeforeCommitHandler(IHttpContextAccessor httpContextAccessor)
            : base(httpContextAccessor)
        {
        }

        protected override bool IsApplicable(EntityEntry entry) => entry.State == EntityState.Added;

        protected override void Execute(EntityEntry entry, string userId, DateTime now)
        {
            if (entry.Entity is IHaveUpdatedBy updatedEntity)
            {
                updatedEntity.UpdatedAt = now;
                updatedEntity.UpdatedById = userId;
            }

            if (entry.Entity is IHaveCreatedBy createdEntity)
            {
                createdEntity.CreatedAt = now;
                createdEntity.CreatedById = userId;
            }
        }
    }
}