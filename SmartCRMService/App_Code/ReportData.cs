using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SmartCRMService.App_Code
{
    public class ReportData
    {
        public List<ReportAmount> Amounts { get; set; }
        public List<ReportTransaction> Transactions { get; set; }
        public List<ReportAvgAmount> AvgAmounts { get; set; }
        public ReportCustomerStructure CustomerStructure { get; set; }

    }

    public class ReportAmount : ReportDataBase
    {
        public decimal Amount { get; set; }
    }

    public class ReportTransItem : ReportDataBase
    {
        public decimal Amount { get; set; }
        public int TranCount { get; set; }
        public decimal AvgAmount { get; set; }
    }

    public class ReportTransaction : ReportDataBase
    {
        public int TranCount { get; set; }
    }

    public class ReportDataBase
    {
        public string DatePeriod { get; set; }
        public int? GenderID { get; set; }
        public int? AgeRangeID { get; set; }
    }

    public class ReportAvgAmount : ReportDataBase
    {
        public decimal AvgAmount { get; set; }
    }

    public class ReportCustomerStructure
    {
        public int CustomerCountDay { get; set; }
        public int NewCustomerCountDay { get; set; }
        public int ReactivateCustomerCountDay { get; set; }
        public int ActiveCustomerCount { get; set; }
        public int PassiveCustomerCount { get; set; }
    }

    [DataContract]
    public class ReportModelBase
    {
        [DataMember]
        public int? GroupByDate { get; set; }
        [DataMember]
        public int? FilterOrgID { get; set; }
        [DataMember]
        public string FilterObjectID { get; set; }
        [DataMember]
        public string FilterMerchname { get; set; }
        [DataMember]
        public string FilterAgeRangeID { get; set; }
        [DataMember]
        public int? FilterGender { get; set; }
        [DataMember]
        public DateTime? FilterEndDate { get; set; }
        [DataMember]
        public DateTime? FilterStartDate { get; set; }
        [DataMember]
        public int? FilterDateID { get; set; }
        [DataMember]
        public int? GroupByAge { get; set; }
        [DataMember]
        public int? GroupByGender { get; set; }
        [DataMember]
        public int? UserID { get; set; }
        [DataMember]
        public int? GroupByFilial { get; set; }
    }

    public class ReportFilial
    {
        public int ObjectID { get; set; }
        public string StreetName { get; set; }
        public string CityName { get; set; }
        public string RaioniName { get; set; }
        public string MerchName { get; set; }
        public string Address { get; set; }
    }

    public class ReportGender
    {
        public string Description { get; set; }
        public int? Count { get; set; }
    }

}