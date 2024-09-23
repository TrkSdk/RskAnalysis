
using RskAnalysis.CORE.IntRepository.IntBusinessesRepository;
using RskAnalysis.CORE.IntRepository.IntCitiesRepository;
using RskAnalysis.CORE.IntRepository.IntContractsRepository;
using RskAnalysis.CORE.IntRepository.IntPartnersRepository;
using RskAnalysis.CORE.IntRepository.IntRisksRepository;
using RskAnalysis.CORE.IntRepository.IntSectorsRepository;

namespace RskAnalysis.CORE.IntUnitOfWork
{
    public interface IUnitOfWork
    {
        IBusinessesRepository Businesses { get; }
        ICitiesRepository Cities { get; }
        IContractsRepository Contracts { get; }
        IPartnersRepository Partners { get; }
        IRisksRepository Risks { get; }
        ISectorsRepository Sectors { get; }
        
        

        //ICategoryRepository category { get; }

        Task CommitAsync();
        void Commit();
    }
}
