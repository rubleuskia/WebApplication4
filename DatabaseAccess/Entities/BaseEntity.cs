using System;

namespace DatabaseAccess.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdatedAt { get; set; }

        public string LastUpdatedById { get; set; }

        public string CreatedById { get; set; }

        public User CreatedBy { get; set; }

        public User LastUpdatedBy { get; set; }
    }
}