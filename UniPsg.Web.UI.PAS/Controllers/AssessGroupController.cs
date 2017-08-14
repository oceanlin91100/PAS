using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using UniPsg.Model.PAS.ViewModels;
using UniPsg.Web.UI.PAS.Models;
using PagedList;

namespace UniPsg.Web.UI.PAS.Controllers
{
    public class AssessGroupController : Controller
    {
        // 分頁後每頁顯示的筆數
        private const int PageSize = 100;

        private AssessGroupClient client = new AssessGroupClient();

        // GET: AssessGroup        
        public ActionResult Index(string sortOrder, int? page)
        {
            var model = client.FindAll();

            AssessGroupIndexView viewModel = new AssessGroupIndexView();
           
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            switch (sortOrder)
            {
                case "name_desc":
                    model = model.OrderByDescending(m => m.Name);
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
            viewModel.Groups = model.ToPagedList(pageIndex, PageSize);

            return View(viewModel);
        }

        // GET: AssessGroup/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: AssessGroup/Create    
        [HttpPost]
        public ActionResult Create(AssessGroupViewModel models)
        {   
            client.Create(models);
            return RedirectToAction("Index");
        }


        // GET: AssessGroup/Edit/5  
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Edit(int id)
        {  
            AssessGroupViewModel models = new AssessGroupViewModel();
            models = client.Find(id);
            return View("Edit", models);
        }

        // GET: AssessGroup/Edit/5    
        [HttpPost]
        public ActionResult Edit(AssessGroupViewModel models)
        {  
            client.Edit(models);
            return RedirectToAction("Index");
        }

        // GET: AssessGroup/Delete/5    
        public ActionResult Delete(int id)
        {
            client.Delete(id);
            return RedirectToAction("Index");
        }

        // GET: AssessGroup/Detail/5  
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Detail(int id)
        {
            AssessGroupViewModel models = new AssessGroupViewModel();
            models = client.Find(id);
            return View("Detail", models);
        }

    }
}