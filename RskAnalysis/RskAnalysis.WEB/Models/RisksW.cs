using System.ComponentModel.DataAnnotations;


namespace RskAnalysis.WEB.Models
{
    public class RisksW
    {

        public int RiskId { get; set; }  // Primary key

        [Required]
        public int RiskScore { get; set; }  // Hesaplanacak olan Risk Skoru
       
        [Required]
        public int RiskEstimationSuccess { get; set; }  // Risk başarı oranı

        public virtual List<ContractsW> ContractsList { get; set; }
    }
}
