using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SmartCRMService.App_Code
{
    [DataContract]
    public class RFMAddSegmentModel
    {
        [DataMember]
        public int? UserID { get; set; }
        [DataMember]
        public int? OrgID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Coordinates { get; set; }
        [DataMember]
        public string AgeRangeID { get; set; }
        [DataMember]
        public string GenderID { get; set; }
        [DataMember]
        public string Filial { get; set; }
    }
}