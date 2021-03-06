﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using UniPsg.Model.PAS.ViewModels;
using UniPsg.Web.UI.PAS.Models;
using PagedList;

namespace UniPsg.Web.UI.PAS.Controllers
{
    public class KPIItemController : Controller
    {
        // 分頁後每頁顯示的筆數
        private const int PageSize = 100;

        private ScopeItemClient itemClient = new ScopeItemClient();
        private KPICategoryClient categoryClient = new KPICategoryClient();
        private AssessGroupClient groupClient = new AssessGroupClient();        

        private List<SelectListItem> GroupSelectListItems(string selected = "")
        {
            var groups = groupClient.FindByStatus(0);
            var items = new List<SelectListItem>();
            var selectedGroups = string.IsNullOrWhiteSpace(selected) ? null : selected.Split(',');
            foreach (var g in groups)
            {
                items.Add(item: new SelectListItem()
                {
                    Value = g.Id.ToString(),
                    Text = g.Name,
                    Selected = selectedGroups == null ? false : selectedGroups.Contains(g.Id.ToString())
                });
            }
            return items;
        }

        // GET: KPIItem        
        public ActionResult Index(string sortOrder, int? page)
        {
            var models = itemClient.FindByScope(3);

            ScopeItemIndexView viewModel = new ScopeItemIndexView();

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

            var groups = this.GroupSelectListItems();
            var result = new List<ScopeItemViewModel>();
            foreach (var item in models)
            {
                var selectedList = item.Groups.Split(',').ToList();
                item.Groups = string.IsNullOrWhiteSpace(item.Groups) ? "" : string.Join(",", groups.Where(x => selectedList.Contains(x.Value)).Select(x => x.Text));                
                result.Add(item);
            }

            int pageIndex = (page ?? 1);
            viewModel.Items = result.ToPagedList(pageIndex, PageSize);
            return View(viewModel);
        }

        public ActionResult Index1(string sortOrder, int? page)
        {
            var models = itemClient.FindByScope(2);

            ScopeItemIndexView viewModel = new ScopeItemIndexView();

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

            var groups = this.GroupSelectListItems();
            var result = new List<ScopeItemViewModel>();
            foreach (var item in models)
            {
                var selectedList = item.Groups.Split(',').ToList();
                item.Groups = string.IsNullOrWhiteSpace(item.Groups) ? "" : string.Join(",", groups.Where(x => selectedList.Contains(x.Value)).Select(x => x.Text));
                result.Add(item);
            }

            int pageIndex = (page ?? 1);
            viewModel.Items = result.ToPagedList(pageIndex, PageSize);
            return View(viewModel);
        }

        // GET: KPIItem/Create
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Categories = categoryClient.FindByStatus(0);            
            var items = this.GroupSelectListItems();
            ViewBag.GroupItems = items;
            return View("Create");
        }

        // POST: KPIItem/Create    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create1(ScopeItemViewModel models, string[] groups)
        {
            models.Groups = string.Join(",", groups);
            itemClient.Create(models);
            return RedirectToAction("Index1");
        }

        [HttpGet]
        public ActionResult Create1()
        {
            ViewBag.Categories = categoryClient.FindByStatus(0);
            var items = this.GroupSelectListItems();
            ViewBag.GroupItems = items;
            return View("Create1");
        }

        // POST: KPIItem/Create    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ScopeItemViewModel models, string[] groups)
        {
            models.Groups = string.Join(",", groups);
            itemClient.Create(models);
            return RedirectToAction("Index");
        }

        // GET: KPIItem/Edit/5  
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            ScopeItemViewModel models = new ScopeItemViewModel();
            models = itemClient.Find(id.Value);
            if (models == null)
                return HttpNotFound();

            var items = this.GroupSelectListItems(models.Groups);
            ViewBag.GroupItems = items;

            ViewBag.Categories = categoryClient.FindByStatus(0);
            return View("Edit", models);
        }

        // GET: KPIItem/Edit/5    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ScopeItemViewModel models, string[] groups)
        {
            models.Groups = string.Join(",", groups);
            itemClient.Edit(models);
            return RedirectToAction("Index");
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Edit1(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            ScopeItemViewModel models = new ScopeItemViewModel();
            models = itemClient.Find(id.Value);
            if (models == null)
                return HttpNotFound();

            var items = this.GroupSelectListItems(models.Groups);
            ViewBag.GroupItems = items;

            ViewBag.Categories = categoryClient.FindByStatus(0);
            return View("Edit1", models);
        }

        // GET: KPIItem/Edit/5    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit1(ScopeItemViewModel models, string[] groups)
        {
            models.Groups = string.Join(",", groups);
            itemClient.Edit(models);
            return RedirectToAction("Index1");
        }

        // GET: KPIItem/Delete/5    
        public ActionResult Delete(int id)
        {
            itemClient.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Delete1(int id)
        {
            itemClient.Delete(id);
            return RedirectToAction("Index1");
        }


        // GET: KPIItem/Detail/5  
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Detail(int id)
        {
            ScopeItemViewModel models = new ScopeItemViewModel();
            models = itemClient.Find(id);

            var groups = this.GroupSelectListItems();
            var selectedList = models.Groups.Split(',').ToList();
            models.Groups = string.IsNullOrWhiteSpace(models.Groups) ? "" : string.Join(",", groups.Where(x => selectedList.Contains(x.Value)).Select(x => x.Text));

            return View("Detail", models);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Detail1(int id)
        {
            ScopeItemViewModel models = new ScopeItemViewModel();
            models = itemClient.Find(id);

            var groups = this.GroupSelectListItems();
            var selectedList = models.Groups.Split(',').ToList();
            models.Groups = string.IsNullOrWhiteSpace(models.Groups) ? "" : string.Join(",", groups.Where(x => selectedList.Contains(x.Value)).Select(x => x.Text));

            return View("Detail1", models);
        }

    }
}