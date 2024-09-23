using RskAnalysis.CORE.Models;
using System.ComponentModel.DataAnnotations;

namespace RskAnalysis.API.DTOs
{
    public class SectorsDto
    {
        public int SectorId { get; set; }  // Primary key
        public string SectorName { get; set; }
        public string SectorDescription { get; set; }
        public DateTime CreatedDate { get; set; }  // Kaydın oluşturulma tarihi
        public virtual Businesses Business { get; set; }  // 1-1 ilişki için navigation property
    }
}
