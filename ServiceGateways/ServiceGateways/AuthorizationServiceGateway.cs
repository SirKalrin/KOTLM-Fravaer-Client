using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Configuration;
using Newtonsoft.Json.Linq;

namespace ServiceGateways.ServiceGateways
{
    class AuthorizationServiceGateway
    {
        private HttpClient _client = new HttpClient();

        public AuthorizationServiceGateway()
        {
            string baseAddress = WebConfigurationManager.AppSettings["RestAPIBaseAddress"];
            _client.BaseAddress = new Uri(baseAddress);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public HttpResponseMessage Login(string userName, string password)
        {
            //Setup login data
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", userName),
                new KeyValuePair<string, string>("password", password)
            });

            //Request token
            HttpResponseMessage response = _client.PostAsync("/token", formContent).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseJson = response.Content.ReadAsStringAsync().Result;
                var jObject = JObject.Parse(responseJson);
                string token = jObject.GetValue("access_token").ToString();
                HttpContext.Current.Session["token"] = token;
            }

            return response;
        }
    }
}
