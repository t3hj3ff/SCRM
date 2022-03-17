using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SmartCRMService.App_Code
{
    [DataContract]
    public class LoginResult : ResultBase
    {
        [DataMember]
        public int UserID { get; set; }
        [DataMember]
        public string FullName { get;  set; }
        [DataMember]
        public int RoleID { get;  set; }
        [DataMember]
        public string OrgName { get;  set; }
        [DataMember]
        public string OrgID { get;  set; }
    }
}