using RskAnalysis.CORE.IntRepository.IntRisksRepository;
using RskAnalysis.CORE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RskAnalysis.CORE.IntRepository.IntSectorsRepository;

namespace RskAnalysis.DATA.Repository.SectorsRepo
{
    public class SectorsRepository : Repository<Sectors>, ISectorsRepository
    {
        public SectorsRepository(AppDbContext db) : base(db)
        {
        }
    }
}
