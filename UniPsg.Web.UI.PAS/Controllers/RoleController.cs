using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using UniPsg.Model.PAS.ViewModels;
using UniPsg.Web.UI.PAS.Models;
using PagedList;


namespace UniPsg.Web.UI.PAS.Controllers
{
    public class RoleController : Controller
    {
        
        // 分頁後每頁顯示的筆數
        private const int PageSize = 100;
        private RoleClient client = new RoleClient();

        // GET: Role
        public ActionResult Index(string sortOrder, int? page)
        {
            var model = client.FindAll();

            RloeIndexView viewModel = new RloeIndexView();

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
            viewModel.Roles = model.ToPagedList(pageIndex, PageSize);

            return View(viewModel);
        }

        // GET: Role/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: Role/Create    
        [HttpPost]
        public ActionResult Create(RoleViewModel models)
        {
            RoleClient client = new RoleClient();
            client.Create(models);
            return RedirectToAction("Index");
        }


        // GET: Role/Edit/5  
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Edit(int id)
        {
            RoleViewModel models = new RoleViewModel();
            models = client.Find(id);
            return View("Edit", models);
        }

        // GET: Role/Details/5  
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Details(int id)
        {
            RoleViewModel ac = new RoleViewModel();
            ac = client.Find(id);
            return View("Details", ac);
        }

        // GET: Role/Edit/5    
        [HttpPost]
        public ActionResult Edit(RoleViewModel models)
        {
            client.Edit(models);
            return RedirectToAction("Index");
        }

        // GET: Role/Delete/5    
        public ActionResult Delete(int id)
        {
            client.Delete(id);
            return RedirectToAction("Index");
        }

        // GET: Role/Detail/5  
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Detail(int id)
        {
            RoleViewModel models = new RoleViewModel();
            models = client.Find(id);
            return View("Detail", models);
        }


    }
}