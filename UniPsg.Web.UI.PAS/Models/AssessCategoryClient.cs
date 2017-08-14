using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using UniPsg.Model.PAS.ViewModels;

namespace UniPsg.Web.UI.PAS.Models
{
    public class AssessCategoryClient
    {   
        private string  BaseUrl = System.Web.Configuration.WebConfigurationManager.AppSettings["BaseUrl"];
        public IEnumerable<AssessCategoryViewModel> FindAll()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("AssessCategory").Result;
                if (response.IsSuccessStatusCode)
                    return response.Content.ReadAsAsync<IEnumerable<AssessCategoryViewModel>>().Result;
                return null;
            }
            catch
            {
                return null;
            }
        }
        
        public IEnumerable<AssessCategoryViewModel> FindByStatus(int status)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("AssessCategory?status=" + status).Result;
                if (response.IsSuccessStatusCode)
                    return response.Content.ReadAsAsync<IEnumerable<AssessCategoryViewModel>>().Result;
                return null;
            }
            catch
            {
                return null;
            }
        }

        public AssessCategoryViewModel Find(int id)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("AssessCategory/" + id).Result;

                if (response.IsSuccessStatusCode)
                    return response.Content.ReadAsAsync<AssessCategoryViewModel>().Result;
                return null;
            }
            catch (Exception ex)
            {
                // 發生錯誤，寫入Log，回傳失敗及錯誤訊息。
                string  a = ex.Message.ToString();
                return null;
            }
        }

        public bool Create(AssessCategoryViewModel model)
        {  
            try  
            {  
                HttpClient client = new HttpClient();  
                client.BaseAddress = new Uri(BaseUrl);  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));  
                HttpResponseMessage response = client.PostAsJsonAsync("AssessCategory", model).Result;  
                return response.IsSuccessStatusCode;  
            }  
            catch  
            {  
                return false;  
            }  
        }  
        public bool Edit(AssessCategoryViewModel model)
        {  
            try  
            {  
                HttpClient client = new HttpClient();  
                client.BaseAddress = new Uri(BaseUrl);  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));  
                HttpResponseMessage response = client.PutAsJsonAsync("AssessCategory/" + model.Id, model).Result;  
                return response.IsSuccessStatusCode;  
            }  
            catch  
            {  
                return false;  
            }  
        }  
        public bool Delete(int id)
        {  
            try  
            {  
                HttpClient client = new HttpClient();  
                client.BaseAddress = new Uri(BaseUrl);  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));  
                HttpResponseMessage response = client.DeleteAsync("AssessCategory/" + id).Result;  
                return response.IsSuccessStatusCode;  
            }  
            catch  
            {  
                return false;  
            }  
        }  
    }
}