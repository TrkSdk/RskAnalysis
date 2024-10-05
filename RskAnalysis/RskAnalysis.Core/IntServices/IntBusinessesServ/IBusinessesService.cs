using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RskAnalysis.CORE.Models;

namespace RskAnalysis.CORE.IntServices.IntBusinessesServ
{
    public interface IBusinessesService : IService<Businesses>
    {
        Task<IEnumerable<Businesses>> GetBussinessWithSectors();
        Task<List<Businesses>> GetBussinessByIdWithSectors(int id);
    }
}
