using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UniPsg.Business.PAS.Function;
using UniPsg.Model.PAS.ViewModels;

namespace UniPsg.Web.PAS.API
{
    public class FunctionBehavioralIndicatorController : ApiController
    {
        private BehavioralIndicatorService service;
        public FunctionBehavioralIndicatorController()
        {
            service = new BehavioralIndicatorService();
        }
        // GET: api/FunctionBehavioralIndicator
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
        
        // GET: api/FunctionBehavioralIndicator/5
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

        // POST: api/FunctionBehavioralIndicator
        public HttpResponseMessage Post(FunctionBehavioralIndicatorViewModel models)
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

        // PUT: api/FunctionBehavioralIndicator/5
        public HttpResponseMessage Put(FunctionBehavioralIndicatorViewModel models)
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

        // DELETE: api/FunctionBehavioralIndicator/5
        public HttpResponseMessage Delete(int id)
        {
            try
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
