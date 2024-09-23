using RskAnalysis.CORE.IntRepository;
using RskAnalysis.CORE.IntServices.IntRisksServ;
using RskAnalysis.CORE.IntUnitOfWork;
using RskAnalysis.CORE.Models;

namespace RskAnalysis.SERVICE.Services.RisksServ
{
    public class RisksService : Service<Risks>, IRisksService
    {
        public RisksService(IUnitOfWork unitOfWork, IRepository<Risks> repo) : base(unitOfWork, repo)
        {
        }
    }
}
