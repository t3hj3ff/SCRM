using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace SmartCRM.Pages
{
    [Authorize(Roles = "UnicardAdmin")]
    public class UniDashboardModel : PageModel
    {
        private readonly HttpContext _httpContext;

        public UniDashboardModel(IHttpContextAccessor contextAccessor)
        {
            _httpContext = contextAccessor.HttpContext;
        }


        public List<SmartCRMSvc.Organization> Orgs { get; set; }
        public async Task<IActionResult> OnGet()
        {

            var cl = new SmartCRMSvc.Service1Client();
            var _orgs = await cl.GetOrganizationsAsync();

            Orgs = _orgs.ToList();
            return Page();
        }

        public async Task<IActionResult> OnGetChooseOrg(int org_id, string org_name)
        {
            User.AddClaim("ChoosedOrgId", org_id.ToString());
            User.AddClaim("ChoosedOrgName", org_name);

            await _httpContext.SignInAsync(User as ClaimsPrincipal);
            return RedirectToPage("Index");
        }

    }

}