using System.Net.Http;
using ServiceGateways.Entities;
using ServiceGateways.Interfaces;
using ServiceGateways.ServiceGateways;

namespace ServiceGateways.Facade
{
    public class ServiceGatewayFacade
    {
        private IServiceGateway<Absence, int> _absenceGateway;
        private IServiceGateway<Department, int> _departmentGateway;
        private IServiceGateway<User, int> _userGateway;
        private IAuthorizationServiceGateway _authorizationServiceGateway;

        public IServiceGateway<Absence, int> GetAbsenceServiceGateway()
        {
            return _absenceGateway ?? (_absenceGateway = new AbsenceServiceGateway());
        }
     
        public IServiceGateway<Department, int> GetDepartmentServiceGateway()
        {
            return _departmentGateway ?? (_departmentGateway = new DepartmentServiceGateway());
        }
        
        public IServiceGateway<User, int> GetUserServiceGateway()
        {
            return _userGateway ?? (_userGateway = new UserServiceGateway());
        }

        public IAuthorizationServiceGateway GetAuthorisationServiceGateway()
        {
            return _authorizationServiceGateway ?? (_authorizationServiceGateway = new AuthorizationServiceGateway());
        }
    }
}
