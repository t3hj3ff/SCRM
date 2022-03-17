using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SmartCRMService.App_Code
{
    [DataContract]
    public class RFMNewSegmentScoreResult
    {
        [DataMember]
        public int Count { get; set; }
    }
}