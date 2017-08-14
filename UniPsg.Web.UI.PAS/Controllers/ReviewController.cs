using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using UniPsg.Model.PAS.ViewModels;
using UniPsg.Web.UI.PAS.Models;
using PagedList;
using System.Net.Mail;
using System.Text;
using MvcSiteMapProvider.Web.Mvc.Filters;
using System.DirectoryServices;

namespace UniPsg.Web.UI.PAS.Controllers
{
    public class ReviewController : Controller
    {
        // 分頁後每頁顯示的筆數
        private const int PageSize = 100;
        private string LDAPPath = System.Web.Configuration.WebConfigurationManager.AppSettings["LDAPPath"];

        //email通知網域
        private string NoticeDomain = System.Web.Configuration.WebConfigurationManager.AppSettings["NoticeDomain"];
        private string FromMailAccount = System.Web.Configuration.WebConfigurationManager.AppSettings["FromMailAccount"];
        private string FromMailName = System.Web.Configuration.WebConfigurationManager.AppSettings["FromMailName"];
        private string NoticeUrl = System.Web.Configuration.WebConfigurationManager.AppSettings["NoticeUrl"];

        //連結WebApi
        private AssessPersonClient client = new AssessPersonClient();
        private AssessFormClient formClient = new AssessFormClient();
        private ScopeItemClient itemeClient = new ScopeItemClient();
        private OrganizationClient orgClient = new OrganizationClient();
        private ProjectReviewClient reviewClinet = new ProjectReviewClient();
        private MailClient mailClinet = new MailClient();

