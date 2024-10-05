namespace RskAnalysis.API.DTOs
{
    public class PartnerRequestDto
    {
        public string PartnerName { get; set; }  
        public string ContractName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double Amount { get; set; }
        public DateTime CreatedDateTime { get; set; }  

    }
}
