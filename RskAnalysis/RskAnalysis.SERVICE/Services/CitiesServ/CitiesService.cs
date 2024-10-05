using RskAnalysis.CORE.IntRepository;
using RskAnalysis.CORE.IntServices.IntCitiesServ;
using RskAnalysis.CORE.IntUnitOfWork;
using RskAnalysis.CORE.Models;

namespace RskAnalysis.SERVICE.Services.CitiesServ
{
    public class CitiesService : Service<Cities>, ICitiesService
    {
        public CitiesService(IUnitOfWork unitOfWork, IRepository<Cities> repo) : base(unitOfWork, repo)
        {
        }
    }
}
