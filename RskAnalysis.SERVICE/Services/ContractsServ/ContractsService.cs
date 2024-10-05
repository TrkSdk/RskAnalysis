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

        public async Task<List<Contracts>> GetContractByIdWithPartners(int id)
        {
            return await _UnitOfWork.Contracts.GetContractByIdWithPartners(id);
        }

        public async Task<List<Contracts>> GetContractWithPartners()
        {
            return await _UnitOfWork.Contracts.GetContractWithPartners();
        }

    }
}
