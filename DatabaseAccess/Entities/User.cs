using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace DatabaseAccess.Entities
{
    public class User : IdentityUser
    {
        public int Age { get; set; }

        public ICollection<Account> Accounts { get; set; }
    }
}