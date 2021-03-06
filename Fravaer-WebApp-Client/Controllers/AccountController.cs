﻿using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Fravaer_WebApp_Client.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Fravaer_WebApp_Client.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using ServiceGateways.Entities;
using ServiceGateways.Facade;
using ServiceGateways.Interfaces;

namespace Fravaer_WebApp_Client.Controllers
{
    [LoginRequired]
    public class AccountController : Controller
    {

        private IAuthorizationServiceGateway _authorizationServiceGateway = new ServiceGatewayFacade().GetAuthorisationServiceGateway();
        private IServiceGateway<User, int> _userServiceGateway = new ServiceGatewayFacade().GetUserServiceGateway();

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        public ActionResult LogOut()
        {
            System.Web.HttpContext.Current.Session["token"] = null;
            System.Web.HttpContext.Current.Session["currentUser"] = null;
            return RedirectToAction("Login", "Account");
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = _authorizationServiceGateway.Login(model.UserName, model.Password);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Details", "Users", new {id = _userServiceGateway.ReadAll().FirstOrDefault(x => x.UserName == model.UserName)?.Id});
                }
                ModelState.AddModelError("", "Invalid login attempt!");

            }
            return View(model);
        }

    }
}