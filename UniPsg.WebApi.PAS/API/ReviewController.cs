using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UniPsg.Business.PAS.Assess;
using UniPsg.Model.PAS.ViewModels;

namespace UniPsg.WebApi.PAS.API
{
    public class ReviewController : ApiController
    {
        private ReviewService service;

        public ReviewController()
        {
            service = new ReviewService();
        }

        // POST: api/Review
        public HttpResponseMessage Post(List<ProjectReviewViewModel> models, string status, string manager)
        {
            try
            {
                service.Update(models, status, manager);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }


    }
}
