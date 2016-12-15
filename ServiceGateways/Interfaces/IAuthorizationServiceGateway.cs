using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ServiceGateways.Entities;

namespace ServiceGateways.Interfaces
{
    public interface IAuthorizationServiceGateway
    {
        HttpResponseMessage Login(string userName, string password);
        HttpResponseMessage Register(User user);

    }
}
