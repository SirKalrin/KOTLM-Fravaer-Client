﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BusinessLogic.Managers;
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
        private IServiceGateway<Absence, int> _absenceServiceGateway = new ServiceGatewayFacade().GetAbsenceServiceGateway();
        private UserManager _userManager = new UserManager();

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
        /* Return the DetailsView, which visualizes the user with the given id, 
         and show its absences in the given month, 
         and select the chosenAbsence type if one is given*/
        public ActionResult Details(int? id, DateTime? monthDate, string chosenAbsence)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //read and checks the user
            User user = _userServiceGateway.Read(id.Value);
            if (user == null)
            {
                return HttpNotFound();
            }
            
            //Setting the dateTime
            DateTime monthShow = DateTime.Now;
            if (monthDate != null)
            {
                monthShow = monthDate.Value;
            }
            //Getting the index to start the month from
            var initIndex = _userManager.GetInitIndex(monthShow);
            //Getting the different types of absences + description
            var absenceTypes = _userManager.GetAbsenceTypes();

            

            //Creating the ViewModel
            var viewModel = new UserDetailsViewModel()
            {
                User = user,
                DateTime = monthShow,
                InitIndex = initIndex,
                AbsenceTypes = absenceTypes,
                ChosenAbsence = chosenAbsence,
            };

            return View(viewModel);
        }

        /* This POST method deleted an absence if an absence Id is given with the absence type of delete, 
         or creates an absence if the above criterias isnt met an absence DateTime and absence type is given, 
         where it redirect to DetailsView afterwards*/ 
        [HttpPost]
        public ActionResult Details(int? id, DateTime? monthDate, string absenceType, DateTime? absenceDate, int? deletableAbsenceId)
        {
            User user = _userServiceGateway.Read(id.Value);

            //Either adds or deleted an absence
            if (absenceType.Equals("Slet") && deletableAbsenceId != null)
            {
                _userManager.DeleteAbsenceFromUser(deletableAbsenceId.Value);
            }
            else if(absenceType.Equals("Slet"))
            {
                //Do nothing
            }
            else if(absenceDate != null && absenceType != null)
            {
                _userManager.AddAbsenceToUser(user, absenceDate, absenceType);
            }

            return RedirectToAction("Details", new RouteValueDictionary(new {id = id.Value, monthDate = monthDate.Value, chosenAbsence = absenceType}));
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
        public ActionResult Create(
            [Bind(Include = "Id,FirstName,LastName,UserName,Email,Password,ConfirmPassword,Department,Role")] User user)
        {
            if (ModelState.IsValid)
            {
                _userServiceGateway.Create(user);
                return RedirectToAction("Index");
            }

            return View(new CreateUserViewModel()
            {
                User = user,
                Departments = _departmentServiceGateway.ReadAll()
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
        public ActionResult Edit(
            [Bind(Include = "Id,FirstName,LastName,UserName,Email,Password,ConfirmPassword,Department,Role")] User user)
        {
            if (ModelState.IsValid)
            {
                _userServiceGateway.Update(user);
                return RedirectToAction("Index");
            }
            return View(new CreateUserViewModel() {User = user, Departments = _departmentServiceGateway.ReadAll()});
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

        [HttpPost]
        public ActionResult AddGrayDaysToUser(int? id, DateTime dateFrom, DateTime dateEnd, List<string> chosenDays)
        {
            var user = _userServiceGateway.Read(id.Value);
            for (DateTime i = dateFrom; i <= dateEnd;)
            {
                foreach (var dayType in chosenDays)
                {
                    if (i.DayOfWeek.ToString().Equals(dayType))
                    {
                        _userManager.AddAbsenceToUser(user, i, Statuses.GRAY.ToString());
                    }
                }
                i = i.AddDays(1);
            }
            return RedirectToAction("Details", new RouteValueDictionary(new { id = id.Value, monthDate = dateFrom}));
        }
        
    }
}