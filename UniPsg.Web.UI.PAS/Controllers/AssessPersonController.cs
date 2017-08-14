using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using UniPsg.Model.PAS.ViewModels;
using UniPsg.Web.UI.PAS.Models;
using PagedList;
using System.IO;
using System.Data;
using ClosedXML.Excel;
using UniPsg.Web.UI.PAS;
using System.Net.Mail;
using System.Text;
using System.DirectoryServices;

namespace UniPsg.Web.UI.PAS.Controllers
{
    public class AssessPersonController : Controller
    {
        // 分頁後每頁顯示的筆數
        private const int PageSize = 100;
        private string LDAPPath = System.Web.Configuration.WebConfigurationManager.AppSettings["LDAPPath"];

        private AssessPersonClient client = new AssessPersonClient();
        private AssessGroupClient groupClient = new AssessGroupClient();
        private AssessProjectClient projectClient = new AssessProjectClient();
        private AssessFormClient formClient = new AssessFormClient();
        private OrganizationClient orgClient = new OrganizationClient();
        private RoleUserMappingClient roleUserClient = new RoleUserMappingClient();
        private RoleClient roleClient = new RoleClient();

        //email通知網域
        private string NoticeDomain = System.Web.Configuration.WebConfigurationManager.AppSettings["NoticeDomain"];

