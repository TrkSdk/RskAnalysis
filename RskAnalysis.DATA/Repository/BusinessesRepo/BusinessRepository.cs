using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RskAnalysis.CORE.IntRepository.IntBusinessesRepository;
using RskAnalysis.CORE.Models;

namespace RskAnalysis.DATA.Repository.BusinessesRepo
{
    public class BusinessRepository : Repository<Businesses>, IBusinessesRepository
    {
        public BusinessRepository(AppDbContext db) : base(db)
        {
        }

        

        public async Task<List<Businesses>> GetBussinessByIdWithSectors(int id)
        {
            var businesses = await _db.Businesses
                .Include(b => b.Sector)
                .Where(x=>x.BusinessId==id)
                .ToListAsync();

            return businesses;
        }

        public async Task<IEnumerable<Businesses>> GetBussinessWithSectors()
        {
            var businesses = await _db.Businesses
                .Include(b => b.Sector)
                .ToListAsync() ;

            return businesses;

        }

        
    }
}
