using System.Collections.Generic;
using System.Net.Http;
using ServiceGateways.Entities;
using ServiceGateways.Interfaces;

namespace ServiceGateways.ServiceGateways
{
    public class EmployeeServiceGateway : AbstractServiceGateway, IServiceGateway<Employee, int>
    {
        public EmployeeServiceGateway() : base()
        {
            
        }

        public Employee Create(Employee t)
        {
            AddAuthorizationHeader();
            HttpResponseMessage response = Client.PostAsJsonAsync("api/employees", t).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<Employee>().Result;
            }
            return null;
        }

        public Employee Read(int id)
        {
            AddAuthorizationHeader();
            HttpResponseMessage response = Client.GetAsync($"api/employees/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<Employee>().Result;
            }
            return null;
        }

        public List<Employee> ReadAll()
        {
            AddAuthorizationHeader();
            HttpResponseMessage response = Client.GetAsync("api/employees/").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<List<Employee>>().Result;
            }
            return null;
        }

        public Employee Update(Employee t)
        {
            AddAuthorizationHeader();
            HttpResponseMessage response = Client.PutAsJsonAsync($"api/employees/{t.Id}", t).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<Employee>().Result;
            }
            return null;
        }

        public bool Delete(int id)
        {
            AddAuthorizationHeader();
            HttpResponseMessage response = Client.DeleteAsync($"api/employees/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<Employee>().Result != null;
            }
            return false;
        }
    }
}
