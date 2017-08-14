using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UniPsg.Business.PAS.Assess;
using UniPsg.Model.PAS.ViewModels;

namespace UniPsg.WebApi.PAS.API
{
    [RoutePrefix("api/Organization")]
    public class OrganizationController : ApiController
    {   
        private OrganizationService service;
       
        public OrganizationController()
        {
            service = new OrganizationService();
        }

        // GET： api/Organization
        [HttpGet]
        public HttpResponseMessage Get()
        {
            try
            {
                // 取得 Organization 資料 
                var datas = service.Get();
                return Request.CreateResponse(HttpStatusCode.OK, datas);
            }
            catch (Exception ex)
            {
                // 發生錯誤，寫入Log，回傳失敗及錯誤訊息 
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }

        // GET: api/Organization?status={status}
        [HttpGet]
        //[Route("GetByStatus")]
        public HttpResponseMessage GetByStatus(int status)
        {
            try
            {
                // 取得 Organization 資料 
                var datas = service.GetByStatus(status);
                return Request.CreateResponse(HttpStatusCode.OK, datas);
            }
            catch (Exception ex)
            {
                // 發生錯誤，寫入Log，回傳失敗及錯誤訊息 
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }

        // GET: api/Organization?projectId={projectId} 
        [HttpGet]
        public HttpResponseMessage Get(int projectId)
        {
            try
            {
                // 取得 Organization 資料 
                var datas = service.GetByProjectId(projectId);
                return Request.CreateResponse(HttpStatusCode.OK, datas);
            }
            catch (Exception ex)
            {
                // 發生錯誤，寫入Log，回傳失敗及錯誤訊息 
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }

        // GET: api/Organization?account={account}&manager={manager}
        [HttpGet]
        public HttpResponseMessage GetEmployeeNumber(string account, int manager)
        {
            try
            {
                // 取得 Organization 資料 
                var datas = service.GetEmployeeNumber(account, manager);
                return Request.CreateResponse(HttpStatusCode.OK, datas);
            }
            catch (Exception ex)
            {
                // 發生錯誤，寫入Log，回傳失敗及錯誤訊息 
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }

        //// GET: api/Organization?CurrPage={CurrPage}&PageSize={PageSize} 
        //[HttpGet]
        //public HttpResponseMessage Get(int CurrPage, int PageSize)
        //{
        //    try
        //    {
        //        // 總數量 
        //        int TotalRow = 0;
        //        // 向BLL取得資料 
        //        var datas = service.Get(CurrPage, PageSize, out TotalRow);
        //        // 回傳一個JSON Object 
        //        var Rvl = new { Total = TotalRow, Data = datas };
        //        return Request.CreateResponse(HttpStatusCode.OK, Rvl);
        //    }
        //    catch (Exception ex)
        //    {
        //        // 發生錯誤，寫入Log，回傳失敗及錯誤訊息 
        //        return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
        //    }
        //}

        // GET: api/Organization?projectId={projectId}&employeeNo={employeeNo} 
        [HttpGet]
        public HttpResponseMessage Get(int projectId, string employeeNo)
        {
            try
            {
                var data = service.Get(projectId, employeeNo);
                return Request.CreateResponse(HttpStatusCode.OK, data);

            }
            catch (Exception ex)
            {
                // 發生錯誤，寫入Log，回傳失敗及錯誤訊息。
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());

            }
        }

        // POST: api/Organization
        public HttpResponseMessage Post(OrganizationViewModel models)
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

        //POST: api/Organization/Imports
        [HttpPost, Route("Imports")]
        public HttpResponseMessage Post(OrganizationImportModel model)
        {
            try
            {
                service.AddAll(model.ProjectId,model.Creator);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }

        // Get: api/Organization?projectId={projectId}&creator={creator}
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

        // PUT: api/Organization/5
        public HttpResponseMessage Put(OrganizationViewModel models)
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

        // DELETE: api/Organization?projectId={projectId}&employeeNo={employeeNo} 
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

        // Get: api/Organization/Branches
        [HttpGet, Route("Branches")]
        public HttpResponseMessage GetBranches()
        {
            try
            {
                var datas = service.GetBranches();
                return Request.CreateResponse(HttpStatusCode.OK, datas);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }

        // Get: api/Organization/Departments
        [HttpGet, Route("Departments")]
        public HttpResponseMessage GetDepartments()
        {
            try
            {
                var datas = service.GetDepartments();
                return Request.CreateResponse(HttpStatusCode.OK, datas);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }

        // GET: api/Organization/Account?reviewer={reviewer}&projectId={projectId}
        [HttpGet]
        [Route("Account")]
        public HttpResponseMessage GetManagerAccount(string reviewer, int projectId)
        {
            try
            {
                // 取得 Organization 資料 
                var datas = service.GetManagerAccount(reviewer, projectId);
                return Request.CreateResponse(HttpStatusCode.OK, datas);
            }
            catch (Exception ex)
            {
                // 發生錯誤，寫入Log，回傳失敗及錯誤訊息 
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }

        // GET: api/Organization/Account?reviewer={reviewer}&projectId={projectId}
        [HttpGet]
        [Route("BackAccount")]
        public HttpResponseMessage GetBackAccount(string reviewer, int projectId)
        {
            try
            {
                // 取得 Organization 資料 
                var datas = service.GetBackAccount(reviewer, projectId);
                return Request.CreateResponse(HttpStatusCode.OK, datas);
            }
            catch (Exception ex)
            {
                // 發生錯誤，寫入Log，回傳失敗及錯誤訊息 
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }
    }
}
