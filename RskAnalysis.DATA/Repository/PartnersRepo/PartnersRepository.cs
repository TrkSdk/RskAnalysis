using RskAnalysis.CORE.IntRepository.IntCitiesRepository;
using RskAnalysis.CORE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RskAnalysis.CORE.IntRepository.IntPartnersRepository;

namespace RskAnalysis.DATA.Repository.PartnersRepo
{
    public class PartnersRepository : Repository<Partners>, IPartnersRepository
    {
        public PartnersRepository(AppDbContext db) : base(db)
        {
        }
    }
}
