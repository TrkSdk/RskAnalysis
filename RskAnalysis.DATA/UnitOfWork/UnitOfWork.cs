using Microsoft.Extensions.Configuration;
using RskAnalysis.CORE.IntRepository.IntBusinessesRepository;
using RskAnalysis.CORE.IntRepository.IntCitiesRepository;
using RskAnalysis.CORE.IntRepository.IntContractsRepository;
using RskAnalysis.CORE.IntRepository.IntPartnersRepository;
using RskAnalysis.CORE.IntRepository.IntRisksRepository;
using RskAnalysis.CORE.IntRepository.IntSectorsRepository;
using RskAnalysis.CORE.IntUnitOfWork;
using RskAnalysis.DATA.Repository.BusinessesRepo;
using RskAnalysis.DATA.Repository.CitiesRepo;
using RskAnalysis.DATA.Repository.ContractsRepo;
using RskAnalysis.DATA.Repository.PartnersRepo;
using RskAnalysis.DATA.Repository.RisksRepo;
using RskAnalysis.DATA.Repository.SectorsRepo;

namespace RskAnalysis.DATA.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;
        
        private BusinessRepository _businessRepository;        
        private CitiesRepository _citiesRepository;        
        private ContractsRepository _contractsRepository;        
        private PartnersRepository _partnersRepository;        
        private RisksRepository _risksRepository;
        private SectorsRepository _sectorsRepository;
       
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

        public IRisksRepository Risks => _risksRepository ??= new
            RisksRepository(_db);

        public ISectorsRepository Sectors => _sectorsRepository ??= new
            SectorsRepository(_db);

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
