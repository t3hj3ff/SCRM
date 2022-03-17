using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartCRMService.App_Code
{
    public class CampaignSendTestMsgModel : ModelBase
    {
        public string PerformMsgText { get; set; }
        public string MsgText { get; set; }
        public string RemindMsgText { get; set; }
    }
}