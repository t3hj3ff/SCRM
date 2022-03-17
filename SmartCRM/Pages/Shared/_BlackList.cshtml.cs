using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartCRM.Pages.Shared
{
    public class _BlackListModel : PageModel
    {
        public SmartCRMSvc.BlackListResult BlackList { get; set; }
        public async Task<ActionResult> OnGet()
        {
            return Page();
        }
    }
}