using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using UniPsg.Model.PAS.ViewModels;

namespace UniPsg.Web.UI.PAS.Models
{
    public class AssessPersonClient
    {
        private string BaseUrl = System.Web.Configuration.WebConfigurationManager.AppSettings["BaseUrl"];
        public IEnumerable<AssessPersonViewModel> FindAll()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("AssessPerson").Result;
                if (response.IsSuccessStatusCode)
                    return response.Content.ReadAsAsync<IEnumerable<AssessPersonViewModel>>().Result;
                return null;
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<AssessPersonViewModel> FindByProjectId(int projectId)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("AssessPerson?projectId=" + projectId).Result;
                if (response.IsSuccessStatusCode)
                    return response.Content.ReadAsAsync<IEnumerable<AssessPersonViewModel>>().Result;
                return null;
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<AssessPersonViewModel> FindByStatus(int status)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("AssessPerson?status=" + status).Result;
                if (response.IsSuccessStatusCode)
                    return response.Content.ReadAsAsync<IEnumerable<AssessPersonViewModel>>().Result;
                return null;
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<ReviewerViewModel> FindByReviewer(string reviewer, int manager)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("AssessPerson?reviewer=" + reviewer + "&manager=" + manager).Result;
                if (response.IsSuccessStatusCode)
                    return response.Content.ReadAsAsync<IEnumerable<ReviewerViewModel>>().Result;
                return null;
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<ReviewerViewModel> FindByManager(string reviewer, string manager)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("AssessPerson?reviewer=" + reviewer + "&manager="+ manager).Result;
                if (response.IsSuccessStatusCode)
                    return response.Content.ReadAsAsync<IEnumerable<ReviewerViewModel>>().Result;
                return null;
            }
            catch
            {
                return null;
            }
        }

        public ReviewerUpdateViewModel FindReviewer(int projectId, string employeeNo, string reviewer)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("AssessPerson?projectId=" + projectId + "&employeeNo=" + employeeNo + "&reviewer=" + reviewer).Result;
                if (response.IsSuccessStatusCode)
                    return response.Content.ReadAsAsync<ReviewerUpdateViewModel>().Result;
                return null;
            }
            catch
            {
                return null;
            }
       
        }

        public AssessPersonViewModel Find(int projectId, string employeeNo)
        {
            try
            {
                var aa = "AssessPerson?projectId=" + projectId + "&employeeNo=" + employeeNo;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("AssessPerson?projectId=" + projectId +"&employeeNo=" + employeeNo).Result;

                if (response.IsSuccessStatusCode)
                    return response.Content.ReadAsAsync<AssessPersonViewModel>().Result;
                return null;
            }
            catch (Exception ex)
            {
                // 發生錯誤，寫入Log，回傳失敗及錯誤訊息。
                string a = ex.Message.ToString();
                return null;
            }
        }

        public bool Create(AssessPersonViewModel model)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.PostAsJsonAsync("AssessPerson", model).Result;
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }



        public bool Imposts(OrganizationImportModel model)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("AssessPerson?projectId=" + model.ProjectId + "&creator=" + model.Creator).Result;
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
       
        public bool Edit(AssessPersonViewModel model)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.PutAsJsonAsync("AssessPerson/" + model.ProjectId , model).Result;
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
        public bool Delete(int projectId, string employeeNo)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.DeleteAsync("AssessPerson?projectId=" + projectId + "&employeeNo=" + employeeNo).Result;
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(ManageUpdateViewModel model, string status, string editer, string manager)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.PostAsJsonAsync("Manager?status=" + status + "&editer=" + editer + "&manager=" + manager , model).Result;
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}