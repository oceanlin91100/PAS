using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using UniPsg.Model.PAS.ViewModels;
using UniPsg.Web.UI.PAS.Models;
using PagedList;

namespace UniPsg.Web.UI.PAS.Controllers
{
    public class KPICategoryController : Controller
    {
        // 分頁後每頁顯示的筆數
        private const int PageSize = 100;
        KPICategoryClient client = new KPICategoryClient();
        // GET: KPICategory
        public ActionResult Index(string sortOrder, int? page)
        {
            var models = client.FindAll();
            KPICategoryIndexView viewModel = new KPICategoryIndexView();
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
            int pageIndex = (page ?? 1);
            viewModel.Categories = models.ToPagedList(pageIndex, PageSize);

            return View(viewModel);
        }

        // GET: KPICategory/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: KPICategory/Create    
        [HttpPost]
        public ActionResult Create(KPICategoryViewModel models)
        {
            client.Create(models);
            return RedirectToAction("Index");
        }

        // GET: KPICategory/Edit/5  
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Edit(int id)
        {
            KPICategoryViewModel models = new KPICategoryViewModel();
            models = client.Find(id);
            return View("Edit", models);
        }

        // GET: KPICategory/Details/5  
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Details(int id)
        {
            KPICategoryViewModel models = new KPICategoryViewModel();
            models = client.Find(id);
            return View("Details", models);
        }

        // GET: KPICategory/Edit/5    
        [HttpPost]
        public ActionResult Edit(KPICategoryViewModel models)
        {
            client.Edit(models);
            return RedirectToAction("Index");
        }

        // GET: KPICategory/Delete/5    
        public ActionResult Delete(int id)
        {
            client.Delete(id);
            return RedirectToAction("Index");
        }

        // GET: KPICategory/Detail/5  
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Detail(int id)
        {
            KPICategoryViewModel models = new KPICategoryViewModel();
            models = client.Find(id);
            return View("Detail", models);
        }

    }
}
