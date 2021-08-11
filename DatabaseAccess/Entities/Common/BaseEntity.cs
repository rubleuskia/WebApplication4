using System;

namespace DatabaseAccess.Entities.Common
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
    }
}