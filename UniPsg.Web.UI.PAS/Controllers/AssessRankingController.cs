using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using UniPsg.Model.PAS.ViewModels;
using UniPsg.Web.UI.PAS.Models;
using PagedList;

namespace UniPsg.Web.UI.PAS.Controllers
{
    public class AssessRankingController : Controller
    {
        // 分頁後每頁顯示的筆數
        private const int PageSize = 100;

        AssessRankingClient client = new AssessRankingClient();

        // GET: AssessRanking
        public ActionResult Index(string sortOrder, int? page)
        {
            var models = client.FindAll();

            AssessRankingIndexView viewModel = new AssessRankingIndexView();
            
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
            viewModel.Rankings = models.ToPagedList(pageIndex, PageSize);

            return View(viewModel);
        }
        // GET: AssessRanking/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: AssessRanking/Create    
        [HttpPost]
        public ActionResult Create(AssessRankingViewModel models)
        {   
            client.Create(models);
            return RedirectToAction("Index");
        }


        // GET: AssessRanking/Edit/5  
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Edit(int id)
        {  
            AssessRankingViewModel models = new AssessRankingViewModel();
            models = client.Find(id);
            return View("Edit", models);
        }

        // GET: AssessRanking/Edit/5    
        [HttpPost]
        public ActionResult Edit(AssessRankingViewModel models)
        {
            client.Edit(models);
            return RedirectToAction("Index");
        }

        // GET: AssessRanking/Delete/5    
        public ActionResult Delete(int id)
        {
            client.Delete(id);
            return RedirectToAction("Index");
        }

        // GET: AssessRanking/Detail/5  
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Detail(int id)
        {
            AssessRankingViewModel models = new AssessRankingViewModel();
            models = client.Find(id);
            return View("Detail", models);
        }

    }
}