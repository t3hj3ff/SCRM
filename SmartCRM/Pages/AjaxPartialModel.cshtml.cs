using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNet.Identity;
using SmartCRMSvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using SmartCRM.Pages.Shared;
using Microsoft.AspNetCore.Http;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;

namespace SmartCRM.Pages
{
    public class AjaxPartialModelModel : PageModel
    {
        private bool UserIsAdmin { get { return User.IsInRole("UnicardAdmin"); } }
        public List<ReportFilial> Filials { get; set; }
        public List<Employ> Employes { get; set; }
        public List<string> ChoosedFilials { get; set; }
        public SmartCRMSvc.BlackListResult BlackList { get; set; }

        public void OnGet()
        {

        }

        public async Task<PartialViewResult> OnGetFilials()
        {
            var cl = new SmartCRMSvc.Service1Client();
            Filials = (await cl.GetReportFilialsAsync(UserIsAdmin ? (int?)null : User.Identity.GetUserId<int>(),
                UserIsAdmin ? Convert.ToInt32(User.GetClaim("ChoosedOrgId")) : (int?)null)).ToList();

            return new PartialViewResult
            {
                ViewName = "_MerchantFilials",
                ViewData = new ViewDataDictionary<List<SmartCRMSvc.ReportFilial>>(ViewData, Filials)
            };
        }

        public async Task<PartialViewResult> OnGetTesterGroups()
        {
            var cl = new SmartCRMSvc.Service1Client();
            Employes = (await cl.GetEmployesAsync(new ModelBase
            {
                UserID = UserIsAdmin ? (int?)null : User.Identity.GetUserId<int>(),
                OrgID = UserIsAdmin ? Convert.ToInt32(User.GetClaim("ChoosedOrgId")) : (int?)null
            })
        ).ToList();

            var model = new _TesterGroupModel();
            model.Employes = Employes;

            return new PartialViewResult
            {
                ViewName = "_TesterGroup",
                ViewData = new ViewDataDictionary<_TesterGroupModel>(ViewData, model)
            };
        }

        public async Task<JsonResult> OnPostCreateTester(SmartCRMSvc.Employ employer)
        {
            var cl = new SmartCRMSvc.Service1Client();
            var res = await cl.AddEmployerAsync(new EmployModel
            {
                UserID = UserIsAdmin ? (int?)null : User.Identity.GetUserId<int>(),
                OrgID = UserIsAdmin ? Convert.ToInt32(User.GetClaim("ChoosedOrgId")) : (int?)null,
                Employer = employer
            });

            return new JsonResult(res);
        }


        public async Task<JsonResult> OnPostDeleteTester(SmartCRMSvc.Employ employer)
        {
            var cl = new SmartCRMSvc.Service1Client();
            var res = await cl.DeleteEmployeeAsync(new EmployDeleteModel
            {
                UserID = UserIsAdmin ? (int?)null : User.Identity.GetUserId<int>(),
                OrgID = UserIsAdmin ? Convert.ToInt32(User.GetClaim("ChoosedOrgId")) : (int?)null,
                ID = employer.ID
            });

            return new JsonResult(res);
        }

        public async Task<PartialViewResult> OnGetBlackList()
        {
            var cl = new SmartCRMSvc.Service1Client();

            BlackList = await cl.GetBlackListAsync(new ModelBase
            {
                UserID = UserIsAdmin ? (int?)null : User.Identity.GetUserId<int>(),
                OrgID = UserIsAdmin ? Convert.ToInt32(User.GetClaim("ChoosedOrgId")) : (int?)null
            });

            var data = new _BlackListModel { BlackList = BlackList, };

            return new PartialViewResult
            {
                ViewName = "_BlackList",
                ViewData = new ViewDataDictionary<_BlackListModel>(ViewData, data)
            };
        }

        public async Task<IActionResult> OnGetBlackListTable()
        {
            var cl = new SmartCRMSvc.Service1Client();

            BlackList = await cl.GetBlackListAsync(new ModelBase
            {
                UserID = UserIsAdmin ? (int?)null : User.Identity.GetUserId<int>(),
                OrgID = UserIsAdmin ? Convert.ToInt32(User.GetClaim("ChoosedOrgId")) : (int?)null
            });

            if (BlackList == null)
            {
                BlackList = new BlackListResult();
            }


            return new PartialViewResult
            {
                ViewName = "_BlackListTable",
                ViewData = new ViewDataDictionary<BlackListResult>(ViewData, BlackList)
            };
        }

        public async Task<JsonResult> OnPostImportBlackList()
        {
            IFormFile file = Request.Form.Files[0];
            var Phones = new List<string>();

            if (file.Length > 0)
            {
                string sFileExtension = Path.GetExtension(file.FileName).ToLower();
                ISheet sheet;
                //   string fullPath = Path.Combine(newPath, file.FileName);
                using (var stream = new MemoryStream())
                {
                    file.CopyTo(stream);
                    stream.Position = 0;
                    if (sFileExtension == ".xls")
                    {
                        HSSFWorkbook hssfwb = new HSSFWorkbook(stream); //This will read the Excel 97-2000 formats  
                        sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook  
                    }
                    else
                    {
                        XSSFWorkbook hssfwb = new XSSFWorkbook(stream); //This will read 2007 Excel format  
                        sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook   
                    }
                    IRow headerRow = sheet.GetRow(0); //Get Header Row
                    int cellCount = headerRow.LastCellNum;

                    for (int j = 0; j < cellCount; j++)
                    {
                        NPOI.SS.UserModel.ICell cell = headerRow.GetCell(j);
                        if (cell == null || string.IsNullOrWhiteSpace(cell.ToString())) continue;
                        Phones.Add(cell.ToString());
                    }

                    for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++) //Read Excel File
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue;
                        if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;
                        for (int j = row.FirstCellNum; j < cellCount; j++)
                        {
                            if (row.GetCell(j) != null)
                                Phones.Add(row.GetCell(j).ToString());
                        }
                    }
                }
            }

            if (Phones.Count > 0)
            {
                var cl = new SmartCRMSvc.Service1Client();
                var user_is_admin = User.IsInRole("UnicardAdmin");

                var res = await cl.BlackListAddAsync(new BlackListModel
                {
                    UserID = User.Identity.GetUserId<int>(),
                    OrgID = Convert.ToInt32(User.GetClaim("OrgID")),
                    BulkID = (User.Identity.GetUserId<int>().ToString() + DateTime.Now.ToLongDateString()),
                    Phones = Phones.ToArray()
                });

                return new JsonResult(res);
            }

            return new JsonResult(new ResultBase { Result = 500, ResultMsg = "დაფიქსირდა შეცდომა" });

        }
    }
}