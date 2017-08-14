using System.Web.Mvc;
using UniPsg.Model.PAS.ViewModels;
using UniPsg.Web.UI.PAS.Models;

namespace UniPsg.Web.UI.PAS.Controllers
{
    public class FlowLogController : Controller
    {
        private FlowLogClient client = new FlowLogClient();
       
        // GET: FlowLog
        public ActionResult Index()
        {

            ViewBag.ListClients = client.FindAll();
            return View();
        }
        // GET: FlowLog/Create
        [HttpGet]
        public ActionResult Create()
        {            
            return View("Create");
        }

        // POST: FlowLog/Create    
        [HttpPost]
        public ActionResult Create(FlowLogViewModel models)
        {

            client.Create(models);
            return RedirectToAction("Index");
        }


        // GET: FlowLog/Edit/5  
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Edit(int id)
        {
            FlowLogViewModel models = new FlowLogViewModel();
            models = client.Find(id);
            return View("Edit", models);
        }

        // GET: FlowLog/Edit/5    
        [HttpPost]
        public ActionResult Edit(FlowLogViewModel models)
        {
            client.Edit(models);
            return RedirectToAction("Index");
        }

        // GET: FlowLog/Delete/5    
        public ActionResult Delete(int id)
        {

            client.Delete(id);
            return RedirectToAction("Index");
        }

        // GET: FlowLog/Detail/5  
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Detail(int id)
        {
            FlowLogViewModel models = new FlowLogViewModel();
            models = client.Find(id);
            return View("Detail", models);
        }

    }
}