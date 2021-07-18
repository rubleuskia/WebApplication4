using System;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public Guid UserId => Guid.Parse(Id);

        public int Age { get; set; }
    }
}