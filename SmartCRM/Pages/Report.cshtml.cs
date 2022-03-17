using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNet.Identity;
using SmartCRMSvc;
using System.Globalization;
using System.ServiceModel;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using System.Data;
using Microsoft.AspNetCore.Hosting;

namespace SmartCRM.Pages
{
    public class ReportModel : PageModel
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public ReportModel(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public List<ReportFilial> Filials { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var cl = new SmartCRMSvc.Service1Client();
            var user_is_admin = User.IsInRole("UnicardAdmin");

            Filials = (await cl.GetReportFilialsAsync(user_is_admin ? (int?)null : User.Identity.GetUserId<int>(),
                 user_is_admin ? Convert.ToInt32(User.GetClaim("ChoosedOrgId")) : (int?)null)).ToList();
            return Page();
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
                FilterDateID = date.HasValue ? date.Value : 2,
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
            switch (chart_type)
            {
                case 1:
                    return new JsonResult(await cl.GetReportTransDataAsync(model));
                //  case 2:
                //   return new JsonResult(await cl.GetReportTransactionDataAsync(model));
                //case 3:
                //    return new JsonResult(await cl.GetReportAverageAmountDataAsync(model));
                case 4:
                    model.FilterEndDate = _struct_date;
                    return new JsonResult(await cl.GetReportStructureDataAsync(model));
                case 5: return new JsonResult(await cl.GetReportAgesDataAsync(model));
                case 6: return new JsonResult(await cl.GetReportGenderDataAsync(model));
                default:
                    return new JsonResult(new { });
            }



            // return new JsonResult(data);
        }


        public async Task<IActionResult> OnGetExport()
        {

            string file_path = Path.Combine( _hostingEnvironment.WebRootPath, "report", "trans.xlsx");
            string sFileName = @"TransactionReport.xlsx";
            using (var memory = new NpoiMemoryStream())
            {
                memory.AllowClose = false;


                //  ISheet sheet = null;

                //var workbook = new XSSFWorkbook();

                //sheet = workbook.CreateSheet(sheetName);

                #region DataTableToExcel
                //int i = 0;
                //int j = 0;
                //int count = 0;

                //if (isColumnWritten == true)
                //{
                //    IRow row = sheet.CreateRow(0);
                //    for (j = 0; j < data.Columns.Count; ++j)
                //    {
                //        row.CreateCell(j).SetCellValue(data.Columns[j].ColumnName);
                //    }
                //    count = 1;
                //}
                //else
                //{
                //    count = 0;
                //}

                //for (i = 0; i < dt.Rows.Count; ++i)
                //{
                //    IRow row = sheet.CreateRow(count);
                //    for (j = 0; j < dt.Columns.Count; ++j)
                //    {
                //        row.CreateCell(j).SetCellValue(dt.Rows[i][j].ToString());
                //    }
                //    ++count;
                //}
                #endregion;

                
                using (FileStream file = new FileStream(file_path, FileMode.Open, FileAccess.Read, FileShare.None, 4096, true))
                {
                    var workbook = new XSSFWorkbook(file);

                    var sheet = workbook.GetSheetAt(0);

                    sheet.GetRow(1).Cells[1].SetCellValue("სან"); 

                    workbook.Write(memory);

                    memory.Flush();
                    memory.Seek(0, SeekOrigin.Begin);
                    // memory.AllowClose = true;
                    // memory.Close();
                    return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);

                }

            }
        }
    }


    public class NpoiMemoryStream : MemoryStream
    {
        public NpoiMemoryStream()
        {
            AllowClose = true;
        }

        public bool AllowClose { get; set; }

        public override void Close()
        {
            if (AllowClose)
                base.Close();
        }
    }
}