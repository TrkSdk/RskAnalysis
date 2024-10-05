using RskAnalysis.CORE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RskAnalysis.CORE.IntRepository.IntPartnerRequestRepository
{
    public interface IPartnerRequestRepository : IRepository<PartnerRequest>
    {
        Task<Contracts> TakePartnerRequest(Contracts contract);
    }
}
