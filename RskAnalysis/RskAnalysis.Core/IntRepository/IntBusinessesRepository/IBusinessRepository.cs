using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RskAnalysis.CORE.Models;

namespace RskAnalysis.CORE.IntRepository.IntBusinessesRepository
{
    public interface IBusinessesRepository : IRepository<Businesses>
    {
        Task<List<Businesses>> GetBussinessWithSectors();
        Task<List<Businesses>> GetBussinessByIdWithSectors(int id);
    }
}
