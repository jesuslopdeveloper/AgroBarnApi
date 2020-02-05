using System.Collections.Generic;

namespace AgroBarn.Domain.ApiModels.V1.Response
{
    public class ErrorResponse
    {
        public ErrorResponse()
        {
        }

        public ErrorResponse(ErrorModel error)
        {
            Errors.Add(error);
        }

        public List<ErrorModel> Errors { get; set; } = new List<ErrorModel>();
    }
}
