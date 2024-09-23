using RskAnalysis.CORE.IntRepository;
using RskAnalysis.CORE.IntServices.IntPartnersServ;
using RskAnalysis.CORE.IntUnitOfWork;
using RskAnalysis.CORE.Models;

namespace RskAnalysis.SERVICE.Services.PartnersServ
{
    public class PartnersService : Service<Partners>, IPartnersService
    {
        public PartnersService(IUnitOfWork unitOfWork, IRepository<Partners> repo) : base(unitOfWork, repo)
        {
        }
    }
}
