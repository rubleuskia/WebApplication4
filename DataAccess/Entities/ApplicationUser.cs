using System;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public int Age { get; set; }
    }
}