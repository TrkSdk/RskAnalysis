using RskAnalysis.CORE.IntRepository;
using RskAnalysis.CORE.IntServices.IntContractsServ;
using RskAnalysis.CORE.IntUnitOfWork;
using RskAnalysis.CORE.Models;

namespace RskAnalysis.SERVICE.Services.ContractsServ
{
    public class ContractsService : Service<Contracts>, IContractsService
    {
        public ContractsService(IUnitOfWork unitOfWork, IRepository<Contracts> repo) : base(unitOfWork, repo)
        {
        }
    }
}
