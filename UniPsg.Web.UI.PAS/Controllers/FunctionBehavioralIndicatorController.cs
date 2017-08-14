using System.Web.Mvc;
using UniPsg.Model.PAS.ViewModels;
using UniPsg.Web.UI.PAS.Models;

namespace UniPsg.Web.UI.PAS.Controllers
{
    public class FunctionBehavioralIndicatorController : Controller
    {
        // GET: FunctionBehavioralIndicator
        public ActionResult Index()
        {
            FunctionBehavioralIndicatorClient client = new FunctionBehavioralIndicatorClient();
            ViewBag.ListClients = client.FindAll();
            return View();
        }
        // GET: FunctionBehavioralIndicator/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: FunctionBehavioralIndicator/Create    
        [HttpPost]
        public ActionResult Create(FunctionBehavioralIndicatorViewModel models)
        {
            FunctionBehavioralIndicatorClient client = new FunctionBehavioralIndicatorClient();
            client.Create(models);
            return RedirectToAction("Index");
        }


        // GET: FunctionBehavioralIndicator/Edit/5  
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Edit(int id)
        {
            FunctionBehavioralIndicatorClient client = new FunctionBehavioralIndicatorClient();
            FunctionBehavioralIndicatorViewModel models = new FunctionBehavioralIndicatorViewModel();
            models = client.Find(id);
            return View("Edit", models);
        }

        // GET: FunctionBehavioralIndicator/Edit/5    
        [HttpPost]
        public ActionResult Edit(FunctionBehavioralIndicatorViewModel models)
        {
            FunctionBehavioralIndicatorClient client = new FunctionBehavioralIndicatorClient();
            client.Edit(models);
            return RedirectToAction("Index");
        }

        // GET: FunctionBehavioralIndicator/Delete/5    
        public ActionResult Delete(int id)
        {
            FunctionBehavioralIndicatorClient client = new FunctionBehavioralIndicatorClient();
            client.Delete(id);
            return RedirectToAction("Index");
        }

    }
}