        // GET: AssessPerson        
        //public ActionResult Index(int projectId,string sortOrder, string searchString, int? page)
        public ActionResult Index(int? projectId, string branchCode, string deptCode, string sortOrder, int? page)
        {
            var model = client.FindAll();
            ViewBag.Projects = projectClient.FindAll();
            ViewBag.Branches = orgClient.FindBraches();
            ViewBag.Depts = orgClient.FindDepartments();

            AssessPersonIndexView viewModel = new AssessPersonIndexView();

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.BrachSortParm = sortOrder == "Brach" ? "brach_desc" : "Brach";
            ViewBag.DeptSortParm = sortOrder == "Dept" ? "dept_desc" : "Dept";
            ViewBag.TeamSortParm = sortOrder == "Team" ? "team_desc" : "Team";
            ViewBag.GroupSortParm = sortOrder == "Group" ? "group_desc" : "Group";

            if (projectId != null)
            {
                model = model.Where(p => p.ProjectId == projectId);
                ViewBag.CurrentProjectId = projectId;
            }
            if ((!string.IsNullOrWhiteSpace(branchCode)))
            {
                model = model.Where(p => p.BranchCode == branchCode);
                ViewBag.CurrentBranchCode = branchCode;
            }

            if ((!string.IsNullOrWhiteSpace(deptCode)))
            {
                model = model.Where(p => p.DeptCode == deptCode);
                ViewBag.CurrentDeptCode = deptCode;
            }
            switch (sortOrder)
            {
                case "name_desc":
                    model = model.OrderByDescending(m => m.FormName);
                    break;
                case "Date":
                    model = model.OrderBy(m => m.ModifiedDate);
                    break;
                case "date_desc":
                    model = model.OrderByDescending(m => m.ModifiedDate);
                    break;
                case "Brach":
                    model = model.OrderBy(m => m.BranchName);
                    break;
                case "brach_desc":
                    model = model.OrderByDescending(m => m.BranchName);
                    break;
                case "Dept":
                    model = model.OrderBy(m => m.DeptCode);
                    break;
                case "dept_desc":
                    model = model.OrderByDescending(m => m.DeptCode);
                    break;
                case "Team":
                    model = model.OrderBy(m => m.TeamCode);
                    break;
                case "team_desc":
                    model = model.OrderByDescending(m => m.TeamCode);
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

            viewModel.People = model.ToPagedList(pageIndex, PageSize);

            return View(viewModel);
        }


        [HttpPost]
        public ActionResult Index(AssessPersonIndexView vm, string sortOrder, int? page)
        {
            var model = client.FindAll();
            ViewBag.Projects = projectClient.FindAll();
            ViewBag.Branches = orgClient.FindBraches();
            ViewBag.Depts = orgClient.FindDepartments();

            AssessPersonIndexView viewModel = new AssessPersonIndexView();

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.BrachSortParm = sortOrder == "Brach" ? "brach_desc" : "Brach";           
            ViewBag.DeptSortParm = sortOrder == "Dept" ? "dept_desc" : "Dept";
            ViewBag.TeamSortParm = sortOrder == "Team" ? "team_desc" : "Team";
            ViewBag.GroupSortParm = sortOrder == "Group" ? "group_desc" : "Group";

            if (vm.ProjectId > 0)
            {
                model = model.Where(p => p.ProjectId == vm.ProjectId);
                ViewBag.CurrentProjectId = vm.ProjectId;
            }
            if ((!string.IsNullOrWhiteSpace(vm.BranchCode)))
            {
                model = model.Where(p => p.BranchCode == vm.BranchCode);
                ViewBag.CurrentBranchCode = vm.BranchCode;
            }

            if ((!string.IsNullOrWhiteSpace(vm.DeptCode)))
            {
                model = model.Where(p => p.DeptCode == vm.DeptCode);
                ViewBag.CurrentDeptCode = vm.DeptCode;
            }

            switch (sortOrder)
            {
                case "name_desc":
                    model = model.OrderByDescending(m => m.FormName);
                    break;
                case "Date":
                    model = model.OrderBy(m => m.ModifiedDate);
                    break;
                case "date_desc":
                    model = model.OrderByDescending(m => m.ModifiedDate);
                    break;
                case "Brach":
                    model = model.OrderBy(m => m.BranchName);
                    break;
                case "brach_desc":
                    model = model.OrderByDescending(m => m.BranchName);
                    break;
                case "Dept":
                    model = model.OrderBy(m => m.DeptCode);
                    break;
                case "dept_desc":
                    model = model.OrderByDescending(m => m.DeptCode);
                    break;
                case "Team":
                    model = model.OrderBy(m => m.TeamCode);
                    break;
                case "team_desc":
                    model = model.OrderByDescending(m => m.TeamCode);
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

            viewModel.People = model.ToPagedList(pageIndex, PageSize);

            return View(viewModel);
        }

        // GET: AssessPerson/Create
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Projects = projectClient.FindByStatus(0);
            ViewBag.Forms = formClient.FindByStatus(0);
            ViewBag.Groups = groupClient.FindByStatus(0);

            return View("Create");
        }

        // POST: AssessPerson/Create    
        [HttpPost]
        public ActionResult Create(AssessPersonViewModel models)
        {
            client.Create(models);
            return RedirectToAction("Index");
        }


        // GET: AssessPerson/Edit/5  
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Edit(int projectId, string employeeNo)
        {
            ViewBag.Projects = projectClient.FindByStatus(0);
            ViewBag.Forms = formClient.FindByStatus(0);
            ViewBag.Groups = groupClient.FindByStatus(0);

            AssessPersonViewModel models = new AssessPersonViewModel();
            models = client.Find(projectId, employeeNo);
            return View("Edit", models);
        }

        // GET: AssessPerson/Edit/5    
        [HttpPost]
        public ActionResult Edit(AssessPersonViewModel models)
        {
            client.Edit(models);
            return RedirectToAction("Index");
        }

        // GET: AssessPerson/Update/5  
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Update(int projectId, string employeeNo)
        {
            ViewBag.ProjectId = projectId;
           
            AssessPersonViewModel models = new AssessPersonViewModel();
            models = client.Find(projectId, employeeNo);
            return View("Update", models);
        }

        // GET: AssessPerson/Update/5    
        [HttpPost]
        public ActionResult Update(AssessPersonViewModel models)
        {
            int projectId = models.ProjectId;
            client.Edit(models);
            return RedirectToAction("List", "AssessPerson", new { projectId = projectId });
        }

        // GET: AssessPerson/Delete/5    
        public ActionResult Delete(int projectId, string employeeNo)
        {
            client.Delete(projectId, employeeNo);
            return RedirectToAction("Index");
        }

        // GET: AssessPerson/Detail/5  
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Detail(int projectId, string employeeNo)
        {
            AssessPersonViewModel models = new AssessPersonViewModel();
            models = client.Find(projectId, employeeNo);
            return View("Detail", models);
        }


        // GET: AssessPerson/Imports/5  
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Imports(int projectId)
        {
            var project = projectClient.Find(projectId);
            ViewBag.ProjectId = project.Id;
            ViewBag.ProjectName = project.Name;
            return View("Imports");
        }

        // GET: AssessPerson/Imports/5    
        [HttpPost]
        public ActionResult Imports(OrganizationImportModel model)
        {
            client.Imposts(model);
            return RedirectToAction("Index", "AssessProject");
        }


        // GET: AssessPerson/Export/5  
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Export(int projectId, string All)
        {
            ViewBag.Projects = projectClient.FindByStatus(0);
            ViewBag.Forms = formClient.FindByStatus(0);
            ViewBag.Groups = groupClient.FindByStatus(0);

            List<AssessPersonViewModel> model = new List<AssessPersonViewModel>();
            //2017/6/22玉卿說只要匯出要考核人員即可。
            model = client.FindByProjectId(projectId).Where(p => p.Status == 0).ToList();
            if (All != "Y")
            {
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
                if (role != "HRAdmin" && role != "SysAdmin")
                {
                    IEnumerable<AssessPersonViewModel> models = Enumerable.Empty<AssessPersonViewModel>();

                    var dutys = roleClient.FindHBPDUT(employeeNo);

                    foreach (var item in dutys)
                    {
                        var result = model.Where(m => m.BranchCode == item.BRHCOD && m.DeptCode == item.DEPTCD).AsEnumerable();
                        if (result != null)
                            models = models.Concat(result);
                    }
                    model = models.ToList();
                }
            }
            UniPsg.Web.UI.PAS.ListtoDataTableConverter converter = new UniPsg.Web.UI.PAS.ListtoDataTableConverter();

            DataTable dt = converter.ToDataTable(model);

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "AssessPerson");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=AssessPerson.xlsx");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
            return RedirectToAction("Index", "AssessProject");
        }

