using RskAnalysis.CORE.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RskAnalysis.API.DTOs
{
    public class ContractsDto
    {
        public int ContractId { get; set; }  // Primary key
        public bool IsRejected { get; set; }      
        public double Amount { get; set; }  // Kontrat tutarı
        public int PartnerId { get; set; }  // Foreign key. Businesses tablosuna bağlı
        public string ContractName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int RiskFactor { get; set; }  // Contract için hesaplanan Risk Faktörü

        public DateTime CreatedDate { get; set; }  // Kaydın oluşturulma tarihi
    }
}
