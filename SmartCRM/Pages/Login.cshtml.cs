using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartCRM.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public LoginData loginData { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var cl = new SmartCRMSvc.Service1Client();

                var auth_res = await cl.LoginAsync(new SmartCRMSvc.LoginModel { UserName = loginData.Username, Password = loginData.Password });

                var isValid = auth_res.Result == 200;

                if (!isValid)
                {
                    ModelState.AddModelError("Auth", "მომხმარებლის სახელი ან პაროლი არასწორია");
                    return Page();
                }
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, auth_res.UserID.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.SerialNumber, loginData.Username));
                identity.AddClaim(new Claim(ClaimTypes.Name, loginData.Username));
                identity.AddClaim(new Claim("FullName", auth_res.FullName));
                identity.AddClaim(new Claim("OrgName", auth_res.OrgName));
                identity.AddClaim(new Claim("OrgID", auth_res.OrgID));

                if (auth_res.RoleID == 1)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, "UnicardAdmin"));
                }
                else
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, "MerchantRole"));
                }

                //identity.AddClaim(new Claim("UserID", auth_res.UserID.ToString()));


                var principal = new ClaimsPrincipal(identity);


                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties { IsPersistent = loginData.RememberMe });
                return RedirectToPage("Index");
            }
            else
            {
                ModelState.AddModelError("Auth", "დაფიქსირდა შეცდომა");
                return Page();
            }
        }

        public class LoginData
        {
            [Required]
            public string Username { get; set; }

            [Required, DataType(DataType.Password)]
            public string Password { get; set; }

            public bool RememberMe { get; set; }
        }
    }
}