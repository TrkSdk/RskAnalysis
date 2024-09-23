using RskAnalysis.CORE.IntServices.IntBusinessesServ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RskAnalysis.CORE.Models;
using RskAnalysis.CORE.IntUnitOfWork;
using RskAnalysis.CORE.IntRepository;


namespace RskAnalysis.SERVICE.Services.BusinessesServ
{
    public class BusinessesService : Service<Businesses>, IBusinessesService
    {
        public BusinessesService(IUnitOfWork unitOfWork, IRepository<Businesses> repo) : base(unitOfWork, repo)
        {
        }

        public async Task<List<Businesses>> GetBussinessByIdWithSectors(int id)
        {
            return await _UnitOfWork.Businesses.GetBussinessByIdWithSectors(id);
        }

        public async Task<IEnumerable<Businesses>> GetBussinessWithSectors()
        {
            return await _UnitOfWork.Businesses.GetBussinessWithSectors();
        }
    }
}
