using System.ComponentModel.DataAnnotations;

namespace RskAnalysis.WEB.Models
{
    public class CitiesW
    {
        public int CityId { get; }  // Primary key


        [Required(AllowEmptyStrings = false)]
        public string CityName { get; set; }

        //public virtual List<Partners> PartnersList { get; set; }  // 1-N ilişki, Partners için "PartnersList" kullanarak çakışmayı önledik

    }
}
