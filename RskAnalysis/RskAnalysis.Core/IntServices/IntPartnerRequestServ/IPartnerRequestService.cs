using RskAnalysis.CORE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RskAnalysis.CORE.IntServices.IntPartnerRequestServ
{
    public interface IPartnerRequestService : IService<PartnerRequest>
    {
        Task<PartnerRequest> TakePartnerRequest(PartnerRequest partnerRequest);
    }
}
