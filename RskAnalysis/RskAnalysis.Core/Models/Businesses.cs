using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace RskAnalysis.CORE.Models
{
    public class Businesses 
    {
        public Businesses()
        {
            Sector = new Sectors();
        }

        [JsonPropertyName("businessId")]
        public int BusinessId { get; set; }  // Primary key

        [JsonPropertyName("sectorId")]
        [ForeignKey("SectorId")]
        public int SectorId { get; set; }  // Foreign key. Sectors tablosuna bağlar. 1-1 bağlantı

        //public virtual List<Contracts> ContractsList { get; set; }  // 1-N ilişki, Contracts için "ContractsList" kullanarak çakışmayı önledik

        [JsonPropertyName("businessName")]
        [Required(AllowEmptyStrings = false)]
        public string BusinessName { get; set; }

        [JsonPropertyName("businessDescription")]
        [Required(AllowEmptyStrings = false)]
        public string BusinessDescription { get; set; }

        [JsonPropertyName("riskFactor")]
        [Range(0, 100, ErrorMessage = "0 ile 100 arasında bir değer giriniz.")]
        public int RiskFactor { get; set; }  // İş alanının genel riski

        [JsonPropertyName("createdDate")]
        public DateTime CreatedDate { get; set; }  // Kaydın oluşturulma tarihi

        public Sectors? Sector { get; set; }  // Navigation property, burada "Sector" olarak değiştirdik

        
    }

}
