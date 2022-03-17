using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNet.Identity;
using SmartCRMSvc;

namespace SmartCRM.Pages
{
    public class RFMModel : PageModel
    {
        [BindProperty]
        public RFMSegmentModel NewSegment { get; set; }

        public bool UserIsAdmin { get { return User.IsInRole("UnicardAdmin"); } }

        public RFMStats Stat { get; set; }
        public List<ReportFilial> Filials { get; set; }
        public async Task<IActionResult> OnGet()
        {
            var user_is_admin = User.IsInRole("UnicardAdmin");

            var cl = new SmartCRMSvc.Service1Client();
            Stat = await cl.GetRFMStatsAsync(new SmartCRMSvc.RFMStatModel
            {
                OrgID = user_is_admin ? Convert.ToInt32(User.GetClaim("ChoosedOrgId")) : (int?)null,
                UserID = user_is_admin ? (int?)null : User.Identity.GetUserId<int>(),
            });

            Filials = (await cl.GetReportFilialsAsync(user_is_admin ? (int?)null : User.Identity.GetUserId<int>(),
                 user_is_admin ? Convert.ToInt32(User.GetClaim("ChoosedOrgId")) : (int?)null)).ToList();
            return Page();
        }

        public async Task<JsonResult> OnPost()
        {
            var cl = new SmartCRMSvc.Service1Client();
            var res = await cl.RFMAddSegmentAsync(new RFMAddSegmentModel
            {
                OrgID = UserIsAdmin ? Convert.ToInt32(User.GetClaim("ChoosedOrgId")) : (int?)null,
                UserID = UserIsAdmin ? (int?)null : User.Identity.GetUserId<int>(),
                AgeRangeID = NewSegment.ages != null ? String.Join(',', NewSegment.ages) : null,
                Coordinates = NewSegment.sid != null ? String.Join(',', NewSegment.sid) : null,
                Filial = !String.IsNullOrEmpty(NewSegment.merchants) ? NewSegment.merchants : null,
                GenderID = NewSegment.gender != null ? String.Join(',', NewSegment.gender) : null,
                Name = NewSegment.name
            });

            return new JsonResult(res);
        }


        public async Task<JsonResult> OnGetNewSegmentScore(RFMSegmentModel NewSegment)
        {
            var cl = new SmartCRMSvc.Service1Client();

            var res = await cl.RFMGetNewSegmentScoreAsync(new RFMAddSegmentModel
            {
                OrgID = UserIsAdmin ? Convert.ToInt32(User.GetClaim("ChoosedOrgId")) : (int?)null,
                UserID = UserIsAdmin ? (int?)null : User.Identity.GetUserId<int>(),
                AgeRangeID = NewSegment.ages != null ? String.Join(',', NewSegment.ages) : null,
                Coordinates = NewSegment.sid != null ? String.Join(',', NewSegment.sid) : null,
                Filial = NewSegment.merchants != null ? String.Join(',', NewSegment.merchants) : null,
                GenderID = NewSegment.gender != null ? String.Join(',', NewSegment.gender) : null
            });

            return new JsonResult(res);
        }

        public async Task<ActionResult> OnGetSegmentDelete(int id)
        {
            var cl = new SmartCRMSvc.Service1Client();
            var res = await cl.RFMSegmentDeleteAsync(new RFMSegmentDeleteModel()
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
    }



    public class RFMSegmentModel
    {
        public List<string> ages { get; set; }
        public List<string> sid { get; set; }
        public string merchants { get; set; }
        public List<string> gender { get; set; }
        public string name { get; set; }
    }
}