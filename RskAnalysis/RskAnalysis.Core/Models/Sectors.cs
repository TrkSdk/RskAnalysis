using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RskAnalysis.CORE.Models
{
    public class Sectors
    {
        public int SectorId { get; set; }  // Primary key

        
        
        public string SectorName { get; set; }

        
        
        public string SectorDescription { get; set; }

                
        public DateTime CreatedDate { get; set; }  // Kaydın oluşturulma tarihi

        //public virtual Businesses Business { get; set; }  // 1-1 ilişki için navigation property
    }

}
