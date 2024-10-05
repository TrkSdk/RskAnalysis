using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RskAnalysis.CORE.Models
{
    public class Contracts 
    {
        public Contracts()
        {
            Partner = new Partners();
        }
        
        public int ContractId { get; set; }  // Primary key

        public double Amount { get; set; }  // Kontrat tutarı

        [ForeignKey("PartnerId")]
        public int PartnerId { get; set; }  // Foreign key. Businesses tablosuna bağlı

        [Required(AllowEmptyStrings = false)]
        public string ContractName { get; set; }
        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public int RiskFactor { get; set; }  // Contract için hesaplanan Risk Faktörü

        public bool IsRejected { get; set; }    //Reddedilmiş bir kontrat mı

        public DateTime CreatedDate { get; set; }  // Kaydın oluşturulma tarihi

        public Partners? Partner { get; set; }  // Navigation property for 1-N relationship
    }

}
