using RskAnalysis.CORE.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RskAnalysis.API.DTOs
{
    public class BusinessesDto
    {
        public int BusinessId { get; set; }  // Primary key
        public int SectorId { get; set; }  // Foreign key. Sectors tablosuna bağlar. 1-1 bağlantı
        public virtual List<Contracts> ContractsList { get; set; }  // 1-N ilişki, Contracts için "ContractsList" kullanarak çakışmayı önledik
        public string BusinessName { get; set; }
        public string BusinessDescription { get; set; }
        public int RiskFactor { get; set; }  // İş alanının genel riski
        public DateTime CreatedDate { get; set; }  // Kaydın oluşturulma tarihi
    }
}
