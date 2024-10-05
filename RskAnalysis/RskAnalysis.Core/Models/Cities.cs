using System.ComponentModel.DataAnnotations;

namespace RskAnalysis.CORE.Models
{
    public class Cities
    {
        public Cities()
        {
            
        }
        public int CityId { get; set; }  // Primary key

        public string CityName { get; set; }

        //public virtual List<Partners> PartnersList { get; set; }  // 1-N ilişki, Partners için "PartnersList" kullanarak çakışmayı önledik

    }
}
