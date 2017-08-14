using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using UniPsg.Model.PAS.ViewModels;
using UniPsg.Web.UI.PAS.Models;
using PagedList;

namespace UniPsg.Web.UI.PAS.Controllers
{
    public class AssessCategoryController : Controller
    {
        // 分頁後每頁顯示的筆數
        private const int PageSize = 100;
        private AssessCategoryClient client = new AssessCategoryClient();

        // GET: AssessCategory
        public ActionResult Index(string sortOrder, int? page)
        {
            var model = client.FindAll();

            AssessCategoryIndexView viewModel = new AssessCategoryIndexView();                                  

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
            viewModel.Categories = model.ToPagedList(pageIndex, PageSize);

            return View(viewModel);
        }
        
        // GET: AssessCategory/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: AssessCategory/Create    
        [HttpPost]
        public ActionResult Create(AssessCategoryViewModel models)
        {
            AssessCategoryClient client = new AssessCategoryClient();
            client.Create(models);
            return RedirectToAction("Index");
        }


        // GET: AssessCategory/Edit/5  
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Edit(int id)
        {   
            AssessCategoryViewModel models = new AssessCategoryViewModel();
            models = client.Find(id);
            return View("Edit", models);            
        }

        //// GET: AssessCategory/Details/5  
        //[AcceptVerbs(HttpVerbs.Get)]
        //public ActionResult Details(int id)
        //{
        //    AssessCategoryViewModel ac = new AssessCategoryViewModel();
        //    ac = client.Find(id);
        //    return View("Details", ac);
        //}

        // GET: AssessCategory/Edit/5    
        [HttpPost]
        public ActionResult Edit(AssessCategoryViewModel models)
        {
            client.Edit(models);
            return RedirectToAction("Index");
        }

        // GET: AssessCategory/Delete/5    
        public ActionResult Delete(int id)
        {
            client.Delete(id);
            return RedirectToAction("Index");
        }

        // GET: AssessCategory/Detail/5  
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Detail(int id)
        {
            AssessCategoryViewModel models = new AssessCategoryViewModel();
            models = client.Find(id);
            return View("Detail", models);
        }


    }
}