using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using UniPsg.Model.PAS.ViewModels;

namespace UniPsg.Web.UI.PAS.Models
{
    public class ProjectScoreClient
    {
        private string BaseUrl = System.Web.Configuration.WebConfigurationManager.AppSettings["BaseUrl"];

        public IEnumerable<ProjectScoreViewModel> FindAll()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("ProjectScore").Result;
                if (response.IsSuccessStatusCode)
                    return response.Content.ReadAsAsync<IEnumerable<ProjectScoreViewModel>>().Result;
                return null;
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<ProjectScoreViewModel> FindByStatus(int status)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("ProjectScore?status=" + status).Result;
                if (response.IsSuccessStatusCode)
                    return response.Content.ReadAsAsync<IEnumerable<ProjectScoreViewModel>>().Result;
                return null;
            }
            catch
            {
                return null;
            }
        }

        public ProjectScoreViewModel Find(string id)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("ProjectScore/" + id).Result;

                if (response.IsSuccessStatusCode)
                    return response.Content.ReadAsAsync<ProjectScoreViewModel>().Result;
                return null;
            }
            catch (Exception ex)
            {
                // 發生錯誤，寫入Log，回傳失敗及錯誤訊息。
                string a = ex.Message.ToString();
                return null;
            }
        }

        //public ProjectScoreViewModel FindBack(string projectId, string employeeNo, string reviewer )
        //{
        //    try
        //    {
        //        HttpClient client = new HttpClient();
        //        client.BaseAddress = new Uri(BaseUrl);
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        HttpResponseMessage response = client.GetAsync("ProjectScore/" + id).Result;

        //        if (response.IsSuccessStatusCode)
        //            return response.Content.ReadAsAsync<ProjectScoreViewModel>().Result;
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        // 發生錯誤，寫入Log，回傳失敗及錯誤訊息。
        //        string a = ex.Message.ToString();
        //        return null;
        //    }
        //}

        public bool Create(ProjectScoreViewModel model)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.PostAsJsonAsync("ProjectScore", model).Result;
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
        public bool Edit(ProjectScoreViewModel model)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.PutAsJsonAsync("ProjectScore/" + model.Id, model).Result;
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
        public bool Delete(string id)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.DeleteAsync("ProjectScore/" + id).Result;
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}