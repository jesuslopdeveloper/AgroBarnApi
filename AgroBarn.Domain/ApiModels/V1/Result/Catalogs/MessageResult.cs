using AgroBarn.Domain.ApiModels.V1.Response;
using System.Collections.Generic;

namespace AgroBarn.Domain.ApiModels.V1.Result
{
    public class MessageResult
    {
        public int Id { get; set; }

        public string Module { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public bool Success { get; set; }

        public int CodeError { get; set; }

        public List<ErrorResponse> Errors { get; set; }
    }
}
