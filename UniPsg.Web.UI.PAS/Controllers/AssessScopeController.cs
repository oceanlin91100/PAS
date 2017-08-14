using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using UniPsg.Model.PAS.ViewModels;
using UniPsg.Web.UI.PAS.Models;
using PagedList;

namespace UniPsg.Web.UI.PAS.Controllers
{
    public class AssessScopeController : Controller
    {
        // 分頁後每頁顯示的筆數
        private const int PageSize = 100;

        AssessScopeClient client = new AssessScopeClient();

        // GET: AssessScope
        public ActionResult Index(string sortOrder, int? page)
        {
            var models = client.FindAll();
            AssessScopeIndexView viewModel = new AssessScopeIndexView();

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
            viewModel.Scopes = models.ToPagedList(pageIndex, PageSize);

            return View(viewModel);
        }

        // GET: AssessScope/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: AssessScope/Create    
        [HttpPost]
        public ActionResult Create(AssessScopeViewModel models)
        {
            client.Create(models);
            return RedirectToAction("Index");
        }


        // GET: AssessScope/Edit/5  
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Edit(int id)
        {
            AssessScopeViewModel ac = new AssessScopeViewModel();
            ac = client.Find(id);
            return View("Edit", ac);
        }


        // GET: AssessScope/Edit/5    
        [HttpPost]
        public ActionResult Edit(AssessScopeViewModel models)
        {
            client.Edit(models);
            return RedirectToAction("Index");
        }

        // GET: AssessScope/Delete/5    
        public ActionResult Delete(int id)
        {
            client.Delete(id);
            return RedirectToAction("Index");
        }

        // GET: AssessScope/Detail/5  
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Detail(int id)
        {
            AssessScopeViewModel models = new AssessScopeViewModel();
            models = client.Find(id);
            return View("Detail", models);
        }


    }
}