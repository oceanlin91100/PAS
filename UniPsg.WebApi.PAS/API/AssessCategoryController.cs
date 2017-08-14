using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UniPsg.Business.PAS.Assess;
using UniPsg.Model.PAS.ViewModels;

namespace UniPsg.Web.PAS.API
{
    public class AssessCategoryController : ApiController
    {
        private CategoryService service;

        public AssessCategoryController()
        {
            service = new CategoryService();
        }

        // GET: api/AssessCategory
        [HttpGet]
        public HttpResponseMessage Get()
        {
            try
            {
                // 取得Category資料 
                var datas = service.Get();
                return Request.CreateResponse(HttpStatusCode.OK, datas);
            }
            catch (Exception ex)
            {
                // 發生錯誤，寫入Log，回傳失敗及錯誤訊息 
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }

        // GET: api/AssessCategory
        [HttpGet]
        public HttpResponseMessage GetByStatus(int status)
        {
            try
            {
                // 取得Category資料 
                var datas = service.GetByStatus(status);
                return Request.CreateResponse(HttpStatusCode.OK, datas);
            }
            catch (Exception ex)
            {
                // 發生錯誤，寫入Log，回傳失敗及錯誤訊息 
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }
        
        // GET: api/AssessCategory/5
        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                var data = service.Get(id);
                return Request.CreateResponse(HttpStatusCode.OK, data);

            }
            catch (Exception ex)
            {
                // 發生錯誤，寫入Log，回傳失敗及錯誤訊息。
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());

            }
        }

        // POST: api/AssessCategory
        public HttpResponseMessage Post(AssessCategoryViewModel models)
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

        // PUT: api/AssessCategory/5
        public HttpResponseMessage Put(AssessCategoryViewModel models)
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

        // DELETE: api/AssessCategory/5
        public HttpResponseMessage Delete(int id)
        { try
            {
                service.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
           
        }
    }
}
