using RskAnalysis.CORE.Models;

namespace RskAnalysis.API.DTOs
{
    public class RejectedContractsDto
    {
        public int RejectedContractId { get; }  // Primary key
        public double Amount { get; set; }  // Kontrat tutarı
        public int BusinessId { get; set; }  // Foreign key. Businesses tablosuna bağlı
        public int PartnerId { get; set; }  // Foreign key. Businesses tablosuna bağlı
        public string ContractName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Riskfactor { get; set; }  // Bu kontratla ilgili risk 
        public DateTime CreatedDateTime { get; set; }  // Kaydın oluşturulma tarihi

    }
}
