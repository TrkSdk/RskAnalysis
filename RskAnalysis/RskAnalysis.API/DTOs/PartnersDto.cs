using RskAnalysis.CORE.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RskAnalysis.API.DTOs
{
    public class PartnersDto
    {
        public int PartnerId { get; set; }  // Primary key
        public int BusinessId { get; set; }  // Foreign key. Sectors tablosuna bağlı
        public string PartnerName { get; set; }
        public string ContactPerson { get; set; }
        public int CityId { get; set; } //FK Cities tablosuna bağlıyor
        public string ContactEMail { get; set; }
        public int RiskFactor { get; set; }  // Varsa firmanın geçmiş işlemlerden gelen belirlenmiş risk oranı
        public DateTime CreatedDate { get; set; }  // Kaydın oluşturulma tarihi
        //public virtual List<Contracts> ContractsList { get; set; }  // 1-N ilişki, Contracts için "ContractsList" kullanarak çakışmayı önledik
    }
}
