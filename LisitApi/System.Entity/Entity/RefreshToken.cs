using System;

namespace SystemTheLastBugSpa.Data.Entity
{
    public class RefreshToken:BaseEntity
    {
        public Guid UserId { get; set; }

        public string Token { get; set; }

        public string JwtId { get; set; }

        public bool IsUsed { get; set; }

        public bool IsRevorked { get; set; }

        public DateTime AddedDate { get; set; }

        public DateTime ExpiryDate { get; set; }

       

    }
}
