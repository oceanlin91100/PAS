using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using UniPsg.Model.PAS.ViewModels;
using UniPsg.Web.UI.PAS.Models;
using PagedList;
using UniPsg.Model.Schedule;
using System.Text;
using System.Net.Mail;
using System.DirectoryServices;

namespace UniPsg.Web.UI.PAS.Controllers
{
    public class ManageController : Controller
    {

        // 分頁後每頁顯示的筆數
        private const int PageSize = 100;
        private string LDAPPath = System.Web.Configuration.WebConfigurationManager.AppSettings["LDAPPath"];

        //email通知網域
        private string NoticeDomain = System.Web.Configuration.WebConfigurationManager.AppSettings["NoticeDomain"];
        private string FromMailAccount = System.Web.Configuration.WebConfigurationManager.AppSettings["FromMailAccount"];
        private string FromMailName = System.Web.Configuration.WebConfigurationManager.AppSettings["FromMailName"];
        private string NoticeUrl = System.Web.Configuration.WebConfigurationManager.AppSettings["NoticeUrl"];

        private AssessPersonClient client = new AssessPersonClient();
        private ManagerClient mangeClient = new ManagerClient();
        private AssessFormClient formClient = new AssessFormClient();
        private ScopeItemClient itemeClient = new ScopeItemClient();
        private OrganizationClient orgClient = new OrganizationClient();
        private ProjectReviewClient reviewClinet = new ProjectReviewClient();
        private MailClient mailClinet = new MailClient();
        private ProjectScoreClient scoreClient = new ProjectScoreClient();

