using System;

namespace AgroBarn.Domain.ApiModels.V1.Response
{
    public class AuthSuccessResponse
    {
        public string Token { get; set; }

        public Guid RefreshToken { get; set; }
    }
}
