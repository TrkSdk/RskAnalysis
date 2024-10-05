using RskAnalysis.CORE.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RskAnalysis.API.DTOs
{
    public class ContractsDto
    {
        public int ContractId { get; set; }  // Primary key
        public double Amount { get; set; }  // Kontrat tutarı
        public int PartnerId { get; set; }  // Foreign key. Businesses tablosuna bağlı
        public string ContractName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedDate { get; set; }  // Kaydın oluşturulma tarihi
    }
}
