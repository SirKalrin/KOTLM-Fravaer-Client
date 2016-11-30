using System.Collections.Generic;
using System.Net.Http;
using ServiceGateways.Entities;
using ServiceGateways.Interfaces;

namespace ServiceGateways.ServiceGateways
{
    class AbsenceServiceGateway : AbstractServiceGateway, IServiceGateway<Absence, int>
    {
        public AbsenceServiceGateway() : base()
        {
            
        }

        public Absence Create(Absence t)
        {
            AddAuthorizationHeader();
            HttpResponseMessage response = Client.PostAsJsonAsync("api/absences", t).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<Absence>().Result;
            }
            return null;
        }

        public Absence Read(int id)
        {
            AddAuthorizationHeader();
            HttpResponseMessage response = Client.GetAsync($"api/absences/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<Absence>().Result;
            }
            return null;
        }

        public List<Absence> ReadAll()
        {
            AddAuthorizationHeader();
            HttpResponseMessage response = Client.GetAsync("api/absences/").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<List<Absence>>().Result;
            }
            return null;
        }

        public Absence Update(Absence t)
        {
            AddAuthorizationHeader();
            HttpResponseMessage response = Client.PutAsJsonAsync($"api/absences/{t.Id}", t).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<Absence>().Result;
            }
            return null;
        }

        public bool Delete(int id)
        {
            AddAuthorizationHeader();
            HttpResponseMessage response = Client.DeleteAsync($"api/absences/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<Absence>().Result != null;
            }
            return false;
        }
    }
}
