using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace SystemTheLastBugSpa.Data.Entity
{
    public class Users : IdentityUser<Guid>
    {
        public bool FirstLogin { get; set; }
        public bool CompletedForm { get; set; }
        public Guid RolesId { get; set; }
        public virtual ICollection<RefreshToken> RefreshToken { get; set; }
    }
}
