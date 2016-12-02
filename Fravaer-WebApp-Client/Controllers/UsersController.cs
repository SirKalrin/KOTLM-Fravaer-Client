using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Mvc;
using Fravaer_WebApp_Client.Models;
using ServiceGateways.Entities;
using ServiceGateways.Facade;
using ServiceGateways.Interfaces;
using ServiceGateways.ServiceGateways;

namespace Fravaer_WebApp_Client.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private IServiceGateway<User, int> _userServiceGateway = new ServiceGatewayFacade().GetUserServiceGateway();

        private IServiceGateway<Department, int> _departmentServiceGateway =
            new ServiceGatewayFacade().GetDepartmentServiceGateway();

        // GET: User
        public ActionResult Index()
        {
            //var user = new User() { FirstName = "Nico", LastName = "Jørg", Email = "nico@gmail.com", UserName = "nico@gmail.com", Password = "1234gtx", Id = 1, Absences = new List<Absence>() { new Absence() { Id = 1, Date = new DateTime(2017, 2, 2) { }, Status = Statuses.F } } };
            //var user1 = new User() { FirstName = "Nico", LastName = "Jørg", Email = "nico@gmail.com", UserName = "nico@gmail.com", Password = "1234gtx", Id = 1, Absences = new List<Absence>() { new Absence() { Id = 1, Date = new DateTime(2017, 2, 2) { }, Status = Statuses.F } } };
            //var user2 = new User() { FirstName = "Nico", LastName = "Jørg", Email = "nico@gmail.com", UserName = "nico@gmail.com", Password = "1234gtx", Id = 1, Absences = new List<Absence>() { new Absence() { Id = 1, Date = new DateTime(2017, 2, 2) { }, Status = Statuses.F } } };
            //var user3 = new User() { FirstName = "Nico", LastName = "Jørg", Email = "nico@gmail.com", UserName = "nico@gmail.com", Password = "1234gtx", Id = 1, Absences = new List<Absence>() { new Absence() { Id = 1, Date = new DateTime(2017, 2, 2) { }, Status = Statuses.F } } };
            //List<User> users = new List<User>();

            //users.Add(user);
            //users.Add(user1);
            //users.Add(user2);
            //users.Add(user3);

            //var department = new Department() { Users = users, Id = 1, Name = "Fælles"};
            //var department2 = new Department() { Users = users, Id = 1, Name = "Erhverv" };
            //var department3 = new Department() { Users = users, Id = 1, Name = "Ribe" };
            //var department4 = new Department() { Users = users, Id = 1, Name = "Esbjerg" };
            //List<Department> departments = new List<Department>();
            //departments.Add(department);
            //departments.Add(department2);
            //departments.Add(department3);
            //departments.Add(department4);

            return View(_departmentServiceGateway.ReadAll());
        }

        // GET: User/Details/5
        public ActionResult Details(int? id, DateTime? monthDate)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = _userServiceGateway.Read(id.Value);
            
            if (user == null)
            {
                return HttpNotFound();
            }

            // BLL LAYER!
            var list = new ArrayList();
            list.Add("Monday");
            list.Add("Tuesday");
            list.Add("Wednesday");
            list.Add("Thursday");
            list.Add("Friday");
            list.Add("Saturday");
            list.Add("Sunday");
            //int index = list.IndexOf(new DateTime(2017, 2, 1).DayOfWeek.ToString());

            DateTime monthShow = DateTime.Now;

            if (monthDate != null)
            {
                monthShow = monthDate.Value;
            }

            int index = list.IndexOf(monthShow.DayOfWeek.ToString());

            var viewModel = new UserDetailsViewModel() {
                User = user,
                DateTime = monthShow,
                InitIndex = index};

            return View(viewModel);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View(new CreateUserViewModel()
            {
                User = new User(),
                Departments = _departmentServiceGateway.ReadAll()
            });
        }

        // POST: Employee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,UserName,Password,Email")] User user)
        {
            if (ModelState.IsValid)
            {
                _userServiceGateway.Create(user);
                return RedirectToAction("Index");
            }

            return View(new CreateUserViewModel()
            {
                User = user, Departments = _departmentServiceGateway.ReadAll()
            });
        }

        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = _userServiceGateway.Read(id.Value);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,UserName,Password,Email")] User user)
        {
            if (ModelState.IsValid)
            {
                _userServiceGateway.Update(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = _userServiceGateway.Read(id.Value);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _userServiceGateway.Delete(id);
            return RedirectToAction("Index");
        }


    }
}
