using System;

namespace AgroBarn.Domain.ApiModels.V1.Response
{
    public class RoleResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string NormalizedName { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }
    }
}
