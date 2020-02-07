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
            //Identity
            CreateMap<PeopleDto, UserResult>();
            CreateMap<UserResult, UserResponse>();
            CreateMap<ApplicationRole, RoleResponse>();

            //Catalog Breed
            CreateMap<BreedRequest, BreedDto>();
            CreateMap<BreedDto, BreedResult>();
            CreateMap<BreedResult, BreedResponse>();

            //Catalog Message
            CreateMap<MessageRequest, MessageDto>();
            CreateMap<MessageDto, MessageResult>();
            CreateMap<MessageResult, MessageResponse>();
            
            //Others TODO
        }
    }
}
