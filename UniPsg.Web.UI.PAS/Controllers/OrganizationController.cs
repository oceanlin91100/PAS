using System;
using System.Linq;
using System.Web.Mvc;
using UniPsg.Model.PAS.ViewModels;
using UniPsg.Web.UI.PAS.Models;
using PagedList;
using UniPsg.Web.UI.PAS;

namespace UniPsg.Web.UI.PAS.Controllers
{
    public class OrganizationController : Controller
    {
        // 分頁後每頁顯示的筆數
        private const int PageSize = 100;

        private OrganizationClient client = new OrganizationClient();
        private AssessProjectClient projectClient = new AssessProjectClient();
        private AssessPersonClient prnclient = new AssessPersonClient();

        // GET: Organization
        public ActionResult Index(int? projectId, string branchCode, string deptCode, string employee, string sortOrder, int? page)
        {
            var models = client.FindAll();
            ViewBag.Projects = projectClient.FindAll();
            ViewBag.Branches = client.FindBraches();
            ViewBag.Depts = client.FindDepartments();

            OrganizationIndexView viewModel = new OrganizationIndexView();

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.BrachSortParm = sortOrder == "Brach" ? "brach_desc" : "Brach";
            ViewBag.DeptSortParm = sortOrder == "Dept" ? "dept_desc" : "Dept";

            if (projectId != null)
            {
                models = models.Where(p => p.ProjectId == projectId);
                ViewBag.CurrentProjectId = projectId;
            }
            if ((!string.IsNullOrWhiteSpace(branchCode)))
            {
                models = models.Where(p => p.BranchCode == branchCode);
                ViewBag.CurrentBranchCode = branchCode;
            }

            if ((!string.IsNullOrWhiteSpace(deptCode)))
            {
                models = models.Where(p => p.DeptCode == deptCode);
                ViewBag.CurrentDeptCode = deptCode;
            }

            if ((!string.IsNullOrWhiteSpace(employee)))
            {
                models = models.Where(p => p.EmployeeNo == employee);                
            }

            switch (sortOrder)
            {
                case "name_desc":
                    models = models.OrderByDescending(m => m.EmployeeNo);
                    break;
                case "Date":
                    models = models.OrderBy(m => m.ModifiedDate);
                    break;
                case "date_desc":
                    models = models.OrderByDescending(m => m.ModifiedDate);
                    break;
                case "Brach":
                    models = models.OrderBy(m => m.BranchName);
                    break;
                case "brach_desc":
                    models = models.OrderByDescending(m => m.BranchName);
                    break;
                case "Dept":
                    models = models.OrderBy(m => m.DeptCode);
                    break;
                case "dept_desc":
                    models = models.OrderByDescending(m => m.DeptCode);
                    break;                
                default:
                    models = models.OrderBy(m => m.ProjectId);
                    break;
            }

            int pageIndex = (page ?? 1);
            viewModel.Organizations = models.ToPagedList(pageIndex, PageSize);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(OrganizationIndexView vm, string sortOrder, int? page)
        {
            var model = client.FindAll();
            ViewBag.Projects = projectClient.FindAll();
            ViewBag.Branches = client.FindBraches();
            ViewBag.Depts = client.FindDepartments();

            OrganizationIndexView viewModel = new OrganizationIndexView();

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.BrachSortParm = sortOrder == "Brach" ? "brach_desc" : "Brach";
            ViewBag.DeptSortParm = sortOrder == "Dept" ? "dept_desc" : "Dept";

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

            if ((!string.IsNullOrWhiteSpace(vm.EmployeeNo)))
            {
                model = model.Where(p => p.EmployeeNo == vm.EmployeeNo);
            }

            switch (sortOrder)
            {
                case "name_desc":
                    model = model.OrderByDescending(m => m.EmployeeNo);
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
                default:
                    model = model.OrderBy(m => m.ProjectId);
                    break;
            }
            int pageIndex = (page ?? 1);

            viewModel.Organizations = model.ToPagedList(pageIndex, PageSize);

            return View(viewModel);
        }
        // GET: Organization/Create
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Projects = projectClient.FindByStatus(0);
            return View("Create");
        }

        // POST: Organization/Create    
        [HttpPost]
        public ActionResult Create(OrganizationViewModel models)
        {
            client.Create(models);
            return RedirectToAction("Index");
        }


        // GET: Organization/Edit/5  
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Edit(int projectId, string employeeNo)
        {
            ViewBag.Projects = projectClient.FindByStatus(0);
            OrganizationViewModel models = new OrganizationViewModel();
            models = client.Find(projectId, employeeNo);
            return View("Edit", models);
        }
        

        // GET: Organization/Edit/5    
        [HttpPost]
        public ActionResult Edit(OrganizationViewModel models)
        {
            client.Edit(models);
            return RedirectToAction("Index");
        }

        // GET: Organization/Delete/5    
        public ActionResult Delete(int projectId, string employeeNo)
        {
            client.Delete(projectId, employeeNo);
            return RedirectToAction("Index");
        }

        // GET: Organization/Detail/5  
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Detail(int projectId, string employeeNo)
        {
            OrganizationViewModel models = new OrganizationViewModel();
            models = client.Find(projectId, employeeNo);
            return View("Detail", models);
        }

        // GET: Organization/Imports/5  
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Imports(int projectId)
        {
            var project = projectClient.Find(projectId);
            ViewBag.ProjectId = project.Id;
            ViewBag.ProjectName = project.Name;
            ViewBag.Rows = prnclient.FindByProjectId(projectId);
            return View("Imports");
        }

        // GET: Organization/Imports/5    
        [HttpPost]
        public ActionResult Imports(OrganizationImportModel model)
        {
            client.Imposts(model);
            return RedirectToAction("Index", "AssessProject");
        }

        // GET: Organization/Flow/5  
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Flow(int projectId, string employeeNo)
        {
            ViewBag.Projects = projectClient.FindByStatus(0);
            ViewBag.ProjectId = projectId;
            OrganizationViewModel models = new OrganizationViewModel();
            models = client.Find(projectId, employeeNo);
            return View("Flow", models);
        }


        // GET: Organization/Flow/5    
        [HttpPost]
        public ActionResult Flow(OrganizationViewModel models)
        {
            int projectId = models.ProjectId;
            client.Edit(models);
            return RedirectToAction("List", "AssessPerson", new { projectId = projectId});
        }

        // GET: Organization/Account/5  
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Account(int projectId, string employeeNo)
        {
            ViewBag.Projects = projectClient.FindByStatus(0);
            ViewBag.ProjectId = projectId;
            OrganizationViewModel models = new OrganizationViewModel();
            models = client.Find(projectId, employeeNo);
            return View("Account", models);
        }


        // GET: Organization/Flow/5    
        [HttpPost]
        public ActionResult Account(OrganizationViewModel models)
        {
            int projectId = models.ProjectId;
            client.Edit(models);
            return RedirectToAction("Index");
        }


    }
}