using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SystemTheLastBugSpa.Data.Entity
{
    public class Roles : IdentityRole<Guid>
    {
        [StringLength(150)]
        public string Description { get; set; }
        public virtual ICollection<Users> Users { get; set; }
        public virtual ICollection<RefreshToken> RefreshToken { get; set; }
    }
}
