using RskAnalysis.CORE.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RskAnalysis.API.DTOs
{
    public class ContractsDto
    {
        public int ContractId { get; set; }  // Primary key
        public double Amount { get; set; }  // Kontrat tutarı
        public int BusinessId { get; set; }  // Foreign key. Businesses tablosuna bağlı
        public int PartnerId { get; set; }  // Foreign key. Businesses tablosuna bağlı
        public string ContractName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int RiskId { get; set; }  // FK, Bu kontratla ilgili risk değerleri
        public virtual Risks Risk { get; set; }  // Navigation property, burada "Risk" olarak değiştirdik
        public DateTime CreatedDate { get; set; }  // Kaydın oluşturulma tarihi
    }
}
