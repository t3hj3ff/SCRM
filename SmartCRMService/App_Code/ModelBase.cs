using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SmartCRMService.App_Code
{
    [DataContract]
    public class ModelBase
    {
        [DataMember]
        public int? UserID { get; set; }
        [DataMember]
        public int? OrgID { get; set; }
    }
}