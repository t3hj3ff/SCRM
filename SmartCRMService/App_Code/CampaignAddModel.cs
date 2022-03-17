using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SmartCRMService.App_Code
{
    [DataContract]
    public class CampaignAddModel
    {
        [DataMember]
        public int? UserID { get; set; }
        [DataMember]
        public int? OrgID { get; set; }
        [DataMember]
        public int SegmentID { get; set; }
        [DataMember]
        public int SegmentCount { get; set; }
        [DataMember]
        public decimal? ActionPercent { get; set; }
        [DataMember]
        public decimal? ActionCashVaucher { get; set; }
        [DataMember]
        public decimal? ActionCashVaucherPercent { get; set; }
        [DataMember]
        public DateTime? ActionCashVaucherStart { get; set; }
        [DataMember]
        public DateTime? ActionCashVaucherEnd { get; set; }
        [DataMember]
        public decimal? ActionBonusFix { get; set; }
        [DataMember]
        public decimal? ActionBonusPercent { get; set; }
        [DataMember]
        public string ActionProductName { get; set; }
        [DataMember]
        public int? ActionProductCount { get; set; }
        [DataMember]
        public int? PersentCaseVisitCount { get; set; }
        [DataMember]
        public decimal? PersentCaseVisitAmount { get; set; }
        //[DataMember]
        //public int? PersentCaseVisitReceiveCount { get; set; }
        [DataMember]
        public decimal? PersentCaseSpentAmount { get; set; }
        [DataMember]
        public int? PersentCaseSpentCount { get; set; }
        //[DataMember]
        //public int? PersentCaseSpentReceiveCount { get; set; }
        [DataMember]
        public int? PersentCaseSpentWDay1 { get; set; }
        [DataMember]
        public int? PersentCaseSpentWDay2 { get; set; }
        [DataMember]
        public int? PersentCaseSpentWDay3 { get; set; }
        [DataMember]
        public int? PersentCaseSpentWDay4 { get; set; }
        [DataMember]
        public int? PersentCaseSpentWDay5 { get; set; }
        [DataMember]
        public int? PersentCaseSpentWDay6 { get; set; }
        [DataMember]
        public int? PersentCaseSpentWDay7 { get; set; }
        [DataMember]
        public string PersentCaseSpentWDayStartTime { get; set; }
        [DataMember]
        public string PersentCaseSpentWDayEndTime { get; set; }
        [DataMember]
        public int? PersentCaseSpentUncondition { get; set; }
        [DataMember]
        public DateTime? ActionStartDate { get; set; }
        [DataMember]
        public DateTime? ActionEndDate { get; set; }
        [DataMember]
        public string PresentLocation { get; set; }
        [DataMember]
        public string PresentText { get; set; }
        [DataMember]
        public string ChampName { get; set; }
        [DataMember]
        public string ChampDescription { get; set; }
        [DataMember]
        public int? CampID { get; set; }
        [DataMember]
        public int? PersentCaseReceiveCount { get; set; }
        [DataMember]
        public DateTime? SendMSGDate { get; set; }
        [DataMember]
        public string MsgText { get; set; }
        [DataMember]
        public string RemindMsgText { get; set; }
        [DataMember]
        public string PerformMsgText { get; set; }
        [DataMember]
        public int? HasBlackList { get; set; }
        [DataMember]
        public int? SendTestMSG { get; set; }
        [DataMember]
        public int? AddGroupInAction { get; set; }

        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public int? SegmentCompareCount { get;  set; }
    }
}