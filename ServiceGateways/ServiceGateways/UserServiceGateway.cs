using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ServiceGateways.Entities;
using ServiceGateways.Interfaces;

namespace ServiceGateways.ServiceGateways
{
    public class UserServiceGateway : AbstractServiceGateway, IServiceGateway<User, int>
    {
        public UserServiceGateway() : base()
        {
            
        }
        public User Create(User t)
        {
            AddAuthorizationHeader();
            HttpResponseMessage response = Client.PostAsJsonAsync("api/users", t).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<User>().Result;
            }
            return null;
        }

        public User Read(int id)
        {
            AddAuthorizationHeader();
            HttpResponseMessage response = Client.GetAsync($"api/users/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<User>().Result;
            }
            return null;
        }

        public List<User> ReadAll()
        {
            AddAuthorizationHeader();
            HttpResponseMessage response = Client.GetAsync("api/users/").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<List<User>>().Result;
            }
            return null;
        }

        public User Update(User t)
        {
            AddAuthorizationHeader();
            HttpResponseMessage response = Client.PutAsJsonAsync($"api/users/{t.Id}", t).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<User>().Result;
            }
            return null;
        }

        public bool Delete(int id)
        {
            AddAuthorizationHeader();
            HttpResponseMessage response = Client.DeleteAsync($"api/users/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<User>().Result != null;
            }
            return false;
        }
    }
}
