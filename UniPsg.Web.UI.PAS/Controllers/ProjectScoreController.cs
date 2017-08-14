using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using UniPsg.Model.PAS.ViewModels;
using UniPsg.Web.UI.PAS.Models;
using PagedList;

namespace UniPsg.Web.UI.PAS.Controllers
{
    public class ProjectScoreController : Controller
    {
        
        // 分頁後每頁顯示的筆數
        private const int PageSize = 100;
        private ProjectScoreClient client = new ProjectScoreClient();

        // GET: ProjectScore
        public ActionResult Index(string sortOrder, int? page)
        {
            var model = client.FindAll();

            ProjectScoreIndexView viewModel = new ProjectScoreIndexView();

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

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
                default:
                    model = model.OrderBy(m => m.Id);
                    break;
            }

            int pageIndex = (page ?? 1);
            viewModel.Scores = model.ToPagedList(pageIndex, PageSize);

            return View(viewModel);
        }

        // GET: ProjectScore/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: ProjectScore/Create    
        [HttpPost]
        public ActionResult Create(ProjectScoreViewModel models)
        {            
            client.Create(models);
            return RedirectToAction("Index");
        }


        // GET: ProjectScore/Edit/5  
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Edit(string id)
        {
            ProjectScoreViewModel models = new ProjectScoreViewModel();
            models = client.Find(id);
            return View("Edit", models);
        }
               
        // GET: ProjectScore/Edit/5    
        [HttpPost]
        public ActionResult Edit(ProjectScoreViewModel models)
        {
            client.Edit(models);
            return RedirectToAction("Index");
        }

        // GET: ProjectScore/Delete/5    
        public ActionResult Delete(string id)
        {
            client.Delete(id);
            return RedirectToAction("Index");
        }

        // GET: ProjectScore/Detail/5  
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Detail(string id)
        {
            ProjectScoreViewModel models = new ProjectScoreViewModel();
            models = client.Find(id);
            return View("Detail", models);
        }       

    }
}