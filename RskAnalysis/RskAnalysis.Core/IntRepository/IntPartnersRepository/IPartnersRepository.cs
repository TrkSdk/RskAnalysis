using RskAnalysis.CORE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RskAnalysis.CORE.IntRepository.IntPartnersRepository
{
    public interface IPartnersRepository : IRepository<Partners>
    {
        Task<List<Partners>> GetPartnersWithBussinessAndCity();
        Task<List<Partners>> GetPartnersByIdWithBussinessAndCity(int id);
    }
}
