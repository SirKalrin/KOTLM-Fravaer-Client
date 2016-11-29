using System.Collections.Generic;
using System.Net.Http;
using ServiceGateways.Entities;
using ServiceGateways.Interfaces;

namespace ServiceGateways.ServiceGateways
{
    public class AdminServiceGateway : AbstractServiceGateway, IServiceGateway<Admin, int>
    {
        public AdminServiceGateway() : base()
        {
            
        }

        public Admin Create(Admin t)
        {
            AddAuthorizationHeader();
            HttpResponseMessage response = Client.PostAsJsonAsync("api/admins", t).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<Admin>().Result;
            }
            return null;
        }

        public Admin Read(int id)
        {
            AddAuthorizationHeader();
            HttpResponseMessage response = Client.GetAsync($"api/admins/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<Admin>().Result;
            }
            return null;
        }

        public List<Admin> ReadAll()
        {
            AddAuthorizationHeader();
            HttpResponseMessage response = Client.GetAsync("api/admins/").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<List<Admin>>().Result;
            }
            return null;
        }

        public Admin Update(Admin t)
        {
            AddAuthorizationHeader();
            HttpResponseMessage response = Client.PutAsJsonAsync($"api/admins/{t.Id}", t).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<Admin>().Result;
            }
            return null;
        }

        public bool Delete(int id)
        {
            AddAuthorizationHeader();
            HttpResponseMessage response = Client.DeleteAsync($"api/admins/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<Admin>().Result != null;
            }
            return false;
        }
    }
}
