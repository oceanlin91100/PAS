using System.Web.Mvc;
using UniPsg.Model.PAS.ViewModels;
using UniPsg.Web.UI.PAS.Models;

namespace UniPsg.Web.UI.PAS.Controllers
{
    public class MenuController : Controller
    {
        [Authorize]
        // GET: Menu
        public ActionResult Index()
        {
            MenuClient client = new MenuClient();
            ViewBag.ListClients = client.FindAll();
            return View();
        }
        // GET: Menu/Create
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: Menu/Create    
        [HttpPost]
        public ActionResult Create(MenuViewModel models)
        {
            MenuClient client = new MenuClient();
            client.Create(models);
            return RedirectToAction("Index");
        }


        // GET: Menu/Edit/5  
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Edit(int id)
        {
            MenuClient client = new MenuClient();
            MenuViewModel models = new MenuViewModel();
            models = client.Find(id);
            return View("Edit", models);
        }

        // GET: Menu/Edit/5    
        [HttpPost]
        public ActionResult Edit(MenuViewModel models)
        {
            MenuClient client = new MenuClient();
            client.Edit(models);
            return RedirectToAction("Index");
        }

        // GET: Menu/Delete/5    
        public ActionResult Delete(int id)
        {
            MenuClient client = new MenuClient();
            client.Delete(id);
            return RedirectToAction("Index");
        }


        // GET: Menu/Detail/5  
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Detail(int id)
        {
            MenuClient client = new MenuClient();

            MenuViewModel models = new MenuViewModel();
            models = client.Find(id);
            return View("Detail", models);
        }
    }
}