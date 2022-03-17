using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using SmartCRMService.App_Code;
using SmartCRMService.App_Code.Communication;

namespace SmartCRMService
{
    public class Service1 : IService1
    {
        public Dashboard GetDashboardData(int user_id, DateTime date)
        {
            var db = new DB();

            return db.GetDashboardData(user_id, date);
        }

        public LoginResult Login(LoginModel m)
        {
            return new DB().Login(m);
        }

        public List<Organization> GetOrganizations()
        {
            return new DB().GetOrganizations();
        }


        public Dashboard GetDashboardDataByOrg(int org_id, DateTime generate_date)
        {
            return new DB().GetDashboardDataByOrg(org_id, generate_date);
        }

        public List<ReportAmount> GetReportAmountData(ReportModelBase m)
        {
            return new DB().GetReportAmountData(m);
        }

        public List<ReportTransaction> GetReportTransactionData(ReportModelBase m)
        {
            return new DB().GetReportTransactionData(m);
        }

        public List<ReportAvgAmount> GetReportAverageAmountData(ReportModelBase m)
        {
            return new DB().GetReportAverageAmountData(m);
        }

        public List<ReportFilial> GetReportFilials(int? UserID, int? OrgID)
        {
            return new DB().GetReportFilials(UserID, OrgID);
        }

        public List<ReportStructure> GetReportStructureData(ReportModelBase m)
        {
            return new DB().GetReportStructureData(m);
        }

        public List<ReportGender> GetReportGenderData(ReportModelBase m)
        {
            return new DB().GetReportGenderData(m);
        }

        public List<ReportGender> GetReportAgesData(ReportModelBase m)
        {
            return new DB().GetReportAgesData(m);
        }

        public List<ReportTransItem> GetReportTransData(ReportModelBase m)
        {
            return new DB().GetReportTransData(m);
        }

        public List<ReportTransExportItem> GetReportExportData(ReportModelBase m)
        {
            return new DB().GetReportExportData(m);
        }

        public RFMStats GetRFMStats(RFMStatModel m)
        {
            return new DB().GetRFMStats(m);
        }

        public RFMAddSegmentResult RFMAddSegment(RFMAddSegmentModel m)
        {
            return new DB().RFMAddSegment(m);
        }

        public RFMNewSegmentScoreResult RFMGetNewSegmentScore(RFMAddSegmentModel m)
        {
            return new DB().RFMGetNewSegmentScore(m);
        }

        public CampaignsAddResult CampaignsAdd(CampaignAddModel m)
        {
            return new DB().CampaignsAdd(m);
        }

        public List<CampaignItem> GetCampaigns(GetCampaignsModel m)
        {
            return new DB().GetCampaigns(m);
        }

        public CampaignDetails GetCampaignDetails(int CampID)
        {
            return new DB().GetCampaignDetails(CampID);
        }

        public List<RFMSegment> GetSegments(RFMStatModel m)
        {
            return new DB().GetSegments(m);
        }

        public BlackListConfirmResult BlackListAdd(BlackListModel m)
        {
            return new DB().BlackListAdd(m);
        }

        public BlackListResult GetBlackList(ModelBase m)
        {
            return new DB().GetBlackList(m);
        }

        public ResultBase RFMSegmentDelete(RFMSegmentDeleteModel m)
        {
            return new DB().RFMSegmentDelete(m);
        }

        public ResultBase CampaignDelete(CampaignDeleteModel m)
        {
            return new DB().CampaignDelete(m);
        }

        public ResultBase CampaignSendTestMsg(CampaignSendTestMsgModel m)
        {
            return new DB().CampaignSendTestMsg(m);
        }

        public List<Employ> GetEmployes(ModelBase m)
        {
            return new DB().GetEmployes(m);
        }

        public EmployResult AddEmployer(EmployModel m)
        {
            return new DB().AddEmployer(m);
        }

        public ResultBase DeleteEmployee(EmployDeleteModel m)
        {
            return new DB().DeleteEmployee(m);
        }

        public CommunicationAddResult CommunicationAdd(CommunicationAddModel m)
        {
            return new DB().CommunicationAdd(m);
        }

        public List<CommunicationItem> CommunicationsGet(ModelBase m)
        {
            return new DB().CommunicationsGet(m);
        }

        public CummunicationDetails GetCummunicationDetails(CummunicationDetailsModel m)
        {
            return new DB().GetCummunicationDetails(m);
        }

        public ResultBase CommunicationDelete(CummunicationDetailsModel m)
        {
            return new DB().CommunicationDelete(m);
        }

        public ResultBase CommunicationSendTestMsg(CommunicationSendTestMsgModel m)
        {
            return new DB().CommunicationSendTestMsg(m);
        }

        public ResultBase CRMCampStartCampaign(CRMCampStartCampaignModel m)
        {
            return new DB().CRMCampStartCampaign(m);
        }

        public ResultBase CRMCommunicationStart(CommunicationIDModel m)
        {
            return new DB().CRMCommunicationStart(m);
        }
    }
}
