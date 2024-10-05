using RskAnalysis.CORE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RskAnalysis.CORE.IntRepository.IntCitiesRepository
{
    public interface ICitiesRepository : IRepository<Cities>
    {
        //Task<IEnumerable<Cities>> GetByIdAsync(int id);
    }
}
