using RskAnalysis.CORE.IntRepository.IntSectorsRepository;
using RskAnalysis.CORE.Models;

namespace RskAnalysis.DATA.Repository.SectorsRepo
{
    public class SectorsRepository : Repository<Sectors>, ISectorsRepository
    {
        public SectorsRepository(AppDbContext db) : base(db)
        {
        }
    }
}
