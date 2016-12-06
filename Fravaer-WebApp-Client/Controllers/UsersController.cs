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
using DateTimeExtensions;
using Fravaer_WebApp_Client.Models;
using ServiceGateways.Entities;
using ServiceGateways.Facade;
using ServiceGateways.Interfaces;
using ServiceGateways.ServiceGateways;

namespace Fravaer_WebApp_Client.Controllers
{
    //[Authorize(Roles = "Administrator")]
    public class UsersController : Controller
    {
        private IServiceGateway<User, int> _userServiceGateway = new ServiceGatewayFacade().GetUserServiceGateway();

        private IServiceGateway<Department, int> _departmentServiceGateway =
            new ServiceGatewayFacade().GetDepartmentServiceGateway();

        // GET: User
        public ActionResult Index()
        {
            decimal averageDays = Decimal.Divide(100, DateTime.Now.LastDayOfTheMonth().Day);
            var ViewModel = new UserIndexViewModel()
            {
                Departments = _departmentServiceGateway.ReadAll(),
                MonthDateTime = DateTime.Now,
                AverageDaysInt = averageDays
            };
            return View(ViewModel);
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

        // GET: Medarbejder/Create
        public ActionResult Create()
        {
            return View(new CreateUserViewModel()
            {
                User = new User(),
                Departments = _departmentServiceGateway.ReadAll()
            });
        }

        // POST: Medarbejder/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,UserName,Email,Password,ConfirmPassword,Department,Role")] User user)
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
            user.ConfirmPassword = user.Password;
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(new CreateUserViewModel() {User = user, Departments = _departmentServiceGateway.ReadAll()});
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,UserName,Email,Password,ConfirmPassword,Department,Role")] User user)
        {
            if (ModelState.IsValid)
            {
                _userServiceGateway.Update(user);
                return RedirectToAction("Index");
            }
            return View(new CreateUserViewModel() { User = user, Departments = _departmentServiceGateway.ReadAll() });
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
