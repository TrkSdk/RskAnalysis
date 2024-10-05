using RskAnalysis.CORE.IntRepository.IntCitiesRepository;
using RskAnalysis.CORE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RskAnalysis.CORE.IntRepository.IntRejectedContractsRepository;


namespace RskAnalysis.CORE.IntRepository.IntRejectedContractsRepository
{
    public interface IRejectedContractsRepository : IRepository<Contracts>
    {
        Task<List<Contracts>> GetRejectedContractWithPartners();
        Task<List<Contracts>> GetRejectedContractByIdWithPartners(int id);


    }
}
