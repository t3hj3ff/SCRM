using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SmartCRMService.App_Code.Communication
{
    [DataContract]
    public class CommunicationAddModel : ModelBase
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int? SegmentID { get; set; }
        [DataMember]
        public int? SegmentCount { get; set; }
        [DataMember]
        public DateTime? SendDate { get; set; }
        [DataMember]
        public string MsgText { get; set; }
        [DataMember]
        public bool TestGroupAdd { get; set; }
        [DataMember]
        public bool? BlackListAdd { get; set; }
        [DataMember]
        public int? CommunicationID { get; set; }
        [DataMember]
        public string AgeRangeID { get;  set; }
        [DataMember]
        public string GenderRangeID { get;  set; }
        [DataMember]
        public string Description { get;  set; }
    }
}