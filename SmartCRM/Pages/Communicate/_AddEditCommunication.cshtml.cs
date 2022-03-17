using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartCRM.Pages.Communicate
{
    public class _AddEditCommunicationModel : PageModel
    {
        public List<SmartCRMSvc.RFMSegment> Segments { get; set; }

        [BindProperty]
        public CummunicateVM Cummunicate { get; set; }
        
        [BindProperty]
        public SmartCRMSvc.CommunicationAddModel NewCommunication { get; set; }


    }
}