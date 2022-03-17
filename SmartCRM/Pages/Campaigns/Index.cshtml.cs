using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using SmartCRMSvc;
using Microsoft.AspNetCore.Http.Internal;
using NPOI.SS.UserModel;
using System.IO;
using NPOI.XSSF.UserModel;
using Microsoft.AspNetCore.Http;
using System.Text;
using NPOI.HSSF.UserModel;
using System.Globalization;
using SmartCRM.Pages.Shared;
using System.Web;

namespace SmartCRM.Pages.Campaigns
{
    public class IndexModel : PageModel
    {
        private bool UserIsAdmin { get { return User.IsInRole("UnicardAdmin"); } }
        public SmartCRMSvc.BlackListResult BlackList { get; set; }
        public List<ReportFilial> Filials { get; set; }

        [BindProperty]
        public SmartCRMSvc.CampaignAddModel NewCampaign { get; set; }

        [BindProperty]
        public CampaginsVM CampaignsVM { get; set; }

        [BindProperty]
        public IFormFile BlackListFile { get; set; }

        //[BindProperty]
        //public SendTestMessage Msg { get; set; }

        public List<SmartCRMSvc.CampaignItem> Campaigns { get; set; }
        public List<SmartCRMSvc.RFMSegment> Segments { get; set; }

        public int? FilterStatus { get; set; }

        public async Task<IActionResult> OnGet(int? status)
        {
            FilterStatus = status;
            var cl = new SmartCRMSvc.Service1Client();
            // var data =  await cl.GetCampaignDetailsAsync(198);

            var user_is_admin = User.IsInRole("UnicardAdmin");

            Campaigns = (await cl.GetCampaignsAsync(new SmartCRMSvc.GetCampaignsModel
            {
                UserID = user_is_admin ? (int?)null : User.Identity.GetUserId<int>(),
                OrgID = user_is_admin ? Convert.ToInt32(User.GetClaim("ChoosedOrgId")) : (int?)null,
                Status = status
            })).ToList();

            Segments = (await cl.GetSegmentsAsync(new SmartCRMSvc.RFMStatModel
            {
                UserID = user_is_admin ? (int?)null : User.Identity.GetUserId<int>(),
                OrgID = user_is_admin ? Convert.ToInt32(User.GetClaim("ChoosedOrgId")) : (int?)null
            })).Select(s => new RFMSegment { ID = s.ID, Name = s.CustomerCount + ": " + s.Name }).ToList();

            CampaignsVM = new CampaginsVM();


            Filials = (await cl.GetReportFilialsAsync(user_is_admin ? (int?)null : User.Identity.GetUserId<int>(),
                user_is_admin ? Convert.ToInt32(User.GetClaim("ChoosedOrgId")) : (int?)null)).ToList();

            return Page();
        }

        public async Task<JsonResult> OnPost()
        {
            CampaignAddModel new_camp = BindCampaignAddModel();
            try
            {
                SmartCRMSvc.Service1Client cl = new SmartCRMSvc.Service1Client();
                var result = await cl.CampaignsAddAsync(new_camp);


                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                return new JsonResult(new SmartCRMSvc.ResultBase { ResultMsg = "შეცდომა", Result = 500 });
            }
        }

        public async Task<JsonResult> OnPostStartCampaign()
        {
            CampaignAddModel new_camp = BindCampaignAddModel();
            try
            {
                SmartCRMSvc.Service1Client cl = new SmartCRMSvc.Service1Client();
                var result = await cl.CampaignsAddAsync(new_camp);

                if (result.Result != 200)
                {
                    return new JsonResult(result);
                }

                var start_res = await cl.CRMCampStartCampaignAsync(new CRMCampStartCampaignModel
                {
                    CampID = result.ID,
                    OrgID = UserIsAdmin ? Convert.ToInt32(User.GetClaim("ChoosedOrgId")) : (int?)null,
                    UserID = User.Identity.GetUserId<int>()
                });

                if (start_res.Result != 200)
                {
                    return new JsonResult(new CampaignsAddResult
                    {
                        ID = result.ID,
                        Result = start_res.Result,
                        ResultMsg = start_res.ResultMsg
                    });
                }

                return new JsonResult(start_res);
            }
            catch (Exception ex)
            {
                return new JsonResult(new SmartCRMSvc.ResultBase { ResultMsg = "შეცდომა", Result = 500 });
            }
        }

