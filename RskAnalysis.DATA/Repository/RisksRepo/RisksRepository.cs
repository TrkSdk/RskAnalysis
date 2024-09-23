using RskAnalysis.CORE.IntRepository.IntPartnersRepository;
using RskAnalysis.CORE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RskAnalysis.CORE.IntRepository.IntRisksRepository;

namespace RskAnalysis.DATA.Repository.RisksRepo
{
    public class RisksRepository : Repository<Risks>, IRisksRepository
    {
        public RisksRepository(AppDbContext db) : base(db)
        {
        }
    }
}
