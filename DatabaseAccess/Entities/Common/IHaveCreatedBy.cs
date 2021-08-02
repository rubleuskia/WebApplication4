using System;

namespace DatabaseAccess.Entities.Common
{
    public interface IHaveCreatedBy
    {
        DateTime CreatedAt { get; set; }

        string CreatedById { get; set; }
    }
}