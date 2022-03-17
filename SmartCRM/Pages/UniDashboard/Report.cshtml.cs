using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;

namespace SmartCRM.Pages.UniDashboard
{
    [Authorize(Roles = "UnicardAdmin")]
    public class ReportModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int OrgId { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            OrgId = id.Value;

            return Page();
        }

        public async Task<JsonResult> OnGetDashboard(int id)
        {
            var cl = new SmartCRMSvc.Service1Client();
            var data = await cl.GetDashboardDataByOrgAsync(id, DateTime.Now.Date);
            return new JsonResult(data);
        }

    }
}