        private CampaignAddModel BindCampaignAddModel()
        {
            var new_camp = new SmartCRMSvc.CampaignAddModel();

            var user_is_admin = User.IsInRole("UnicardAdmin");
            new_camp.UserID = user_is_admin ? (int?)null : User.Identity.GetUserId<int>();
            new_camp.OrgID = user_is_admin ? Convert.ToInt32(User.GetClaim("ChoosedOrgId")) : (int?)null;

            new_camp.SegmentCount = NewCampaign.SegmentCount;

            if (!String.IsNullOrEmpty(CampaignsVM.SendMSGDate))
            {
                DateTime dt;
                if (DateTime.TryParseExact(CampaignsVM.SendMSGDate, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None,
                    out dt))
                {
                    new_camp.SendMSGDate = dt;
                }



            }


            switch (CampaignsVM.selectradio)
            {
                case "sale":
                    new_camp.ActionPercent = NewCampaign.ActionPercent;
                    break;
                case "voucher":
                    new_camp.ActionCashVaucher = NewCampaign.ActionCashVaucher;
                    new_camp.ActionCashVaucherPercent = NewCampaign.ActionCashVaucherPercent;

                    if (!String.IsNullOrEmpty(CampaignsVM.ActionCashVaucherStart))
                    {
                        DateTime v_start;
                        if (DateTime.TryParseExact(CampaignsVM.ActionCashVaucherStart, "dd/MM/yyyy", CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None,
                            out v_start))
                        {
                            new_camp.ActionCashVaucherStart = v_start;
                        }
                    }

                    if (!String.IsNullOrEmpty(CampaignsVM.ActionCashVaucherEnd))
                    {
                        DateTime v_end;
                        if (DateTime.TryParseExact(CampaignsVM.ActionCashVaucherEnd, "dd/MM/yyyy", CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None,
                            out v_end))
                        {
                            new_camp.ActionCashVaucherEnd = v_end;
                        }
                    }

                    break;
                case "points":
                    new_camp.ActionBonusFix = NewCampaign.ActionBonusFix;
                    new_camp.ActionBonusPercent = NewCampaign.ActionBonusPercent;
                    break;
                case "product":
                    new_camp.ActionProductName = NewCampaign.ActionProductName;
                    new_camp.ActionProductCount = NewCampaign.ActionProductCount;
                    break;
                default:
                    break;
            }



            switch (CampaignsVM.selectradio1)
            {
                case "visitcount":
                    new_camp.PersentCaseVisitCount = NewCampaign.PersentCaseVisitCount;
                    new_camp.PersentCaseVisitAmount = NewCampaign.PersentCaseVisitAmount;

                    break;
                case "moneyspent":
                    new_camp.PersentCaseSpentAmount = NewCampaign.PersentCaseSpentAmount;
                    new_camp.PersentCaseSpentCount = NewCampaign.PersentCaseSpentCount;
                    //  new_camp.PersentCaseSpentReceiveCount = NewCampaign.PersentCaseSpentReceiveCount;
                    break;
                case "timeinterval":
                    new_camp.PersentCaseSpentWDay1 = CampaignsVM.Week1 ? 1 : (int?)null;
                    new_camp.PersentCaseSpentWDay2 = CampaignsVM.Week2 ? 1 : (int?)null;
                    new_camp.PersentCaseSpentWDay3 = CampaignsVM.Week3 ? 1 : (int?)null;
                    new_camp.PersentCaseSpentWDay4 = CampaignsVM.Week4 ? 1 : (int?)null;
                    new_camp.PersentCaseSpentWDay5 = CampaignsVM.Week5 ? 1 : (int?)null;
                    new_camp.PersentCaseSpentWDay6 = CampaignsVM.Week6 ? 1 : (int?)null;
                    new_camp.PersentCaseSpentWDay7 = CampaignsVM.Week7 ? 1 : (int?)null;

                    new_camp.PersentCaseSpentWDayStartTime = NewCampaign.PersentCaseSpentWDayStartTime;
                    new_camp.PersentCaseSpentWDayEndTime = NewCampaign.PersentCaseSpentWDayEndTime;
                    break;
                case "upirobod":
                    new_camp.PersentCaseSpentUncondition = 1;
                    break;
                default:
                    break;
            }

            new_camp.PersentCaseReceiveCount = NewCampaign.PersentCaseReceiveCount;

            new_camp.HasBlackList = CampaignsVM.BlackList == "1" ? 1 : 0;


            if (!String.IsNullOrEmpty(CampaignsVM.ActionStartDate))
            {
                DateTime act_dt_start;
                if (DateTime.TryParseExact(CampaignsVM.ActionStartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None,
                    out act_dt_start))
                {
                    new_camp.ActionStartDate = act_dt_start;
                }
            }
            if (!String.IsNullOrEmpty(CampaignsVM.ActionEndDate))
            {
                DateTime act_dt_end;
                if (DateTime.TryParseExact(CampaignsVM.ActionEndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None,
                    out act_dt_end))
                {
                    new_camp.ActionEndDate = act_dt_end;
                }
            }
            // = CampaignsVM.selectradio1 == ""  NewCampaign.PersentCaseSpentUncondition;
            //new_camp.ActionStartDate = CampaignsVM.ActionStartDate;
            //new_camp.ActionEndDate = CampaignsVM.ActionEndDate;
            new_camp.PresentText = NewCampaign.PresentText;
            new_camp.ChampName = NewCampaign.ChampName;
            new_camp.ChampDescription = NewCampaign.ChampDescription;
            new_camp.SegmentID = NewCampaign.SegmentID;
            new_camp.SegmentCompareCount = NewCampaign.SegmentCompareCount;

            new_camp.PresentLocation = NewCampaign.PresentLocation;

            new_camp.MsgText = NewCampaign.MsgText;
            new_camp.PerformMsgText = NewCampaign.PerformMsgText;
            new_camp.RemindMsgText = (String.IsNullOrEmpty(CampaignsVM.RemindMsgText1) ? "" : (CampaignsVM.RemindMsgText1.EndsWith(' ') ? CampaignsVM.RemindMsgText1 : CampaignsVM.RemindMsgText1 + " ")) + 
                                     (String.IsNullOrEmpty( CampaignsVM.RemindMsgTextKey)? "" : CampaignsVM.RemindMsgTextKey)  +
                     (String.IsNullOrEmpty(CampaignsVM.RemindMsgText2) ? "" :
                     (CampaignsVM.RemindMsgText2.StartsWith(' ')
                     ? CampaignsVM.RemindMsgText2 :
                     " " + CampaignsVM.RemindMsgText2));

            new_camp.AddGroupInAction = CampaignsVM.AddGroupInAction ? 1 : (int?)null;


            new_camp.CampID = NewCampaign.CampID;
            return new_camp;
        }

