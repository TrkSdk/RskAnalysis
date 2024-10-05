using RskAnalysis.CORE.Models;
using RskAnalysis.CORE.IntRepository.IntRejectedContractsRepository;
using Microsoft.EntityFrameworkCore;

namespace RskAnalysis.DATA.Repository.RejectedContractsRepo
{
    public class RejectedContractsRepository : Repository<Contracts>, IRejectedContractsRepository
    {
        private Task task;


        public RejectedContractsRepository(AppDbContext db) : base(db)
        {
        }

        public async Task<List<Contracts>> GetRejectedContractByIdWithPartners(int id)
        {
            var rejectedcontracts = await _db.Contracts
                .Include(b => b.Partner)
                .Where(x => x.ContractId == id)
                .Where(y => y.IsRejected == true)
                .ToListAsync();

            return rejectedcontracts;
        }

        public async Task<List<Contracts>> GetRejectedContractWithPartners()
        {
            var rejectedcontracts = await _db.Contracts
                .Include(b => b.Partner)
                .Where(y => y.IsRejected == true)
                .ToListAsync();

            return rejectedcontracts;

        }

    }
}
