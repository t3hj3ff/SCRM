using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartCRMService.App_Code
{
    public class Dashboard
    {
        public List<DashboardItemBase> CustomerAges { get; set; }
        public List<DashboardItemBase> CustomerGenders { get; set; }
        public List<VisitTurnover> VisitTurnovers { get; set; }
        public CustomerSturcure Customer { get; set; }
        public RemainingSMS SMS { get; set; }
        public List<VizitorRange> VizitorRanges { get; set; }
    }

    public class VisitTurnover
    {
        public string Day { get; set; }
        public int Visit { get; set; }  
        public decimal Turnover { get; set; }
        public decimal AvgAmount { get;  set; }
    }

    public class CustomerSturcure
    {
        public int NewClients { get; set; }
        public int ActiveClients { get; set; }
        public int InActiveClients { get; set; }
        public int TotalClients { get; set; }
        public decimal AverageAmount { get; set; }
    }

    public class RemainingSMS
    {
        public int SMSQuantity { get; set; }
        public decimal PointQuantity { get; set; }
        public int PointRemaining { get; set; }
        public int SMSRemaining { get; set; }
    }

    public class VizitorRange : DashboardItemBase
    {
        public decimal Amount { get;  set; }
        public decimal VisitorCntPerc { get;  set; }
        public decimal AmountPerc { get;  set; }
        public decimal AverageAmount { get;  set; }
    }

}