        public async Task<PartialViewResult> OnGetFillCampaign(int? id, int? s_id)
        {
            var user_is_admin = User.IsInRole("UnicardAdmin");
            var cl = new SmartCRMSvc.Service1Client();

            CampaignsVM = new CampaginsVM();
            var data = new _CampaignDetailsModel();
            data.NewCampaign = new SmartCRMSvc.CampaignAddModel();
            data.CampaignsVM = CampaignsVM;
            data.NewCampaign.PresentLocation = "";

            Segments = (await cl.GetSegmentsAsync(new SmartCRMSvc.RFMStatModel
            {
                UserID = user_is_admin ? (int?)null : User.Identity.GetUserId<int>(),
                OrgID = user_is_admin ? Convert.ToInt32(User.GetClaim("ChoosedOrgId")) : (int?)null
            })).Select(s => new RFMSegment { ID = s.ID, Name = s.CustomerCount + ": " + s.Name }).ToList();
            // Name = System.Web.HttpUtility.HtmlDecode(String.Format("{0,-7}", s.CustomerCount).Replace(" ", "&nbsp;") +" "+s.Name)}
            Filials = (await cl.GetReportFilialsAsync(user_is_admin ? (int?)null : User.Identity.GetUserId<int>(),
            user_is_admin ? Convert.ToInt32(User.GetClaim("ChoosedOrgId")) : (int?)null)).ToList();

            data.Filials = Filials;

            data.Segments = Segments;

            if (id.HasValue)
            {
                var result = (await cl.GetCampaignDetailsAsync(id.Value));

                CampaignsVM.selectradio = result.ActionPercent.HasValue ? "sale" :
                                         result.ActionCashVaucher.HasValue ||
               result.ActionCashVaucherPercent.HasValue ||
               result.ActionCashVaucherStart.HasValue ||
               result.ActionCashVaucherEnd.HasValue ? "voucher" :
               result.ActionBonusFix.HasValue ||
               result.ActionBonusPercent.HasValue ? "points" :
             !String.IsNullOrEmpty(result.ActionProductName) ||
               result.ActionProductCount.HasValue ? "product" : "";


                CampaignsVM.moneych = result.ActionPercent.HasValue ? "პროცენტული" : null;
                CampaignsVM.voucherch = result.ActionCashVaucher.HasValue ? "ფულადი" : result.ActionCashVaucherPercent.HasValue ? "პროცენტული" : "";
                CampaignsVM.pointsch = result.ActionBonusFix.HasValue ? "ქულა" : result.ActionBonusPercent.HasValue ? "-მაგი ქულა" : "";

                CampaignsVM.BlackList = result.HasBlackList != null ? result.HasBlackList : null;

                CampaignsVM.Week1 = result.PersentCaseSpentWDay1.HasValue && result.PersentCaseSpentWDay1.Value > 0;
                CampaignsVM.Week2 = result.PersentCaseSpentWDay2.HasValue && result.PersentCaseSpentWDay2.Value > 0;
                CampaignsVM.Week3 = result.PersentCaseSpentWDay3.HasValue && result.PersentCaseSpentWDay3.Value > 0;
                CampaignsVM.Week4 = result.PersentCaseSpentWDay4.HasValue && result.PersentCaseSpentWDay4.Value > 0;
                CampaignsVM.Week5 = result.PersentCaseSpentWDay5.HasValue && result.PersentCaseSpentWDay5.Value > 0;
                CampaignsVM.Week6 = result.PersentCaseSpentWDay6.HasValue && result.PersentCaseSpentWDay6.Value > 0;
                CampaignsVM.Week7 = result.PersentCaseSpentWDay7.HasValue && result.PersentCaseSpentWDay7.Value > 0;


                CampaignsVM.selectradio1 = result.PersentCaseVisitCount.HasValue
                                         || result.PersentCaseVisitAmount.HasValue ? "visitcount"
                                         : result.PersentCaseSpentAmount.HasValue
                                         || result.PersentCaseSpentCount.HasValue
                                         //  || result.PersentCaseSpentReceiveCount.HasValue 
                                         ? "moneyspent"
                                         : CampaignsVM.Week1 ||
                                         CampaignsVM.Week2 ||
                                         CampaignsVM.Week3 ||
                                         CampaignsVM.Week4 ||
                                         CampaignsVM.Week5 ||
                                         CampaignsVM.Week6 ||
                                         CampaignsVM.Week7 ||
                                       !String.IsNullOrEmpty(result.PersentCaseSpentWDayStartTime) ||
                                       !String.IsNullOrEmpty(result.PersentCaseSpentWDayEndTime)
                                       ? "timeinterval" :
                                       result.PersentCaseSpentUncondition.HasValue ? "upirobod" : "";


                data.NewCampaign = new SmartCRMSvc.CampaignAddModel
                {
                    ActionBonusFix = result.ActionBonusFix,
                    ActionBonusPercent = result.ActionBonusPercent,
                    ActionCashVaucher = result.ActionCashVaucher,
                    ActionCashVaucherEnd = result.ActionCashVaucherEnd,
                    ActionCashVaucherPercent = result.ActionCashVaucherPercent,
                    ActionCashVaucherStart = result.ActionCashVaucherStart,
                    ActionEndDate = result.ActionEndDate,
                    ActionPercent = result.ActionPercent,
                    ActionProductCount = result.ActionProductCount,
                    ActionProductName = result.ActionProductName,
                    ActionStartDate = result.ActionStartDate,
                    CampID = result.CampID,
                    ChampDescription = result.ChampDescription,
                    ChampName = result.ChampName,
                    OrgID = result.OrgID,
                    PersentCaseSpentAmount = result.PersentCaseSpentAmount,
                    PersentCaseSpentCount = result.PersentCaseSpentCount,
                    // PersentCaseSpentReceiveCount = result.PersentCaseSpentReceiveCount,
                    PersentCaseSpentUncondition = result.PersentCaseSpentUncondition,
                    PersentCaseSpentWDay1 = result.PersentCaseSpentWDay1,
                    PersentCaseSpentWDay2 = result.PersentCaseSpentWDay2,
                    PersentCaseSpentWDay3 = result.PersentCaseSpentWDay3,
                    PersentCaseSpentWDay4 = result.PersentCaseSpentWDay4,
                    PersentCaseSpentWDay5 = result.PersentCaseSpentWDay5,
                    PersentCaseSpentWDay6 = result.PersentCaseSpentWDay6,
                    PersentCaseSpentWDay7 = result.PersentCaseSpentWDay7,
                    PersentCaseSpentWDayEndTime = result.PersentCaseSpentWDayEndTime,
                    PersentCaseSpentWDayStartTime = result.PersentCaseSpentWDayStartTime,
                    PersentCaseVisitAmount = result.PersentCaseVisitAmount,
                    PersentCaseVisitCount = result.PersentCaseVisitCount,
                    //PersentCaseVisitReceiveCount = result.PersentCaseVisitReceiveCount,
                    PersentCaseReceiveCount = result.PersentCaseReceiveCount,
                    PresentLocation = result.PresentLocation == null ? "" : result.PresentLocation,
                    MsgText = result.MsgText,
                    RemindMsgText = result.RemindMsgText,
                    PerformMsgText = result.PerformMsgText,
                    SendMSGDate = result.SendMSGDate,
                    AddGroupInAction = result.AddGroupInAction ? 1 : (int?)null,
                    SegmentCount = result.SegmentCount,
                    SegmentID = result.SegmentID,
                    SegmentCompareCount = result.SegmentCompareCount,
                    Status = result.Status
                };
                try
                {

                    CampaignsVM.RemindMsgText1 = !String.IsNullOrEmpty(result.RemindMsgText) ? result.RemindMsgText.Substring(0, result.RemindMsgText.IndexOf('{')) : "";
                    CampaignsVM.RemindMsgText2 = !String.IsNullOrEmpty(result.RemindMsgText) ? result.RemindMsgText.Substring(result.RemindMsgText.IndexOf('}') + 1) : "";

                }
                catch
                {
                }

                CampaignsVM.AddGroupInAction = result.AddGroupInAction;
                CampaignsVM.SendMSGDate = result.SendMSGDate.HasValue ? result.SendMSGDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : "";

                CampaignsVM.ActionStartDate = result.ActionStartDate.HasValue ? result.ActionStartDate.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "";
                CampaignsVM.ActionEndDate = result.ActionEndDate.HasValue ? result.ActionEndDate.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "";

                CampaignsVM.ActionCashVaucherStart = result.ActionCashVaucherStart.HasValue ? result.ActionCashVaucherStart.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "";
                CampaignsVM.ActionCashVaucherEnd = result.ActionCashVaucherEnd.HasValue ? result.ActionCashVaucherEnd.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "";

                data.CampaignsVM = CampaignsVM;
                data.CampID = id;
            }
            else
            {
                if (s_id.HasValue)
                {
                    data.NewCampaign.SegmentID = s_id.Value;
                }
            }

            return new PartialViewResult
            {
                ViewName = "_CampaignDetails",
                ViewData = new ViewDataDictionary<_CampaignDetailsModel>(ViewData, data)
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


        public async Task<ActionResult> OnGetCampaignDelete(int id)
        {
            var cl = new SmartCRMSvc.Service1Client();
            var res = await cl.CampaignDeleteAsync(new CampaignDeleteModel
            {
                OrgID = UserIsAdmin ? Convert.ToInt32(User.GetClaim("ChoosedOrgId")) : (int?)null,
                UserID = UserIsAdmin ? (int?)null : User.Identity.GetUserId<int>(),
                ID = id
            });

            if (res != null && res.Result == 200)
            {
                return RedirectToAction("OnGet");
            }
            return Page();
        }

        public async Task<JsonResult> OnPostSendTestMsg(string msg, string p_msg, string r_msg)
        {
            var cl = new SmartCRMSvc.Service1Client();
            return new JsonResult(await cl.CampaignSendTestMsgAsync(new CampaignSendTestMsgModel
            {
                OrgID = UserIsAdmin ? Convert.ToInt32(User.GetClaim("ChoosedOrgId")) : (int?)null,
                UserID = UserIsAdmin ? (int?)null : User.Identity.GetUserId<int>(),
                MsgText = msg,
                PerformMsgText = p_msg,
                RemindMsgText = r_msg
            }));
        }


    }

    public class SendTestMessage
    {
        public string msg { get; set; }
        public string p_msg { get; set; }
        public string r_msg { get; set; }
    }

    public class CampaginsVM
    {
        public bool Week1 { get; set; }
        public bool Week2 { get; set; }
        public bool Week3 { get; set; }
        public bool Week4 { get; set; }
        public bool Week5 { get; set; }
        public bool Week6 { get; set; }
        public bool Week7 { get; set; }

        public string selectradio { get; set; }
        public string moneych { get; set; }
        public string voucherch { get; set; }
        public string pointsch { get; set; }

        public string selectradio1 { get; set; }

        public string BlackList { get; set; }
        public bool SendTestMSG { get; set; }
        public bool AddGroupInAction { get; set; }

        public string SendMSGDate { get; set; }
        public string ActionStartDate { get; set; }
        public string ActionEndDate { get; set; }
        public string ActionCashVaucherStart { get; set; }
        public string ActionCashVaucherEnd { get; set; }

        public string RemindMsgText1 { get; set; }
        public string RemindMsgText2 { get; set; }
        public string RemindMsgTextKey { get; set; }
    }
}