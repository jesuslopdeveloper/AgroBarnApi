using System;

namespace AgroBarn.Domain.ApiModels.V1.Request
{
    public class RefreshTokenRequest
    {
        public string Token { get; set; }

        public Guid RefreshToken { get; set; }
    }
}
