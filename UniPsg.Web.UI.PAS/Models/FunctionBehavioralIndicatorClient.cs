﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using UniPsg.Model.PAS.ViewModels;

namespace UniPsg.Web.UI.PAS.Models
{
    public class FunctionBehavioralIndicatorClient
    {
        private string BaseUrl = System.Web.Configuration.WebConfigurationManager.AppSettings["BaseUrl"];

        public IEnumerable<FunctionBehavioralIndicatorViewModel> FindAll()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("FunctionBehavioralIndicator").Result;
                if (response.IsSuccessStatusCode)
                    return response.Content.ReadAsAsync<IEnumerable<FunctionBehavioralIndicatorViewModel>>().Result;
                return null;
            }
            catch
            {
                return null;
            }
        }

        public FunctionBehavioralIndicatorViewModel Find(int id)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("FunctionBehavioralIndicator/" + id).Result;

                if (response.IsSuccessStatusCode)
                    return response.Content.ReadAsAsync<FunctionBehavioralIndicatorViewModel>().Result;
                return null;
            }
            catch (Exception ex)
            {
                // 發生錯誤，寫入Log，回傳失敗及錯誤訊息。
                string a = ex.Message.ToString();
                return null;
            }
        }

        public bool Create(FunctionBehavioralIndicatorViewModel model)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.PostAsJsonAsync("FunctionBehavioralIndicator", model).Result;
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
        public bool Edit(FunctionBehavioralIndicatorViewModel model)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.PutAsJsonAsync("FunctionBehavioralIndicator/" + model.Id, model).Result;
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
                HttpResponseMessage response = client.DeleteAsync("FunctionBehavioralIndicator/" + id).Result;
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}