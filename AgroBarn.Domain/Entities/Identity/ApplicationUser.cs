using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace AgroBarn.Domain.Entities
{
    public class ApplicationUser : IdentityUser<int>
    {
        public int Status { get; set; }

        public virtual ICollection<PeopleDto> PeopleDto { get; set; }

        public virtual ICollection<RefreshToken> RefreshToken { get; set; }
    }
}
