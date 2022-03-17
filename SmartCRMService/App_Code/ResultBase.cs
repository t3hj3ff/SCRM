using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SmartCRMService.App_Code
{
    [DataContract]
    public class ResultBase
    {
        [DataMember]
        public int Result { get; set; }
        [DataMember]
        public string ResultMsg { get; set; }

    }
}