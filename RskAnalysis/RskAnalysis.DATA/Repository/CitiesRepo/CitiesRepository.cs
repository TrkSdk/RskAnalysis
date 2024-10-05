using RskAnalysis.CORE.IntRepository.IntBusinessesRepository;
using RskAnalysis.CORE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RskAnalysis.CORE.IntRepository.IntCitiesRepository;
using Microsoft.EntityFrameworkCore;

namespace RskAnalysis.DATA.Repository.CitiesRepo
{
    public class CitiesRepository : Repository<Cities>, ICitiesRepository
    {
        private AppDbContext AppDbContext { get => _db as AppDbContext; }
        public CitiesRepository(AppDbContext db) : base(db)
        {
            
        }

        
    }
}
