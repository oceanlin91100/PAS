using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using UniPsg.Model.PAS.ViewModels;
using UniPsg.Web.UI.PAS.Models;
using PagedList;
using System.Net.Mail;
using System.Text;
using System.DirectoryServices;

namespace UniPsg.Web.UI.PAS.Controllers
{
    public class AssessProjectController : Controller
    {
        // 分頁後每頁顯示的筆數
        private const int PageSize = 100;
        private string LDAPPath = System.Web.Configuration.WebConfigurationManager.AppSettings["LDAPPath"];

        private AssessProjectClient clinet = new AssessProjectClient();
        private AssessGroupClient groupClient = new AssessGroupClient();
        private AssessFormClient formClient = new AssessFormClient();
        private AssessCategoryClient categoryClient = new AssessCategoryClient();
        private AssessPersonClient perClient = new AssessPersonClient();
        private OrganizationClient orgClient = new OrganizationClient();
        private RoleUserMappingClient roleUserClient = new RoleUserMappingClient();

        //email通知網域
        private string NoticeDomain = System.Web.Configuration.WebConfigurationManager.AppSettings["NoticeDomain"];
        private string FromMailAccount = System.Web.Configuration.WebConfigurationManager.AppSettings["FromMailAccount"];
        private string FromMailName = System.Web.Configuration.WebConfigurationManager.AppSettings["FromMailName"];
        private string NoticeUrl = System.Web.Configuration.WebConfigurationManager.AppSettings["NoticeUrl"];

        private List<SelectListItem> GroupSelectListItems(string selected = "")
        {
            var groups = groupClient.FindByStatus(0);
            var items = new List<SelectListItem>();
            var selectedGroups = string.IsNullOrWhiteSpace(selected) ? null : selected.Split(',');
            foreach (var g in groups)
            {
                items.Add(item: new SelectListItem()
                {
                    Value = g.Id.ToString(),
                    Text = g.Name,
                    Selected = selectedGroups == null ? false : selectedGroups.Contains(g.Id.ToString())
                });
            }
            return items;
        }

        private List<SelectListItem> FormSelectListItems(string selected = "")
        {
            var forms = formClient.FindByStatus(0);
            var items = new List<SelectListItem>();
            var selectedForms = string.IsNullOrWhiteSpace(selected) ? null : selected.Split(',');
            foreach (var g in forms)
            {
                items.Add(item: new SelectListItem()
                {
                    Value = g.Id.ToString(),
                    Text = g.Name,
                    Selected = selectedForms == null ? false : selectedForms.Contains(g.Id.ToString())
                });
            }
            return items;
        }

