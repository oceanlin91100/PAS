using System.Web.Mvc;
using UniPsg.Model.PAS.ViewModels;
using UniPsg.Web.UI.PAS.Models;

namespace UniPsg.Web.UI.PAS.Controllers
{
    public class RoleUserMappingController : Controller
    {
        private RoleUserMappingClient client = new RoleUserMappingClient();
        private RoleClient roleClient = new RoleClient();
        // GET: RoleUserMapping
        public ActionResult Index()
        {
            
            ViewBag.ListClients = client.FindAll();
            return View();
        }
        // GET: RoleUserMapping/Create
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Roles = roleClient.FindByStatus(0);
            return View("Create");
        }

        // POST: RoleUserMapping/Create    
        [HttpPost]
        public ActionResult Create(RoleUserMappingViewModel models)
        {
            
            client.Create(models);
            return RedirectToAction("Index");
        }


        // GET: RoleUserMapping/Edit/5  
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Edit(int id)
        {
            ViewBag.Roles = roleClient.FindByStatus(0);

            RoleUserMappingViewModel models = new RoleUserMappingViewModel();
            models = client.Find(id);
            return View("Edit", models);
        }

        // GET: RoleUserMapping/Edit/5    
        [HttpPost]
        public ActionResult Edit(RoleUserMappingViewModel models)
        {   
            client.Edit(models);
            return RedirectToAction("Index");
        }

        // GET: RoleUserMapping/Delete/5    
        public ActionResult Delete(int id)
        {
           
            client.Delete(id);
            return RedirectToAction("Index");
        }

        // GET: AssessForm/Detail/5  
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Detail(int id)
        {
            RoleUserMappingViewModel models = new RoleUserMappingViewModel();
            models = client.Find(id);
            return View("Detail", models);
        }

    }
}