using RskAnalysis.CORE.IntRepository.IntCitiesRepository;
using RskAnalysis.CORE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RskAnalysis.CORE.IntRepository.IntPartnersRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace RskAnalysis.DATA.Repository.PartnersRepo
{
    public class PartnersRepository : Repository<Partners>, IPartnersRepository
    {
        public PartnersRepository(AppDbContext db) : base(db)
        {
        }

        public async Task<List<Partners>> GetPartnersByIdWithBussinessAndCity(int id)
        {
            var part = await _db.Partners
                .Include(b => b.Business)
                .Include(b => b.City)
                .Where(x => x.PartnerId == id)
                .ToListAsync();

            return part;
        }

        public async Task<List<Partners>> GetPartnersWithBussinessAndCity()
        {
            var part = await _db.Partners
                .Include(b => b.Business)
                .Include(c => c.City)
                .ToListAsync();

            return part;
        }
    }
}
