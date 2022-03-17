using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SmartCRMService.App_Code
{
    public class Employ
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Card { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }

    [DataContract]
    public class EmployModel : ModelBase
    {
        [DataMember]
        public Employ Employer { get; set; }
    }

    public class EmployResult : ResultBase
    {
        public int ID { get; set; }
    }


    [DataContract]
    public class EmployDeleteModel : ModelBase
    {
        [DataMember]
        public int ID { get; set; }
    }
}