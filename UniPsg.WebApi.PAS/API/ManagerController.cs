using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UniPsg.Business.PAS.Assess;
using UniPsg.Model.PAS.ViewModels;

namespace UniPsg.WebApi.PAS.API
{
    public class ManagerController : ApiController
    {

        private PersonService service;
        private ReviewService service1;

        public ManagerController()
        {
            service = new PersonService();
            service1 = new ReviewService();
        }

        // GET: api/Manager
        [HttpGet]
        public HttpResponseMessage Get(int projectId, string employeeNo)
        {
            try
            {
                // 取得 ProjectReview 資料 
                var datas = service.GetManagerReview(projectId, employeeNo);
                return Request.CreateResponse(HttpStatusCode.OK, datas);
            }
            catch (Exception ex)
            {
                // 發生錯誤，寫入Log，回傳失敗及錯誤訊息 
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }


        // POST: api/Manager
        public HttpResponseMessage Post(ManageUpdateViewModel models, string status, string manager)
        {
            try
            {
                service1.ManagerUpdate(models, status, manager);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }

        // POST: api/Manager
        public HttpResponseMessage Post(ManageUpdateViewModel models, string status, string editer, string manager)
        {
            try
            {
                service.RevievwerUpdate(models, status, editer);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }
    }
}
