using SmartCRMService.App_Code;
using SmartCRMService.App_Code.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SmartCRMService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        Dashboard GetDashboardData(int user_id, DateTime date);
        [OperationContract]
        LoginResult Login(LoginModel m);
        [OperationContract]
        List<Organization> GetOrganizations();
        [OperationContract]
        Dashboard GetDashboardDataByOrg(int org_id, DateTime generate_date);

        [OperationContract]
        List<ReportAmount> GetReportAmountData(ReportModelBase m);

        [OperationContract]
        List<ReportTransaction> GetReportTransactionData(ReportModelBase m);

        [OperationContract]
        List<ReportAvgAmount> GetReportAverageAmountData(ReportModelBase m);

        [OperationContract]
        List<ReportFilial> GetReportFilials(int? UserID, int? OrgID);

        [OperationContract]
        List<ReportStructure> GetReportStructureData(ReportModelBase m);

        [OperationContract]
        List<ReportGender> GetReportGenderData(ReportModelBase m);

        [OperationContract]
        List<ReportGender> GetReportAgesData(ReportModelBase m);

        [OperationContract]
        List<ReportTransItem> GetReportTransData(ReportModelBase m);

        [OperationContract]
        List<ReportTransExportItem> GetReportExportData(ReportModelBase m);

        [OperationContract]
        RFMStats GetRFMStats(RFMStatModel m);

        [OperationContract]
        RFMAddSegmentResult RFMAddSegment(RFMAddSegmentModel m);

        [OperationContract]
        RFMNewSegmentScoreResult RFMGetNewSegmentScore(RFMAddSegmentModel m);

        [OperationContract]
        CampaignsAddResult CampaignsAdd(CampaignAddModel m);

        [OperationContract]
        List<CampaignItem> GetCampaigns(GetCampaignsModel m);

        [OperationContract]
        CampaignDetails GetCampaignDetails(int CampID);

        [OperationContract]
        List<RFMSegment> GetSegments(RFMStatModel m);

        [OperationContract]
        BlackListConfirmResult BlackListAdd(BlackListModel m);

        [OperationContract]
        BlackListResult GetBlackList(ModelBase m);

        [OperationContract]
        ResultBase RFMSegmentDelete(RFMSegmentDeleteModel m);

        [OperationContract]
        ResultBase CampaignDelete(CampaignDeleteModel m);

        [OperationContract]
        ResultBase CampaignSendTestMsg(CampaignSendTestMsgModel m);

        [OperationContract]
        List<Employ> GetEmployes(ModelBase m);

        [OperationContract]
        EmployResult AddEmployer(EmployModel m);

        [OperationContract]
        ResultBase DeleteEmployee(EmployDeleteModel m);

        [OperationContract]
        CommunicationAddResult CommunicationAdd(CommunicationAddModel m);

        [OperationContract]
        List<CommunicationItem> CommunicationsGet(ModelBase m);

        [OperationContract]
        CummunicationDetails GetCummunicationDetails(CummunicationDetailsModel m);

        [OperationContract]
        ResultBase CommunicationDelete(CummunicationDetailsModel m);

        [OperationContract]
        ResultBase CommunicationSendTestMsg(CommunicationSendTestMsgModel m);

        [OperationContract]
        ResultBase CRMCampStartCampaign(CRMCampStartCampaignModel m);

        [OperationContract]
        ResultBase CRMCommunicationStart(CommunicationIDModel m);
    }


}
