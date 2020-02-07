using AgroBarn.Domain.ApiModels.V1.Response;

using System;
using System.Collections.Generic;

namespace AgroBarn.Domain.ApiModels.V1.Result
{
    public class AuthenticationResult
    {
        public string Token { get; set; }

        public Guid RefreshToken { get; set; }

        public bool Success { get; set; }

        public List<ErrorModel> Errors { get; set; }
    }
}
