using AgroBarn.Domain.Repositories.V1;

namespace AgroBarn.Domain.Supervisor.V1
{
    public partial class AgroBarnSupervisor : IAgroBarnSupervisor
    {
        //Catalogs Repository
        private readonly IBreedRepository _breedRepository;

        public AgroBarnSupervisor(
            IBreedRepository breedRepository
        )
        {
            //Catalog Breed
            _breedRepository = breedRepository;
        }
    }
}
