using AgroBarn.Domain.Entities;
using AgroBarn.Domain.ApiModels.V1.Request;
using AgroBarn.Domain.ApiModels.V1.Response;
using AgroBarn.Domain.ApiModels.V1.Result;

using AutoMapper;

namespace AgroBarn.Domain.MappingProfiles
{
    public class DomainToResponseProfile : Profile
    {
        public DomainToResponseProfile()
        {
            //Catalog Breed
            CreateMap<BreedRequest, BreedDto>();
            CreateMap<BreedDto, BreedResult>();
            CreateMap<BreedResult, BreedResponse>();

            //Others TODO
        }
    }
}