        // GET: Review
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Notice(int? projectId, string branchCode, string deptCode, string sortOrder, int? page)
        {
            if (projectId != null)
                ViewBag.CurrentProjectId = projectId;

            var model = client.FindByProjectId(Convert.ToInt32(projectId));
            model = model.Where(m => m.Reviewer != "00000");
            
            ViewBag.Branches = orgClient.FindBraches();
            ViewBag.Depts = orgClient.FindDepartments();

            // 1.取得員編
            string account = User.Identity.Name.ToString();
            //string account = @"PSCNET\WENDY";
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

            if (role != "HRAdmin" && role != "SysAdmin")
            {
                IEnumerable<AssessPersonViewModel> models = Enumerable.Empty<AssessPersonViewModel>();

                var dutys = roleClient.FindHBPDUT(employeeNo);

                foreach (var item in dutys)
                {
                    var result = model.Where(m => m.BranchCode == item.BRHCOD && m.DeptCode == item.DEPTCD).AsEnumerable();
                    if (result != null)
                        models = models.Concat(result);
                }
                model = models;
            }

            AssessPersonIndexView viewModel = new AssessPersonIndexView();

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.BrachSortParm = sortOrder == "Brach" ? "brach_desc" : "Brach";
            ViewBag.DeptSortParm = sortOrder == "Dept" ? "dept_desc" : "Dept";
            ViewBag.TeamSortParm = sortOrder == "Team" ? "team_desc" : "Dept";
            ViewBag.GroupSortParm = sortOrder == "Group" ? "group_desc" : "Group";

            //if (projectId != null)
            //{
            //    model = model.Where(p => p.ProjectId == projectId);
            //    ViewBag.CurrentProjectId = projectId;
            //}

            if ((!string.IsNullOrWhiteSpace(branchCode)))
            {
                model = model.Where(p => p.BranchCode == branchCode);
                ViewBag.CurrentBranchCode = branchCode;
            }

            if ((!string.IsNullOrWhiteSpace(deptCode)))
            {
                model = model.Where(p => p.DeptCode == deptCode);
                ViewBag.CurrentDeptCode = deptCode;
            }

            switch (sortOrder)
            {
                case "name_desc":
                    model = model.OrderByDescending(m => m.FormName);
                    break;
                case "Date":
                    model = model.OrderBy(m => m.ModifiedDate);
                    break;
                case "date_desc":
                    model = model.OrderByDescending(m => m.ModifiedDate);
                    break;
                case "Brach":
                    model = model.OrderBy(m => m.BranchName);
                    break;
                case "brach_desc":
                    model = model.OrderByDescending(m => m.BranchName);
                    break;
                case "Dept":
                    model = model.OrderBy(m => m.DeptCode);
                    break;
                case "dept_desc":
                    model = model.OrderByDescending(m => m.DeptCode);
                    break;
                case "Team":
                    model = model.OrderBy(m => m.TeamCode);
                    break;
                case "team_desc":
                    model = model.OrderByDescending(m => m.TeamCode);
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

            viewModel.People = model.ToPagedList(pageIndex, PageSize);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Notice(AssessPersonIndexView vm, string sortOrder, int? page)
        {
            if (vm.ProjectId != null)
                ViewBag.CurrentProjectId = vm.ProjectId;

            var model = client.FindByProjectId(Convert.ToInt32(vm.ProjectId));
            model = model.Where(m => m.Reviewer != "00000");

            ViewBag.Branches = orgClient.FindBraches();
            ViewBag.Depts = orgClient.FindDepartments();

            // 1.取得員編
            string account = User.Identity.Name.ToString();
            //string account = @"PSCNET\WENDY";
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

            if (role != "HRAdmin" && role != "SysAdmin")
            {
                IEnumerable<AssessPersonViewModel> models = Enumerable.Empty<AssessPersonViewModel>();

                var dutys = roleClient.FindHBPDUT(employeeNo);

                foreach (var item in dutys)
                {
                    var result = model.Where(m => m.BranchCode == item.BRHCOD && m.DeptCode == item.DEPTCD).AsEnumerable();
                    if (result != null)
                        models = models.Concat(result);
                }
                model = models;
            }

            AssessPersonIndexView viewModel = new AssessPersonIndexView();
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";            
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.BrachSortParm = sortOrder == "Brach" ? "brach_desc" : "Brach";
            ViewBag.DeptSortParm = sortOrder == "Dept" ? "dept_desc" : "Dept";
            ViewBag.TeamSortParm = sortOrder == "Team" ? "team_desc" : "Dept";
            ViewBag.GroupSortParm = sortOrder == "Group" ? "group_desc" : "Group";

            //if (vm.ProjectId > 0)
            //{
            //    model = model.Where(p => p.ProjectId == vm.ProjectId);
            //    ViewBag.CurrentProjectId = vm.ProjectId;
            //}
            if ((!string.IsNullOrWhiteSpace(vm.BranchCode)))
            {
                model = model.Where(p => p.BranchCode == vm.BranchCode);
                ViewBag.CurrentBranchCode = vm.BranchCode;
            }

            if ((!string.IsNullOrWhiteSpace(vm.DeptCode)))
            {
                model = model.Where(p => p.DeptCode == vm.DeptCode);
                ViewBag.CurrentDeptCode = vm.DeptCode;
            }

            switch (sortOrder)
            {
                case "name_desc":
                    model = model.OrderByDescending(m => m.FormName);
                    break;
                case "Date":
                    model = model.OrderBy(m => m.ModifiedDate);
                    break;
                case "date_desc":
                    model = model.OrderByDescending(m => m.ModifiedDate);
                    break;
                case "Brach":
                    model = model.OrderBy(m => m.BranchName);
                    break;
                case "brach_desc":
                    model = model.OrderByDescending(m => m.BranchName);
                    break;
                case "Dept":
                    model = model.OrderBy(m => m.DeptCode);
                    break;
                case "dept_desc":
                    model = model.OrderByDescending(m => m.DeptCode);
                    break;
                case "Team":
                    model = model.OrderBy(m => m.TeamCode);
                    break;
                case "team_desc":
                    model = model.OrderByDescending(m => m.TeamCode);
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
            viewModel.People = model.ToPagedList(pageIndex, PageSize);
            return View(viewModel);
        }


        // GET: AssessPerson/Send/5  
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Send(int projectId, string employeeNo)
        {

            if (employeeNo == "all")
            {
                var model = client.FindByProjectId(projectId);
                model = model.Where(m => m.Reviewer != "00000");

                // 1.取得員編
                string account = User.Identity.Name.ToString();
                //string account = @"PSCNET\AMY-LIN";
                account = account.Replace(@"labpsc\", "");
                account = account.Replace(@"LABPSC\", "");
                account = account.Replace(@"pscnet\", "");
                account = account.Replace(@"PSCNET\", "");
                //var duty = orgClient.FindEmployeeNumber(account, 0);
                string duty = string.Empty;
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
                var role = roleUserClient.FindRole(duty);

                if (role != "HRAdmin" && role != "SysAdmin")
                {
                    IEnumerable<AssessPersonViewModel> models = Enumerable.Empty<AssessPersonViewModel>();

                    var dutys = roleClient.FindHBPDUT(employeeNo);

                    foreach (var item in dutys)
                    {
                        var result = model.Where(m => m.BranchCode == item.BRHCOD && m.DeptCode == item.DEPTCD).AsEnumerable();
                        if (result != null)
                            models = models.Concat(result);
                    }
                    model = models;
                }

                foreach (var item in model)
                {
                    //var org = orgClient.Find(item.ProjectId, item.Reviewer);
                    string reviewAccount = string.Empty;
                    //var employeeNo = orgClient.FindEmployeeNumber(account, 0);
                    DirectoryEntry searchRoot1 = new DirectoryEntry(LDAPPath);
                    DirectorySearcher search1 = new DirectorySearcher(searchRoot1);
                    search1.Filter = "(&(objectClass=user)(description=" + item.Reviewer + "))";
                    SearchResult results1 = search1.FindOne();
                    if (results1 != null)
                    {
                        ResultPropertyCollection resultPropColl;
                        resultPropColl = results1.Properties;
                        reviewAccount = (string)resultPropColl["SAMAccountName"][0];
                    }
                    if (reviewAccount != string.Empty)
                    {
                        #region 建立MailMessage物件

                        string FromMailAccount = System.Web.Configuration.WebConfigurationManager.AppSettings["FromMailAccount"];
                        string FromMailName = System.Web.Configuration.WebConfigurationManager.AppSettings["FromMailName"];
                        string NoticeUrl = System.Web.Configuration.WebConfigurationManager.AppSettings["NoticeUrl"];

                        MailMessage msg = new MailMessage();
                        msg.To.Add(reviewAccount + NoticeDomain);
                        msg.From = new MailAddress(FromMailAccount + NoticeDomain, FromMailName, System.Text.Encoding.UTF8);
                        if (item.Reviewer == item.EmployeeNo)
                        {
                            msg.Subject = "考核催促通知：(自評)";
                            msg.SubjectEncoding = Encoding.UTF8;
                            msg.Body = @"您好," + item.ProjectName + "已於7月1日起開放作業,您尚未完成考核作業,請儘快進入系統完成考核。謝謝您的配合。<br/>" +
                                "→點入績效管理系統作業<a href =" + NoticeUrl + "Review/Index>" + item.ProjectName + "-" + item.EmployeeName + "</a>";
                        }
                        else
                        {
                            msg.Subject = "考核催促通知：(主管複評)";
                            msg.SubjectEncoding = Encoding.UTF8;
                            msg.Body = msg.Body = @"您好," + item.ProjectName + "考核作業,,您尚未完成考核作業,請儘快進入系統完成考核。謝謝您的配合。<br/>" +
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
            else
            {
                var item = client.Find(projectId, employeeNo);
                //var org = orgClient.Find(projectId, item.Reviewer);

                string reviewAccount = string.Empty;
                //var employeeNo = orgClient.FindEmployeeNumber(account, 0);
                DirectoryEntry searchRoot1 = new DirectoryEntry(LDAPPath);
                DirectorySearcher search1 = new DirectorySearcher(searchRoot1);
                search1.Filter = "(&(objectClass=user)(description=" + item.Reviewer + "))";
                SearchResult results1 = search1.FindOne();
                if (results1 != null)
                {
                    ResultPropertyCollection resultPropColl;
                    resultPropColl = results1.Properties;
                    reviewAccount = (string)resultPropColl["SAMAccountName"][0];
                }
                if (reviewAccount != string.Empty)
                { 
                    #region 建立MailMessage物件
                    string FromMailAccount = System.Web.Configuration.WebConfigurationManager.AppSettings["FromMailAccount"];
                    string FromMailName = System.Web.Configuration.WebConfigurationManager.AppSettings["FromMailName"];
                    string NoticeUrl = System.Web.Configuration.WebConfigurationManager.AppSettings["NoticeUrl"];

                    MailMessage msg = new MailMessage();
                    msg.To.Add(reviewAccount + NoticeDomain);
                    msg.From = new MailAddress(FromMailAccount + NoticeDomain, FromMailName, System.Text.Encoding.UTF8);
                    if (item.Reviewer == item.EmployeeNo)
                    {
                        msg.Subject = "考核催促通知：(自評)";
                        msg.SubjectEncoding = Encoding.UTF8;
                        msg.Body = @"您好," + item.ProjectName + "已於7月1日起開放作業,您尚未完成考核作業,請儘快進入系統完成考核。謝謝您的配合。<br/>" +
                            "→點入績效管理系統作業<a href =" + NoticeUrl + "Review/Index>" + item.ProjectName + "-" + item.EmployeeName + "</a>";
                    }
                    else
                    {
                        msg.Subject = "考核催促通知：(主管複評)";
                        msg.SubjectEncoding = Encoding.UTF8;
                        msg.Body = msg.Body = @"您好," + item.ProjectName + "考核作業,,您尚未完成考核作業,請儘快進入系統完成考核。謝謝您的配合。<br/>" +
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

            return RedirectToAction("Notice", "AssessPerson", new { projectId = projectId });
        }

        #region List考核人員檢視
        
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult List(int? projectId, string branchCode, string deptCode, string employee, string sortOrder, int? page)
        {
            if (projectId != null)
                ViewBag.CurrentProjectId = projectId;

            var model = client.FindByProjectId(Convert.ToInt32(projectId));
            ViewBag.Branches = orgClient.FindBraches();
            ViewBag.Depts = orgClient.FindDepartments();

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
            ViewBag.Rore = role;
            if (role != "HRAdmin" && role != "SysAdmin")
            {
                IEnumerable<AssessPersonViewModel> models = Enumerable.Empty<AssessPersonViewModel>();

                var dutys = roleClient.FindHBPDUT(employeeNo);

                foreach (var item in dutys)
                {
                    var result = model.Where(m => m.BranchCode == item.BRHCOD && m.DeptCode == item.DEPTCD).AsEnumerable();
                    if (result != null)
                        models = models.Concat(result);
                }
                model = models;
            }

            AssessPersonIndexView viewModel = new AssessPersonIndexView();

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.BrachSortParm = sortOrder == "Brach" ? "brach_desc" : "Brach";
            ViewBag.DeptSortParm = sortOrder == "Dept" ? "dept_desc" : "Dept";
            ViewBag.TeamSortParm = sortOrder == "Team" ? "team_desc" : "Dept";
            ViewBag.GroupSortParm = sortOrder == "Group" ? "group_desc" : "Group";

            if ((!string.IsNullOrWhiteSpace(branchCode)))
            {
                model = model.Where(p => p.BranchCode == branchCode);
                ViewBag.CurrentBranchCode = branchCode;
            }

            if ((!string.IsNullOrWhiteSpace(deptCode)))
            {
                model = model.Where(p => p.DeptCode == deptCode);
                ViewBag.CurrentDeptCode = deptCode;
            }
            if ((!string.IsNullOrWhiteSpace(employee)))
            {
                model = model.Where(p => p.EmployeeNo == employee);
            }

            switch (sortOrder)
            {
                case "name_desc":
                    model = model.OrderByDescending(m => m.FormName);
                    break;
                case "Date":
                    model = model.OrderBy(m => m.ModifiedDate);
                    break;
                case "date_desc":
                    model = model.OrderByDescending(m => m.ModifiedDate);
                    break;
                case "Brach":
                    model = model.OrderBy(m => m.BranchName);
                    break;
                case "brach_desc":
                    model = model.OrderByDescending(m => m.BranchName);
                    break;
                case "Dept":
                    model = model.OrderBy(m => m.DeptCode);
                    break;
                case "dept_desc":
                    model = model.OrderByDescending(m => m.DeptCode);
                    break;
                case "Team":
                    model = model.OrderBy(m => m.TeamCode);
                    break;
                case "team_desc":
                    model = model.OrderByDescending(m => m.TeamCode);
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

            viewModel.People = model.ToPagedList(pageIndex, PageSize);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult List(AssessPersonIndexView vm, string sortOrder, int? page)
        {
            if (vm.ProjectId != null)
                ViewBag.CurrentProjectId = vm.ProjectId;

            var model = client.FindByProjectId(Convert.ToInt32(vm.ProjectId));           
            
            ViewBag.Branches = orgClient.FindBraches();
            ViewBag.Depts = orgClient.FindDepartments();

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
            ViewBag.Rore = role;
            if (role != "HRAdmin" && role != "SysAdmin")
            {
                IEnumerable<AssessPersonViewModel> models = Enumerable.Empty<AssessPersonViewModel>();

                var dutys = roleClient.FindHBPDUT(employeeNo);

                foreach (var item in dutys)
                {
                    var result = model.Where(m => m.BranchCode == item.BRHCOD && m.DeptCode == item.DEPTCD).AsEnumerable();
                    if (result != null)
                        models = models.Concat(result);
                }
                model = models;
            }

            AssessPersonIndexView viewModel = new AssessPersonIndexView();

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.BrachSortParm = sortOrder == "Brach" ? "brach_desc" : "Brach";
            ViewBag.DeptSortParm = sortOrder == "Dept" ? "dept_desc" : "Dept";
            ViewBag.TeamSortParm = sortOrder == "Team" ? "team_desc" : "Dept";
            ViewBag.GroupSortParm = sortOrder == "Group" ? "group_desc" : "Group";
            
            if ((!string.IsNullOrWhiteSpace(vm.BranchCode)))
            {
                model = model.Where(p => p.BranchCode == vm.BranchCode);
                ViewBag.CurrentBranchCode = vm.BranchCode;
            }

            if ((!string.IsNullOrWhiteSpace(vm.DeptCode)))
            {
                model = model.Where(p => p.DeptCode == vm.DeptCode);
                ViewBag.CurrentDeptCode = vm.DeptCode;
            }

            if ((!string.IsNullOrWhiteSpace(vm.EmployeeNo)))
            {
                model = model.Where(p => p.EmployeeNo == vm.EmployeeNo);                
            }

            switch (sortOrder)
            {
                case "name_desc":
                    model = model.OrderByDescending(m => m.FormName);
                    break;
                case "Date":
                    model = model.OrderBy(m => m.ModifiedDate);
                    break;
                case "date_desc":
                    model = model.OrderByDescending(m => m.ModifiedDate);
                    break;
                case "Brach":
                    model = model.OrderBy(m => m.BranchName);
                    break;
                case "brach_desc":
                    model = model.OrderByDescending(m => m.BranchName);
                    break;
                case "Dept":
                    model = model.OrderBy(m => m.DeptCode);
                    break;
                case "dept_desc":
                    model = model.OrderByDescending(m => m.DeptCode);
                    break;
                case "Team":
                    model = model.OrderBy(m => m.TeamCode);
                    break;
                case "team_desc":
                    model = model.OrderByDescending(m => m.TeamCode);
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

            viewModel.People = model.ToPagedList(pageIndex, PageSize);

            return View(viewModel);
        }

        #endregion
    }
}