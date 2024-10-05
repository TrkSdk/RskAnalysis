using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RskAnalysis.WEB.Models
{
    public class SectorsW
    {
        public int SectorId { get; set; }  // Primary key

        
        [Required(AllowEmptyStrings = false)]
        public string SectorName { get; set; }

        
        [Required(AllowEmptyStrings = false)]
        public string SectorDescription { get; set; }

                
        public DateTime CreatedDate { get; set; }  // Kaydın oluşturulma tarihi

        public virtual BusinessesW Business { get; set; }  // 1-1 ilişki için navigation property
    }

}
