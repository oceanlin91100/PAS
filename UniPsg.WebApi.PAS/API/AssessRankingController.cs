using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UniPsg.Business.PAS.Assess;
using UniPsg.Model.PAS.ViewModels;

namespace UniPsg.Web.PAS.API
{
    public class AssessRankingController : ApiController
    {
        private RankingService service;

        public AssessRankingController()
        {
            service = new RankingService();
        }

        // GET: api/AssessRanking
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


        // GET: api/AssessRanking?Status={status}
        [HttpGet]
        public HttpResponseMessage GetByStatus(int status)
        {
            try
            {
                // 取得 AssessRanking 資料 
                var datas = service.GetByStatus(status);
                return Request.CreateResponse(HttpStatusCode.OK, datas);
            }
            catch (Exception ex)
            {
                // 發生錯誤，寫入Log，回傳失敗及錯誤訊息 
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }
        
        // GET: api/AssessRanking/5
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

        // POST: api/AssessRanking
        public HttpResponseMessage Post(AssessRankingViewModel models)
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

        // PUT: api/AssessRanking/5
        public HttpResponseMessage Put(AssessRankingViewModel models)
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

        // DELETE: api/AssessRanking/5
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
