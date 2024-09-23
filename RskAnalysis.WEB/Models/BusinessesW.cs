using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RskAnalysis.WEB.Models
{
    public class BusinessesW 
    {
        public int BusinessId { get; set; }  // Primary key
        public int SectorId { get; set; }  // Foreign key. Sectors tablosuna bağlar. 1-1 bağlantı
        
        public virtual SectorsW Sector { get; set; }  // Navigation property, burada "Sector" olarak değiştirdik

        
        public virtual List<ContractsW> ContractsList { get; set; }  // 1-N ilişki, Contracts için "ContractsList" kullanarak çakışmayı önledik

        
        [Required(AllowEmptyStrings = false)]
        public string BusinessName { get; set; }

        
        [Required(AllowEmptyStrings = false)]
        public string BusinessDescription { get; set; }

        
        [Range(0, 100, ErrorMessage = "0 ile 100 arasında bir değer giriniz.")]
        public int RiskFactor { get; set; }  // İş alanının genel riski

        
        public DateTime CreatedDate { get; set; }  // Kaydın oluşturulma tarihi
    }

}
