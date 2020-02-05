using AgroBarn.Domain.ApiModels.V1.Response;
using System.Collections.Generic;

namespace AgroBarn.Domain.ApiModels.V1.Result
{
    public class BreedResult
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Success { get; set; }

        public int CodeError { get; set; }

        public List<ErrorModel> Errors { get; set; }
    }
}
