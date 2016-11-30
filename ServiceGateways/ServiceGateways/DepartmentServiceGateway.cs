using System.Collections.Generic;
using System.Net.Http;
using ServiceGateways.Entities;
using ServiceGateways.Interfaces;

namespace ServiceGateways.ServiceGateways
{
    class DepartmentServiceGateway : AbstractServiceGateway, IServiceGateway<Department, int>
    {
        public DepartmentServiceGateway() : base()
        {
            
        }

        public Department Create(Department t)
        {
            AddAuthorizationHeader();
            HttpResponseMessage response = Client.PostAsJsonAsync("api/departments", t).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<Department>().Result;
            }
            return null;
        }

        public Department Read(int id)
        {
            AddAuthorizationHeader();
            HttpResponseMessage response = Client.GetAsync($"api/departments/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<Department>().Result;
            }
            return null;
        }

        public List<Department> ReadAll()
        {
            AddAuthorizationHeader();
            HttpResponseMessage response = Client.GetAsync("api/departments/").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<List<Department>>().Result;
            }
            return null;
        }

        public Department Update(Department t)
        {
            AddAuthorizationHeader();
            HttpResponseMessage response = Client.PutAsJsonAsync($"api/departments/{t.Id}", t).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<Department>().Result;
            }
            return null;
        }

        public bool Delete(int id)
        {
            AddAuthorizationHeader();
            HttpResponseMessage response = Client.DeleteAsync($"api/departments/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<Department>().Result != null;
            }
            return false;
        }
    }
}
