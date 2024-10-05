using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RskAnalysis.CORE.IntRepository.IntContractsRepository;
using RskAnalysis.CORE.IntRepository.IntPartnerRequestRepository;
using RskAnalysis.CORE.IntRepository.IntRejectedContractsRepository;
using RskAnalysis.CORE.Models;

namespace RskAnalysis.DATA.Repository.PartnerRequestRepo
{
    public class PartnerRequestRepository : Repository<PartnerRequest>, IPartnerRequestRepository
    {
        private IContractsRepository _contractsRepository;
        private IRejectedContractsRepository _rejectedContractsRepository;
        public PartnerRequestRepository(AppDbContext db,IContractsRepository contractsRepository, IRejectedContractsRepository rejectedContractsRepository) : base(db)
        {
            _contractsRepository = contractsRepository;
            _rejectedContractsRepository = rejectedContractsRepository;
        }

        /** Miktara göre artan risk basamakları **/
        double VeryLowRiskLimit = 1000000;
        double LowRiskLimit = 5000000;
        double MediumRiskLimit = 10000000;
        double HighRiskLimit = 25000000;
        double VeryHighRiskLimit = 50000000;

        public async Task<PartnerRequest> TakePartnerRequest(PartnerRequest partnerRequest)
        {
            int busId = _db.Partners.Where(x => x.PartnerId == partnerRequest.PartnerId).Select(x => x.BusinessId).FirstOrDefault();
            int BusRiskFactor = _db.Businesses.Where(x => x.BusinessId == busId).Select(x => x.RiskFactor).FirstOrDefault();
            int ParRiskFactor = _db.Partners.Where(x => x.PartnerId == partnerRequest.PartnerId).Select(x => x.RiskFactor).FirstOrDefault();


            var RiskFact = (ParRiskFactor + BusRiskFactor) / 2;

            if (partnerRequest.Amount <= VeryLowRiskLimit) RiskFact = Convert.ToInt32(RiskFact * 1.1);
            else if (partnerRequest.Amount <= LowRiskLimit) RiskFact = Convert.ToInt32(RiskFact * 1.15);
            else if (partnerRequest.Amount <= MediumRiskLimit) RiskFact = Convert.ToInt32(RiskFact * 1.20);
            else if (partnerRequest.Amount <= HighRiskLimit) RiskFact = Convert.ToInt32(RiskFact * 1.25);
            else if (partnerRequest.Amount <= VeryHighRiskLimit) RiskFact = Convert.ToInt32(RiskFact * 1.30);

            if (RiskFact>60)
            {
                RejectedContracts rejContracts = new RejectedContracts
                {
                    ContractName = partnerRequest.ContractName,
                    CreatedDate = DateTime.Now,
                    StartDate = partnerRequest.StartDate,
                    EndDate = partnerRequest.EndDate,
                    Amount = partnerRequest.Amount,
                    Partner = null,
                    PartnerId = partnerRequest.PartnerId
                };

                await _rejectedContractsRepository.AddAsync(rejContracts);

                await _db.SaveChangesAsync();

                return partnerRequest;
            }
            else
            {
                Contracts contract = new Contracts
                {
                    ContractName = partnerRequest.ContractName,
                    CreatedDate = DateTime.Now,
                    StartDate = partnerRequest.StartDate,
                    EndDate = partnerRequest.EndDate,
                    Amount = partnerRequest.Amount,
                    Partner = null,
                    PartnerId = partnerRequest.PartnerId
                };

                var contr = contract;

                await _contractsRepository.AddAsync(contr);
                await _db.SaveChangesAsync();
                //metod yazacagiz (Contract tablosuna kayit)
                return partnerRequest;
            }

            
        }
    }
}
//RiskFactor = (PartnerRisk + BusinessRisk) / 2;

//if (Amount <= VeryLowRiskLimit) RiskFactor = Convert.ToInt32(RiskFactor * 1.1);
//else if (Amount <= LowRiskLimit) RiskFactor = Convert.ToInt32(RiskFactor * 1.15);
//else if (Amount <= MediumRiskLimit) RiskFactor = Convert.ToInt32(RiskFactor * 1.20);
//else if (Amount <= HighRiskLimit) RiskFactor = Convert.ToInt32(RiskFactor * 1.25);
//else if (Amount <= VeryHighRiskLimit) RiskFactor = Convert.ToInt32(RiskFactor * 1.30);


//return RiskFactor;


