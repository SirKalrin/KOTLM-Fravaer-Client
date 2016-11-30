﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
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
            return View(_userServiceGateway.ReadAll());
        }

        // GET: User/Details/5
        public ActionResult Details(int? id)
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

            return View(user);
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