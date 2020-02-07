using System;

namespace AgroBarn.Domain.ApiModels.V1.Others
{
    public class JwtSettings
    {
        public string Secret { get; set; }

        public TimeSpan TokenLifeTime { get; set; }

        public int RefreshTokenLifeTime { get; set; }

    }
}
