using RskAnalysis.CORE.Models;
using RskAnalysis.CORE.IntRepository.IntContractsRepository;
using Microsoft.EntityFrameworkCore;

namespace RskAnalysis.DATA.Repository.ContractsRepo
{
    public class ContractsRepository : Repository<Contracts>, IContractsRepository
    {
        public ContractsRepository(AppDbContext db) : base(db)
        {
        }

        public async Task<List<Contracts>> GetContractByIdWithPartners(int id)
        {
            var contracts = await _db.Contracts
                .Include(b => b.Partner)
                .Where(x => x.ContractId == id)
                .Where(y => y.IsRejected == false)
                .ToListAsync();

            return contracts;
        }

        public async Task<List<Contracts>> GetContractWithPartners()
        {
            var contracts = await _db.Contracts
                .Include(b => b.Partner)
                .Where(y => y.IsRejected == false)
                .ToListAsync();

            return contracts;

        }

    }
}