        // GET: Manage
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Index(string sortOrder, int? page)
        {
            string account = User.Identity.Name.ToString();
            //string account = @"PSCNET\GINOCHEN";
            //string account = @"PSCNET\KEVINCHUANG";
            //string account = @"PSCNET\LIN58515";
            //string account = @"PSCNET\BRIGHT";
            //string account = @"PSCNET\KAVIN";
            //string account = @"PSCNET\JAMES";
            account = account.Replace(@"labpsc\", "");
            account = account.Replace(@"LABPSC\", "");
            account = account.Replace(@"pscnet\", "");
            account = account.Replace(@"PSCNET\", "");
            string employeeNo = string.Empty;            
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
            var model = client.FindByReviewer(employeeNo,1);
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

        // GET: Manage
        public ActionResult Update1(int projectId, string employeeNo)
        {            
            var model = mangeClient.FindReviewer(projectId, employeeNo);

            if (model.GroupId == 6)
            {
                ManageUpdateIndexView mv = new ManageUpdateIndexView();
                mv.BranchCode = model.BranchCode;
                mv.BranchName = model.BranchName;
                mv.CategoryId = model.CategoryId;
                mv.DeptCode = model.DeptCode;
                mv.DeptName = model.DeptName;
                mv.Education = model.Education;
                mv.EmployeeName = model.EmployeeName;
                mv.EmployeeNo = model.EmployeeNo;
                mv.FormId = model.FormId;
                mv.FormName = model.FormName;
                mv.GroupId = model.GroupId;
                mv.GroupName = model.GroupName;
                mv.JobCapName = model.JobCapName;
                mv.ProjectId = model.ProjectId;
                mv.ProjectName = model.ProjectName;
                mv.Reviewer = model.Reviewer;
                mv.StartDate = model.StartDate;
                mv.TeamCode = model.TeamCode;
                mv.TeamName = model.TeamName;
                mv.ViewStarDate = model.ViewStarDate;
                mv.ViewEndDate = model.ViewEndDate;
                if (model.Reviews.Count != 0)
                {
                    mv.Reviews1 = model.Reviews.FindAll(r => r.KPICategoryId == 11).OrderBy(r => r.CreatedDate).ToList(); 
                    mv.Reviews2 = model.Reviews.FindAll(r => r.KPICategoryId == 9).OrderBy(r => r.CreatedDate).ToList();
                    mv.Reviews3 = model.Reviews.FindAll(r => r.KPICategoryId == 12).OrderBy(r => r.CreatedDate).ToList();
                    mv.Reviews4 = model.Reviews.FindAll(r => r.KPICategoryId == 13).OrderBy(r => r.CreatedDate).ToList();
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

                ProjectScoreViewModel score = model.Scores.Find(s => s.Reviewer == model.Reviewer);
                ViewBag.Order = score.OderSerial;
                ViewBag.MaxValue = model.Scores.Max(s => s.OderSerial);
                mv.Scores = model.Scores.FindAll(s => s.OderSerial <= ViewBag.Order).OrderByDescending(s => s.OderSerial).ToList();

                ViewBag.Review1Rows = mv.Reviews1.Count();
                ViewBag.Review2Rows = mv.Reviews2.Count();
                ViewBag.Review3Rows = mv.Reviews3.Count();
                ViewBag.Review4Rows = mv.Reviews4.Count();
                ViewBag.ScoreRows = mv.Scores.Count();

                return View("Update16", mv);
            }
            else
            {
                ProjectScoreViewModel score = model.Scores.Find(s => s.Reviewer == model.Reviewer);
                ViewBag.Order = score.OderSerial;
                ViewBag.MaxValue = model.Scores.Max(s => s.OderSerial);
                model.Scores = model.Scores.FindAll(s => s.OderSerial <= ViewBag.Order).OrderByDescending(s => s.OderSerial).ToList();

                ViewBag.ReviewRows = model.Reviews.Count();
                ViewBag.ScoreRows = model.Scores.Count();
            }
            model.Reviews = model.Reviews.OrderBy(r => r.CreatedDate).ToList();
            if (model.GroupId == 4)
                return View("Update14", model);            
                
            if (model.GroupId == 7)
                return View("Update17", model);

            return View("Update1", model);
        }

        // POST: Manage/Create    
        [HttpPost]
        public ActionResult Update17(ManageUpdateViewModel models, string save, string reviw, string back, string approve)
        {
            string reviewer = models.Reviewer;
            string employee = models.EmployeeNo;
            int projectId = models.ProjectId;

            if (!string.IsNullOrEmpty(save))
                reviewClinet.Update(models, "save", "1");
            if (!string.IsNullOrEmpty(reviw))
            {   
                var statusCode = reviewClinet.Update(models, "reviw", "1");
                if (statusCode)
                {
                    var item = client.Find(projectId, employee);
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
                        
                        msg.Subject = item.ProjectName + "主管複評完成通知";
                        msg.SubjectEncoding = Encoding.UTF8;
                        msg.Body = msg.Body = @"主管您好:<br/>您所屬同仁  " + item.EmployeeName + "  已完成考核作業, 請進入系統進行考核核定作業。<br/>" +   
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
            if (!string.IsNullOrEmpty(back))
            {
               var statusCode = client.Update(models, "back", User.Identity.Name.ToString(), "1");
                if (statusCode)
                {
                    var item = client.Find(projectId, employee);
                    //var nextReviewerAccount = orgClient.GetBackAccount(employee, projectId);
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

                        msg.Subject = item.ProjectName + "主管複評退件通知";
                        msg.SubjectEncoding = Encoding.UTF8;
                        if (item.Reviewer == item.EmployeeNo)
                        {
                            msg.Body = msg.Body = @"您好:<br/>" + item.EmployeeName + "  考核系統已退件,請與您的主管進行溝通並進入系統完成考核作業。<br/>" +
                               "→點入績效管理系統作業<a href =" + NoticeUrl + "Review/Index>" + item.ProjectName + "-" + item.EmployeeName + "</a>";
                        }
                        else
                        {
                            msg.Body = msg.Body = @"您好:<br/>" + item.EmployeeName + "  考核系統已退件,請與您的主管進行溝通並進入系統完成考核作業。<br/>" +
                                "→點入績效管理系統作業<a href =" + NoticeUrl + "Manage/Index>" + item.ProjectName + "-" + item.EmployeeName + "</a>";
                        }
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
            if (!string.IsNullOrEmpty(approve))
                reviewClinet.Update(models, "approve", "1");

            return RedirectToAction("Index");
        }

        // POST: Manage/Create    
        [HttpPost]
        public ActionResult Update16(ManageUpdateIndexView models, string save, string reviw,string back, string approve)
        {
            ManageUpdateViewModel mv = new ManageUpdateViewModel();

            mv.BranchCode = models.BranchCode;
            mv.BranchName = models.BranchName;
            mv.CategoryId = models.CategoryId;
            mv.DeptCode = models.DeptCode;
            mv.DeptName = models.DeptName;
            mv.Education = models.Education;
            mv.EmployeeName = models.EmployeeName;
            mv.EmployeeNo = models.EmployeeNo;
            mv.FormId = models.FormId;
            mv.FormName = models.FormName;
            mv.GroupId = models.GroupId;
            mv.GroupName = models.GroupName;
            mv.JobCapName = models.JobCapName;
            mv.ProjectId = models.ProjectId;
            mv.ProjectName = models.ProjectName;
            mv.Reviewer = models.Reviewer;
            mv.StartDate = models.StartDate;
            mv.TeamCode = models.TeamCode;
            mv.TeamName = models.TeamName;
            mv.ViewStarDate = models.ViewStarDate;
            mv.ViewEndDate = models.ViewEndDate;

            mv.Scores = models.Scores;

            List<ProjectReviewViewModel> reviews = new List<ProjectReviewViewModel>();

            if (models.Reviews1 != null)
            {
                foreach (ProjectReviewViewModel pv in models.Reviews1)
                {
                    ProjectReviewViewModel pr = new ProjectReviewViewModel();
                    pr.Id = pv.Id;
                    pr.ProjectId = pv.ProjectId;
                    pr.EmployeeNo = pv.EmployeeNo;
                    pr.Reviewer = pv.Reviewer;
                    pr.ScopeId = pv.ScopeId;
                    pr.ItemId = pv.ItemId;
                    pr.ItemName = pv.ItemName;
                    pr.KPICategoryId = pv.KPICategoryId;
                    pr.KPICategoryName = pv.KPICategoryName;
                    pr.Weight = pv.Weight;
                    pr.Rate = pv.Rate;
                    pr.Score = pv.Score;
                    pr.Score1 = pv.Score1;
                    pr.Comment = pv.Comment;
                    pr.Comment1 = pv.Comment1;
                    pr.Comment2 = pv.Comment2;
                    pr.Status = pv.Status;
                    pr.Creator = pv.Creator;
                    pr.Modifier = pv.Modifier;
                    pr.ModifiedDate = System.DateTime.Now.ToString("yyyyMMddHHmmss");
                    reviews.Add(pr);
                }
            }

            if (models.Reviews2 != null)
            {
                foreach (ProjectReviewViewModel pv in models.Reviews2)
                {
                    ProjectReviewViewModel pr = new ProjectReviewViewModel();
                    pr.Id = pv.Id;
                    pr.ProjectId = pv.ProjectId;
                    pr.EmployeeNo = pv.EmployeeNo;
                    pr.Reviewer = pv.Reviewer;
                    pr.ScopeId = pv.ScopeId;
                    pr.ItemId = pv.ItemId;
                    pr.ItemName = pv.ItemName;
                    pr.KPICategoryId = pv.KPICategoryId;
                    pr.KPICategoryName = pv.KPICategoryName;
                    pr.Weight = pv.Weight;
                    pr.Rate = pv.Rate;
                    pr.Score = pv.Score;
                    pr.Score1 = pv.Score1;
                    pr.Comment = pv.Comment;
                    pr.Comment1 = pv.Comment1;
                    pr.Comment2 = pv.Comment2;
                    pr.Status = pv.Status;
                    pr.Creator = pv.Creator;
                    pr.Modifier = pv.Modifier;
                    pr.ModifiedDate = System.DateTime.Now.ToString("yyyyMMddHHmmss");
                    reviews.Add(pr);
                }

            }
            if (models.Reviews3 != null)
            {
                foreach (ProjectReviewViewModel pv in models.Reviews3)
                {
                    ProjectReviewViewModel pr = new ProjectReviewViewModel();
                    pr.Id = pv.Id;
                    pr.ProjectId = pv.ProjectId;
                    pr.EmployeeNo = pv.EmployeeNo;
                    pr.Reviewer = pv.Reviewer;
                    pr.ScopeId = pv.ScopeId;
                    pr.ItemId = pv.ItemId;
                    pr.ItemName = pv.ItemName;
                    pr.KPICategoryId = pv.KPICategoryId;
                    pr.KPICategoryName = pv.KPICategoryName;
                    pr.Weight = pv.Weight;
                    pr.Rate = pv.Rate;
                    pr.Score = pv.Score;
                    pr.Score1 = pv.Score1;
                    pr.Comment = pv.Comment;
                    pr.Comment1 = pv.Comment1;
                    pr.Comment2 = pv.Comment2;
                    pr.Status = pv.Status;
                    pr.Creator = pv.Creator;
                    pr.Modifier = pv.Modifier;
                    pr.ModifiedDate = System.DateTime.Now.ToString("yyyyMMddHHmmss");
                    reviews.Add(pr);
                }

            }
            if (models.Reviews4 != null)
            {
                foreach (ProjectReviewViewModel pv in models.Reviews4)
                {
                    ProjectReviewViewModel pr = new ProjectReviewViewModel();
                    pr.Id = pv.Id;
                    pr.ProjectId = pv.ProjectId;
                    pr.EmployeeNo = pv.EmployeeNo;
                    pr.Reviewer = pv.Reviewer;
                    pr.ScopeId = pv.ScopeId;
                    pr.ItemId = pv.ItemId;
                    pr.ItemName = pv.ItemName;
                    pr.KPICategoryId = pv.KPICategoryId;
                    pr.KPICategoryName = pv.KPICategoryName;
                    pr.Weight = pv.Weight;
                    pr.Rate = pv.Rate;
                    pr.Score = pv.Score;
                    pr.Score1 = pv.Score1;
                    pr.Comment = pv.Comment;
                    pr.Comment1 = pv.Comment1;
                    pr.Comment2 = pv.Comment2;
                    pr.Status = pv.Status;
                    pr.Creator = pv.Creator;
                    pr.Modifier = pv.Modifier;
                    pr.ModifiedDate = System.DateTime.Now.ToString("yyyyMMddHHmmss");
                    reviews.Add(pr);
                }
            }
            mv.Reviews = reviews;
            string reviewer = models.Reviewer;
            int projectId = models.ProjectId;
            string employee = models.EmployeeNo;

            if (!string.IsNullOrEmpty(save))
                reviewClinet.Update(mv, "save", "1");
           
            if (!string.IsNullOrEmpty(reviw))
            {
                var statusCode = reviewClinet.Update(mv, "reviw", "1");
                if (statusCode)
                {
                    var item = client.Find(projectId, employee);
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

                        msg.Subject = item.ProjectName + "主管複評完成通知";
                        msg.SubjectEncoding = Encoding.UTF8;
                        msg.Body = msg.Body = @"主管您好:<br/>您所屬同仁  " + item.EmployeeName + "  已完成考核作業, 請進入系統進行考核核定作業。<br/>" +
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
            if (!string.IsNullOrEmpty(back))
            {
               var statusCode = client.Update(mv, "back", User.Identity.Name.ToString(), "1");
                if (statusCode)
                {
                    var item = client.Find(projectId, employee);
                    //var nextReviewerAccount = orgClient.GetBackAccount(employee, projectId);
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

                        msg.Subject = item.ProjectName + "主管複評退件通知";
                        msg.SubjectEncoding = Encoding.UTF8;
                        if (item.Reviewer == item.EmployeeNo)
                        {
                            msg.Body = msg.Body = @"您好:<br/>" + item.EmployeeName + "  考核系統已退件,請與您的主管進行溝通並進入系統完成考核作業。<br/>" +
                               "→點入績效管理系統作業<a href =" + NoticeUrl + "Review/Index>" + item.ProjectName + "-" + item.EmployeeName + "</a>";
                        }
                        else
                        {
                            msg.Body = msg.Body = @"您好:<br/>" + item.EmployeeName + "  考核系統已退件,請與您的主管進行溝通並進入系統完成考核作業。<br/>" +
                                "→點入績效管理系統作業<a href =" + NoticeUrl + "Manage/Index>" + item.ProjectName + "-" + item.EmployeeName + "</a>";
                        }

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
            if (!string.IsNullOrEmpty(approve))
                reviewClinet.Update(mv, "approve", "1");

            return RedirectToAction("Index");
        }

        // POST: Manage/Create    
        [HttpPost]
        public ActionResult Update14(ManageUpdateViewModel models, string save, string reviw, string back, string approve)
        {
            string reviewer = models.Reviewer;
            int projectId = models.ProjectId;
            string employee = models.EmployeeNo;

            if (!string.IsNullOrEmpty(save))
                reviewClinet.Update(models, "save", "1");
            if (!string.IsNullOrEmpty(reviw))
            {
                var statusCode =reviewClinet.Update(models, "reviw", "1");
                if (statusCode)
                {
                    var item = client.Find(projectId, employee);
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

                        msg.Subject = item.ProjectName + "主管複評完成通知";
                        msg.SubjectEncoding = Encoding.UTF8;
                        msg.Body = msg.Body = @"主管您好:<br/>您所屬同仁  " + item.EmployeeName + "  已完成考核作業, 請進入系統進行考核核定作業。<br/>" +
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
            if (!string.IsNullOrEmpty(back))
            {
                var statusCode = client.Update(models, "back", User.Identity.Name.ToString(), "1");
                if (statusCode)
                {
                    var item = client.Find(projectId, employee);
                    //var nextReviewerAccount = orgClient.GetBackAccount(employee, projectId);
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

                        msg.Subject = item.ProjectName + "主管複評退件通知";
                        msg.SubjectEncoding = Encoding.UTF8;
                        if (item.Reviewer == item.EmployeeNo)
                        {
                            msg.Body = msg.Body = @"您好:<br/>" + item.EmployeeName + "  考核系統已退件,請與您的主管進行溝通並進入系統完成考核作業。<br/>" +
                               "→點入績效管理系統作業<a href =" + NoticeUrl + "Review/Index>" + item.ProjectName + "-" + item.EmployeeName + "</a>";
                        }
                        else
                        {
                            msg.Body = msg.Body = @"您好:<br/>" + item.EmployeeName + "  考核系統已退件,請與您的主管進行溝通並進入系統完成考核作業。<br/>" +
                                "→點入績效管理系統作業<a href =" + NoticeUrl + "Manage/Index>" + item.ProjectName + "-" + item.EmployeeName + "</a>";
                        }

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
            if (!string.IsNullOrEmpty(approve))
                reviewClinet.Update(models, "approve", "1");

            return RedirectToAction("Index");
        }
    }
}