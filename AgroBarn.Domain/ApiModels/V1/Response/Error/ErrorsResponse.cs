using System.Collections.Generic;

namespace AgroBarn.Domain.ApiModels.V1.Response
{
    public class ErrorsResponse
    {
        public List<ErrorResponse> Errors { get; set; }
    }
}
