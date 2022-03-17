using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartCRM.Pages
{
    public class LogoutModel : PageModel
    {
        public async Task<IActionResult> OnGetLogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToPage("Login");
        }
    }
}