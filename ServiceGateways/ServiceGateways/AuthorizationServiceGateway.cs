using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.ClientServices;
using System.Web.Configuration;
using System.Web.Security;
using Newtonsoft.Json.Linq;
using ServiceGateways.Entities;
using ServiceGateways.Facade;
using ServiceGateways.Interfaces;

namespace ServiceGateways.ServiceGateways
{
    class AuthorizationServiceGateway : AbstractServiceGateway, IAuthorizationServiceGateway
    {
        private IServiceGateway<User, int> _userGateway = new ServiceGatewayFacade().GetUserServiceGateway();

        public AuthorizationServiceGateway() : base()
        {
          
        }

        public HttpResponseMessage Register(User user)
        {
            AddAuthorizationHeader();
            HttpResponseMessage response = Client.PostAsJsonAsync("api/Account/Register", user).Result;
            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            return null;
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
            HttpResponseMessage response = Client.PostAsync("/token", formContent).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseJson = response.Content.ReadAsStringAsync().Result;
                var jObject = JObject.Parse(responseJson);
                string token = jObject.GetValue("access_token").ToString();
                HttpContext.Current.Session["token"] = token;
                HttpContext.Current.Session["currentUser"] =
                    _userGateway.ReadAll().FirstOrDefault(x => x.UserName == userName);

            }
            return response;
        }
    }
}
