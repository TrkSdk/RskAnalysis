using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RskAnalysis.WEB.Models
{
    public class ContractsW 
    {
        public int ContractId { get; set; }  // Primary key
        
        
        
        public double Amount { get; set; }  // Kontrat tutarı


        [ForeignKey("BusinessId")]
        public int BusinessId { get; set; }  // Foreign key. Businesses tablosuna bağlı
        public virtual BusinessesW Business { get; set; }  // Navigation property for 1-N relationship


        [ForeignKey("PartnerId")]
        public int PartnerId { get; set; }  // Foreign key. Businesses tablosuna bağlı
        public virtual PartnersW Partner { get; set; }  // Navigation property for 1-N relationship


        [Required(AllowEmptyStrings = false)]
        public string ContractName { get; set; }

        
        [Required]
        public DateTime StartDate { get; set; }

        
        [Required]
        public DateTime EndDate { get; set; }


        [ForeignKey("RiskId")]
        public int RiskId { get; set; }  // FK, Bu kontratla ilgili risk değerleri
        public virtual RisksW Risk { get; set; }  // Navigation property, burada "Risk" olarak değiştirdik


        public DateTime CreatedDate { get; set; }  // Kaydın oluşturulma tarihi


    }

}
