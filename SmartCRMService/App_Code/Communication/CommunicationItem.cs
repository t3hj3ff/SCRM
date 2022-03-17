using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartCRMService.App_Code.Communication
{
    public class CommunicationItem
    {
        public string Name { get; set; }
        public string Status { get; set; }
        public string StatusName { get; set; }
        public int ID { get; set; }
        public DateTime? SendDate { get;  set; }
    }
}