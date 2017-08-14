using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UniPsg.Business.PAS.Assess;
using UniPsg.Model.PAS.ViewModels;

namespace UniPsg.WebApi.PAS.API
{
    public class ProjectReviewController : ApiController
    {   
        private ReviewService service;
        public ProjectReviewController()
        {
            service = new ReviewService();
        }

        // GET: api/ProjectReview
        [HttpGet]
        public HttpResponseMessage Get()
        {
            try
            {
                // 取得 ProjectReview 資料 
                var datas = service.Get();
                return Request.CreateResponse(HttpStatusCode.OK, datas);
            }
            catch (Exception ex)
            {
                // 發生錯誤，寫入Log，回傳失敗及錯誤訊息 
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }

        // GET: api/ProjectReview?Status={status}
        [HttpGet]
        public HttpResponseMessage GetByStatus(int status)
        {
            try
            {
                // 取得 ProjectReview 資料 
                var datas = service.GetByStatus(status);
                return Request.CreateResponse(HttpStatusCode.OK, datas);
            }
            catch (Exception ex)
            {
                // 發生錯誤，寫入Log，回傳失敗及錯誤訊息 
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }

        // GET: api/ProjectReview?ProjectId={projectId}&EmployeeNo={employeeNo}
        [HttpGet]
        public HttpResponseMessage Get(int projectId, string employeeNo)
        {
            try
            {
                // 取得 ProjectReview 資料 
                var datas = service.Get(projectId, employeeNo);
                return Request.CreateResponse(HttpStatusCode.OK, datas);
            }
            catch (Exception ex)
            {
                // 發生錯誤，寫入Log，回傳失敗及錯誤訊息 
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }

        // GET: api/ProjectReview?ProjectId={projectId}&EmployeeNo={employeeNo}
        [HttpGet]
        public HttpResponseMessage Get(int projectId, string employeeNo, string reviewer, string editer, int status, string manager)
        {
            try
            {   
                service.Review(projectId, employeeNo, reviewer, editer, manager);
                var data = "OK";
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }        
        }

        // GET: api/ProjectReview?projectId={projectId}&employeeNo={employeeNo}&reviewer={reviewer}
        [HttpGet]
        public HttpResponseMessage Get(int projectId, string employeeNo, string reviewer)
        {
            try
            {
                var data = service.GetReviewByManager(projectId, employeeNo, reviewer);
                return Request.CreateResponse(HttpStatusCode.OK, data);

            }
            catch (Exception ex)
            {
                // 發生錯誤，寫入Log，回傳失敗及錯誤訊息。
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());

            }
        }

        // GET: api/ProjectReview/5
        [HttpGet]
        public HttpResponseMessage Get(string id)
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

        // POST: api/ProjectReview
        public HttpResponseMessage Post(ProjectReviewViewModel models)
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

       

        // PUT: api/ProjectReview/5
        public HttpResponseMessage Put(ProjectReviewViewModel models)
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

        // DELETE: api/ProjectReview/5
        public HttpResponseMessage Delete(string id)
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
