using System;

namespace AgroBarn.Domain.Entities
{
    public class RefreshToken
    {
        public Guid Token { get; set; }

        public string JwtId { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ExpiryDate { get; set; }

        public bool Used { get; set; }

        public bool Invalidated { get; set; }

        public int UserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
