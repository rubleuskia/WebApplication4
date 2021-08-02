using System;

namespace DatabaseAccess.Entities.Common
{
    public interface IHaveUpdatedBy
    {
        DateTime UpdatedAt { get; set; }

        string UpdatedById { get; set; }
    }
}