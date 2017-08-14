using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UniPsg.Business.PAS.Assess;
using UniPsg.Model.PAS.ViewModels;

namespace UniPsg.WebApi.PAS.API
{
    public class AssessPersonController : ApiController
    {
        // GET: api/AssessPerson
        private PersonService service;
        private OrganizationService service1;

        public AssessPersonController()
        {
            service = new PersonService();
            service1 = new OrganizationService();
        }

        // GET: api/AssessPerson
        [HttpGet]
        public HttpResponseMessage Get()
        {
            try
            {
                // 取得 AssessPerson 資料 
                var datas = service.Get();
                return Request.CreateResponse(HttpStatusCode.OK, datas);
            }
            catch (Exception ex)
            {
                // 發生錯誤，寫入Log，回傳失敗及錯誤訊息 
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }

        // GET: api/AssessPerson
        [HttpGet]
        public HttpResponseMessage Get(int projectId)
        {
            try
            {
                // 取得 AssessPerson 資料 
                var datas = service.GetByProjectId(projectId);
                return Request.CreateResponse(HttpStatusCode.OK, datas);
            }
            catch (Exception ex)
            {
                // 發生錯誤，寫入Log，回傳失敗及錯誤訊息 
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }

        // GET: api/AssessPerson
        [HttpGet]
        public HttpResponseMessage GetByStatus(int status)
        {
            try
            {
                // 取得 AssessPerson 資料 
                var datas = service.GetByStatus(status);
                return Request.CreateResponse(HttpStatusCode.OK, datas);
            }
            catch (Exception ex)
            {
                // 發生錯誤，寫入Log，回傳失敗及錯誤訊息 
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }

        // GET: api/AssessPerson
        [HttpGet]
        public HttpResponseMessage GetByReviewer(string reviewer, int manager)
        {
            try
            {
                // 取得 AssessPerson 資料               
                var datas = service.GetByReviewer(reviewer, manager);
                return Request.CreateResponse(HttpStatusCode.OK, datas);
            }
            catch (Exception ex)
            {
                // 發生錯誤，寫入Log，回傳失敗及錯誤訊息 
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }

        // GET: api/AssessPerson
        [HttpGet]
        public HttpResponseMessage GetReviewer(int projectId, string employeeNo, string reviewer)
        {
            try
            {
                // 取得 AssessPerson 資料               
                var datas = service.GetReviewer(projectId, employeeNo);
                return Request.CreateResponse(HttpStatusCode.OK, datas);
            }
            catch (Exception ex)
            {
                // 發生錯誤，寫入Log，回傳失敗及錯誤訊息 
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }
        
        // GET: api/AssessPerson/5
        public HttpResponseMessage Get(int projectId, string employeeNo)
        {
            try
            {
                var data = service.Get(projectId,employeeNo);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                // 發生錯誤，寫入Log，回傳失敗及錯誤訊息。
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }

        // GET: api/AssessPerson
        public HttpResponseMessage GetImports(int projectId, string creator)
        {
            try
            {
                service.AddAll(projectId, creator);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }

        // POST: api/AssessPerson
        public HttpResponseMessage Post(AssessPersonViewModel models)
        {
            try
            {
                service.Add(models);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }

        // PUT: api/AssessPerson/5
        public HttpResponseMessage Put(AssessPersonViewModel models)
        {
            try
            {
                service.Save(models);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }

        // DELETE: api/AssessPerson/5
        public HttpResponseMessage Delete(int projectId, string employeeNo)
        {
            try
            {
                service.Delete(projectId, employeeNo);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }
    }
}
