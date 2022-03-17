using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SmartCRMService.App_Code
{
    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}