using RskAnalysis.CORE.IntRepository;
using RskAnalysis.CORE.IntServices.IntRejectedContractsServ;
using RskAnalysis.CORE.IntUnitOfWork;
using RskAnalysis.CORE.Models;

namespace RskAnalysis.SERVICE.Services.RejectedContractsServ
{
    public class RejectedContractsService : Service<RejectedContracts>, IRejectedContractsService
    {
        public RejectedContractsService(IUnitOfWork unitOfWork, IRepository<RejectedContracts> repo) : base(unitOfWork, repo)
        {
        }
    }
}
