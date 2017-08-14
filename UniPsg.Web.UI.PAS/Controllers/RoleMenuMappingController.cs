using System.Web.Mvc;
using UniPsg.Model.PAS.ViewModels;
using UniPsg.Web.UI.PAS.Models;

namespace UniPsg.Web.UI.PAS.Controllers
{
    public class RoleMenuMappingController : Controller
    {
        private RoleMenuMappingClient client = new RoleMenuMappingClient();
        private RoleClient roleClient = new RoleClient();
        private MenuClient menuClient = new MenuClient();
        // GET: RoleMenuMapping
        public ActionResult Index()
        {            
            ViewBag.ListClients = client.FindAll();
            return View();
        }
        // GET: RoleMenuMapping/Create
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Roles = roleClient.FindByStatus(0);
            ViewBag.Menus = menuClient.FindByStatus(0);

            return View("Create");
        }

        // POST: RoleMenuMapping/Create    
        [HttpPost]
        public ActionResult Create(RoleMenuMappingViewModel models)
        {            
            client.Create(models);
            return RedirectToAction("Index");
        }


        // GET: RoleMenuMapping/Edit/5  
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Edit(int id)
        {
            ViewBag.Roles = roleClient.FindByStatus(0);
            ViewBag.Menus = menuClient.FindByStatus(0);

            RoleMenuMappingViewModel models = new RoleMenuMappingViewModel();
            models = client.Find(id);
            return View("Edit", models);
        }

        // GET: RoleMenuMapping/Edit/5    
        [HttpPost]
        public ActionResult Edit(RoleMenuMappingViewModel models)
        {           
            client.Edit(models);
            return RedirectToAction("Index");
        }

        // GET: RoleMenuMapping/Delete/5    
        public ActionResult Delete(int id)
        {           
            client.Delete(id);
            return RedirectToAction("Index");
        }

        // GET: RoleMenuMapping/Detail/5  
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Detail(int id)
        {
            RoleMenuMappingViewModel models = new RoleMenuMappingViewModel();
            models = client.Find(id);
            return View("Detail", models);
        }

    }
}