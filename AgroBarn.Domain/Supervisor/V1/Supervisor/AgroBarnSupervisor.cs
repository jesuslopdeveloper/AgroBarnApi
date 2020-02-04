using AgroBarn.Domain.Repositories.V1;

using AutoMapper;

namespace AgroBarn.Domain.Supervisor.V1
{
    public partial class AgroBarnSupervisor : IAgroBarnSupervisor
    {
        //AutoMapper
        private readonly IMapper _mapper;

        //Catalogs Repository
        private readonly IBreedRepository _breedRepository;
        private readonly IMessageRepository _messageRepository;

        public AgroBarnSupervisor(
            IMapper mapper,
            IBreedRepository breedRepository,
            IMessageRepository messageRepository
        )
        {
            //AutoMapper
            _mapper = mapper;

            //Catalog Breed
            _breedRepository = breedRepository;
            _messageRepository = messageRepository;
        }        
    }
}
