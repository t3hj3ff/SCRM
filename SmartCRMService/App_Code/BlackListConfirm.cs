using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SmartCRMService.App_Code
{
    [DataContract]
    public class BlackListConfirmModel : ModelBase
    {
        [DataMember]
        public string BulkID { get; set; }
    }

    public class BlackListConfirmResult : ResultBase
    {
        public string NewPhoneCount { get; set; }
        public string BadPhoneCount { get; set; }
    }
}