        [SiteMapCacheRelease]
        // GET: Review
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Index(string sortOrder, int? page)
        {
            string account = User.Identity.Name.ToString();
            //string account = @"PSCNET\79338";
            //string account = @"PSCNET\KAVIN";
            //string account = @"PSCNET\ELICE_WU";            
            //string account = @"PSCNET\AMY-LIN";
            account = account.Replace(@"labpsc\", "");
            account = account.Replace(@"LABPSC\", "");
            account = account.Replace(@"pscnet\", "");
            account = account.Replace(@"PSCNET\", "");
            string employeeNo = string.Empty;
            //var employeeNo = orgClient.FindEmployeeNumber(account, 0);
            DirectoryEntry searchRoot = new DirectoryEntry(LDAPPath);
            DirectorySearcher search = new DirectorySearcher(searchRoot);
            search.Filter = "(&(objectClass=user)(SAMAccountName=" + account.ToUpper() + "))";
            SearchResult results = search.FindOne();
            if (results != null)
            {
                ResultPropertyCollection resultPropColl;
                resultPropColl = results.Properties;
                employeeNo = (string)resultPropColl["description"][0];
            }
            var model = client.FindByReviewer(employeeNo, 0);
            ReviewerIndexView viewModel = new ReviewerIndexView();
            ViewBag.CurrentSort = sortOrder;
            ViewBag.FormSortParm = String.IsNullOrEmpty(sortOrder) ? "form_desc" : "";
            ViewBag.GroupSortParm = sortOrder == "Group" ? "group_desc" : "Group";

            switch (sortOrder)
            {
                case "form_desc":
                    model = model.OrderByDescending(m => m.FormName);
                    break;
                case "Group":
                    model = model.OrderBy(m => m.GroupName);
                    break;
                case "group_desc":
                    model = model.OrderByDescending(m => m.GroupName);
                    break;
                default:
                    model = model.OrderBy(m => m.ProjectId);
                    break;
            }

            int pageIndex = (page ?? 1);
            viewModel.Reviews = model.ToPagedList(pageIndex, PageSize);

            return View(viewModel);
        }

        // GET: Review
        public ActionResult Update1(int projectId, string employeeNo)
        {
            var model = client.FindReviewer(projectId, employeeNo, employeeNo);
            if (model.GroupId == 6)
            {
                if (model.Reviews.Count != 0)
                {
                    ViewBag.Reviews1 = model.Reviews.FindAll(r => r.KPICategoryId == 11).OrderBy(r => r.CreatedDate).ToList();
                    ViewBag.Reviews2 = model.Reviews.FindAll(r => r.KPICategoryId == 9).OrderBy(r => r.CreatedDate).ToList();
                    ViewBag.Reviews3 = model.Reviews.FindAll(r => r.KPICategoryId == 12).OrderBy(r => r.CreatedDate).ToList();
                    ViewBag.Reviews4 = model.Reviews.FindAll(r => r.KPICategoryId == 13).OrderBy(r => r.CreatedDate).ToList();
                }
                var item1 = itemeClient.Find(52);
                var item2 = itemeClient.Find(53);
                var item3 = itemeClient.Find(54);
                var item4 = itemeClient.Find(55);
                ViewBag.ScopItem1 = item1.Name;
                ViewBag.ScopItem2 = item2.Name;
                ViewBag.ScopItem3 = item3.Name;
                ViewBag.ScopItem4 = item4.Name;

                ViewBag.Definition1 = item1.Definition;
                ViewBag.Definition2 = item2.Definition;
                ViewBag.Definition3 = item3.Definition;
                ViewBag.Definition4 = item4.Definition;

            }
            if (model.GroupId == 4)
            {
                if (model.Reviews.Count == 0)
                {
                    var scope = formClient.Find(model.FormId);
                    if (model.CategoryId == 1)
                    {
                        List<ProjectReviewViewModel> models = new List<ProjectReviewViewModel>();

                        var items = itemeClient.FindByScopeItems(Convert.ToInt32(scope.Items), model.GroupId.ToString()).OrderBy(r => r.CreatedDate);
                        if (items != null)
                        {
                            foreach (var item in items)
                            {
                                ProjectReviewViewModel review = new ProjectReviewViewModel();
                                review.ProjectId = projectId;
                                review.EmployeeNo = employeeNo;
                                review.Reviewer = employeeNo;
                                review.ScopeId = Convert.ToInt32(scope.Items);
                                review.ItemId = item.Id;
                                review.ItemName = item.Name;
                                review.Definition = item.Definition;
                                review.KPICategoryId = item.KPICategoryId;
                                review.KPICategoryName = item.KPICategoryName;
                                review.EmployeeNo = employeeNo;
                                review.Comment = "";
                                review.Score = 0;
                                review.Weight = item.Weight;
                                review.ScopeId = item.ScopeId;
                                review.Status = 0;
                                review.Creator = User.Identity.Name.ToString();
                                review.Modifier = User.Identity.Name.ToString();

                                models.Add(review);
                            }
                            model.Reviews = models.OrderBy(r => r.CreatedDate).ToList();
                        }
                    }
                }
            }
            ViewBag.Rows = model.Reviews.Count();
            model.Reviews = model.Reviews.OrderBy(r => r.CreatedDate).ToList();
            if (model.GroupId == 4)
                return View("Update14", model);
            if (model.GroupId == 6)
                return View("Update16", model);
            if (model.GroupId == 7)
                return View("Update17", model);
            return View("Update1", model);
        }

        // POST: Review/Create    
        [HttpPost]
        public ActionResult Update14(ReviewerUpdateViewModel models, string save, string reviw)
        {
            if (!string.IsNullOrEmpty(save))
                reviewClinet.Update(models.Reviews, User.Identity.Name.ToString(), "save", "0");
            if (!string.IsNullOrEmpty(reviw))
            {
                string reviewer = models.Reviewer;
                int projectId = models.ProjectId;
                string employeeNo = models.EmployeeNo;
                var statusCode = reviewClinet.Update(models.Reviews, User.Identity.Name.ToString(), "reviw", "0");
                if (statusCode)
                {
                    var item = client.Find(projectId, employeeNo);
                    //var nextReviewerAccount = orgClient.GetManagerAccount(reviewer, projectId);
                    string  nextReviewerAccount = string.Empty;
                    DirectoryEntry searchRoot = new DirectoryEntry(LDAPPath);
                    DirectorySearcher search = new DirectorySearcher(searchRoot);
                    search.Filter = "(&(objectClass=user)(description=" + item.Reviewer + "))";
                    SearchResult results = search.FindOne();
                    if (results != null)
                    {
                        ResultPropertyCollection resultPropColl;
                        resultPropColl = results.Properties;
                        nextReviewerAccount = (string) resultPropColl["SAMAccountName"][0];
                    }
                    if (nextReviewerAccount != string.Empty)
                    {
                        #region 建立MailMessage物件

                        MailMessage msg = new MailMessage();
                        msg.To.Add(nextReviewerAccount + NoticeDomain);
                        msg.From = new MailAddress(FromMailAccount + NoticeDomain, FromMailName, System.Text.Encoding.UTF8);

                        msg.Subject = "員工自評完成通知主管";
                        msg.SubjectEncoding = Encoding.UTF8;
                        msg.Body = msg.Body = @"主管您好:<br/>您所屬同仁  " + item.EmployeeName + "  已完成考核作業,請進入系統進行複核作業。<br/>" +
                            "→點入績效管理系統作業<a href =" + NoticeUrl + "Manage/Index>" + item.ProjectName + "-" + item.EmployeeName + "</a>";

                        msg.BodyEncoding = Encoding.UTF8;
                        msg.IsBodyHtml = true;
                        msg.Priority = MailPriority.Low; //Mail priority
                        #endregion

                        #region SmtpClient (network)

                        SmtpClient smtpClient = new SmtpClient();
                        //smtpClient.Credentials = new System.Net.NetworkCredential("intranet@labpsc.com.tw", "lab123");
                        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                        smtpClient.Send(msg);

                        #endregion
                    }
                }
            }
            return RedirectToAction("Index");
        }

        // GET: Review/Create    

        public ActionResult Update17(int projectId, string employeeNo, string reviewer)
        {
            var statusCode = reviewClinet.Review(projectId, employeeNo, reviewer, User.Identity.Name.ToString(), 0, "0");
            if (statusCode)
            {
                var item = client.Find(projectId, employeeNo);
                //var nextReviewerAccount = orgClient.GetManagerAccount(reviewer, projectId);
                string nextReviewerAccount = string.Empty;
                DirectoryEntry searchRoot = new DirectoryEntry(LDAPPath);
                DirectorySearcher search = new DirectorySearcher(searchRoot);
                search.Filter = "(&(objectClass=user)(description=" + item.Reviewer + "))";
                SearchResult results = search.FindOne();
                if (results != null)
                {
                    ResultPropertyCollection resultPropColl;
                    resultPropColl = results.Properties;
                    nextReviewerAccount = (string)resultPropColl["SAMAccountName"][0];
                }
                if (nextReviewerAccount != null)
                {
                    #region 建立MailMessage物件

                    MailMessage msg = new MailMessage();
                    //msg.To.Add("janewu@uni-psg.com");
                    msg.To.Add(nextReviewerAccount + NoticeDomain);
                    msg.From = new MailAddress(FromMailAccount + NoticeDomain, FromMailName, System.Text.Encoding.UTF8);
                    //msg.From = new MailAddress("janewu@uni-psg.com", FromMailName, System.Text.Encoding.UTF8);

                    msg.Subject = "員工自評完成通知主管";
                    msg.SubjectEncoding = Encoding.UTF8;
                    msg.Body = msg.Body = @"主管您好:<br/>您所屬同仁  " + item.EmployeeName + "  已完成考核作業,請進入系統進行複核作業。<br/>" +
                        "→點入績效管理系統作業<a href =" + NoticeUrl + "Manage/Index>" + item.ProjectName + "-" + item.EmployeeName + "</a>";

                    msg.BodyEncoding = Encoding.UTF8;
                    msg.IsBodyHtml = true;
                    msg.Priority = MailPriority.Low; //Mail priority
                    #endregion

                    #region SmtpClient (network)

                    SmtpClient smtpClient = new SmtpClient();
                    //smtpClient.Credentials = new System.Net.NetworkCredential("janewu@uni-psg.com", "01012@Janewu");
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                    smtpClient.Send(msg);

                    #endregion
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Update16(int projectId, string employeeNo, string reviewer)
        {
            var statusCode = reviewClinet.Review(projectId, employeeNo, reviewer, User.Identity.Name.ToString(), 0, "0");
            if (statusCode)
            {
                var item = client.Find(projectId, employeeNo);
                //var nextReviewerAccount = orgClient.GetManagerAccount(reviewer, projectId);
                string nextReviewerAccount = string.Empty;
                DirectoryEntry searchRoot = new DirectoryEntry(LDAPPath);
                DirectorySearcher search = new DirectorySearcher(searchRoot);
                search.Filter = "(&(objectClass=user)(description=" + item.Reviewer + "))";
                SearchResult results = search.FindOne();
                if (results != null)
                {
                    ResultPropertyCollection resultPropColl;
                    resultPropColl = results.Properties;
                    nextReviewerAccount = (string)resultPropColl["SAMAccountName"][0];
                }
                if (nextReviewerAccount != null)
                {
                    #region 建立MailMessage物件

                    MailMessage msg = new MailMessage();
                    msg.To.Add(nextReviewerAccount + NoticeDomain);
                    msg.From = new MailAddress(FromMailAccount + NoticeDomain, FromMailName, System.Text.Encoding.UTF8);

                    msg.Subject = "員工自評完成通知主管";
                    msg.SubjectEncoding = Encoding.UTF8;
                    msg.Body = msg.Body = @"主管您好:<br/>您所屬同仁  " + item.EmployeeName + "  已完成考核作業,請進入系統進行複核作業。<br/>" +
                        "→點入績效管理系統作業<a href =" + NoticeUrl + "Manage/Index>" + item.ProjectName + "-" + item.EmployeeName + "</a>";

                    msg.BodyEncoding = Encoding.UTF8;
                    msg.IsBodyHtml = true;
                    msg.Priority = MailPriority.Low; //Mail priority
                    #endregion

                    #region SmtpClient (network)

                    SmtpClient smtpClient = new SmtpClient();
                    //smtpClient.Credentials = new System.Net.NetworkCredential("intranet@labpsc.com.tw", "lab123");
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                    smtpClient.Send(msg);

                    #endregion
                }
            }
            return RedirectToAction("Index");
        }
    }
}