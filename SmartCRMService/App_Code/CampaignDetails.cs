using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartCRMService.App_Code
{
    public class CampaignDetails
    {
        public int? OrgID { get; set; }

        public int SegmentID { get; set; }

        public int SegmentCount { get; set; }

        public decimal? ActionPercent { get; set; }

        public decimal? ActionCashVaucher { get; set; }

        public decimal? ActionCashVaucherPercent { get; set; }

        public DateTime? ActionCashVaucherStart { get; set; }

        public DateTime? ActionCashVaucherEnd { get; set; }

        public decimal? ActionBonusFix { get; set; }

        public decimal? ActionBonusPercent { get; set; }

        public string ActionProductName { get; set; }

        public int? ActionProductCount { get; set; }

        public int? PersentCaseVisitCount { get; set; }

        public decimal? PersentCaseVisitAmount { get; set; }

        //public int? PersentCaseVisitReceiveCount { get; set; }

        public decimal? PersentCaseSpentAmount { get; set; }

        public int? PersentCaseSpentCount { get; set; }

        // public int? PersentCaseSpentReceiveCount { get; set; }

        public int? PersentCaseSpentWDay1 { get; set; }

        public int? PersentCaseSpentWDay2 { get; set; }

        public int? PersentCaseSpentWDay3 { get; set; }

        public int? PersentCaseSpentWDay4 { get; set; }

        public int? PersentCaseSpentWDay5 { get; set; }

        public int? PersentCaseSpentWDay6 { get; set; }

        public int? PersentCaseSpentWDay7 { get; set; }

        public string PersentCaseSpentWDayStartTime { get; set; }

        public string PersentCaseSpentWDayEndTime { get; set; }

        public int? PersentCaseSpentUncondition { get; set; }

        public DateTime? ActionStartDate { get; set; }

        public DateTime? ActionEndDate { get; set; }

        public string PresentLocation { get; set; }

        public string ChampName { get; set; }

        public string ChampDescription { get; set; }

        public int? CampID { get; set; }
        public int? PersentCaseReceiveCount { get; set; }

        public DateTime? SendMSGDate { get; set; }
        public string MsgText { get; set; }
        public string RemindMsgText { get; set; }
        public string PerformMsgText { get; set; }
        public string HasBlackList { get; set; }
        public bool SendTestMSG { get; set; }
        public bool AddGroupInAction { get; set; }
        public string Status { get;  set; }

        public int? SegmentCompareCount { get;  set; }
    }
}