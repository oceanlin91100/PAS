using System;
using System.Linq;
using System.Web.Mvc;
using UniPsg.Web.UI.PAS.Models;
using PagedList;
using System.Data.Linq.SqlClient;
using UniPsg.Model.PAS.ViewModels;
using System.DirectoryServices;

namespace UniPsg.Web.UI.PAS.Controllers
{
    public class PerformanceController : Controller
    {
        // 分頁後每頁顯示的筆數
        private const int PageSize = 100;
        private string LDAPPath = System.Web.Configuration.WebConfigurationManager.AppSettings["LDAPPath"];

        private AssessPersonClient client = new AssessPersonClient();
        private AssessGroupClient groupClient = new AssessGroupClient();
        private AssessProjectClient projectClient = new AssessProjectClient();
        private AssessFormClient formClient = new AssessFormClient();
        private OrganizationClient orgClient = new OrganizationClient();
        private RoleUserMappingClient ruClient = new RoleUserMappingClient();
        private ManagerClient mangeClient = new ManagerClient();
        private ScopeItemClient itemeClient = new ScopeItemClient();

        // GET: AssessPerson        
        //public ActionResult Index(int projectId,string sortOrder, string searchString, int? page)
        public ActionResult Index(int? projectId, string branchCode, string deptCode, string sortOrder, int? page)
        {

            var model = client.FindAll();

            // 1.取得員編
            string account = User.Identity.Name.ToString();
            //string account = @"PSCNET\79338";
            //string account = @"PSCNET\ALEN";
            //string account = @"PSCNET\CHEEKO.WU";
            //string account = @"PSCNET\KAVIN";            
            //string account = @"PSCNET\james";
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
            // 2.取得角色            
            model = model.Where(m => m.FlowPath.Contains(employeeNo));
            ViewBag.Projects = projectClient.FindByStatus(0);
            ViewBag.Branches = orgClient.FindBraches();
            ViewBag.Depts = orgClient.FindDepartments();

            ViewBag.Count = model.Count();
            ViewBag.Viewer = employeeNo;

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
            ViewBag.Projects = projectClient.FindByStatus(0);
            ViewBag.Branches = orgClient.FindBraches();
            ViewBag.Depts = orgClient.FindDepartments();

            AssessPersonIndexView viewModel = new AssessPersonIndexView();

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
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
                default:
                    model = model.OrderBy(m => m.ProjectId);
                    break;
            }
            int pageIndex = (page ?? 1);

            viewModel.People = model.ToPagedList(pageIndex, PageSize);

            return View(viewModel);
        }

        public ActionResult Detail1(int projectId, string employeeNo)
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

                mv.Scores = model.Scores;

                return View("Detail16", mv);
            }
            model.Reviews = model.Reviews.OrderBy(r => r.CreatedDate).ToList();
            if (model.GroupId == 4)
                return View("Detail14", model);

            if (model.GroupId == 7)
                return View("Detail17", model);

            return View("Detail1", model);
        }

        // GET: Manage
        public ActionResult View1(int projectId, string employeeNo)
        {
            var model = mangeClient.FindReviewer(projectId, employeeNo);
            // 1.取得員編
            string account = User.Identity.Name.ToString();
            //string account = @"PSCNET\ANNYHSU";
            //string account = @"PSCNET\CHEEKO.WU";
            //string account = @"PSCNET\KAVIN";            
            //string account = @"PSCNET\james";
            account = account.Replace(@"labpsc\", "");
            account = account.Replace(@"LABPSC\", "");
            account = account.Replace(@"pscnet\", "");
            account = account.Replace(@"PSCNET\", "");
            //var empNo = orgClient.FindEmployeeNumber(account, 0);
            string empNo = string.Empty;
            //var employeeNo = orgClient.FindEmployeeNumber(account, 0);
            DirectoryEntry searchRoot = new DirectoryEntry(LDAPPath);
            DirectorySearcher search = new DirectorySearcher(searchRoot);
            search.Filter = "(&(objectClass=user)(SAMAccountName=" + account.ToUpper() + "))";
            SearchResult results = search.FindOne();
            if (results != null)
            {
                ResultPropertyCollection resultPropColl;
                resultPropColl = results.Properties;
                empNo = (string)resultPropColl["description"][0];
            }
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
                if (model.EmployeeNo == empNo)
                    return View("View16", mv);
                else
                {
                    var order = model.Scores.Where(s => s.Reviewer == empNo).FirstOrDefault();
                    if (order != null)
                    {
                        mv.Scores = model.Scores.Where(s => s.OderSerial <= order.OderSerial).ToList();
                        return View("Detail16", mv);
                    }
                    else
                        return View("View1", model);
                }
            }
            if (model.GroupId == 4)
            {
                model.Reviews = model.Reviews.OrderBy(r => r.CreatedDate).ToList();
                if (model.EmployeeNo == empNo)
                    return View("View14", model);
                else
                {
                    var order = model.Scores.Where(s => s.Reviewer == empNo).FirstOrDefault();
                    if (order != null)
                    {
                        model.Scores = model.Scores.Where(s => s.OderSerial <= order.OderSerial).ToList();
                        return View("Detail14", model);
                    }
                    else
                        return View("View1", model);
                }
            }
            if (model.GroupId == 7)
            {
                model.Reviews = model.Reviews.OrderBy(r => r.CreatedDate).ToList();
                if (model.EmployeeNo == empNo)
                    return View("View17", model);
                else
                {
                    var order = model.Scores.Where(s => s.Reviewer == empNo).FirstOrDefault();
                    if (order != null)
                    {
                        model.Scores = model.Scores.Where(s => s.OderSerial <= order.OderSerial).ToList();
                        return View("Detail17", model);
                    }
                    else
                        return View("View1", model);
                }
            }
            return View("View1", model);
        }

        public ActionResult Print1(int projectId, string employeeNo)
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

                mv.Scores = model.Scores;

                return View("Print16", mv);
            }
            model.Reviews = model.Reviews.OrderBy(r => r.CreatedDate).ToList();
            if (model.GroupId == 4)
                return View("Print14", model);

            if (model.GroupId == 7)
                return View("Print17", model);

            return View("Print1", model);
        }

    }
}