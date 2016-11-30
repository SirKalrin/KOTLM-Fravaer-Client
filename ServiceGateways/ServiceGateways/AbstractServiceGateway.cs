using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Configuration;

namespace ServiceGateways.ServiceGateways
{
    abstract class AbstractServiceGateway
    {
        protected HttpClient Client = new HttpClient();

        protected AbstractServiceGateway()
        {
            Client.BaseAddress = new Uri(WebConfigurationManager.AppSettings["RestAPIBaseAddress"]);
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        protected void AddAuthorizationHeader()
        {
            if (HttpContext.Current.Session["token"] != null)
            {
                string token = HttpContext.Current.Session["token"].ToString();
                Client.DefaultRequestHeaders.Remove("Authorization");
                Client.DefaultRequestHeaders.Add("Authorization", "Bearer" + token);
            }
        }

    }
}
