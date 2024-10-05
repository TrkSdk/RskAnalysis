using RskAnalysis.CORE.IntRepository;
using RskAnalysis.CORE.IntServices.IntRejectedContractsServ;
using RskAnalysis.CORE.IntUnitOfWork;
using RskAnalysis.CORE.Models;

namespace RskAnalysis.SERVICE.Services.RejectedContractsServ
{
    public class RejectedContractsService : Service<Contracts>, IRejectedContractsService
    {
        public RejectedContractsService(IUnitOfWork unitOfWork, IRepository<Contracts> repo) : base(unitOfWork, repo)
        {
        }

        public async Task<List<Contracts>> GetRejectedContractByIdWithPartners(int id)
        {
            return await _UnitOfWork.RejectedContracts.GetRejectedContractByIdWithPartners(id);
        }

        public async Task<List<Contracts>> GetRejectedContractWithPartners()
        {
            return await _UnitOfWork.RejectedContracts.GetRejectedContractWithPartners();
        }

    }
}
