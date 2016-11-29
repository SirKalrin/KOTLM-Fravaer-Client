using ServiceGateways.Entities;
using ServiceGateways.Interfaces;
using ServiceGateways.ServiceGateways;

namespace ServiceGateways.Facade
{
    public class ServiceGatewayFacade
    {
        private IServiceGateway<Absence, int> _absenceGateway;
        private IServiceGateway<Admin, int> _adminGateway;
        private IServiceGateway<Department, int> _departmentGateway;
        private IServiceGateway<DeptChief, int> _deptChiefGateway;
        private IServiceGateway<Employee, int> _employeeGateway;

        public IServiceGateway<Absence, int> GetAbsenceServiceGateway()
        {
            return _absenceGateway ?? (_absenceGateway = new AbsenceServiceGateway());
        }
        public IServiceGateway<Admin, int> GetAdminServiceGateway()
        {
            return _adminGateway ?? (_adminGateway = new AdminServiceGateway());
        }
        public IServiceGateway<Department, int> GetDepartmentServiceGateway()
        {
            return _departmentGateway ?? (_departmentGateway = new DepartmentServiceGateway());
        }
        public IServiceGateway<DeptChief, int> GetDeptChiefServiceGateway()
        {
            return _deptChiefGateway ?? (_deptChiefGateway = new DeptChiefServiceGateway());
        }
        public IServiceGateway<Employee, int> GetEmployeeServiceGateway()
        {
            return _employeeGateway ?? (_employeeGateway = new EmployeeServiceGateway());
        }
    }
}
