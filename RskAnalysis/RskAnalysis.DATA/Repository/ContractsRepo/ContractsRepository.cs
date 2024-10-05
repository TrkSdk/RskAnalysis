using RskAnalysis.CORE.IntRepository.IntCitiesRepository;
using RskAnalysis.CORE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RskAnalysis.CORE.IntRepository.IntContractsRepository;

namespace RskAnalysis.DATA.Repository.ContractsRepo
{
    public class ContractsRepository : Repository<Contracts>, IContractsRepository
    {
        public ContractsRepository(AppDbContext db) : base(db)
        {
        }
    }
}
