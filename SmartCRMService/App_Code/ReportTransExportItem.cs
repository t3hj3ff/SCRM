using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartCRMService.App_Code
{
    public class ReportTransExportItem
    {
        public string AuthDateTime { get; set; }
        public string Card { get; set; }
        public string ClientName { get; set; }
        public string Gender { get; set; }
        public string AgeRange { get; set; }
        public decimal Amount { get; set; }
        public string TerminalId { get; set; }
        public string OrgCode { get; set; }
        public string Address { get; set; }
    }
}