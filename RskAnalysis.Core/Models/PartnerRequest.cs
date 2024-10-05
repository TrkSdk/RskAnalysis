using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RskAnalysis.CORE.Models
{
    public class PartnerRequest
    {
        public int PartnerId { get; set; }
        //public string PartnerName { get; set; }
        public string ContractName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double Amount { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
