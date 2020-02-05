using Microsoft.AspNetCore.Identity;

namespace AgroBarn.Domain.Entities
{
    public class ApplicationRole : IdentityRole<int>
    {
        public string Description { get; set; }

        public int Status { get; set; }
    }
}
