using System.Web.Mvc;
using UniPsg.Model.PAS.ViewModels;
using UniPsg.Web.UI.PAS.Models;

namespace UniPsg.Web.UI.PAS.Controllers
{
    public class AuditorController : Controller
    {   
        private AuditorClient client = new AuditorClient();
        // GET: Auditor
        public ActionResult Index()
        {
            ViewBag.ListClients = client.FindAll();
            return View();
        }
        // GET: Auditor/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: Auditor/Create    
        [HttpPost]
        public ActionResult Create(AuditorViewModel models)
        {
            client.Create(models);
            return RedirectToAction("Index");
        }

        // GET: Auditor/Edit/5  
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Edit(int id)
        {
            AuditorViewModel models = new AuditorViewModel();
            models = client.Find(id);
            return View("Edit", models);
        }

        // GET:  Auditor/Edit/5    
        [HttpPost]
        public ActionResult Edit(AuditorViewModel models)
        {
            client.Edit(models);
            return RedirectToAction("Index");
        }

        // GET:  Auditor/Delete/5    
        public ActionResult Delete(int id)
        {
            client.Delete(id);
            return RedirectToAction("Index");
        }

        // GET:  Auditor/Detail/5  
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Detail(int id)
        {
            AuditorViewModel models = new AuditorViewModel();
            models = client.Find(id);
            return View("Detail", models);
        }

    }
}