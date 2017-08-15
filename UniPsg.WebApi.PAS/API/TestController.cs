using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UniPsg.Data.AS400.PAS.NEW;


namespace UniPsg.WebApi.PAS.API
{
    public class TestController : ApiController
    {
        DB2_Data_Access db = new DB2_Data_Access();
        
        public HttpResponseMessage Get(string table)
        {
            try
            {
                var result = db.Select_Table(table);
                return new HttpResponseMessage() {
                    Content = new StringContent(result, System.Text.Encoding.UTF8, "application/json")
                };
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }


    }
}
