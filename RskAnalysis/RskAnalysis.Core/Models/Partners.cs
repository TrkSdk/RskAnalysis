using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RskAnalysis.CORE.Models
{
    public class Partners // Çoğul isim
    {
        public int PartnerId { get; set; }  // Primary key

        [ForeignKey("BusinessId")]
        public int BusinessId { get; set; }  // Foreign key. Sectors tablosuna bağlı
        

        public string PartnerName { get; set; }
        public string ContactPerson { get; set; }
        [ForeignKey("CityId")]
        public int CityId { get; set; } //FK Cities tablosuna bağlıyor
        
        public string ContactEMail { get; set; }

        [Range(0, 100, ErrorMessage = "0 ile 100 arasında bir değer giriniz.")]
        public int RiskFactor { get; set; }  // Varsa firmanın geçmiş işlemlerden gelen belirlenmiş risk oranı

        public DateTime CreatedDate { get; set; }  // Kaydın oluşturulma tarihi

        //public virtual List<Contracts> ContractsList { get; set; }  // 1-N ilişki, Contracts için "ContractsList" kullanarak çakışmayı önledik

        public Businesses? Business { get; set; }  // Navigation property for 1-1 relationship (Tekil "Sector")
        public Cities? City { get; set; }  // Navigation property, burada "City" olarak değiştirdik
    }

}
