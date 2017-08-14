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
    public class AssessFormController : Controller
    {
        // 分頁後每頁顯示的筆數
        private const int PageSize = 100;

        private AssessFormClient itemClient = new AssessFormClient();
        private AssessCategoryClient categoryClient = new AssessCategoryClient();
        private AssessScopeClient scopeClient = new AssessScopeClient();
        private AssessGroupClient groupClient = new AssessGroupClient();
        private FormWeightClient weightClient = new FormWeightClient();

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

        private List<SelectListItem> ItemSelectListItems(string selected = "")
        {
            var categories = scopeClient.FindByStatus(0);
            var items = new List<SelectListItem>();
            var selectedCategories = string.IsNullOrWhiteSpace(selected) ? null : selected.Split(',');
            foreach (var c in categories)
            {
                items.Add(item: new SelectListItem()
                {
                    Value = c.Id.ToString(),
                    Text = c.Name,
                    Selected = selectedCategories == null ? false : selectedCategories.Contains(c.Id.ToString())
                });
            }
            return items;
        }

        // GET: AssessForm  
        public ActionResult Index(string sortOrder, int? page)
        {
            var model = itemClient.FindAll();

            AssessFormIndexView viewModel = new AssessFormIndexView();          

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.CategorySortParm = sortOrder == "Category" ? "category_desc" : "Category";

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
                case "Category":
                    model = model.OrderBy(m => m.CategoryId);
                    break;
                case "category_desc":
                    model = model.OrderByDescending(m => m.CategoryId);
                    break;
                default:
                    model = model.OrderBy(c => c.Id);
                    break;
            }

            var groups = GroupSelectListItems();
            var items = ItemSelectListItems();
            var result = new List<AssessFormViewModel>();
            foreach (var item in model)
            {
                var selectedList = item.Groups.Split(',').ToList();
                item.Groups = string.IsNullOrWhiteSpace(item.Groups) ? "" : string.Join(",", groups.Where(x => selectedList.Contains(x.Value)).Select(x => x.Text));

                var selectedList1 = item.Items.Split(',').ToList();
                item.Items = string.IsNullOrWhiteSpace(item.Items) ? "" : string.Join(",", items.Where(x => selectedList1.Contains(x.Value)).Select(x => x.Text));
                
                result.Add(item);
            }

            int pageIndex = (page ?? 1);
            viewModel.Forms= result.ToPagedList(pageIndex, PageSize); 

            return View(viewModel);
        }
        // GET: AssessForm/Create
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Categories = categoryClient.FindByStatus(0);

            var groups = GroupSelectListItems();
            ViewBag.GroupItems = groups;

            var items = ItemSelectListItems();
            ViewBag.ItemItems = items;

            return View("Create");
        }

        // POST: AssessForm/Create    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AssessFormViewModel models, string[] groups, string[] items)
        {
            models.Groups = string.Join(",", groups);
            models.Items = string.Join(",", items);
            itemClient.Create(models);
            return RedirectToAction("Index");
        }

        // GET: AssessForm/Edit/5  
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            AssessFormViewModel models = new AssessFormViewModel();
            models = itemClient.Find(id.Value);
            ViewBag.Categories = categoryClient.FindByStatus(0);
            if (models == null)
                return HttpNotFound();

            var groups = GroupSelectListItems(models.Groups);
            ViewBag.GroupItems = groups;

            var items = ItemSelectListItems(models.Items);
            ViewBag.ItemItems = items;

            
            return View("Edit", models);
        }

        // GET: AssessForm/Edit/5    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AssessFormViewModel models, string[] groups, string[] items)
        {
            models.Groups = string.Join(",", groups);
            models.Items = string.Join(",", items);
            itemClient.Edit(models);
            return RedirectToAction("Index");
        }

        // GET: AssessForm/Delete/5    
        public ActionResult Delete(int id)
        {
            itemClient.Delete(id);
            return RedirectToAction("Index");
        }


        // GET: AssessForm/Detail/5  
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Detail(int id)
        {
            AssessFormViewModel models = new AssessFormViewModel();
            models = itemClient.Find(id);

            var groups = GroupSelectListItems();
            var selectedList = models.Groups.Split(',').ToList();
            models.Groups = string.IsNullOrWhiteSpace(models.Groups) ? "" : string.Join(",", groups.Where(x => selectedList.Contains(x.Value)).Select(x => x.Text));

            var items = ItemSelectListItems();
            var selectedList1 = models.Items.Split(',').ToList();
            models.Items = string.IsNullOrWhiteSpace(models.Items) ? "" : string.Join(",", items.Where(x => selectedList1.Contains(x.Value)).Select(x => x.Text));

            ViewBag.Scopes = weightClient.FindByForm(models.Id);
           

            return View("Detail", models);
        }

    }
}