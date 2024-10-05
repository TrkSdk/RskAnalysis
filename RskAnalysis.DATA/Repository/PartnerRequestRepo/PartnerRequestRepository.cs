using RskAnalysis.CORE.IntRepository.IntContractsRepository;
using RskAnalysis.CORE.IntRepository.IntPartnerRequestRepository;
using RskAnalysis.CORE.IntRepository.IntRejectedContractsRepository;
using RskAnalysis.CORE.Models;
using RskAnalysis.DATA.Repository.ContractsRepo;
using RskAnalysis.DATA.Repository.RejectedContractsRepo;

namespace RskAnalysis.DATA.Repository.PartnerRequestRepo
{

    public class PartnerRequestRepository : Repository<PartnerRequest>, IPartnerRequestRepository
    {
        private IContractsRepository _contractsRepository;
        private IRejectedContractsRepository _rejectedContractsRepository;
        private readonly AppDbContext _db;
        private readonly IPartnerRequestRepository _partnerRequestRepository;

        private bool IsRejected;
         
        public PartnerRequestRepository(AppDbContext db, ContractsRepository contractsRepository, RejectedContractsRepository rejectedContractsRepository) : base(db)
        {
            _contractsRepository = contractsRepository;
            _rejectedContractsRepository = rejectedContractsRepository;
            _db = db;
        }



        public async Task<Contracts> TakePartnerRequest(Contracts contract)
        {

            /** Miktara göre artan risk basamakları **/
            double VeryLowRiskLimit = 1000000;
            double LowRiskLimit = 5000000;
            double MediumRiskLimit = 10000000;
            double HighRiskLimit = 25000000;
            double VeryHighRiskLimit = 50000000;


            Partners part = _db.Partners.Where(x => x.PartnerId == contract.PartnerId).FirstOrDefault();
            Businesses buss = _db.Businesses.Where(x => x.BusinessId == part.BusinessId).FirstOrDefault();
            Cities cty = _db.Cities.Where(x => x.CityId == part.CityId).FirstOrDefault();
            Sectors sect = _db.Sectors.Where(x => x.SectorId == buss.SectorId).FirstOrDefault();


            int ParRiskFactor = part.RiskFactor;
            int BusRiskFactor = buss.RiskFactor;

            var RiskFact = (ParRiskFactor + BusRiskFactor) / 2;

            if (contract.Amount <= VeryLowRiskLimit) RiskFact = Convert.ToInt32(RiskFact * 1.1);
            else if (contract.Amount <= LowRiskLimit) RiskFact = Convert.ToInt32(RiskFact * 1.15);
            else if (contract.Amount <= MediumRiskLimit) RiskFact = Convert.ToInt32(RiskFact * 1.20);
            else if (contract.Amount <= HighRiskLimit) RiskFact = Convert.ToInt32(RiskFact * 1.25);
            else if (contract.Amount <= VeryHighRiskLimit) RiskFact = Convert.ToInt32(RiskFact * 1.30);


            if (RiskFact > 60)
            {
                contract.RiskFactor = RiskFact;
                contract.IsRejected = true;
                return contract; //2  Burada contract doğru
            }
            else
            {
                contract.RiskFactor = RiskFact;
                contract.IsRejected = false;
                return contract;

            };
        }
    }
}