        // GET: AssessProject 
        public ActionResult Index(string sortOrder, int? page)
        {
            var models = clinet.FindAll();

            // 1.取得員編
            string account = User.Identity.Name.ToString();
            //string account = @"PSCNET\AMY-LIN";
            account = account.Replace(@"labpsc\", "");
            account = account.Replace(@"LABPSC\", "");
            account = account.Replace(@"pscnet\", "");
            account = account.Replace(@"PSCNET\", "");
            //var employeeNo = orgClient.FindEmployeeNumber(account, 0);
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
            //2.取得角色
            var role = roleUserClient.FindRole(employeeNo);
            ViewBag.Role = role;

            AssessProjectIndexView viewModel = new AssessProjectIndexView();

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            switch (sortOrder)
            {
                case "name_desc":
                    models = models.OrderByDescending(m => m.Name);
                    break;
                case "Date":
                    models = models.OrderBy(m => m.ModifiedDate);
                    break;
                case "date_desc":
                    models = models.OrderByDescending(m => m.ModifiedDate);
                    break;
                default:
                    models = models.OrderBy(m => m.Id);
                    break;
            }

            var groups = this.GroupSelectListItems();
            var forms = this.FormSelectListItems();
            var result = new List<AssessProjectViewModel>();

            foreach (var item in models)
            {
                var selectedList = item.Groups.Split(',').ToList();
                item.Groups = string.IsNullOrWhiteSpace(item.Groups) ? "" : string.Join(",", groups.Where(x => selectedList.Contains(x.Value)).Select(x => x.Text));

                var selectedList1 = item.Forms.Split(',').ToList();
                item.Forms = string.IsNullOrWhiteSpace(item.Forms) ? "" : string.Join(",", forms.Where(x => selectedList1.Contains(x.Value)).Select(x => x.Text));

                result.Add(item);
            }

            int pageIndex = (page ?? 1);
            viewModel.Projects = models.ToPagedList(pageIndex, PageSize);
            return View(viewModel);
        }

        // GET: AssessProject/Create
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Categories = categoryClient.FindByStatus(0);

            var groups = this.GroupSelectListItems();
            ViewBag.GroupItems = groups;

            var forms = this.FormSelectListItems();
            ViewBag.FormItems = forms;

            return View("Create");
        }

        // POST: AssessProject/Create    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AssessProjectViewModel models, string[] groups, string[] forms)
        {
            models.Groups = string.Join(",", groups);
            models.Forms = string.Join(",", forms);
            clinet.Create(models);

            return RedirectToAction("Index");
        }

        // GET: AssessProject/Edit/5  
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            AssessProjectViewModel models = new AssessProjectViewModel();
            models = clinet.Find(id.Value);
            if (models == null)
                return HttpNotFound();

            var groups = this.GroupSelectListItems(models.Groups);
            ViewBag.GroupItems = groups;

            var forms = this.FormSelectListItems(models.Forms);
            ViewBag.FormItems = forms;

            ViewBag.Categories = categoryClient.FindByStatus(0);

            return View("Edit", models);
        }

        // GET: AssessProject/Edit/5    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AssessProjectViewModel models, string[] groups, string[] forms)
        {
            models.Groups = string.Join(",", groups);
            models.Forms = string.Join(",", forms);
            clinet.Edit(models);
            return RedirectToAction("Index");
        }

        // GET: AssessProject/Delete/5    
        public ActionResult Delete(int id)
        {
            clinet.Delete(id);
            return RedirectToAction("Index");
        }


        // GET: AssessProject/Detail/5  
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Detail(int id)
        {
            AssessProjectViewModel models = new AssessProjectViewModel();
            models = clinet.Find(id);

            var groups = GroupSelectListItems();
            var selectedList = models.Groups.Split(',').ToList();
            models.Groups = string.IsNullOrWhiteSpace(models.Groups) ? "" : string.Join(",", groups.Where(x => selectedList.Contains(x.Value)).Select(x => x.Text));

            var forms = FormSelectListItems();
            var selectedList1 = models.Forms.Split(',').ToList();
            models.Forms = string.IsNullOrWhiteSpace(models.Forms) ? "" : string.Join(",", forms.Where(x => selectedList1.Contains(x.Value)).Select(x => x.Text));

            return View("Detail", models);
        }

        // GET: AssessProject/Closed/5  
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Closed(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            AssessProjectViewModel models = new AssessProjectViewModel();
            models = clinet.Find(id.Value);
            if (models == null)
                return HttpNotFound();
            ViewBag.ProjectName = models.Name;
            ViewBag.Suject = models.Name + "考核作業完成結案通知";
            ViewBag.Body = @"各位同仁大家好：<br/>" + models.Name + "考核作業已完成,請逕行進入系統查個個人考核結果。<br/>" +
                            "→點入績效管理系統作業<a href =" + NoticeUrl + "Performance/Index>" + models.Name + " - 績效查詢</a>";

            return View("Closed", models);
        }

        // GET: AssessProject/Closed/5    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Closed(AssessProjectViewModel models, string subject, string projectName)
        {
            int projectId = models.Id;
            int projectStatus = models.Status;
            var statusCode = clinet.Edit(models);
            if (statusCode)
            {
                if (projectStatus == 2)
                {
                    var model = perClient.FindByProjectId(projectId);
                    model = model.Where(m => m.Reviewer == "00000");

                    foreach (var item in model)
                    {
                        //var org = orgClient.Find(item.ProjectId, item.EmployeeNo);
                        string Account = string.Empty;
                        DirectoryEntry searchRoot = new DirectoryEntry(LDAPPath);
                        DirectorySearcher search = new DirectorySearcher(searchRoot);
                        search.Filter = "(&(objectClass=user)(description=" + item.EmployeeNo + "))";
                        SearchResult results = search.FindOne();
                        if (results != null)
                        {
                            ResultPropertyCollection resultPropColl;
                            resultPropColl = results.Properties;
                            Account = (string)resultPropColl["SAMAccountName"][0];
                        }

                        if (Account != null)
                        {
                            #region 建立MailMessage物件

                            MailMessage msg = new MailMessage();
                            msg.To.Add(Account + NoticeDomain);
                            msg.From = new MailAddress(FromMailAccount + NoticeDomain, FromMailName, System.Text.Encoding.UTF8);

                            msg.Subject = subject;
                            msg.SubjectEncoding = Encoding.UTF8;
                            msg.Body = @"各位同仁大家好：<br/>" + projectName + "考核作業已完成,請逕行進入系統查個個人考核結果。<br/>" +
                                "→點入績效管理系統作業<a href =" + NoticeUrl + "Performance/Index>" + projectName + " - 績效查詢 - " + item.EmployeeName + "</a>";


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
            }
            return RedirectToAction("Index");
        }

        // GET: AssessProject/Opened/5  
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Opened(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            AssessProjectViewModel models = new AssessProjectViewModel();
            models = clinet.Find(id.Value);
            if (models == null)
                return HttpNotFound();
            ViewBag.ProjectName = models.Name;
            ViewBag.Suject = models.Name + "考核作業啟動通知";
            ViewBag.Body = @"1.	公司" + models.Name + "考核作業自西元" + models.FromDate.ToString().Substring(0, 4) + "年" + models.FromDate.ToString().Substring(4, 2) + "月" + models.FromDate.ToString().Substring(6, 2) + "日起開放作業," +
                                "請進入績效管理系統進行,並請於西元" + models.Deadline.ToString().Substring(0, 4) + "年" + models.Deadline.ToString().Substring(4, 2) + "月" + models.Deadline.ToString().Substring(6, 2) + "日前完成自評。<br/>" +
                                "2.	若系統操作業有問題請洽人力資源處。<br/>" +
                                "→點入績效管理系統作業<a href =" + NoticeUrl + "Review/Index>" + models.Name + " - 員工自評</a>";
            return View("Opened", models);
        }

        // GET: AssessProject/Opened/5    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Opened(AssessProjectViewModel models, string subject, string projectName)
        {
            int projectStatus = models.Status;
            if (projectStatus == 0)
            {
                int projectId = models.Id;
                string fromDate = models.FromDate.ToString().Substring(0, 4) + "年" + models.FromDate.ToString().Substring(4, 2) + "月" + models.FromDate.ToString().Substring(6, 2) + "日";
                string deadline = models.Deadline.ToString().Substring(0, 4) + "年" + models.Deadline.ToString().Substring(4, 2) + "月" + models.Deadline.ToString().Substring(6, 2) + "日";
                var model = perClient.FindByProjectId(projectId);
                model = model.Where(m => m.Reviewer != "00000" && m.Status == 0);
                foreach (var item in model)
                {
                    //var org = orgClient.Find(item.ProjectId, item.EmployeeNo);
                    string Account = string.Empty;
                    DirectoryEntry searchRoot = new DirectoryEntry(LDAPPath);
                    DirectorySearcher search = new DirectorySearcher(searchRoot);
                    search.Filter = "(&(objectClass=user)(description=" + item.EmployeeNo + "))";
                    SearchResult results = search.FindOne();
                    if (results != null)
                    {
                        ResultPropertyCollection resultPropColl;
                        resultPropColl = results.Properties;
                        Account = (string)resultPropColl["SAMAccountName"][0];
                    }
                    if (Account != string.Empty)
                    {
                        #region 建立MailMessage物件

                        MailMessage msg = new MailMessage();
                        msg.To.Add(Account + NoticeDomain);
                        msg.From = new MailAddress(FromMailAccount + NoticeDomain, FromMailName, System.Text.Encoding.UTF8);

                        msg.Subject = subject;
                        msg.SubjectEncoding = Encoding.UTF8;
                        msg.Body = @"1.	公司" + projectName + "考核作業自西元" + fromDate + "起開放作業," +
                            "請進入績效管理系統進行,並請於西元" + deadline + "前完成自評。<br/>" +
                            "2.	若系統操作業有問題請洽人力資源處。<br/>" +
                            "→點入績效管理系統作業<a href =" + NoticeUrl + "Review/Index>" + subject + " - 員工自評 - " + item.EmployeeName + "</a>";

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

    }
}