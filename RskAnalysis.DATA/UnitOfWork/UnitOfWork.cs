using RskAnalysis.CORE.IntRepository.IntBusinessesRepository;
using RskAnalysis.CORE.IntRepository.IntCitiesRepository;
using RskAnalysis.CORE.IntRepository.IntContractsRepository;
using RskAnalysis.CORE.IntRepository.IntRejectedContractsRepository;
using RskAnalysis.CORE.IntRepository.IntPartnerRequestRepository;
using RskAnalysis.CORE.IntRepository.IntPartnersRepository;
using RskAnalysis.CORE.IntRepository.IntRejectedContractsRepository;
using RskAnalysis.CORE.IntRepository.IntSectorsRepository;

using RskAnalysis.CORE.IntUnitOfWork;

using RskAnalysis.DATA.Repository.BusinessesRepo;
using RskAnalysis.DATA.Repository.CitiesRepo;
using RskAnalysis.DATA.Repository.ContractsRepo;
using RskAnalysis.DATA.Repository.RejectedContractsRepo;
using RskAnalysis.DATA.Repository.PartnerRequestRepo;
using RskAnalysis.DATA.Repository.PartnersRepo;
using RskAnalysis.DATA.Repository.RejectedContractsRepo;
using RskAnalysis.DATA.Repository.SectorsRepo;
using System.Net.Http;

namespace RskAnalysis.DATA.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;
        
        private BusinessRepository _businessRepository;        
        private CitiesRepository _citiesRepository;        
        private ContractsRepository _contractsRepository;        
        private PartnersRepository _partnersRepository;        
        private PartnerRequestRepository _partnerRequestRepository;       
        
        private SectorsRepository _sectorsRepository;
        private RejectedContractsRepository _rejectedContractsRepository;

        //private IConfiguration _config;


        public UnitOfWork(AppDbContext db/*,IConfiguration config*/)
        {
            _db= db;
            //_config = config;  
        }       

        public IBusinessesRepository Businesses => _businessRepository ??= new
            BusinessRepository(_db);

        public ICitiesRepository Cities => _citiesRepository ??= new
            CitiesRepository(_db);

        public IContractsRepository Contracts => _contractsRepository ??= new
            ContractsRepository(_db);

        public IPartnersRepository Partners => _partnersRepository ??= new
            PartnersRepository(_db);
        
        public IPartnerRequestRepository PartnerRequest => _partnerRequestRepository ??= new
            PartnerRequestRepository(_db, _contractsRepository, _rejectedContractsRepository);

        

        public ISectorsRepository Sectors => _sectorsRepository ??= new
            SectorsRepository(_db);
        public IRejectedContractsRepository RejectedContracts => _rejectedContractsRepository ??= new
            RejectedContractsRepository(_db);

        public void Commit()
        {
            _db.SaveChanges(); 
        }

        public async Task CommitAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
