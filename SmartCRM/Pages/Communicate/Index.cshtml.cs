using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNet.Identity;
using SmartCRMSvc;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace SmartCRM.Pages.Communicate
{
    public class IndexModel : PageModel
    {
        public List<SmartCRMSvc.RFMSegment> Segments { get; set; }
        [BindProperty]
        public CummunicateVM Cummunicate { get; set; }
        private bool UserIsAdmin { get { return User.IsInRole("UnicardAdmin"); } }
        [BindProperty]
        public SmartCRMSvc.CommunicationAddModel NewCommunication { get; set; }

        public List<SmartCRMSvc.CommunicationItem> Communications { get; set; }

        public async Task<IActionResult> OnGet()
        {
            Segments = new List<SmartCRMSvc.RFMSegment>();
            Cummunicate = new CummunicateVM();

            var cl = new SmartCRMSvc.Service1Client();
            Segments = (await cl.GetSegmentsAsync(new SmartCRMSvc.RFMStatModel
            {
                UserID = UserIsAdmin ? (int?)null : User.Identity.GetUserId<int>(),
                OrgID = UserIsAdmin ? Convert.ToInt32(User.GetClaim("ChoosedOrgId")) : (int?)null
            })).Select(s => new RFMSegment { ID = s.ID, Name = s.CustomerCount + ":" + s.Name }).ToList();

            Communications = (await cl.CommunicationsGetAsync(new ModelBase
            {
                UserID = UserIsAdmin ? (int?)null : User.Identity.GetUserId<int>(),
                OrgID = UserIsAdmin ? Convert.ToInt32(User.GetClaim("ChoosedOrgId")) : (int?)null
            })).ToList();

            return Page();
        }

        public async Task<JsonResult> OnPost()
        {
            var new_communication = new CommunicationAddModel();
            if (Cummunicate == null)
            {
                Cummunicate = new CummunicateVM();
            }

            if (!String.IsNullOrEmpty(Cummunicate.SendMsgDate))
            {
                DateTime dt;
                if (DateTime.TryParseExact(Cummunicate.SendMsgDate, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None,
                    out dt))
                {
                    new_communication.SendDate = dt;
                }
            }

            switch (Cummunicate.VisVesaubro)
            {
                case "my_users":
                    new_communication.SegmentID = NewCommunication.SegmentID;
                    new_communication.SegmentCount = NewCommunication.SegmentCount;
                    break;
                case "new_users":
                    new_communication.Description = NewCommunication.Description;
                    new_communication.GenderRangeID = String.Join(',', Cummunicate.GenderRangeID);
                    new_communication.AgeRangeID = String.Join(',', Cummunicate.AgeRangeID);

                    break;
            }

            new_communication.CommunicationID = NewCommunication.CommunicationID;

            new_communication.BlackListAdd = Cummunicate.BlackListAdd == "1" ? true : Cummunicate.BlackListAdd == "0" ? false : (bool?)null;

            new_communication.MsgText = NewCommunication.MsgText;
            new_communication.Name = NewCommunication.Name;
            new_communication.TestGroupAdd = NewCommunication.TestGroupAdd;
            new_communication.UserID = User.Identity.GetUserId<int>();
            new_communication.OrgID = UserIsAdmin ? Convert.ToInt32(User.GetClaim("ChoosedOrgId")) : Convert.ToInt32(User.GetClaim("OrgID"));


            try
            {
                SmartCRMSvc.Service1Client cl = new SmartCRMSvc.Service1Client();
                var result = await cl.CommunicationAddAsync(new_communication);

                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                return new JsonResult(new SmartCRMSvc.ResultBase { ResultMsg = "შეცდომა", Result = 500 });
            }
        }

        public async Task<PartialViewResult> OnGetAddEditCommunication(int? id)
        {
            var cl = new SmartCRMSvc.Service1Client();

            Cummunicate = new CummunicateVM();
            var data = new _AddEditCommunicationModel();

            data.Cummunicate = new CummunicateVM();
            data.NewCommunication = new CommunicationAddModel();

            Segments = (await cl.GetSegmentsAsync(new SmartCRMSvc.RFMStatModel
            {
                UserID = UserIsAdmin ? (int?)null : User.Identity.GetUserId<int>(),
                OrgID = UserIsAdmin ? Convert.ToInt32(User.GetClaim("ChoosedOrgId")) : (int?)null
            })).Select(s => new RFMSegment { ID = s.ID, Name = s.CustomerCount + ":" + s.Name }).ToList();


            data.Segments = Segments;

            if (id.HasValue)
            {
                var result = (await cl.GetCummunicationDetailsAsync(new CummunicationDetailsModel
                {
                    UserID = UserIsAdmin ? (int?)null : User.Identity.GetUserId<int>(),
                    OrgID = UserIsAdmin ? Convert.ToInt32(User.GetClaim("ChoosedOrgId")) : (int?)null,
                    ID = id.Value
                }));

                Cummunicate.BlackListAdd = result.BlackListAdd.HasValue ? result.BlackListAdd.Value ? "1" : "0" : "";

                Cummunicate.VisVesaubro = result.SegmentID.HasValue ? "my_users" : "new_users";

                Cummunicate.GenderRangeID = result.GenderRangeID != null && result.GenderRangeID.Length > 0 ? result.GenderRangeID.Split(',').ToList() : new List<string>();
                Cummunicate.AgeRangeID = result.AgeRangeID != null && result.AgeRangeID.Length > 0 ? result.AgeRangeID.Split(',').ToList() : new List<string>();

                Cummunicate.SendMsgDate = result.SendDate.HasValue ? result.SendDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : "";

                data.NewCommunication.MsgText = result.MsgText;
                data.NewCommunication.Name = result.Name;
                data.NewCommunication.SegmentCount = result.SegmentCount;
                data.NewCommunication.SegmentID = result.SegmentID;
                data.NewCommunication.TestGroupAdd = result.TestGroupAdd;
                data.NewCommunication.BlackListAdd = result.BlackListAdd;
                data.NewCommunication.SendDate = result.SendDate;
                data.NewCommunication.CommunicationID = result.CommunicationID;
                data.NewCommunication.Description = result.Description;
                data.NewCommunication.AgeRangeID = result.AgeRangeID;
                data.NewCommunication.GenderRangeID = result.GenderRangeID;



                data.Cummunicate = Cummunicate;

            }
            else
            {

            }

            return new PartialViewResult
            {
                ViewName = "_AddEditCommunication",
                ViewData = new ViewDataDictionary<_AddEditCommunicationModel>(ViewData, data)
            };
        }


        public async Task<ActionResult> OnGetCommunicationDelete(int id)
        {
            var cl = new SmartCRMSvc.Service1Client();
            var res = await cl.CommunicationDeleteAsync(new CummunicationDetailsModel
            {
                OrgID = UserIsAdmin ? Convert.ToInt32(User.GetClaim("ChoosedOrgId")) : (int?)null,
                UserID = UserIsAdmin ? (int?)null : User.Identity.GetUserId<int>(),
                ID = id
            });

            if (res != null && res.Result == 200)
            {
                return RedirectToAction("OnGet");
            }
            return Page();
        }


        public async Task<JsonResult> OnPostSendTestMsg(string msg)
        {
            var cl = new SmartCRMSvc.Service1Client();
            return new JsonResult(await cl.CommunicationSendTestMsgAsync(new CommunicationSendTestMsgModel
            {
                OrgID = UserIsAdmin ? Convert.ToInt32(User.GetClaim("ChoosedOrgId")) : (int?)null,
                UserID = UserIsAdmin ? (int?)null : User.Identity.GetUserId<int>(),
                MsgText = msg
            }));
        }
    }



    public class CummunicateVM
    {
        public int SegmentID { get; set; }
        public int SegmentCount { get; set; }
        public string VisVesaubro { get; set; }
        public string SendMsgDate { get; set; }
        public string BlackListAdd { get; set; }
        public List<string> AgeRangeID { get; set; }
        public List<string> GenderRangeID { get; set; }
    }
}