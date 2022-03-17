using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SmartCRMService.App_Code
{
    [DataContract]
    public class BlackListModel
    {
        [DataMember]
        public int? UserID { get; set; }
        [DataMember]
        public int? OrgID { get; set; }
        [DataMember]
        public List<string> Phones { get; set; }
        [DataMember]
        public string BulkID { get; set; }
    }
}