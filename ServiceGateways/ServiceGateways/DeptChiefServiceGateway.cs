using System.Collections.Generic;
using System.Net.Http;
using ServiceGateways.Entities;
using ServiceGateways.Interfaces;

namespace ServiceGateways.ServiceGateways
{
    public class DeptChiefServiceGateway : AbstractServiceGateway, IServiceGateway<DeptChief, int>
    {
        public DeptChiefServiceGateway() : base()
        {
            
        }

        public DeptChief Create(DeptChief t)
        {
            AddAuthorizationHeader();
            HttpResponseMessage response = Client.PostAsJsonAsync("api/deptchiefs", t).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<DeptChief>().Result;
            }
            return null;
        }

        public DeptChief Read(int id)
        {
            AddAuthorizationHeader();
            HttpResponseMessage response = Client.GetAsync($"api/deptchiefs/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<DeptChief>().Result;
            }
            return null;
        }

        public List<DeptChief> ReadAll()
        {
            AddAuthorizationHeader();
            HttpResponseMessage response = Client.GetAsync("api/deptchiefs/").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<List<DeptChief>>().Result;
            }
            return null;
        }

        public DeptChief Update(DeptChief t)
        {
            AddAuthorizationHeader();
            HttpResponseMessage response = Client.PutAsJsonAsync($"api/deptchiefs/{t.Id}", t).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<DeptChief>().Result;
            }
            return null;
        }

        public bool Delete(int id)
        {
            AddAuthorizationHeader();
            HttpResponseMessage response = Client.DeleteAsync($"api/deptchiefs/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<DeptChief>().Result != null;
            }
            return false;
        }
    }
}
