using RskAnalysis.CORE.Models;
using System.ComponentModel.DataAnnotations;

namespace RskAnalysis.API.DTOs
{
    public class RisksDto
    {
        public int RiskId { get; set; }  // Primary key
        public int RiskScore { get; set; }  // Hesaplanacak olan Risk Skoru
        public int RiskEstimationSuccess { get; set; }  // Risk başarı oranı
        public virtual List<Contracts> ContractsList { get; set; }
    }
}
