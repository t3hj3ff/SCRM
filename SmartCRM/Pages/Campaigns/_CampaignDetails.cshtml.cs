using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNet.Identity;
using SmartCRMSvc;

namespace SmartCRM.Pages.Campaigns
{
    public class _CampaignDetailsModel : PageModel
    {
        public SmartCRMSvc.CampaignAddModel NewCampaign { get; set; }
        public int? CampID { get; set; }
        
        public CampaginsVM CampaignsVM { get; set; }
        
        public List<SmartCRMSvc.RFMSegment> Segments { get; set; }
        public List<ReportFilial> Filials { get; set; }

        //public async Task<IActionResult> OnGet()
        //{
        //    var user_is_admin = User.IsInRole("UnicardAdmin");

        //    var cl = new SmartCRMSvc.Service1Client();
        //    Segments = (await cl.GetSegmentsAsync(new SmartCRMSvc.RFMStatModel
        //    {
        //        UserID = user_is_admin ? (int?)null : User.Identity.GetUserId<int>(),
        //        OrgID = user_is_admin ? Convert.ToInt32(User.GetClaim("ChoosedOrgId")) : (int?)null
        //    })).ToList();
        //    return Page();
        //}
    }
}