using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using UniPsg.Model.PAS.ViewModels;
using UniPsg.Web.UI.PAS.Models;
using PagedList;

namespace UniPsg.Web.UI.PAS.Controllers
{
    public class ProjectReviewController : Controller
    {
        // 分頁後每頁顯示的筆數
        private const int PageSize = 100;

        private ProjectReviewClient client = new ProjectReviewClient();
        private ScopeItemClient itemeClient = new ScopeItemClient();

        // GET: ProjectReview
        public ActionResult Index(string sortOrder, int? page)
        {
            var models = client.FindAll();

            ProjectReviewIndexView viewModel = new ProjectReviewIndexView();

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            switch (sortOrder)
            {
                case "name_desc":
                    models = models.OrderByDescending(m => m.ItemName);
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

            int pageIndex = (page ?? 1);
            viewModel.Reviews = models.ToPagedList(pageIndex, PageSize);

            return View(viewModel);
        }

        // GET: ProjectReview/Create
        [HttpGet]
        public ActionResult Create(int projectId, string employeeNo, string reviewer, int scopeId, int KPICategoryId, int itemId)
        {
            ViewBag.Project = projectId;
            ViewBag.Employee = employeeNo;
            ViewBag.Review = reviewer;
            return View("Create");
        }

        // POST: ProjectReview/Create    
        [HttpPost]
        public ActionResult Create(ProjectReviewViewModel models)
        {
            client.Create(models);
            return RedirectToAction("Update1", "Review", new { projectId = models.ProjectId , employeeNo = models.EmployeeNo});
        }

        // GET: ProjectReview/Create1
        [HttpGet]
        public ActionResult Create1(int projectId, string employeeNo, string reviewer, int scopeId, int KPICategoryId, int itemId)
        {  
            var item = itemeClient.Find(itemId);
            ViewBag.ScopItem = item.Name;
            ViewBag.Definition = item.Definition;
            ViewBag.Project = projectId;
            ViewBag.Employee = employeeNo;
            ViewBag.Review = reviewer;

            return View("Create1");
        }

        // POST: ProjectReview/Create1   
        [HttpPost]
        public ActionResult Create1(ProjectReviewViewModel models)
        {
            client.Create(models);
            return RedirectToAction("Update1", "Review", new { projectId = models.ProjectId, employeeNo = models.EmployeeNo });
        }


        // GET: ProjectReview/Create3
        [HttpGet]
        public ActionResult Create3()
        {  
            return View("Create3");
        }

        // POST: ProjectReview/Create 3   
        [HttpPost]
        public ActionResult Create3(ProjectReviewViewModel models)
        {
            client.Create(models);
            return RedirectToAction("Index");
        }
        // GET: ProjectReview/Edit3/5  
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Edit3(string id)
        {
            ProjectReviewViewModel models = new ProjectReviewViewModel();
            models = client.Find(id);
            return View("Edit3", models);
        }
        // GET: ProjectReview/Edit3/5    
        [HttpPost]
        public ActionResult Edit3(ProjectReviewViewModel models)
        {
            client.Edit(models);
            return RedirectToAction("Index");
        }

        // GET: ProjectReview/Edit/5  
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Edit(string id)
        {  
            ProjectReviewViewModel models = new ProjectReviewViewModel();
            models = client.Find(id);
            var item = itemeClient.Find(models.ItemId);
            ViewBag.ScopItem = item.Name;
            ViewBag.Definition = item.Definition;
            ViewBag.Project = models.ProjectId;
            ViewBag.Employee = models.EmployeeNo;
            ViewBag.Review = models.Reviewer;

            return View("Edit1", models);
        }

        // GET: ProjectReview/Edit/5    
        [HttpPost]
        public ActionResult Edit(ProjectReviewViewModel models)
        {
            client.Edit(models);
            return RedirectToAction("Update1", "Review", new { projectId = models.ProjectId, employeeNo = models.EmployeeNo });
        }

        // GET: ProjectReview/Edit/5  
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Edit1(string id)
        {
            ProjectReviewViewModel models = new ProjectReviewViewModel();
            models = client.Find(id);
            ViewBag.Project = models.ProjectId;
            ViewBag.Employee = models.EmployeeNo;
            ViewBag.Review = models.Reviewer;

            return View("Edit", models);
        }
        
        // GET: ProjectReview/Edit/5    
        [HttpPost]
        public ActionResult Edit1(ProjectReviewViewModel models)
        {
            client.Edit(models);
            return RedirectToAction("Update1", "Review", new { projectId = models.ProjectId, employeeNo = models.EmployeeNo });
        }

        // GET: ProjectReview/Delete/5    
        public ActionResult Delete(string id , int projectId, string employeeNo)
        {
            client.Delete(id);
            return RedirectToAction("Update1", "Review", new { projectId = projectId, employeeNo = employeeNo });
        }

        // GET: ProjectReview/Delete3/5    
        public ActionResult Delete3(string id)
        {
            client.Delete(id);
            return RedirectToAction("Index");
        }

        // GET: ProjectReview/Detail/5  
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Detail(string id)
        {
            ProjectReviewViewModel models = new ProjectReviewViewModel();
            models = client.Find(id);
            return View("Detail", models);
        }
    }
}