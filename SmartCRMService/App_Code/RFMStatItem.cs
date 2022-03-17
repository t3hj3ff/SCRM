using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SmartCRMService.App_Code
{
    public class RFMStats
    {
        public List<RFMRFItem> R { get; set; }
        public List<RFMRFItem> F { get; set; }

        public List<RFMSIDItem> S_ID_List { get; set; }

        public List<RFMCell> Cells { get; set; }

        public List<RFMSegment> Segments { get; set; }
    }
    public class RFMRFItem
    {
        public string ID { get; set; }
        public string Description { get; set; }

    }

    public class RFMCell
    {
        public string SID { get; set; }
        public string R { get; set; }
        public string F { get; set; }
        public int CARD_CNT { get; set; }
        public string Description { get;  set; }
    }

    public class RFMSegment
    {
        public string Name { get; set; }
        public int CustomerCount { get; set; }
        public int ID { get;  set; }
    }


    [DataContract]
    public class RFMSIDItem
    {
        [DataMember]
        public string S_id { get; set; }
        [DataMember]
        public string Description { get; set; }
    }

    [DataContract]
    public class RFMStatModel
    {
        [DataMember]
        public int? UserID { get; set; }

        [DataMember]
        public int? OrgID { get; set; }
    }
}