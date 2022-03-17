using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartCRMService.App_Code
{
    public class ReportStructure
    {
        public string Address { get; set; }
        public string AgeRange { get; set; }
        public string Gender { get; set; }
        public int ActiveCustomerCount { get; set; }
        public int NewCustomerCount { get; set; }
        public int PassiveCustomerCount { get; set; }
    }
}