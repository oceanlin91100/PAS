using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using UniPsg.Model.PAS.ViewModels;
using UniPsg.Web.UI.PAS.Models;
using PagedList;

namespace UniPsg.Web.UI.PAS.Controllers
{
    public class FormWeightController : Controller
    {  
        // 分頁後每頁顯示的筆數
        private const int PageSize = 100;

        private FormWeightClient weightClient = new FormWeightClient();
        private AssessScopeClient scopeClient = new AssessScopeClient();
        private AssessFormClient formClient = new AssessFormClient();
        private ScopeItemClient itemClient = new ScopeItemClient();

        // GET: FormWeight
        public ActionResult Index(string sortOrder, int? page)
        {
            var models = weightClient.FindAll();

            FormWeightIndexView viewModel = new FormWeightIndexView();

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";           

            switch (sortOrder)
            {
                case "name_desc":
                    models = models.OrderByDescending(m => m.Weight);
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
            viewModel.Forms = models.ToPagedList(pageIndex, PageSize);

            return View(viewModel);
        }
        // GET: FormWeight/Create
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Forms = formClient.FindByStatus(0);
            ViewBag.Scopes = scopeClient.FindByStatus(0);

            return View("Create");
        }

        // POST: FormWeight/Create    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormWeightViewModel models)
        {          
            weightClient.Create(models);
            return RedirectToAction("Index");
        }

        // GET: FormWeight/Edit/5  
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            FormWeightViewModel models = new FormWeightViewModel();
            models = weightClient.Find(id.Value);

            ViewBag.Forms = formClient.FindAll();
            if (models == null)
                return HttpNotFound();

            ViewBag.Scopes = scopeClient.FindAll();
            if (models == null)
                return HttpNotFound();


            return View("Edit", models);
        }

        // GET: FormWeight/Edit/5    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormWeightViewModel models)
        {
            weightClient.Edit(models);
            return RedirectToAction("Index");
        }

        // GET: FormWeight/Delete/5    
        public ActionResult Delete(int id)
        {
            weightClient.Delete(id);
            return RedirectToAction("Index");
        }


        // GET: FormWeight/Detail/5  
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Detail(int id)
        {
            FormWeightViewModel models = new FormWeightViewModel();
            models = weightClient.Find(id);

            ViewBag.ScopeItems = itemClient.FindByScopeItems(models.ScopeId, models.Groups);

            return View("Detail", models);
        }

    }
}