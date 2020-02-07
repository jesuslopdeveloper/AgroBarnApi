using AgroBarn.Domain.Entities;
using AgroBarn.Domain.Repositories.V1;

using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace AgroBarn.Domain.Supervisor.V1
{
    public partial class AgroBarnSupervisor : IAgroBarnSupervisor
    {
        //Custom Manager Identity
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        //People
        private readonly IPeopleRepository _peopleRepository;

        //AutoMapper
        private readonly IMapper _mapper;

        //Catalogs Repository
        private readonly IBreedRepository _breedRepository;
        private readonly IMessageRepository _messageRepository;

        public AgroBarnSupervisor(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IMapper mapper,
            IPeopleRepository peopleRepository,
            IBreedRepository breedRepository,
            IMessageRepository messageRepository
        )
        {
            //Custom Manager Identity
            _userManager = userManager;
            _roleManager = roleManager;

            //AutoMapper
            _mapper = mapper;

            //People
            _peopleRepository = peopleRepository;

            //Catalog Breed
            _breedRepository = breedRepository;
            _messageRepository = messageRepository;
        }
    }
}
