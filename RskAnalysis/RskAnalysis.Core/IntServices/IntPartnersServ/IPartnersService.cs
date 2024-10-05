using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RskAnalysis.CORE.Models;

namespace RskAnalysis.CORE.IntServices.IntPartnersServ
{
    public interface IPartnersService : IService<Partners>
    {
        Task<IEnumerable<Partners>> GetPartnersWithBussinessAndCity();
        Task<List<Partners>> GetPartnersByIdWithBussinessAndCity(int id);
    }
}
