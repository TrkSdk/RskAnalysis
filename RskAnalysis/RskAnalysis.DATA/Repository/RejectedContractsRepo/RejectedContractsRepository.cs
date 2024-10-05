using RskAnalysis.CORE.IntRepository.IntRejectedContractsRepository;
using RskAnalysis.CORE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RskAnalysis.DATA.Repository.RejectedContractsRepo
{
    public class RejectedContractsRepository : Repository<RejectedContracts>, IRejectedContractsRepository
    {
        public RejectedContractsRepository(AppDbContext db) : base(db)
        {
        }

    }
}
