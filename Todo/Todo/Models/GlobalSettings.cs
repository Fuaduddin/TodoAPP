using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;

namespace Todo.Models
{
    public static class GlobalSettings
    {
        public static HttpClient WebApiClient = new HttpClient();
        static GlobalSettings()
        {
            WebApiClient.BaseAddress = new Uri("https://localhost:44366/api/");
            WebApiClient.DefaultRequestHeaders.Clear();
            WebApiClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}