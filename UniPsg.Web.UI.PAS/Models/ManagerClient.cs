using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using UniPsg.Model.PAS.ViewModels;

namespace UniPsg.Web.UI.PAS.Models
{
    public class ManagerClient
    {
        private string BaseUrl = System.Web.Configuration.WebConfigurationManager.AppSettings["BaseUrl"];

        public ManageUpdateViewModel FindReviewer(int projectId, string employeeNo)
        {
            try
            {
                var aa =" Manager ? projectId = " + projectId + " & employeeNo = " + employeeNo;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("Manager?projectId=" + projectId + "&employeeNo=" + employeeNo).Result;
                if (response.IsSuccessStatusCode)
                    return response.Content.ReadAsAsync<ManageUpdateViewModel>().Result;
                return null;
            }
            catch
            {
                return null;
            }

        }

        public ManageUpdateViewModel FindEmployee(int projectId, string employeeNo)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("Manager?projectId=" + projectId + "&employeeNo=" + employeeNo).Result;
                if (response.IsSuccessStatusCode)
                    return response.Content.ReadAsAsync<ManageUpdateViewModel>().Result;
                return null;
            }
            catch
            {
                return null;
            }

        }
    }
}