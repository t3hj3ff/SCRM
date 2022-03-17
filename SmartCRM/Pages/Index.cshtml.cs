using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using SmartCRMSvc;
using Microsoft.AspNet.Identity;
using System.Globalization;

namespace SmartCRM.Pages
{
    public class IndexModel : PageModel
    {
        public List<ReportFilial> Filials { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var user_is_admin = User.IsInRole("UnicardAdmin");
            // AddressFamily = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();

            if (user_is_admin)
            {
                if (String.IsNullOrEmpty(User.GetClaim("ChoosedOrgId")))
                {
                    return RedirectToPage("UniDashboard");
                }
            }

            var cl = new SmartCRMSvc.Service1Client();
            Filials = (await cl.GetReportFilialsAsync(user_is_admin ? (int?)null : User.Identity.GetUserId<int>(),
               user_is_admin ? Convert.ToInt32(User.GetClaim("ChoosedOrgId")) : (int?)null)).ToList();

            return Page();
        }
        public async Task<JsonResult> OnGetDashboard()
        {
            var cl = new SmartCRMSvc.Service1Client();
            if (User.IsInRole("UnicardAdmin"))
            {
                var org_id = Convert.ToInt32(User.GetClaim("ChoosedOrgId"));
                var data = await cl.GetDashboardDataByOrgAsync(org_id, DateTime.Now.Date);
                return new JsonResult(data);
            }
            else
            {

                var data = await cl.GetDashboardDataAsync(User.Identity.GetUserId<int>(), DateTime.Now.Date);
                return new JsonResult(data);
            }
        }


        public async Task<JsonResult> OnGetReport(int chart_type, int? date, int? gender,
           string[] age, string merchants, int? group_by_date, string start_date, string end_date, string struct_date, int? group_by)
        {
            //var merchants_arr = String.IsNullOrEmpty(merchants) ? new List<string>().ToArray() :merchants.Split(',');

            var ages = String.Join(",", age.ToList().Where(a => a != "null"));
            var merchant_list = merchants;//String.Join(",", merchants.ToList().Where(a => a != "null"));

            if (ages == "") ages = null;
            if (merchant_list == "") merchant_list = null;

            DateTime? _start_date = (DateTime?)null;
            DateTime? _end_date = (DateTime?)null;
            DateTime? _struct_date = (DateTime?)null;

            if (!String.IsNullOrEmpty(start_date))
            {
                try
                {
                    _start_date = DateTime.ParseExact(start_date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);
                }
                catch (Exception)
                {
                    _start_date = (DateTime?)null;
                }
            }

            if (!String.IsNullOrEmpty(end_date))
            {
                try
                {
                    _end_date = DateTime.ParseExact(end_date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);
                }
                catch (Exception)
                {
                    _end_date = (DateTime?)null;
                }
            }

            if (!String.IsNullOrEmpty(struct_date))
            {
                try
                {
                    _struct_date = DateTime.ParseExact(struct_date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);
                }
                catch (Exception)
                {
                    _struct_date = (DateTime?)null;
                }
            }

            var cl = new SmartCRMSvc.Service1Client();

            cl.InnerChannel.OperationTimeout = new TimeSpan(0, 5, 0);


            var model = new SmartCRMSvc.ReportModelBase
            {
                UserID = User.IsInRole("UnicardAdmin") ? (int?)null : User.Identity.GetUserId<int>(),
                FilterOrgID = User.IsInRole("UnicardAdmin") ? Convert.ToInt32(User.GetClaim("ChoosedOrgId")) : (int?)null,
                FilterDateID = date.HasValue ? date.Value : 3,
                FilterAgeRangeID = ages,
                FilterObjectID = merchant_list,
                FilterGender = gender,
                GroupByDate = group_by_date,
                FilterStartDate = _start_date,
                FilterEndDate = _end_date,
                GroupByAge = group_by.HasValue && group_by.Value == 2 ? 1 : (int?)null,
                GroupByGender = group_by.HasValue && group_by.Value == 1 ? 1 : (int?)null,
                GroupByFilial = group_by.HasValue && group_by.Value == 3 ? 1 : (int?)null
            };

            var dashboard = new Dashboard();


            switch (chart_type)
            {

                //DatePeriod = rd["Date_Period"].ToString(),
                //           Amount = (decimal)rd["Amount"],
                //           TranCount = Convert.ToInt32(rd["tran_cnt"].ToString()),
                //           AvgAmount = (decimal)rd["AVG_Amount"]


                case 1:
                    var _res = (await cl.GetReportTransDataAsync(model)).Select(r => new VisitTurnover
                    {
                        Day = r.DatePeriod,
                        Turnover = r.Amount,
                        Visit = r.TranCount,
                        AvgAmount = r.AvgAmount
                    });

                    return new JsonResult(_res);
                //  case 2:
                //   return new JsonResult(await cl.GetReportTransactionDataAsync(model));
                //case 3:
                //    return new JsonResult(await cl.GetReportAverageAmountDataAsync(model));
                case 4:
                    model.FilterEndDate = _struct_date;
                    return new JsonResult(await cl.GetReportStructureDataAsync(model));
                case 5:
                    return new JsonResult((await cl.GetReportAgesDataAsync(model)).Select(r => new DashboardItemBase
                    {
                        Count = r.Count ?? 0,
                        Description = r.Description
                    }));
                case 6: return new JsonResult(await cl.GetReportGenderDataAsync(model));
                default:
                    return new JsonResult(new { });
            }
        }

    }
}
