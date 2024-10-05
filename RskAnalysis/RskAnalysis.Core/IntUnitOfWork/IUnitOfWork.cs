
using RskAnalysis.CORE.IntRepository.IntBusinessesRepository;
using RskAnalysis.CORE.IntRepository.IntCitiesRepository;
using RskAnalysis.CORE.IntRepository.IntContractsRepository;
using RskAnalysis.CORE.IntRepository.IntPartnerRequestRepository;
using RskAnalysis.CORE.IntRepository.IntPartnersRepository;
using RskAnalysis.CORE.IntRepository.IntRejectedContractsRepository;

using RskAnalysis.CORE.IntRepository.IntSectorsRepository;

namespace RskAnalysis.CORE.IntUnitOfWork
{
    public interface IUnitOfWork
    {
        IBusinessesRepository Businesses { get; }
        ICitiesRepository Cities { get; }
        IContractsRepository Contracts { get; }
        IRejectedContractsRepository RejectedContracts { get; }
        IPartnersRepository Partners { get; }
        
        ISectorsRepository Sectors { get; }
        IPartnerRequestRepository PartnerRequest { get; }
        
        

        //ICategoryRepository category { get; }

        Task CommitAsync();
        void Commit();
    }
}
