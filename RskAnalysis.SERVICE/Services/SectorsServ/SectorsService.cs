using RskAnalysis.CORE.IntRepository;
using RskAnalysis.CORE.IntServices.IntSectorsServ;
using RskAnalysis.CORE.IntUnitOfWork;
using RskAnalysis.CORE.Models;

namespace RskAnalysis.SERVICE.Services.SectorsServ
{
    public class SectorsService : Service<Sectors>, ISectorsService
    {
        public SectorsService(IUnitOfWork unitOfWork, IRepository<Sectors> repo) : base(unitOfWork, repo)
        {
        }
    }
}
