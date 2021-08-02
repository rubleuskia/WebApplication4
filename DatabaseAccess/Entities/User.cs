using System;
using System.Collections.Generic;
using DatabaseAccess.Entities.Files;
using Microsoft.AspNetCore.Identity;

namespace DatabaseAccess.Entities
{
    public class User : IdentityUser
    {
        public int Age { get; set; }

        public FileModel Photo { get; set; }

        public Guid? PhotoId { get; set; }

        public ICollection<Account> Accounts { get; set; }
    }
}