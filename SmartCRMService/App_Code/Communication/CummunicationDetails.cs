using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartCRMService.App_Code.Communication
{
    public class CummunicationDetails : ResultBase
    {
        public string Name { get; set; }

        public int? SegmentID { get; set; }

        public int? SegmentCount { get; set; }

        public DateTime? SendDate { get; set; }

        public string MsgText { get; set; }

        public bool TestGroupAdd { get; set; }

        public bool? BlackListAdd { get; set; }

        public int? CommunicationID { get; set; }
        public string GenderRangeID { get; set; }
        public string AgeRangeID { get; set; }
        public string Description { get; set; }
    }
}