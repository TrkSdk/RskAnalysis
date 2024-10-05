using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RskAnalysis.CORE.IntRepository;
using RskAnalysis.CORE.IntRepository.IntPartnerRequestRepository;
using RskAnalysis.CORE.IntServices.IntPartnerRequestServ;
using RskAnalysis.CORE.IntUnitOfWork;
using RskAnalysis.CORE.Models;

namespace RskAnalysis.SERVICE.Services.PartnerRequestServ
{
    public class PartnerRequestService : Service<PartnerRequest>, IPartnerRequestService
    {
        public PartnerRequestService(IUnitOfWork unitOfWork, IRepository<PartnerRequest> repo) : base(unitOfWork, repo)
        {
        }
        
        public  Task<Contracts> TakePartnerRequest(Contracts contract)
        {
            return _UnitOfWork.PartnerRequest.TakePartnerRequest(contract);
        }
    }
}
