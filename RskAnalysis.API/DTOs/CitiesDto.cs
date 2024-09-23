using RskAnalysis.CORE.Models;
using System.ComponentModel.DataAnnotations;

namespace RskAnalysis.API.DTOs
{
    public class CitiesDto
    {
        public int CityId { get; set; }  // Primary key


        [Required(AllowEmptyStrings = false)]
        public string CityName { get; set; }

        //public virtual List<Partners> PartnersList { get; set; }  // 1-N ilişki, Partners için "PartnersList" kullanarak çakışmayı önledik
    }
}
